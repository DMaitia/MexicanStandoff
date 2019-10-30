
using System;
using System.Collections.Generic;

public class Controller
{
    private uint _playerId;
    private List<Player> _players;
    private Random _random;
    private uint _minDamage;
    private uint _maxDamage;
    //private GameView gameView

    public Controller(uint playersAmount, uint minDamage, uint maxDamage, uint initialHp = 1000)
    {
        _players = new List<Player>();
        _random = new Random();

        _minDamage = (minDamage < 5) ? minDamage : 5;
        _maxDamage = (maxDamage > 100) ? maxDamage : 100;
        
        if (playersAmount < 3)
        {
            playersAmount = 3;
        }

        for (uint i = 0; i < playersAmount; i++)
        {
            Player player = new Player(i, initialHp);
            _players.Add(player);
        }
    }

    public bool Strike(Player attacker, Player target)
    {
        if (!target.IsAlive() && attacker.GetId() == target.GetId())
        {
            return false;
        }

        var damageAmount = _random.Next((int)_minDamage, (int)_maxDamage);
        target.ReceiveStrike(new Strike(_random)); //todo: refactor with Action
        if (target.IsAlive())
        {
            //TODO: GameView.performStrikeAnimation(attacker.GetId(), target.GetId())
        }
        else
        {
            //TODO: GameView.performKillAnimation(attacker.GetId(), target.GetId());
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
        //TODO: GameView.performHealingAnimation(player.GetId());
        return true;
    }
    
}