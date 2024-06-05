using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Enum;
using System.Linq;

namespace AFM_DLL.Models.Cards.Spells.ReplaceElement
{
    /// <summary>
    ///     Remplace les cartes pierre oppos�es par des cartes ciseaux.
    /// </summary>
    public class ReplaceEnemyRockWithPaper : SpellCard
    {
        /// <inheritdoc/>
        public override void ActivateSpell(Board board, bool isBlueSide)
        {
            var enemyrocks = board.GetEnemyBoardSide(isBlueSide).AllElementsOfSide
                .Where(c => c.ActiveElement == Element.ROCK);

            foreach (var rock in enemyrocks)
            {
                rock.OverrideElement = Element.PAPER;
            }
        }

        /// <inheritdoc/>
        public override string GetDescription()
        {
            return "Remplace les cartes Pierre de l'adversaire par des cartes Feuille.";
        }

        /// <inheritdoc/>
        public override uint GetManaCost()
        {
            return 3;
        }

        /// <inheritdoc/>
        public override SpellType SpellType => SpellType.REPLACE_ENEMY_ROCK_WITH_PAPER;
    }
}
