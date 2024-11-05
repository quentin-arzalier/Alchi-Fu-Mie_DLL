using AFM_DLL.Models.PlayerInfo;
using AFM_DLL.Models.Unlockables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AFM_DLL.Helpers
{
    /// <summary>
    ///     Réalise les opérations de Gacha
    /// </summary>
    public static class GachaHelper
    {
        const int RAREST_PERCENT_TRESHOLD = 5;
        const int MEDIUM_PERCENT_TRESHOLD = 30; // 5 + 25

        private static readonly List<IUnlockable> _commonUnlockables;
        private static readonly List<IUnlockable> _rareUnlockables;
        private static readonly List<IUnlockable> _epicUnlockables;
        private static readonly List<IUnlockable> _legendaryUnlockables;

        static GachaHelper()
        {
            var unlockables = new List<IUnlockable>();
            unlockables.AddRange(CardBack.GetAllTypes());
            unlockables.AddRange(Emote.GetAllTypes());

            var rarityGroups = unlockables.GroupBy(x => x.Rarity);

            _commonUnlockables = rarityGroups.SingleOrDefault(grp => grp.Key == Rarity.COMMON).ToList()
                ?? new List<IUnlockable>();
            _rareUnlockables = rarityGroups.SingleOrDefault(grp => grp.Key == Rarity.RARE).ToList()
                ?? new List<IUnlockable>();
            _epicUnlockables = rarityGroups.SingleOrDefault(grp => grp.Key == Rarity.EPIC).ToList()
                ?? new List<IUnlockable>();
            _legendaryUnlockables = rarityGroups.SingleOrDefault(grp => grp.Key == Rarity.LEGENDARY).ToList()
                ?? new List<IUnlockable>();
        }



        /// <summary>
        ///     Réalise une partie de gacha normale si le joueur a assez de pièces (1500)
        /// </summary>
        /// <param name="acc">Le joueur qui joue au gacha normal</param>
        /// <returns>Un déblocable de commun à épique, ou null si le joueur n'a pas assez de pièces</returns>
        public static IUnlockable NormalGacha(Account acc)
        {
            if ((acc?.CoinCount ?? 0) < 1500)
            {
                return null;
            }
            acc.CoinCount -= 1500;

            return _GetGachaItem(isPremium: false);
        }

        /// <summary>
        ///     Réalise une partie de gacha premium si le joueur a assez de gemmes (150)
        /// </summary>
        /// <param name="acc">Le joueur qui joue au gacha premium</param>
        /// <returns>Un déblocable de épique à légendaire, ou null si le joueur n'a pas assez de gemmes</returns>
        public static IUnlockable PremiumGacha(Account acc)
        {
            if ((acc?.GemCount ?? 0) < 150)
            {
                return null;
            }
            acc.GemCount -= 150;

            return _GetGachaItem(isPremium: true);
        }

        private static IUnlockable _GetGachaItem(bool isPremium)
        {
            var value = new Random().Next(100);

            if (value < RAREST_PERCENT_TRESHOLD)
            {
                if (isPremium)
                    return _legendaryUnlockables[new Random().Next(_legendaryUnlockables.Count)];
                else
                    return _epicUnlockables[new Random().Next(_epicUnlockables.Count)];
            }
            else if (value < MEDIUM_PERCENT_TRESHOLD)
            {
                if (isPremium)
                    return _epicUnlockables[new Random().Next(_epicUnlockables.Count)];
                else
                    return _rareUnlockables[new Random().Next(_rareUnlockables.Count)];
            }
            else
            {
                if (isPremium)
                    return _epicUnlockables[new Random().Next(_epicUnlockables.Count)];
                else
                    return _commonUnlockables[new Random().Next(_commonUnlockables.Count)];
            }
        }
    }
}
