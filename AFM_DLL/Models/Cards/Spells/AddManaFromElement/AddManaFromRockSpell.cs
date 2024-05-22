using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Enum;
using System.Linq;

namespace AFM_DLL.Models.Cards.Spells
{
    /// <summary>
    ///     Sortilège qui ajoute un point de mana par carte pierre sur le terrain
    /// </summary>
    public class AddManaFromRockSpell : SpellCard
    {
        /// <inheritdoc/>
        public override void ActivateSpell(Board board, bool isBlueSide)
        {
            var count = board.AllElementsOfBoard.Count(c => c.ActiveElement == Element.ROCK);
            board.GetAllyBoardSide(isBlueSide).Player.AddMana(count);
        }

        /// <inheritdoc/>
        public override string GetDescription()
        {
            return "Génère un point de mana par carte pierre jouée ce tour";
        }

        /// <inheritdoc/>
        public override int GetManaCost()
        {
            return 2;
        }

        /// <inheritdoc/>
        public override SpellType GetSpellType()
        {
            return SpellType.ADD_MANA_FROM_ROCK;
        }
    }
}
