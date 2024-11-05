namespace AFM_DLL.Models.Unlockables
{
    /// <summary>
    ///     Représente les objets déblocables 
    /// </summary>
    public interface IUnlockable
    {
        /// <summary>
        ///     Rareté de l'objet déblocable
        /// </summary>
        Rarity Rarity { get; }


        /// <summary>
        ///     Nom de l'objet déblocable
        /// </summary>
        string Name { get; }
    }
}
