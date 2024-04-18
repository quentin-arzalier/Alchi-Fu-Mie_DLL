using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.Cards
{
    /// <summary>
    ///     Représente une carte élement (pierre, feuille ou ciseau)
    /// </summary>
    public class ElementCard : Card
    {

        public ElementCard(Element element) {
            InitialElement = element;
        }

        /// <summary>
        ///     Élément actif de la carte (prend en compte les surcharges par sortilège)
        /// </summary>
        public Element ActiveElement => OverrideElement ?? InitialElement;

        /// <summary>
        ///     Élément de base de la carte
        /// </summary>
        private Element InitialElement { get; set; }

        /// <summary>
        ///     Surcharge d'élément de la carte (potentiellement dû à un sortilège)
        /// </summary>
        public Element? OverrideElement { private get; set; }
    }
}
