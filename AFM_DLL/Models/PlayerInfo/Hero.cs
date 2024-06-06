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
        /// <param name="activeElement">L'élément initial du héros</param>
        public Hero(string name, Element activeElement)
        {
            Name = name;
            InitialElement = activeElement;
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


        private ElementCard _overrideCard;

        /// <summary>
        ///     Surcharge d'élément du héros (potentiellement dû à un remplacement)
        /// </summary>
        public ElementCard OverrideCard
        {
            get { return _overrideCard; }
            internal set
            {
                if (_overrideCard.ActiveElement != value.ActiveElement)
                    OverrideCardChanged?.Invoke(value);
                _overrideCard = value;
            }
        }

        /// <summary>
        ///     Évènement indiquant quand la carte override a changé
        /// </summary>
        public event Action<ElementCard> OverrideCardChanged;

        /// <summary>
        ///     Indique si le remplacement de type peut être annulé
        /// </summary>
        public bool CanRevertOverride { get; internal set; }
    }
}
