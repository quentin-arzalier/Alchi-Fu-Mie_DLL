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
    ///     Représente le sortilège qui double les dégâts des deux côtés pendant un tour uniquement
    /// </summary>
    public class DoubleDamageSpell : SpellCard
    {
        public override void ActivateSpell(Board board, bool isBlueSide)
        {
            board.Modifiers.Add(BoardModifiers.DOUBLE_DAMAGE);
        }

        public override string GetDescription()
        {
            return "Double les dégâts infligés par vous ET votre adversaire pour ce tour uniquement.";
        }

        public override int GetManaCost()
        {
            return 3;
        }

        public override SpellType GetSpellType()
        {
            return SpellType.DOUBLE_DAMAGE;
        }
    }
}
