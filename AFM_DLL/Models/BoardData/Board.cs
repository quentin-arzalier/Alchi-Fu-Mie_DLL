﻿using AFM_DLL.Extensions;
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
        ///     Indique si des cartes peuvent être jouées du côté indiqué
        /// </summary>
        /// <param name="isBlueSide">
        ///     Si le test doit être effectué du côté bleu
        /// </param>
        /// <returns>
        ///     Un booléen indiquant si les cartes peuvent être jouées ou retirées.
        /// </returns>
        public bool CanCardsBePlayedOrRemoved(bool isBlueSide)
            => (isBlueSide ? !BlueSide.IsSideReady : !RedSide.IsSideReady) && NextAction == BoardState.PLAY_CARDS;

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



        private SpellCardEvaluationResultInternal _spellCardResults;

        /// <summary>
        ///     Effectue la phase de tirage du plateau
        /// </summary>
        /// <returns>Un objet contenant des informations sur la </returns>
        /// <exception cref="ApplicationException">
        ///     La méthode a été appelée alors que l'état du plateau ne le permet pas
        /// </exception>
        public DrawingPhaseResult DrawCards()
        {
            if (NextAction != BoardState.DRAW_CARDS)
                throw new ApplicationException($"La fonction DrawCards ne peut pas être appelée lorsque le plateau est en état {NextAction}.");

            NextAction = BoardState.PLAY_CARDS;

            var res = new DrawingPhaseResult();

            res.BlueSideDrawResult = BlueSide.Player.Draw();
            BlueSide.Player.AddMana((uint)(BlueSide.Player.HealthPoints > 5 ? 1 : 2));
            res.RedSideDrawResult = RedSide.Player.Draw();
            RedSide.Player.AddMana((uint)(RedSide.Player.HealthPoints > 5 ? 1 : 2));

            return res;
        }

        /// <summary>
        ///     Indique qu'un côté du plateau a fini son tour
        /// </summary>
        /// <param name="isBlueSide">
        ///     Indique de quel côté du plateau l'opération a lieu
        /// </param>
        /// <returns>
        ///     Un booléen indiquant si les deux joueurs sont prêts et que les sorts peuvent être lancés
        /// </returns>
        /// <exception cref="ApplicationException">
        ///     La méthode a été appelée alors que l'état du plateau ne le permet pas
        /// </exception>
        public bool SetSideReady(bool isBlueSide)
        {
            if (NextAction != BoardState.PLAY_CARDS)
                throw new ApplicationException($"La fonction SetSideReady ne peut pas être appelée lorsque le plateau est en état {NextAction}.");
            var side = GetAllyBoardSide(isBlueSide);
            side.IsSideReady = true;

            if (side.Player.Deck.Hero.CanRevertOverride)
            {
                side.Player.Deck.Hero.CanRevertOverride = false;
                side.Player.Deck.Elements.Add(side.Player.Deck.Hero.OverrideCard);
                side.Player.Deck.Hero.OverrideCard = new ElementCard(side.Player.Deck.Hero.ActiveElement);
            }

            if (BlueSide.IsSideReady && RedSide.IsSideReady)
            {
                NextAction = BoardState.EVALUATE_SPELLS;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Effectue l'évaluation des sortilèges en jeu
        /// </summary>
        /// <returns>Un objet comprenant toutes les informations résultat de l'évaluation des sortilèges</returns>
        /// <exception cref="ApplicationException">
        ///     La méthode a été appelée alors que l'état du plateau ne le permet pas
        /// </exception>
        public SpellCardEvaluationResult EvaluateSpells()
        {
            if (NextAction != BoardState.EVALUATE_SPELLS)
                throw new ApplicationException($"La fonction EvaluateSpells ne peut pas être appelée lorsque le plateau est en état {NextAction}.");

            if (_spellCardResults == null)
                _spellCardResults = FightHelper.GetOrderedSpellCards(this);

            var res = new SpellCardEvaluationResult();

            if (_spellCardResults.SpellsInOrder.Any())
            {

                var (card, isBlueSide) = _spellCardResults.SpellsInOrder.PopFirst();
                card.ActivateSpell(this, isBlueSide);

                res.SpellCard = card;
                res.IsBlueSide = isBlueSide;
            }

            if (_spellCardResults.SpellsInOrder.Any())
                res.HasMoreSpells = true;
            else
                NextAction = BoardState.EVALUATE_ELEMENTS;

            return res;
        }

        /// <summary>
        ///     Évalue chaque colonne du plateau et indique les résultats de tous les combats.
        /// </summary>
        /// <returns>
        ///     Un dictionnaire de résultat de colonne, qui indique pour chaque colonne le déroulement du combat.
        /// </returns>
        /// <exception cref="ApplicationException">
        ///     La méthode a été appelée alors que l'état du plateau ne le permet pas
        /// </exception>
        public Dictionary<BoardPosition, ColumnFightResult> EvaluateCardColumns()
        {
            if (NextAction != BoardState.EVALUATE_ELEMENTS)
                throw new ApplicationException($"La fonction EvaluateCardColumns ne peut pas être appelée lorsque le plateau est en état {NextAction}.");

            NextAction = BoardState.RESET_BOARD;

            var res = new Dictionary<BoardPosition, ColumnFightResult>();

            foreach (BoardPosition pos in System.Enum.GetValues(typeof(BoardPosition)))
            {
                res[pos] = FightHelper.EvaluateColumnFight(this, pos);
            }

            return res;
        }

        /// <summary>
        ///     Permet de réinitialise le plateau à la fin d'un tour de jeu.
        /// </summary>
        /// <exception cref="ApplicationException">
        ///     La méthode a été appelée alors que l'état du plateau ne le permet pas
        /// </exception>
        public void ResetBoard()
        {
            if (NextAction != BoardState.RESET_BOARD)
                throw new ApplicationException($"La fonction ResetBoard ne peut pas être appelée lorsque le plateau est en état {NextAction}.");

            BlueSide.IsSideReady = false;
            BlueSide.DiscardSide(isBlueSide: true);

            _spellCardResults = null;

            RedSide.IsSideReady = false;
            RedSide.DiscardSide(isBlueSide: false);

            Modifiers.Clear();

            NextAction = BoardState.DRAW_CARDS;
        }

        /// <summary>
        ///     Remplace le type actuel du héros avec une carte de la main du joueur.
        /// </summary>
        /// <param name="isBlueSide">Détermine quel joueur on cible</param>
        /// <param name="replacement">La carte utilisée pour remplacer le type du héros</param>
        /// <returns>Si l'opération a eu lieu avec succès</returns>
        public bool ReplacePlayerHeroWithCard(bool isBlueSide, ElementCard replacement)
        {
            return GetAllyBoardSide(isBlueSide).Player.ReplaceHeroType(replacement);
        }

        /// <summary>
        ///     Annule le remplacement du type du héros Si effectué pendant ce tour de jeu (impossible sinon).
        /// </summary>
        /// <param name="isBlueSide">Détermine quel joueur on cible</param>
        /// <returns>Si l'opération a eu lieu avec succès</returns>
        public bool CancelReplacePlayerHero(bool isBlueSide)
        {
            return GetAllyBoardSide(isBlueSide).Player.CancelHeroTypeReplacement();
        }
    }
}
