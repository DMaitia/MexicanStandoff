
using System;
using System.Threading;
using Control;
using Probability;
using UnityEngine;

namespace Bots
{
    public class Bot : MonoBehaviour, ICountdown
    {
        public enum ActionType {Strike, Heal}
        
        private float _attackVsHealRate;
        private int _id;
        private Controller _controller;
        private const int SecondsToWaitAfterStart = 5;

        public static Bot CreateBot(GameObject doll, int id, float attackVsHealRate, Controller controller)
        {
            Bot bot = doll.AddComponent<Bot>();
            bot._id = id;
            bot._attackVsHealRate = attackVsHealRate;
            bot._controller = controller;
            return bot;
        }
        
        private Bot(int id, float attackVsHealRate, Controller controller)
        {
            _id = id;
            _attackVsHealRate = attackVsHealRate;
            _controller = controller;
            StopDateTime = DateTime.Now + new TimeSpan(0, 0, 0, SecondsToWaitAfterStart);
            IsPaused = false;
        }

        public int Id => _id;


        public void Update()
        {
            if (!IsPaused && StopDateTime < DateTime.Now)
            {
                StopDateTime = _controller.AssessBot(_id, Act());
            }
        }

        public bool IsPaused { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime StopDateTime { get; set; }
        public TimeSpan TimeRemaining { get; set; }
        public int SecondsBetweenActions { get; set; }

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
        
        private ActionType Act()
        {
            var n = RandomSingleton.Instance.GetRandomNormalized();
            return n < _attackVsHealRate ? ActionType.Strike : ActionType.Heal;
        }
        
    }
}