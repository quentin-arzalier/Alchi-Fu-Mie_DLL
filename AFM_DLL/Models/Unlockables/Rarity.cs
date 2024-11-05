namespace AFM_DLL.Models.Unlockables
{
    /// <summary>
    ///     Représente la rareté des objets du Gacha
    /// </summary>
    public enum Rarity
    {
        /// <summary>
        ///     Rareté la plus basse, pas obtenable dans le gacha premium
        /// </summary>
        COMMON = 0,
        /// <summary>
        ///     Rareté moyenne du gacha normal, mais la plus basse du gacha premium
        /// </summary>
        RARE = 1,
        /// <summary>
        ///     Rareté la plus élevée du gacha normal, moyenne du gacha premium
        /// </summary>
        EPIC = 2,
        /// <summary>
        ///     Rareté la plus élevée du gacha premium, introuvable dans le gacha normal
        /// </summary>
        LEGENDARY = 3
    }
}
