using AFM_DLL.Models.Cards;
using AFM_DLL.Models.Enum;
using System.Collections.Generic;

namespace AFM_DLL.Models.UnityResults
{
    /// <summary>
    ///     Contient les informations liées à la phase de jeu des cartes sortilèges
    /// </summary>
    public class SpellCardEvaluationResult
    {
        /// <summary>
        ///     Contient les cartes sortilèges exécutées dans l'ordre
        /// </summary>
        public List<(SpellCard card, bool isBlueSide)> SpellsInOrder { get; private set; } = new List<(SpellCard card, bool isBlueSide)>();

        /// <summary>
        ///     Résultat du combat des héros pour la priorité des cartes (si besoin)
        /// </summary>
        public FightResult? HeroFightResult { get; internal set; }

        /// <summary>
        ///     Indique si le jet de pièce a été favorable à l'équipe bleue en cas d'égalité
        /// </summary>
        public bool? BlueSideStartedOnDraw { get; internal set; }
    }
}
