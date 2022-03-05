using System;
using JTools;
using UnityEngine;

namespace ProjectTilly.Menu.Options
{
    [Serializable]
    public class LoopingValue
    {
        [SerializeField, ReadOnly] protected int minValue;
        [SerializeField, ReadOnly] protected int maxValue;
        [SerializeField, ReadOnly] protected int currentValue;

        public int ReadValue => currentValue;
        public int MinValue => minValue;
        public int MaxValue => maxValue;


        public LoopingValue(int minValue, int maxValue, int startingValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            currentValue = startingValue;
        }

        
        public void ChangeValue(int amount)
        {
            currentValue = ValueChecks(amount);
        }


        protected virtual int ValueChecks(int adjustment)
        {
            currentValue += adjustment;
            
            if (currentValue < minValue)
                return maxValue;
            if (currentValue > maxValue)
                return minValue;

            return currentValue;
        }
    }
}