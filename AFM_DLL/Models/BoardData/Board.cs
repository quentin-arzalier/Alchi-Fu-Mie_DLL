using AFM_DLL.Helpers;
using AFM_DLL.Models.Cards;
using AFM_DLL.Models.PlayerInfo;
using AFM_DLL.Models.UnityResults;
using System;
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
        ///     Permet d'instancier un plateau
        /// </summary>
        /// <param name="bluePlayer">Le joueur bleu</param>
        /// <param name="redPlayer">Le joueur rougez</param>
        public Board(PlayerGame bluePlayer, PlayerGame redPlayer)
        {
            BlueSide.Player = bluePlayer;
            RedSide.Player = redPlayer;

            BlueSide.Player.Deck.Shuffle();
            RedSide.Player.Deck.Shuffle();
        }


        /// <summary>
        ///     Représente l'état actuel du plateau, l'action à venir
        /// </summary>
        public BoardState NextAction { get; private set; } = BoardState.DRAW_CARDS;

        /// <summary>
        ///     Contient toutes les informations du côté bleu du plateau
        /// </summary>
        internal BoardSide BlueSide { get; private set; } = new BoardSide();

        /// <summary>
        ///     Contient toutes les informations du côté rouge du plateau
        /// </summary>
        internal BoardSide RedSide { get; private set; } = new BoardSide();

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

        /// <summary>
        ///     Effectue la phase de tirage du plateau
        /// </summary>
        /// <returns>Un objet contenant des informations sur la </returns>
        /// <exception cref="ApplicationException"></exception>
        public DrawingPhaseResult DrawCards()
        {
            if (NextAction != BoardState.DRAW_CARDS)
                throw new ApplicationException($"La fonction DrawCards ne peut pas être appelée lorsque le plateau est en état {NextAction}.");

            NextAction = BoardState.PLAY_CARDS;

            var res = new DrawingPhaseResult();

            res.BlueSideDrawResult = BlueSide.Player.Draw();
            res.RedSideDrawResult = RedSide.Player.Draw();

            return res;
        }


        /// <summary>
        /// Effectue l'évaluation des sortilèges en jeu
        /// </summary>
        /// <returns>Un objet comprenant toutes les informations résultat de l'évaluation des sortilèges</returns>
        public SpellCardEvaluationResult EvaluateSpells()
        {
            if (NextAction != BoardState.EVALUATE_SPELLS)
                throw new ApplicationException($"La fonction EvaluateSpells ne peut pas être appelée lorsque le plateau est en état {NextAction}.");

            var res = FightHelper.GetOrderedSpellCards(this);
            foreach (var card in res.SpellsInOrder) {
                card.card.ActivateSpell(this, card.isBlueSide);
            }
            return res;
        }

    }
}
