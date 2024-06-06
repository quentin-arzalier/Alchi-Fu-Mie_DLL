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
        REPLACE_ENEMY_ROCK_WITH_SCISSORS,

        /// <summary>
        ///     Surcharge les cartes pierre de l'ennemi par des cartes feuille
        /// </summary>
        REPLACE_ENEMY_ROCK_WITH_PAPER,

        /// <summary>
        ///     Surcharge les cartes feuille de l'ennemi par des cartes pierre
        /// </summary>
        REPLACE_ENEMY_PAPER_WITH_ROCK,

        /// <summary>
        ///     Surcharge les cartes feuille de l'ennemi par des cartes ciseaux
        /// </summary>
        REPLACE_ENEMY_PAPER_WITH_SCISSORS,

        /// <summary>
        ///     Surcharge les cartes ciseaux de l'ennemi par des cartes feuille
        /// </summary>
        REPLACE_ENEMY_SCISSORS_WITH_PAPER,

        /// <summary>
        ///     Surcharge les cartes ciseaux de l'ennemi par des cartes pierre
        /// </summary>
        REPLACE_ENEMY_SCISSORS_WITH_ROCK,

        /// <summary>
        ///     Surcharge les cartes de l'ennemi par les cartes du joueur 
        /// </summary>
        SWAP_ENEMY_CARDS_WITH_PLAYER_CARDS,

        /// <summary>
        ///     Surcharge les cartes de l'ennemi par des cartes pierre
        /// </summary>
        REPLACE_ENEMY_CARDS_WITH_ROCK,

        /// <summary>
        ///     Surcharge les cartes de l'ennemi par des cartes papier
        /// </summary>
        REPLACE_ENEMY_CARDS_WITH_PAPER,

        /// <summary>
        ///     Surcharge les cartes de l'ennemi par des cartes ciseaux
        /// </summary>
        REPLACE_ENEMY_CARDS_WITH_SCISSORS,

        /// <summary>
        ///     Annule le sort de l'ennemi s'il en a un
        /// </summary>
        CANCEL_ENEMY_SPELL,

        /// <summary>
        ///     Fait gagner le duel en cas d'egalite au joueur ayant lance ce sort.
        /// </summary>
        WIN_DUEL_TIE,


    }
}
