using System;
using Control;
using Probability;
using UnityEngine;

namespace Model
{
    public abstract class Action
    {
        protected static int _value;
    
        public Action(Distribution distribution)
        {
            _value = (int) (distribution.F(RandomSingleton.Instance.GetRandomNormalized()) * (Settings.MaximumDamage - Settings.MinimumDamage));
        }

        public int GetValue()
        {
            return _value;
        }

    }

    public class Strike : Action
    {
        public Strike(Distribution distribution) : base(distribution)
        {
        }
    }


    public class Healing : Action
    {
        public Healing(Distribution distribution) : base(distribution)
        {
        }
    }
}