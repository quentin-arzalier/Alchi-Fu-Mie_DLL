using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.UnityResults
{
    /// <summary>
    ///     Représente le résultat d'une phase de tirage du plateau
    /// </summary>
    public class DrawingPhaseResult
    {
        /// <summary>
        ///     Résultat de pioche du joueur bleu
        /// </summary>
        public DrawResult BlueSideDrawResult { get; internal set; }
        /// <summary>
        ///     Résultat de pioche du joueur rouge
        /// </summary>
        public DrawResult RedSideDrawResult { get; internal set; }

    }
}
