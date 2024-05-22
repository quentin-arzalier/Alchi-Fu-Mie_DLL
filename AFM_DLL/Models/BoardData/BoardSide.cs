using AFM_DLL.Models.Cards;
using AFM_DLL.Models.PlayerInfo;
using System.Collections.Generic;
using System.Linq;

namespace AFM_DLL.Models.BoardData
{
    /// <summary>
    ///     Correspond aux données présentes dans un côté du plateau
    /// </summary>
    public class BoardSide
    {
        /// <summary>
        ///     Le joueur auquel appartient ce côté du plateau
        /// </summary>
        public PlayerGame Player { get; internal set; }

        /// <summary>
        ///     Les cartes éléments placées (ou non) par le joueur sur les trois colonnes.
        /// </summary>
        public Dictionary<BoardPosition, ElementCard> ElementCards { get; private set; } = new Dictionary<BoardPosition, ElementCard>()
            {
                { BoardPosition.LEFT, null },
                { BoardPosition.MIDDLE, null },
                { BoardPosition.RIGHT, null },
            };


        /// <summary>
        ///     La carte sort jouée (ou non) par le joueur
        /// </summary>
        public SpellCard SpellCard { get; set; }

        /// <summary>
        ///     Raccourci qui donne toutes les cartes éléments de ce côté du plateau
        /// </summary>
        public List<ElementCard> AllElementsOfSide => ElementCards.Values.ToList();
    }
}
