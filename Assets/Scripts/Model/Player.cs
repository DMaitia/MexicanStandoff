using System;
using Control;
using Probability;
using UnityEngine;

namespace Model
{
    public class Player : ITimer
    {
        private readonly int _id;
        
        private int _hp;

        private DateTime _nextActionDateTime;

        private DateTime _lastActionDateTime;
        
        private TimeSpan _pausedGameRemainingTimeToWakeUp;
        
        private Distribution _distribution;
        
        private bool _gameIsPaused = false;

        public Player(int id, int initialHp, Distribution distribution, int secondsBetweenActions)
        {
            _id = id;
            _hp = initialHp;
            _distribution = distribution;
            _lastActionDateTime = DateTime.Now;
            _nextActionDateTime = _lastActionDateTime + new TimeSpan(0,0,0,secondsBetweenActions);
        }
        
        public int Id => _id;

        public int Hp
        {
            get => _hp;
            set => _hp = value;
        }

        public DateTime NextActionDateTime
        {
            get => _nextActionDateTime;
            set => _nextActionDateTime = value;
        }

        public DateTime LastActionDateTime
        {
            get => _lastActionDateTime;
            set => _lastActionDateTime = value;
        }
        
        public bool IsAlive()
        {
            return _hp > 0;
        }
        
        public void Strike(Player target)
        {
            Strike strike = new Strike(_distribution);
            target.ReceiveStrike(strike);
        }
        
        private void ReceiveStrike(Strike strike)
        {
            var newHp = _hp - strike.GetValue();
            _hp =  newHp >= 0 ? newHp : 0;
        }
    
        public void Heal()
        {
            Healing healing = new Healing(_distribution);
            _hp += healing.GetValue();
        }
        
        public bool WaitingTimeToActionIsOver()
        {
            return _nextActionDateTime < DateTime.Now;
        }

        public void SetPause(bool pauseGame)
        {
            _gameIsPaused = pauseGame;
            if (pauseGame)
            {
                _pausedGameRemainingTimeToWakeUp = _nextActionDateTime - DateTime.Now;
            }
            else
            {
                _nextActionDateTime = DateTime.Now + _pausedGameRemainingTimeToWakeUp;
            }
        }
        
        public double SecondsToNextAction()
        {
            return (NextActionDateTime - DateTime.Now).TotalSeconds;
        }
    }
}