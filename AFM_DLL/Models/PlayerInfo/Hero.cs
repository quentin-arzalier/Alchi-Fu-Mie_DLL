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
        public Element ActiveElement => OverrideElement ?? InitialElement;

        /// <summary>
        ///     Élément de base du héros
        /// </summary>
        private Element InitialElement { get; set; }

        private Element? _override;

        /// <summary>
        ///     Surcharge d'élément du héros (potentiellement dû à un remplacement)
        /// </summary>
        public Element? OverrideElement
        {
            get
            {
                return _override;
            }
            set
            {
                _override = value;
                HeroOverrideChanged.Invoke(value);
            }
        }

        /// <summary>
        ///     Évènement indiquand quand un héros voit son type surchargé (ou désurchargé)
        /// </summary>
        public event Action<Element?> HeroOverrideChanged;
    }
}
