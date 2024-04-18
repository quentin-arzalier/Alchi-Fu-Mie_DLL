using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.Cards.Spells.AddManaFromElement
{
    /// <summary>
    ///     Représente le sortilège qui double les dégâts des deux côtés pendant un tour uniquement
    /// </summary>
    public class DoubleDamageSpell : SpellCard
    {
        public override void ActivateSpell()
        {
            throw new NotImplementedException();
        }

        public override string GetDescription()
        {
            return "Double les dégâts infligés par vous ET votre adversaire pour ce tour uniquement.";
        }

        public override int GetManaCost()
        {
            return 3;
        }
    }
}
