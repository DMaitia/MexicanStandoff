using System;
using System.Collections;

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
			
        }

        IEnumerator RunBot()
        {
            yield return null;
        }
    }
}