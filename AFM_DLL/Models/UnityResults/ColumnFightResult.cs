using AFM_DLL.Models.Enum;

namespace AFM_DLL.Models.UnityResults
{
    /// <summary>
    ///     Représente les résultats d'un combat de cartes éléments sur une colonne.
    /// </summary>
    public class ColumnFightResult
    {
        /// <summary>
        ///     Indique le résultat du combat de carte initial
        /// </summary>
        public FightResult CardFightResult { get; internal set; }

        /// <summary>
        ///     Le résultat du combat de Héros (null si pas d'égalité aux cartes)
        /// </summary>

        public FightResult? HeroFightResult { get; internal set; }

        /// <summary>
        ///     Dégâts subis par le joueur bleu
        /// </summary>
        public uint BlueDamage { get; internal set; }

        /// <summary>
        ///     Dégâts subis par le joueur rouge
        /// </summary>
        public uint RedDamage { get; internal set; }
    }
}
