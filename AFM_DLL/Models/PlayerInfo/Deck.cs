using AFM_DLL.Extensions;
using AFM_DLL.Models.Cards;
using System.Collections.Generic;
using System.Linq;

namespace AFM_DLL.Models.PlayerInfo
{
    /// <summary>
    ///     Représente le deck d'un joueur.
    /// </summary>
    public class Deck
    {
        /// <summary>
        ///     Le héros du joueur
        /// </summary>
        public Hero Hero { get; set; }

        /// <summary>
        ///     Les cartes éléments choisies par le joueur
        /// </summary>
        public List<ElementCard> Elements { get; private set; } = new List<ElementCard>();
        /// <summary>
        ///     Les cartes sortilèges choisies par le joueur <br/>
        ///     Il ne peut y avoir que trois fois la même carte dans 
        /// </summary>
        public List<SpellCard> Spells { get; private set; } = new List<SpellCard>();

        /// <summary>
        ///     Indique si le deck est valide et utilisable. <br/>
        ///     Un deck valide possède 10 éléments, 10 sortilèges, et pas plus de 3 exemplaires du même sortilège.
        /// </summary>
        public bool IsDeckValid => Hero != null && Elements.Count == 10 && Spells.Count == 10 && Spells.GroupBy(s => s.SpellType).Max(grp => grp.Count()) <= 3;

        /// <summary>
        ///     Ajoute un élément au deck d'élément
        /// </summary>
        /// <param name="card">
        ///     La carte élément à ajouter
        /// </param>
        /// <returns>
        ///     Si l'ajout a correctement été réalisé
        /// </returns>
        public bool AddElement(ElementCard card)
        {
            if (Elements.Count >= 10)
                return false;
            Elements.Add(card);
            return true;
        }

        /// <summary>
        ///     Supprime un élément du deck d'élément
        /// </summary>
        /// <param name="card">
        ///     La carte élément à supprimer
        /// </param>
        /// <returns>
        ///     Si la suppression a correctement eu lieu
        /// </returns>
        public bool RemoveElement(ElementCard card)
        {
            var eltToRemove = Elements.FirstOrDefault(c => c.ActiveElement == card.ActiveElement);
            return Elements.Remove(eltToRemove);
        }

        /// <summary>
        ///     Ajoute un sortilège au deck de sort
        /// </summary>
        /// <param name="card">
        ///     La carte sort à ajouter
        /// </param>
        /// <returns>
        ///     Si l'ajout a correctement été réalisé
        /// </returns>
        public bool AddSpell(SpellCard card)
        {
            var nbSameSpellsInDeck = Spells.Count(c => c.SpellType == card.SpellType);
            if (Spells.Count >= 10 || nbSameSpellsInDeck >= 3)
                return false;
            Spells.Add(card);
            return true;
        }
        /// <summary>
        ///     Supprime un sortilège du deck de sort
        /// </summary>
        /// <param name="card">
        ///     La carte sort à supprimer
        /// </param>
        /// <returns>
        ///     Si la suppression a correctement eu lieu
        /// </returns>
        public bool RemoveSpell(SpellCard card)
        {
            var eltToRemove = Spells.FirstOrDefault(c => c.SpellType == card.SpellType);
            return Spells.Remove(eltToRemove);
        }

        /// <summary>
        ///     Mélange les decks de sort et d'élément
        /// </summary>
        public void Shuffle()
        {
            Elements.Shuffle();
            Spells.Shuffle();
        }

    }
}
