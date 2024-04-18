using AFM_DLL.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.PlayerInfo
{
    /// <summary>
    ///     Représente la main d'un joueur
    /// </summary>
    public class Hand
    {
        /// <summary>
        ///     Les éléments actuellement dans la main du joueur (pas plus de 4)
        /// </summary>
        public List<ElementCard> Elements { get; private set; } = new List<ElementCard>();

        /// <summary>
        ///     Les sortilèges actuellement dans la main du joueur
        /// </summary>
        public List<SpellCard> Spells { get; private set; } = new List<SpellCard>();
    }
}
