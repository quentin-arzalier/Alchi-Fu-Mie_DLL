using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Enum;

namespace AFM_DLL.Models.Cards.Spells.ReplaceElement
{
    /// <summary>
    ///     Remplace les cartes pierre opposées par des cartes ciseaux.
    /// </summary>
    public class ReplaceEnemyCardsWithPaper : SpellCard
    {
        /// <inheritdoc/>
        public override void ActivateSpell(Board board, bool isBlueSide)
        {
            var enemycards = board.GetEnemyBoardSide(isBlueSide).AllElementsOfSide;

            foreach (var enemycard in enemycards)
            {
                enemycard.OverrideElement = Element.PAPER;
            }
        }

        /// <inheritdoc/>
        public override string GetDescription()
        {
            return "Remplace toute les cartes de l'adversaire par des cartes Feuille.";
        }

        /// <inheritdoc/>
        public override uint GetManaCost()
        {
            return 3;
        }

        /// <inheritdoc/>
        public override SpellType GetSpellType()
        {
            return SpellType.REPLACE_ENEMY_CARDS_WITH_PAPER;
        }
    }
}
