using System;
using System.Collections;
using Control;

namespace MockServer
{
    public class MockServer
    {
        private Controller _controller;
        private int _playersAmount;
        private Random _random;

        public MockServer(Controller controller, int playersAmount) {
            _controller = controller;
            _random = new Random();
            _playersAmount = playersAmount;
        }

        private void InitBots() {
            for (int id = 1; id < _playersAmount; id++)
            {
                
            }
        }

        IEnumerator RunBot()
        {
            yield return null;
        }
    }
}