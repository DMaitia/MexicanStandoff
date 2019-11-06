using System;
using System.Collections.Generic;
using Bots;
using Model;
using Probability;
using UnityEngine;

namespace Control
{
    public class Controller : ICountdown
    {
        private List<Player> _players;
        private GameView _gameView;

        private int _secondsBetweenActions;
        public Controller(GameView gameView)
        {
            _gameView = gameView;
            
            _players = new List<Player>();
            
            for (var id = 0; id < Settings.PlayersAmount; id++)
            {
                _players.Add(new Player(id, Settings.InitialHp, new Uniform(), Settings.SecondsBetweenActions));
            }

            StopDateTime = DateTime.Now + Settings.MatchDuration;
            SecondsBetweenActions = Settings.SecondsBetweenActions;
        }

        public List<Player> GetPlayers()
        { 
            return _players;
        }

        public void Strike(int attackerId, int targetId)
        {
            Player attacker = _players[attackerId];
            Player target = _players[targetId];
            if (!target.IsAlive() || attacker.Id == target.Id || DateTime.Now < attacker.StopDateTime)
            {
                return;
            }

            if (attacker.Strike(target) == false) return;
            
            if (target.IsAlive())
            {
                _gameView.PerformStrikeAnimation(attackerId, targetId, target.Hp);
            }
            else
            {
                _gameView.PerformKillAnimation(attackerId, targetId);
                _gameView.HideKilledPlayer(targetId);
            }

            if (IsGameOver())
            {
                _gameView.FinishGame();
            };
        }

        public bool Heal(int targetId)
        {
            Player target = _players[targetId];
            if (!target.IsAlive() || DateTime.Now < target.StopDateTime)
            {
                return false;
            }
        
            target.Heal();
            _gameView.PerformHealingAnimation(target.Id);
            return true;
        }

        /*
         * Return: Amount of milliseconds for the bot to wait until a new action is possible.
         */
        public void AssessBot(int botId, Bot.ActionType actionType)
        {
            var player = _players[botId];
            if (!player.WaitingTimeToActionIsOver()) return;
            
            switch (actionType)
            {
                case Bot.ActionType.Heal:
                {
                    Heal(player.Id);
                    break;
                }
                case Bot.ActionType.Strike:
                {
                    Strike(player.Id, LowerHpPlayer(player).Id);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
            }
        }

        private Player LowerHpPlayer(Player attacker)
        {
            //Take the first alive enemy
            Player weakestEnemy = GetFirstEnemyAlive(attacker);
            if (weakestEnemy == null)
            {
                return null;
            }
            
            foreach (var player in _players)
            {
                if ( weakestEnemy.Id != player.Id && player.Hp < weakestEnemy.Hp && player.IsAlive())
                {
                    weakestEnemy = player;
                }
            }

            return weakestEnemy;
        }

        private Player GetFirstEnemyAlive(Player attacker)
        {
            foreach (var player in _players)
            {
                if (player.Id != attacker.Id && player.IsAlive())
                {
                    return player;
                }
            }
            return null;
        }

        private bool IsGameOver()
        {
            if (DateTime.Now > StopDateTime)
            {
                return true;
            }

            int playersAlive = 0;
            foreach (var player in _players)
            {
                if (player.IsAlive())
                {
                    playersAlive++;
                }
            }

            if (playersAlive <= 1)
            {
                return true;
            }

            return false;
        }

        public bool IsPaused { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime StopDateTime { get; set; }
        public TimeSpan TimeRemaining { get; set; }
        public int SecondsBetweenActions { get; set; }

        public void SetPause(bool pauseGame)
        {
            if (pauseGame)
            {
                TimeRemaining =  StopDateTime - DateTime.Now;
                foreach (var player in _players)
                {
                    player.SetPause(true);
                }
            }
            else
            {
                StopDateTime = DateTime.Now + TimeRemaining;
                foreach (var player in _players)
                {
                    player.SetPause(false);
                }
            }
            _gameView.PauseBots(pauseGame);
            IsPaused = pauseGame;
        }
    }
}