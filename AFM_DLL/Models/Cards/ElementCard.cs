using AFM_DLL.Models.BoardData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.Cards
{
    /// <summary>
    ///     Représente une carte élement (pierre, feuille ou ciseau)
    /// </summary>
    public class ElementCard : Card
    {

        public ElementCard(Element element) {
            InitialElement = element;
        }

        /// <summary>
        ///     Élément actif de la carte (prend en compte les surcharges par sortilège)
        /// </summary>
        public Element ActiveElement => OverrideElement ?? InitialElement;

        /// <summary>
        ///     Élément de base de la carte
        /// </summary>
        private Element InitialElement { get; set; }

        private Element? _override;


        /// <summary>
        ///     Surcharge d'élément de la carte (potentiellement dû à un sortilège)
        /// </summary>
        public Element? OverrideElement
        {
            get
            {
                return _override;
            }
            set
            {
                _override = value;
                CardOverrided.Invoke(value);
            }
        }

        public event Action<Element?> CardOverrided;

        public override bool AddToBoard(Board board, bool isBlueSide, BoardPosition? position)
        {
            if (!position.HasValue)
                return false;

            var side = board.GetAllyBoardSide(isBlueSide);
            
            if (side.ElementCards.ContainsKey(position.Value) && side.ElementCards[position.Value] != null)
            {
                if (!side.ElementCards[position.Value].RemoveFromBoard(board, isBlueSide, position))
                    return false;
            }

            side.ElementCards[position.Value] = this;
            side.Player.Hand.Elements.Remove(this);
            return true;
        }

        public override bool RemoveFromBoard(Board board, bool isBlueSide, BoardPosition? position)
        {
            if (!position.HasValue)
                return false;

            var side = board.GetAllyBoardSide(isBlueSide);

            if (!side.ElementCards.ContainsKey(position.Value))
                return false;
            
            if (side.ElementCards[position.Value].InitialElement != this.InitialElement)
                return false;

            side.ElementCards[position.Value] = null;
            side.Player.Hand.Elements.Add(this);
            return true;
        }
    }
}
