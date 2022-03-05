using System;
using JTools;
using UnityEngine;

namespace ProjectTilly.Menu.Options
{
    [Serializable]
    public class ClampedValueInt : LoopingValue
    {
        public ClampedValueInt(int minValue, int maxValue, int startingValue) : base(minValue, maxValue, startingValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            currentValue = startingValue;
        }
        

        protected override int ValueChecks(int adjustment)
        {
            currentValue += adjustment;
            
            if (currentValue < minValue)
                return minValue;
            if (currentValue > maxValue)
                return maxValue;

            return currentValue;
        }
    }
}