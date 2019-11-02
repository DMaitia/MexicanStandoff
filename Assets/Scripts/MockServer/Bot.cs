using System.Collections;
using Control;
using Probability;

namespace MockServer
{
    public class Bot
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

//        protected Action Act()
//        {
//            
//        }
    }
}