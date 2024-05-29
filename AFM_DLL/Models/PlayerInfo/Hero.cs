using AFM_DLL.Models.Cards;
using System;

namespace AFM_DLL.Models.PlayerInfo
{
    /// <summary>
    ///     Représente le héros choisi par le joueur
    /// </summary>
    public class Hero
    {
        /// <summary>
        ///     Construit un héros
        /// </summary>
        /// <param name="name">Le nom du héros</param>
        /// <param name="element">L'élément initial du héros</param>
        public Hero(string name, Element element)
        {
            Name = name;
            InitialElement = element;
        }

        /// <summary>
        /// Le nom du Héros
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        ///     Élément actif du héros (prend en compte les surcharges par remplacement)
        /// </summary>
        public Element ActiveElement => OverrideCard?.ActiveElement ?? InitialElement;

        /// <summary>
        ///     Élément de base du héros
        /// </summary>
        private Element InitialElement { get; set; }

        /// <summary>
        ///     Surcharge d'élément du héros (potentiellement dû à un remplacement)
        /// </summary>
        public ElementCard OverrideCard { get; internal set; }

        internal bool CanRevertOverride { get; set; }

        /// <summary>
        ///     Évènement indiquant quand un héros voit son type surchargé (ou désurchargé)
        /// </summary>
        public event Action<Element?> HeroOverrideChanged;
    }
}
