using System;

namespace Probability
{
    public class RandomSingleton
    {
        public const int LOWER_LIMIT = 0;
        public const int UPPER_LIMIT = 100;
		
        private readonly Random _random;
        private static RandomSingleton _instance = null;

        RandomSingleton() {
            _random = new Random();
        }

        public static RandomSingleton Instance {
            get {
                if (_instance == null) {
                    _instance = new RandomSingleton();
                }
                return _instance;
            }
        }

        /**
         * GetRandom
         * Returns a number between LOWER_LIMIT and UPPER_LIMIT
         */
        public int GetRandom() {
            return _random.Next(LOWER_LIMIT, UPPER_LIMIT);
        }

        public int GetRandom(int lowerLimit, int upperLimit)
        {
            return _random.Next(lowerLimit, upperLimit);
        }

        /**
         * GetRandomNormalized()
         * Returns a float in the range [0,1]
         */
        public float GetRandomNormalized()
        {
            return (float) (_random.Next(LOWER_LIMIT, UPPER_LIMIT) - LOWER_LIMIT) / (UPPER_LIMIT - LOWER_LIMIT);
        }
    }
}