namespace AFM_DLL.Models.Enum
{
    /// <summary>
    ///     Contient tous les types de sortilèges différents de l'application
    /// </summary>
    public enum SpellType
    {
        /// <summary>
        ///     Ajout de mana en fonction des pierres
        /// </summary>
        ADD_MANA_FROM_ROCK,
        /// <summary>
        ///     Ajout de mana en fonction des feuilles
        /// </summary>
        ADD_MANA_FROM_PAPER,
        /// <summary>
        ///     Ajout de mana en fonction des ciseaux
        /// </summary>
        ADD_MANA_FROM_SCISSORS,

        /// <summary>
        ///     Doubles les dégâts de la manche
        /// </summary>
        DOUBLE_DAMAGE,

        /// <summary>
        ///     Surcharge les cartes pierre de l'ennemi par des cartes ciseaux
        /// </summary>
        REPLACE_ENEMY_ROCK_WITH_SCISSORS
    }
}
