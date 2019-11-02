namespace Control
{
    public class Settings
    {
        //Ranges
        public const float MinBotAttackHealRate = 0.1f;
        public const float MaxBotAttackHealRate = 0.9f;
        public const int MinPlayers = 3;
        public const int MinDamage = 1;
        public const int MinHp = 1;
        
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
            set => _playersAmount = (value < MinPlayers) ? MinPlayers : value;
        }

        public int MinimumDamage
        {
            get => _minDamage;
            set => _minDamage = (value < MinDamage) ? MinDamage : value;
        }

        public int MaximumDamage
        {
            get => _maxDamage;
            set => _maxDamage = (value < _minDamage) ? _minDamage + 1 : value;
        }

        public int InitialHp
        {
            get => _initialHp;
            set => _initialHp = (value <= 0) ? MinHp : value;
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
                if (value < MinBotAttackHealRate)
                    _botAttackHealRate = MinBotAttackHealRate;
                else if (value > MaxBotAttackHealRate)
                    _botAttackHealRate = MaxBotAttackHealRate;
                else
                    _botAttackHealRate = value;
            }
        }
    }
}