using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Enum;

namespace AFM_DLL.Models.Cards.Spells.CancelSpell
{
    /// <summary>
    ///     Remplace les cartes pierre opposées par des cartes ciseaux.
    /// </summary>
    public class CancelEnemySpell : SpellCard
    {
        /// <inheritdoc/>
        public override void ActivateSpell(Board board, bool isBlueSide)
        {
            board.GetEnemyBoardSide(isBlueSide).SpellCard.CanBeActived = false;
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
        public override SpellType SpellType => SpellType.REPLACE_ENEMY_CARDS_WITH_PAPER;
    }
}
