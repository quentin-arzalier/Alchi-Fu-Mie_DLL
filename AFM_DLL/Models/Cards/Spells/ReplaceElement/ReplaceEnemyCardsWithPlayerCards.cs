using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Enum;

namespace AFM_DLL.Models.Cards.Spells.ReplaceElement
{
    /// <summary>
    ///     Remplace les cartes pierre opposées par des cartes ciseaux.
    /// </summary>
    public class ReplaceEnemyCardsWithPlayerCards : SpellCard
    {
        /// <inheritdoc/>
        public override void ActivateSpell(Board board, bool isBlueSide)
        {
            var enemycards = board.GetEnemyBoardSide(isBlueSide).AllElementsOfSide;
            var playercards = board.GetAllyBoardSide(isBlueSide).AllElementsOfSide;
            for (var cardCount = 0; cardCount < enemycards.Count; cardCount++)
            {
                var currentEnemyCardElement = enemycards[cardCount].ActiveElement;
                var currentPlayerCardElement = playercards[cardCount].ActiveElement;
                enemycards[cardCount].OverrideElement = currentPlayerCardElement;
                playercards[cardCount].OverrideElement = currentEnemyCardElement;
            }
        }

        /// <inheritdoc/>
        public override string GetDescription()
        {
            return "Echange le type des cartes pour chaque couloir";
        }

        /// <inheritdoc/>
        public override uint GetManaCost()
        {
            return 3;
        }

        /// <inheritdoc/>
        public override SpellType SpellType => SpellType.REPLACE_ENEMY_CARDS_WITH_PLAYER_CARDS;
    }
}
