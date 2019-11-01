namespace DefaultNamespace
{
    public class Settings
    {
        //Ranges
        public const float MIN_BOT_ATTACK_HEAL_RATE = 0.1f;
        public const float MAX_BOT_ATTACK_HEAL_RATE = 0.9f;
        public const int MIN_PLAYERS = 3;
        public const int MIN_DAMAGE = 1;
        public const int MIN_HP = 1;
        
        private int _playersAmount;
        private int _minDamage;
        private int _maxDamage;
        private int _initialHp;
        private int _meanSecondsBetweenActions;
        private float _botAttackHealRate;

        public Settings(int playersAmount, int minDamage, int maxDamage, int initialHp, int meanSecondsBetweenActions, float botAttackHealRate)
        {
            _playersAmount = playersAmount;
            _minDamage = minDamage;
            _maxDamage = maxDamage;
            _initialHp = initialHp;
            _meanSecondsBetweenActions = meanSecondsBetweenActions;
            _botAttackHealRate = botAttackHealRate;
        }

        public int PlayersAmount
        {
            get => _playersAmount;
            set => _playersAmount = (value < MIN_PLAYERS) ? MIN_PLAYERS : value;
        }

        public int MinDamage
        {
            get => _minDamage;
            set => _minDamage = (value < MIN_DAMAGE) ? MIN_DAMAGE : value;
        }

        public int MaxDamage
        {
            get => _maxDamage;
            set => _maxDamage = (value < _minDamage) ? _minDamage + 1 : value;
        }

        public int InitialHp
        {
            get => _initialHp;
            set => _initialHp = (value <= 0) ? MIN_HP : value;
        }

        public int MeanTimeBetweenActions
        {
            //TODO: do the random distribution stuff
            get => _meanSecondsBetweenActions;
            set => _meanSecondsBetweenActions = (value <= 0) ? 1 : value;
        }

        public float BotAttackHealRate
        {
            get => _botAttackHealRate;
            set
            {
                if (value < MIN_BOT_ATTACK_HEAL_RATE)
                    _botAttackHealRate = MIN_BOT_ATTACK_HEAL_RATE;
                else if (value > MAX_BOT_ATTACK_HEAL_RATE)
                    _botAttackHealRate = MAX_BOT_ATTACK_HEAL_RATE;
                else
                    _botAttackHealRate = value;
            }
        }
    }
}