using UnityEngine;

public class Player
{
    private int _id;
    private int _hp;
    //Todo: add time to action
    
    public Player(int id, int initialHp)
    {
        _id = id;
        _hp = initialHp;
    }

    public int GetId()
    {
        return _id;
    }

    public bool IsAlive()
    {
        return _hp > 0;
    }

    public void ReceiveStrike(Strike strike)
    {
        int newHp = _hp - strike.GetValue();
        _hp =  newHp >= 0 ? newHp : 0;
        Debug.Log("Player " + _id + " has been hit. New HP: " + _hp);
    }
    
    public void Heal(Healing healing)
    {
        int newHp = _hp + healing.GetValue();
        _hp = (newHp < 1000 ? newHp : 1000); //TODO: deharcode those numbers
    }
    
    public int GetHp()
    {
        return _hp;
    }

    public void SetHp(int newHp)
    {
        _hp = newHp;
    }

    public bool CanStrike()
    {
        //Todo: check timer
        return true;
    }
}