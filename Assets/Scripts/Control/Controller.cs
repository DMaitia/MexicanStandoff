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
        private MockServer _mockServer;
    
        public Controller(GameView gameView, Settings settings)
        {
            _gameView = gameView;
            _settings = settings;
            
            _players = new List<Player>();
            _mockServer = new MockServer(this, _settings.PlayersAmount);
            for (int id = 0; id < _settings.PlayersAmount; id++)
            {
                Player player = new Player(id, _settings.InitialHp, new Uniform());
                _players.Add(player);
            }
            
            _mockServer.InitBots();
        }

        public bool Strike(int attackerId, int targetId)
        {
            Player attacker = _players[attackerId];
            Player target = _players[targetId];
            if (!target.IsAlive() && attacker.GetId() == target.GetId())
            {
                return false;
            }

            attacker.Strike(target);
            if (target.IsAlive())
            {
                _gameView.PerformStrikeAnimation(attackerId, targetId, target.GetHp());
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
            _gameView.PerformHealingAnimation(target.GetId());
            return true;
        }

        public void AssessBot(int botId, Bot.ActionType actionType)
        {
            
        }
    
    }
}