
using Probability;

namespace Bots
{
    public class Bot
    {
        public enum ActionType {Strike, Heal}
        
        private readonly float _attackVsHealRate;
        private readonly int _id;
        private MockServer _mockServer;
        
        public Bot(int id, MockServer mockServer, float attackVsHealRate)
        {
            _id = id;
            _attackVsHealRate = attackVsHealRate;
            _mockServer = mockServer;
        }

        public int GetId()
        {
            return _id;
        }
        
        public void Run()
        {
            _mockServer.InformController(this);
        }
        public ActionType Act()
        {
            var n = RandomSingleton.Instance.GetRandomNormalized();
            return n < _attackVsHealRate ? ActionType.Strike : ActionType.Heal;
        }
    }
}