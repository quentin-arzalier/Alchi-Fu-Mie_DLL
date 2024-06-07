using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Enum;
using System.Linq;

namespace AFM_DLL.Models.Cards.Spells.ReplaceElement
{
    /// <summary>
    ///     Remplace les cartes pierre opposées par des cartes ciseaux.
    /// </summary>
    public class ReplaceEnemyPaperWithScissors : SpellCard
    {
        /// <inheritdoc/>
        public override void ActivateSpell(Board board, bool isBlueSide)
        {
            var enemypapers = board.GetEnemyBoardSide(isBlueSide).AllElementsOfSide
                .Where(c => c.ActiveElement == Element.PAPER);

            foreach (var paper in enemypapers)
            {
                paper.OverrideElement = Element.SCISSORS;
            }
        }

        /// <inheritdoc/>
        public override string GetDescription()
        {
            return "Remplace les cartes Feuille de l'adversaire par des cartes Ciseaux";
        }

        /// <inheritdoc/>
        public override uint GetManaCost()
        {
            return 3;
        }

        /// <inheritdoc/>
        public override SpellType SpellType => SpellType.REPLACE_ENEMY_PAPER_WITH_SCISSORS;
    }
}
