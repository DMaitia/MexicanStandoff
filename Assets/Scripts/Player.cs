public class Player
{
    private uint _id;
    private uint _hp;
    //Todo: add time to action
    
    public Player(uint id, uint initialHp)
    {
        _id = id;
        _hp = initialHp;
    }

    public uint GetId()
    {
        return _id;
    }

    public bool IsAlive()
    {
        return _hp > 0;
    }

    public void ReceiveStrike(Strike strike)
    {
        int newHp = (int) _hp - strike.GetValue();
        _hp = (uint) (newHp >= 0 ? newHp : 0);
    }
    
    public void Heal(Healing healing)
    {
        int newHp = (int) _hp + healing.GetValue();
        _hp = (uint) (newHp < 1000 ? newHp : 1000); //TODO: deharcode those numbers
    }
    
    public uint GetHp()
    {
        return _hp;
    }

    public void SetHp(uint newHp)
    {
        _hp = newHp;
    }

    public bool CanStrike()
    {
        //Todo: check timer
        return true;
    }
}