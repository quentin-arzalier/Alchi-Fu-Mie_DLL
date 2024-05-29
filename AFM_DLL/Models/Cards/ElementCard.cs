using AFM_DLL.Models.BoardData;
using System;

namespace AFM_DLL.Models.Cards
{
    /// <summary>
    ///     Représente une carte élement (pierre, feuille ou ciseau)
    /// </summary>
    public class ElementCard : Card
    {
        /// <summary>
        ///     Constructeur d'une carte élément
        /// </summary>
        /// <param name="element">L'élément initial de la carte</param>
        public ElementCard(Element element)
        {
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


        /// <summary>
        ///     Évènement indiquand quand une carte voit son type surchargé (ou désurchargé)
        /// </summary>
        public event Action<Element?> CardOverrideChanged;

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
                CardOverrideChanged?.Invoke(value);
            }
        }

        /// <summary>
        ///     Évènement indiquand quand une carte voit son type surchargé (ou désurchargé)
        /// </summary>
        public event Action<Element?> CardOverrideChanged;


        /// <inheritdoc/>
        public override bool AddToBoard(Board board, bool isBlueSide, BoardPosition? position)
        {
            if (base.AddToBoard(board, isBlueSide, position) == false)
                return false;

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

        /// <inheritdoc/>
        public override bool RemoveFromBoard(Board board, bool isBlueSide, BoardPosition? position)
        {
            if (base.RemoveFromBoard(board, isBlueSide, position) == false)
                return false;

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

        internal override bool DiscardFromBoardSide(BoardSide side, BoardPosition? position)
        {
            if (!position.HasValue)
                return false;

            OverrideElement = null;

            side.ElementCards[position.Value] = null;
            side.Player.Deck.Elements.Add(this);

            return true;
        }
    }
}
