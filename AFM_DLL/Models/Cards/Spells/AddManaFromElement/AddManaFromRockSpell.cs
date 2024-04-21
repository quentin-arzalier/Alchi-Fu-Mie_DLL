using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.Cards.Spells
{
    /// <summary>
    ///     Sortilège qui ajoute un point de mana par carte pierre sur le terrain
    /// </summary>
    public class AddManaFromRockSpell : SpellCard
    {
        public override void ActivateSpell(Board board, bool isBlueSide)
        {
            var count = board.AllElementsOfBoard.Count(c => c.ActiveElement == Element.ROCK);
            board.GetAllyBoardSide(isBlueSide).Player.AddMana(count);
        }

        public override string GetDescription()
        {
            return "Génère un point de mana par carte pierre jouée ce tour";
        }

        public override int GetManaCost()
        {
            return 2;
        }

        public override SpellType GetSpellType()
        {
            return SpellType.ADD_MANA_FROM_ROCK;
        }
    }
}
