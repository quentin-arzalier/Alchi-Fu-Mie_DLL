using AFM_DLL.Models.Unlockables;
using System.Collections.Generic;
using System.Linq;

namespace AFM_DLL.Models.PlayerInfo
{
    /// <summary>
    ///     Représente le compte du joueur et les informations qui vont avec
    /// </summary>
    public class Account
    {
        /// <summary>
        ///     Le deck actuel du joueur
        /// </summary>
        public Deck CurrentDeck { get; set; }

        /// <summary>
        ///     Nombre de pièces (monnaie courante) du joueur
        /// </summary>
        public int CoinCount { get; set; }

        /// <summary>
        ///     Nombre de gemmes (monnaie premium) du joueur
        /// </summary>
        public int GemCount { get; set; }

        /// <summary>
        ///     Récupère les objets déblocables du joueur
        /// </summary>
        public List<IUnlockable> UnlockableInventory { get; set; }

        /// <summary>
        ///     Les dos de carte débloqués par le joueur
        /// </summary>
        public List<CardBack> UnlockedCardBacks => UnlockableInventory?.OfType<CardBack>().ToList();
    }
}
