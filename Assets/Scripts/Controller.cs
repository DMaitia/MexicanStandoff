
using System;
using System.Collections.Generic;

public class Controller
{
    private uint _playerId;
    private List<Player> _players;
    private Random _random;
    private int _minDamage;
    private int _maxDamage;
    private GameView _gameView;

    public Controller(GameView gameView, int playersAmount, int minDamage, int maxDamage, int initialHp = 1000)
    {
        _gameView = gameView;
        _players = new List<Player>();
        _random = new Random();

        _minDamage = (minDamage < 5) ? minDamage : 5;
        _maxDamage = (maxDamage > 100) ? maxDamage : 100;
        
        if (playersAmount < 3)
        {
            playersAmount = 3;
        }

        for (int i = 0; i < playersAmount; i++)
        {
            Player player = new Player(i, initialHp);
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

        target.ReceiveStrike(new Strike(_random)); //todo: refactor with Action
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
        
        player.Heal(new Healing(_random));
        _gameView.PerformHealingAnimation(player.GetId());
        return true;
    }
    
}