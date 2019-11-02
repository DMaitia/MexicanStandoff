using System;

namespace Probability
{
    public abstract class Distribution {


        public Distribution()
        {
        }

        //F(x) = P(X <= x)	i.e. the distribution function
        public abstract float F(float x);

    }

    /*
     * Uniform probability distribution ~U(lowerLimit,upperLimit)
     */
    public class Uniform : Distribution {
		
        private readonly int _lowerLimit;
        private readonly int _upperLimit;
		 
        public Uniform(int lowerLimit = 0, int upperLimit = 1) {
            _lowerLimit = lowerLimit;
            _upperLimit = upperLimit;
        }

        public override float F(float x)
        {
            if (x < _lowerLimit)
                return 0;
            if (x >= _upperLimit)
                return 1;
            return (x - _lowerLimit) / (_upperLimit - _lowerLimit);	
        }
    }


    /*
     * Exponential probability distribution ~Exp(lambda = 2)
     */
    public class Exponential : Distribution {
        private float _lambda;

        public Exponential(float lambda = 2) {
            _lambda = lambda;
        }

        public override float F(float x) {
            return (float) (1 - Math.Exp(-x*_lambda));
        }
    }
}