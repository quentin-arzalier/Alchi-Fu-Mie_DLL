using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.Enum
{
    /// <summary>
    ///     Contient tous les types de sortilèges différents de l'application
    /// </summary>
    public enum SpellType
    {
        ADD_MANA_FROM_ROCK,
        ADD_MANA_FROM_PAPER,
        ADD_MANA_FROM_SCISSORS,

        DOUBLE_DAMAGE,

        REPLACE_ENEMY_ROCK_WITH_SCISSORS
    }
}
