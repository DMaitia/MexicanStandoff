using System;

public abstract class Action
{
    protected uint MinPoints;
    protected uint MaxPoints;
    protected Random Random;
    protected static int Value;

    public Action(Random random, uint minPoints = 10, uint maxPoints = 100)
    {
        Random = random;
        MinPoints = minPoints;
        MaxPoints = maxPoints;
        Value = random.Next((int)MinPoints, (int)MaxPoints);
    }

    public int GetValue()
    {
        return Value;
    }
        
}

public class Strike : Action
{
    public Strike(Random random) : base(random)
    {
    }
    
}


public class Healing : Action
{
    public Healing(Random random) : base(random)
    {
    }
    
}