
using System;
using System.Threading;
using Control;
using Probability;
using UnityEngine;

namespace Bots
{
    public class Bot : MonoBehaviour
    {
        public enum ActionType {Strike, Heal}
        
        private float _attackVsHealRate;
        private int _id;
        private Controller _controller;
        private DateTime _dateToWakeUp;

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
            _dateToWakeUp = DateTime.Now + new TimeSpan(0, 0, 0, 5);
        }

        public int Id => _id;

        public DateTime DateToWakeUp
        {
            get => _dateToWakeUp;
            set => _dateToWakeUp = value;
        }

        public void Update()
        {
            if (_dateToWakeUp < DateTime.Now)
            {
                _dateToWakeUp = _controller.AssessBot(_id, Act());
            }
        }

        private ActionType Act()
        {
            var n = RandomSingleton.Instance.GetRandomNormalized();
            return n < _attackVsHealRate ? ActionType.Strike : ActionType.Heal;
        }
        
    }
}