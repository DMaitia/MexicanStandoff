using System;
using System.Collections.Generic;
using Bots;
using Model;
using Probability;

namespace Control
{
    public class Controller
    {
//    private uint _playerId;
        private List<Player> _players;

        private GameView _gameView;
        private Settings _settings;
    
        public Controller(GameView gameView, Settings settings)
        {
            _gameView = gameView;
            _settings = settings;
            
            _players = new List<Player>();
            
            for (int id = 0; id < _settings.PlayersAmount; id++)
            {
                _players.Add(new Player(id, _settings.InitialHp, new Uniform(), _settings.MillisecondsBetweenActions));
            }
            
        }

        public bool Strike(int attackerId, int targetId)
        {
            Player attacker = _players[attackerId];
            Player target = _players[targetId];
            if (!target.IsAlive() || attacker.Id == target.Id)
            {
                return false;
            }

            attacker.Strike(target);
            if (target.IsAlive())
            {
                _gameView.PerformStrikeAnimation(attackerId, targetId, target.Hp);
            }
            else
            {
                _gameView.PerformKillAnimation(attackerId, targetId);
            }
            return true;
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
            var weakestEnemy = _players[0];
            for (int i = 1; i < _players.Count; i++)
            {
                Player player = _players[i];
                if (player.Hp < weakestEnemy.Hp && weakestEnemy.Id != attacker.Id)
                {
                    weakestEnemy = player;
                }
            }

            return weakestEnemy;
        }
    }
}