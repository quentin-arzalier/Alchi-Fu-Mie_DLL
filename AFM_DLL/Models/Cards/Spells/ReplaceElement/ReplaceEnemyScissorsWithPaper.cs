using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Enum;
using System.Linq;

namespace AFM_DLL.Models.Cards.Spells.ReplaceElement
{
    /// <summary>
    ///     Remplace les cartes pierre opposées par des cartes ciseaux.
    /// </summary>
    public class ReplaceEnemyScissorsWithPaper : SpellCard
    {
        /// <inheritdoc/>
        public override void ActivateSpell(Board board, bool isBlueSide)
        {
            var enemyscissors = board.GetEnemyBoardSide(isBlueSide).AllElementsOfSide
                .Where(c => c.ActiveElement == Element.SCISSORS);

            foreach (var scissor in enemyscissors)
            {
                scissor.OverrideElement = Element.PAPER;
            }
        }

        /// <inheritdoc/>
        public override string GetDescription()
        {
            return "Remplace les cartes Ciseaux de l'adversaire par des cartes Feuille.";
        }

        /// <inheritdoc/>
        public override uint GetManaCost()
        {
            return 3;
        }

        /// <inheritdoc/>
        public override SpellType SpellType => SpellType.REPLACE_ENEMY_SCISSORS_WITH_PAPER;
    }
}
