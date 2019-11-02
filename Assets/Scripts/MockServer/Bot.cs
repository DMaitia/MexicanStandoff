using System.Collections;
using Probability;

namespace MockServer
{
    public abstract class Bot
    {

        protected Distribution Distribution;
        protected float AttackVsHealRate;
		
        public Bot(float attackVsHealRate) {
            AttackVsHealRate = attackVsHealRate;
        }
		
        public IEnumerator Run()
        {
            yield return null;
        }

        protected abstract Action Act();
    }

    public class UniformBot : Bot {
		
        public UniformBot(float attackVsHealRate) : base(attackVsHealRate) {
            Distribution = new Uniform();
        }

        protected override Action Act()
        {
            return (RandomSingleton.Instance.GetRandomNormalized() < AttackVsHealRate) ? (Action) new Strike(Distribution) : new Healing(Distribution);
        }
    }

    public class ExponentialBot : Bot {

        public ExponentialBot(float attackVsHealRate) : base(attackVsHealRate) {
            Distribution = new Exponential();
        }

        protected override Action Act()
        {
            return (RandomSingleton.Instance.GetRandomNormalized() < AttackVsHealRate) ? (Action) new Strike(Distribution) : new Healing(Distribution);
        }
    }
}