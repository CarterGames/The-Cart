/*
 * Copyright (c) 2018-Present Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;

namespace CarterGames.Common.General
{
    [Serializable]
    public class ClampedValueFloat : ValueSystemBase<float>
    {
        /// <summary>
        /// Converts the value to its read value implicitly for use.
        /// </summary>
        /// <param name="clampedValue">The value to convert</param>
        /// <returns>Int</returns>
        public static implicit operator float(ClampedValueFloat clampedValue)
        {
            return clampedValue.Value;
        }
        
        
        /// <summary>
        /// Constructor | Creates a new clamped int value.
        /// </summary>
        /// <param name="minValue">The min/lower value</param>
        /// <param name="maxValue">The max/upper value</param>
        public ClampedValueFloat(float minValue, float maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            currentValue = minValue;
        }
        
        
        /// <summary>
        /// Constructor | Creates a new clamped int value.
        /// </summary>
        /// <param name="minValue">The min/lower value</param>
        /// <param name="maxValue">The max/upper value</param>
        /// <param name="startingValue">The starting value</param>
        public ClampedValueFloat(float minValue, float maxValue, float startingValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            currentValue = startingValue;
        }
        
        /// <summary>
        /// Increments the value by the amount entered.
        /// </summary>
        /// <param name="amount">The amount to increment by</param>
        public void IncrementValue(float amount)
        {
            currentValue = ValueChecks(amount);
        }

        protected override float ValueChecks(float adjustment)
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