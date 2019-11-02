using Control;
using Probability;
using UnityEngine;

namespace Model
{
    public class Player
    {
        private readonly int _id;
        
        private int _hp;

        private Distribution _distribution;
        //Todo: add time to action
    
        public Player(int id, int initialHp, Distribution distribution)
        {
            _id = id;
            _hp = initialHp;
            _distribution = distribution;
        }

        public int GetId()
        {
            return _id;
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
    
        public int GetHp()
        {
            return _hp;
        }

        public void SetHp(int newHp)
        {
            _hp = newHp;
        }

        public bool CanStrike()
        {
            //Todo: check timer
            return true;
        }
    }
}