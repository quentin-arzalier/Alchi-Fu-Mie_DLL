using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.PlayerInfo
{
    /// <summary>
    ///     Représente le héros choisi par le joueur
    /// </summary>
    public class Hero
    {
        public Hero(string name, Element element)
        {
            Name = name;
            InitialElement = element;
        }

        public string Name { get; private set; }


        /// <summary>
        ///     Élément actif du héros (prend en compte les surcharges par remplacement)
        /// </summary>
        public Element ActiveElement => OverrideElement ?? InitialElement;

        /// <summary>
        ///     Élément de base du héros
        /// </summary>
        private Element InitialElement { get; set; }

        /// <summary>
        ///     Surcharge d'élément du héros (potentiellement dû à un remplacement)
        /// </summary>
        public Element? OverrideElement { private get; set; }
    }
}
