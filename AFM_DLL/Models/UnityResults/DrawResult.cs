using AFM_DLL.Models.Cards;
using System.Collections.Generic;

namespace AFM_DLL.Models.UnityResults
{
    /// <summary>
    ///     Représente le résultat d'une session de tirage
    /// </summary>
    public class DrawResult
    {
        /// <summary>
        ///     Liste des cartes éléments piochées par le joueur lors du tirage
        /// </summary>
        public List<ElementCard> DrawnElements { get; private set; } = new List<ElementCard>();

        /// <summary>
        ///     La carte sort éventuellement piochée par le joueur lors du tirage
        /// </summary>
        public SpellCard DrawnSpell { get; internal set; }
    }
}
