using System;
using System.Collections.Generic;
using Bots;
using Model;
using Probability;
using UnityEngine;

namespace Control
{
    public class Controller
    {
        private List<Player> _players;
        private GameView _gameView;
        private Settings _settings;
        private DateTime _endOfGameDateTime;
        
        public Controller(GameView gameView, Settings settings)
        {
            _gameView = gameView;
            _settings = settings;
            
            _players = new List<Player>();
            
            for (int id = 0; id < _settings.PlayersAmount; id++)
            {
                _players.Add(new Player(id, _settings.InitialHp, new Uniform(), _settings.MillisecondsBetweenActions));
            }

            _endOfGameDateTime = DateTime.Now + settings.MatchDuration;
        }

        public void Strike(int attackerId, int targetId)
        {
            Player attacker = _players[attackerId];
            Player target = _players[targetId];
            if (!target.IsAlive() || attacker.Id == target.Id)
            {
                return;
            }

            attacker.Strike(target);
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
            if (!target.IsAlive())
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
        public DateTime AssessBot(int botId, Bot.ActionType actionType)
        {
            var player = _players[botId];
            if (!player.WaitingTimeToActionIsOver()) return player.NextActionDateTime;
            
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
            
            player.LastActionDateTime = DateTime.Now;
            player.NextActionDateTime = DateTime.Now + new TimeSpan(0,0,0,_settings.MillisecondsBetweenActions);

            return player.NextActionDateTime;
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
            if (DateTime.Now > _endOfGameDateTime)
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
    }
}