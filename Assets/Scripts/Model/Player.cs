using System;
using Control;
using Probability;
using UnityEngine;

namespace Model
{
    public class Player
    {
        private readonly int _id;
        
        private int _hp;

        private DateTime _nextActionDateTime;

        private DateTime _lastActionDateTime;
        
        private Distribution _distribution;
        
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
            Debug.Log("Player " + _id + " has been hit. New HP: " + _hp);
        }
    
        public void Heal()
        {
            Healing healing = new Healing(_distribution);
            int newHp = _hp + healing.GetValue();
            _hp = (newHp < 1000 ? newHp : 1000); //TODO: deharcode those numbers
        }
        
        public bool WaitingTimeToActionIsOver()
        {
            return _nextActionDateTime < DateTime.Now;
        }

        public double SecondsToNextAction()
        {
            return (NextActionDateTime - DateTime.Now).TotalSeconds;
        }
    }
}