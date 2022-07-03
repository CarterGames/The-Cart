// ----------------------------------------------------------------------------
// LoopingValueInt.cs
// 
// Description: A helper class to automatically loop a value between a range.
//              Useful for menus & UI elements.
// ----------------------------------------------------------------------------

using System;
using UnityEngine;

namespace Scarlet.General
{
    [Serializable]
    public class LoopingValueInt : ValueSystemBase<int>
    {
        /// <summary>
        /// Converts the value to its read value implicitly for use.
        /// </summary>
        /// <param name="loopingValueInt">The value to convert</param>
        /// <returns>Int</returns>
        public static implicit operator int(LoopingValueInt loopingValueInt)
        {
            return loopingValueInt.Value;
        }


        /// <summary>
        /// Constructor | Creates a new looping value.
        /// </summary>
        /// <param name="minValue">The min/lower value</param>
        /// <param name="maxValue">The max/upper value</param>
        public LoopingValueInt(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            currentValue = minValue;
        }
        
        
        /// <summary>
        /// Constructor | Creates a new looping value.
        /// </summary>
        /// <param name="minValue">The min/lower value</param>
        /// <param name="maxValue">The max/upper value</param>
        /// <param name="startingValue">The starting value</param>
        public LoopingValueInt(int minValue, int maxValue, int startingValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            currentValue = startingValue;
        }

        
        /// <summary>
        /// Increments the value by the amount entered.
        /// </summary>
        /// <param name="amount">The amount to increment by</param>
        public void IncrementValue(int amount)
        {
            currentValue = ValueChecks(amount);
        }


        protected override int ValueChecks(int adjustment)
        {
            currentValue += adjustment;

            if (currentValue > maxValue)
            {
                var _remainder = currentValue - maxValue;
                
                while (currentValue > maxValue)
                {
                    currentValue = minValue + _remainder;
                    _remainder = currentValue - maxValue;
                }

                return currentValue;
            }

            if (currentValue < minValue)
            {
                var _remainder = (currentValue - minValue) + maxValue;

                while (currentValue < minValue)
                {
                    currentValue = maxValue - _remainder;
                    _remainder = (currentValue - minValue) + maxValue;
                }

                return currentValue;
            }

            return currentValue;
        }
    }
}