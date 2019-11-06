using System;

namespace Control
{
    public static class Settings
    {
        //Ranges
        private const float MinBotAttackHealRate = 0.1f;
        private const float MaxBotAttackHealRate = 0.9f;
        private const int MinPlayers = 3;
        private const int MinDamage = 1;
        private const int MinHp = 1;
        
        private static int _playersAmount = 3;
        private static int _minDamage = 10;
        private static int _maxDamage = 100;
        private static int _initialHp = 1000;
        private static int _secondsBetweenActions = 10;
        private static float _botAttackHealRate = 0.5f;
        private static TimeSpan _matchDuration = new TimeSpan(0,0,1,0);

        public static void SetSettings(int playersAmount, int minDamage, int maxDamage, int initialHp, int secondsBetweenActions, float botAttackHealRate, TimeSpan matchDuration)
        {
            _playersAmount = playersAmount;
            _minDamage = minDamage;
            _maxDamage = maxDamage;
            _initialHp = initialHp;
            _secondsBetweenActions = secondsBetweenActions;
            _botAttackHealRate = botAttackHealRate;
            _matchDuration = matchDuration;
        }

        public static int PlayersAmount
        {
            get => _playersAmount;
            set => _playersAmount = (value < MinPlayers) ? MinPlayers : value;
        }

        public static TimeSpan MatchDuration
        {
            get => _matchDuration;
            set => _matchDuration = value;
        }
        public static int MinimumDamage
        {
            get => _minDamage;
            set => _minDamage = (value < MinDamage) ? MinDamage : value;
        }

        public static int MaximumDamage
        {
            get => _maxDamage;
            set => _maxDamage = (value < _minDamage) ? _minDamage + 1 : value;
        }

        public static int InitialHp
        {
            get => _initialHp;
            set => _initialHp = (value <= 0) ? MinHp : value;
        }

        public static int SecondsBetweenActions
        {
            get => _secondsBetweenActions;
            set => _secondsBetweenActions = (value <= 0) ? 1 : value;
        }

        public static float BotAttackHealRate
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