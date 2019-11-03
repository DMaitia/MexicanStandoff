
using System;
using System.Threading;
using Probability;

namespace Bots
{
    public class Bot
    {
        public enum ActionType {Strike, Heal}
        
        private readonly float _attackVsHealRate;
        private readonly int _id;
        private readonly MockServer _mockServer;
        private DateTime _dateToWakeUp;
        
        public Bot(int id, MockServer mockServer, float attackVsHealRate)
        {
            _id = id;
            _attackVsHealRate = attackVsHealRate;
            _mockServer = mockServer;
            _dateToWakeUp = DateTime.Now + new TimeSpan(0, 0, 0, 5);
        }

        public int Id => _id;

        public DateTime DateToWakeUp
        {
            get => _dateToWakeUp;
            set => _dateToWakeUp = value;
        }

        public void Run()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(_dateToWakeUp - DateTime.Now);
                _mockServer.InformController(this);
            }
        }
        
        public ActionType Act()
        {
            var n = RandomSingleton.Instance.GetRandomNormalized();
            return n < _attackVsHealRate ? ActionType.Strike : ActionType.Heal;
        }
        
    }
}