using AFM_DLL.Models.Cards;

namespace AFM_DLL.Models.UnityResults
{
    /// <summary>
    ///     Résultat d'une séquence d'évaluation de sortilège
    /// </summary>
    public class SpellCardEvaluationResult
    {
        /// <summary>
        ///     La carte sort qui a été jouée
        /// </summary>
        public SpellCard SpellCard { get; internal set; }

        /// <summary>
        ///     Si la carte sort appartient au joueur bleu
        /// </summary>
        public bool IsBlueSide { get; internal set; }

        /// <summary>
        ///     Si un autre tour de carte sort doit être joué après celui ci
        /// </summary>
        public bool HasMoreSpells { get; internal set; }
    }
}
