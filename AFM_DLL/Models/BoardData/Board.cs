using AFM_DLL.Models.Cards;
using System.Collections.Generic;
using System.Linq;

namespace AFM_DLL.Models.BoardData
{
    /// <summary>
    ///     Représente l'état du plateau d'une partie d'AFM
    /// </summary>
    public class Board
    {
        /// <summary>
        ///     Contient toutes les informations du côté bleu du plateau
        /// </summary>
        public BoardSide BlueSide { get; private set; } = new BoardSide();

        /// <summary>
        ///     Contient toutes les informations du côté rouge du plateau
        /// </summary>
        public BoardSide RedSide { get; private set; } = new BoardSide();

        /// <summary>
        ///     Contient la liste des modificateurs de gameplay du plateau (comme double dégâts par exemple)
        /// </summary>
        public List<BoardModifiers> Modifiers { get; private set; } = new List<BoardModifiers>();

        /// <summary>
        ///     Permet de récupérer le plateau "allié" par rapport à un côté.
        /// </summary>
        /// <param name="isBlue">Si le joueur qui souhaite récupérer le plateau allié est bleu.</param>
        /// <returns>Le côté du plateau allié en fonction du côté.</returns>
        public BoardSide GetAllyBoardSide(bool isBlue) => isBlue ? BlueSide : RedSide;

        /// <summary>
        ///     Permet de récupérer le plateau "ennemi" par rapport à un côté.
        /// </summary>
        /// <param name="isBlue">Si le joueur qui souhaite récupérer le plateau ennemi est bleu.</param>
        /// <returns>Le côté du plateau ennemi en fonction du côté.</returns>
        public BoardSide GetEnemyBoardSide(bool isBlue) => isBlue ? RedSide : BlueSide;

        /// <summary>
        ///     Raccourci qui contient toutes les cartes éléments du plateau, bleues ou rouges.
        /// </summary>
        public List<ElementCard> AllElementsOfBoard => BlueSide.AllElementsOfSide.Concat(RedSide.AllElementsOfSide).ToList();
    }
}
