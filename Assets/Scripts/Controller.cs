
using System;
using System.Collections.Generic;
using DefaultNamespace;
using Probability;

public class Controller
{
    private uint _playerId;
    private List<Player> _players;
    private Random _random;
    private int _minDamage;
    private int _maxDamage;
    private GameView _gameView;
    private Settings _settings;
    
    public Controller(GameView gameView, Settings settings)
    {
        _gameView = gameView;
        _settings = settings;
        
        _players = new List<Player>();
        _random = new Random();

        for (int i = 0; i < _settings.PlayersAmount; i++)
        {
            Player player = new Player(i, _settings.InitialHp);
            _players.Add(player);
        }
    }

    public bool Strike(int attackerId, int targetId)
    {
        Player attacker = _players[attackerId];
        Player target = _players[targetId];
        if (!target.IsAlive() && attacker.GetId() == target.GetId())
        {
            return false;
        }

        target.ReceiveStrike(new Strike(new Uniform())); //todo: refactor with Action
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

    public bool Heal(Player player)
    {
        if (!player.IsAlive())
        {
            return false;
        }
        
        player.Heal(new Healing(new Uniform()));
        _gameView.PerformHealingAnimation(player.GetId());
        return true;
    }
    
}