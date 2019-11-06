using System;
using Probability;

namespace Model
{
    public class Player : ICountdown
    {
        private readonly int _id;

        private Distribution _distribution;
        
        public Player(int id, int initialHp, Distribution distribution, int secondsBetweenActions)
        {
            _id = id;
            Hp = initialHp;
            _distribution = distribution;
            StartDateTime = DateTime.Now;
            StopDateTime = StartDateTime + new TimeSpan(0,0,0,secondsBetweenActions);
            IsPaused = false;
        }
        
        public int Id => _id;

        public int Hp { get; private set; }

        public bool IsAlive()
        {
            return Hp > 0;
        }
        
        public void Strike(Player target)
        {
            Strike strike = new Strike(_distribution);
            target.ReceiveStrike(strike);
        }
        
        private void ReceiveStrike(Strike strike)
        {
            var newHp = Hp - strike.GetValue();
            Hp =  newHp >= 0 ? newHp : 0;
        }
    
        public void Heal()
        {
            Healing healing = new Healing(_distribution);
            Hp += healing.GetValue();
        }
        
        public bool WaitingTimeToActionIsOver()
        {
            return StopDateTime < DateTime.Now;
        }

        public bool IsPaused { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime StopDateTime { get; set; }
        public TimeSpan TimeRemaining { get; set; }

        public void SetPause(bool pauseGame)
        {
            IsPaused = pauseGame;
            if (pauseGame)
            {
                TimeRemaining = StopDateTime - DateTime.Now;
            }
            else
            {
                StopDateTime = DateTime.Now + TimeRemaining;
            }
        }
        
        public double SecondsToNextAction()
        {
            return (StopDateTime - DateTime.Now).TotalSeconds;
        }
    }
}