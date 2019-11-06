using Probability;

namespace Control
{
    public abstract class Action
    {
        protected static int _value;
        public static int Value => _value;

        public Action(Distribution distribution)
        {
            _value = (int) (distribution.F(RandomSingleton.Instance.GetRandomNormalized()) * (Settings.MaximumDamage - Settings.MinimumDamage));
        }

        public abstract override string ToString();

        public int GetValue()
        {
            return _value;
        }

    }

    public class Strike : Action
    {
        public Strike(Distribution distribution) : base(distribution)
        {
        }

        public override string ToString()
        {
            return "Strike";
        }
    }


    public class Healing : Action
    {
        public Healing(Distribution distribution) : base(distribution)
        {
        }

        public override string ToString()
        {
            return "Healing";
        }
    }
}