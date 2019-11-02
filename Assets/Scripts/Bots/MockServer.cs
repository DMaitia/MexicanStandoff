using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Control;
using UnityEngine;

namespace Bots
{
    public class MockServer
    {
        private readonly object _controllerAccessLock = new object();

        private Controller _controller;
        private int _playersAmount;
        private List<Thread> _threads;

        public MockServer(Controller controller, int playersAmount) {
            _controller = controller;
            _playersAmount = playersAmount;
            _threads = new List<Thread>();
        }

        public void InitBots() {
            for (int id = 1; id < _playersAmount; id++)
            {
                Thread thread = new Thread(new Bot(id, this, 0.5f).Run);
                _threads.Add(thread);
                thread.Start();
            }
        }

        private void PauseBots()
        {
            
        }

        public void InformController(Bot bot)
        {
            lock (_controllerAccessLock)
            {
                Debug.Log("Bot " + bot.GetId() + " informed the controller that it is going to " + bot.Act());
                _controller.AssessBot(bot.GetId(), bot.Act());
            }
        }
        
    }
}