using System;
using Control;
using Probability;
using UnityEngine;

namespace UnityScripts.Bots
{
    public class Bot : MonoBehaviour, ICountdown
    {
        public enum ActionType {Strike, Heal}
        
        private float _attackVsHealRate;
        private int _id;
        private Controller _controller;

        public static Bot CreateBot(GameObject doll, int id, float attackVsHealRate, Controller controller)
        {
            Bot bot = doll.AddComponent<Bot>();
            bot._id = id;
            bot._attackVsHealRate = attackVsHealRate;
            bot._controller = controller;
            bot.StopDateTime = DateTime.Now + new TimeSpan(0,0,0,Settings.SecondsBetweenActions);
            Debug.Log("Bot created");
            return bot;
        }

        public int Id => _id;
        
        public void Update()
        {
            if (!IsPaused && StopDateTime < DateTime.Now) //Bug Here
            {
                StopDateTime += new TimeSpan(0,0,0, Settings.SecondsBetweenActions);
                Debug.Log("Bot action: " + _id);
                _controller.AssessBot(_id, Act());
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