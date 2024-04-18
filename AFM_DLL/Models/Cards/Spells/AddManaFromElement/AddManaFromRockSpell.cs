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
        public override void ActivateSpell()
        {
            throw new NotImplementedException();
        }

        public override string GetDescription()
        {
            return "Génère un point de mana par carte pierre jouée ce tour";
        }

        public override int GetManaCost()
        {
            return 2;
        }
    }
}
