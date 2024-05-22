using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.Cards.Spells.ReplaceElement
{
    public class ReplaceEnemyRockWithScissors : SpellCard
    {
        public override void ActivateSpell(Board board, bool isBlueSide)
        {
            var enemyrocks = board.GetEnemyBoardSide(isBlueSide).AllElementsOfSide
                .Where(c => c.ActiveElement == Element.ROCK);

            foreach (var rock in enemyrocks)
            {
                rock.OverrideElement = Element.SCISSORS;
            }
        }

        public override string GetDescription()
        {
            return "Remplace les cartes Pierre de l'adversaire par des cartes Ciseaux.";
        }

        public override int GetManaCost()
        {
            return 4;
        }

        public override SpellType GetSpellType()
        {
            return SpellType.REPLACE_ENEMY_ROCK_WITH_SCISSORS;
        }
    }
}
