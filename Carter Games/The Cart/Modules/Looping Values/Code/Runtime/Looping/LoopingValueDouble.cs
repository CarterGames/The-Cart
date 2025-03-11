#if CARTERGAMES_CART_MODULE_LOOPINGVALUES

/*
 * Copyright (c) 2025 Carter Games
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

namespace CarterGames.Cart.Modules.LoopingValues
{
    /// <summary>
    /// A looping double value.
    /// </summary>
    [Serializable]
    public sealed class LoopingValueDouble : LoopingValueBase<double>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the difference between the min & max values.
        /// </summary>
        protected override double Difference => MaxValue - MinValue;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Creates a new looping value.
        /// </summary>
        /// <param name="minValue">The min/lower value</param>
        /// <param name="maxValue">The max/upper value</param>
        public LoopingValueDouble(double minValue, double maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            currentValue = minValue;
        }
        
        
        /// <summary>
        /// Creates a new looping value.
        /// </summary>
        /// <param name="minValue">The min/lower value</param>
        /// <param name="maxValue">The max/upper value</param>
        /// <param name="startingValue">The starting value</param>
        public LoopingValueDouble(double minValue, double maxValue, double startingValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            currentValue = startingValue;
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Operators
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Converts the value to its read value implicitly for use.
        /// </summary>
        /// <param name="loopingValue">The value to convert</param>
        /// <returns>Int</returns>
        public static implicit operator double(LoopingValueDouble loopingValue)
        {
            return loopingValue.Value;
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Sets the value to the required value.
        /// </summary>
        /// <param name="value">The value to set to.</param>
        /// <param name="notify">Should it notify the change?</param>
        /// <returns>The updated value.</returns>
        protected override double SetToValue(double value, bool notify = true)
        {
            currentValue = value;
            return KeepValueInRange(notify);
        }
        

        /// <summary>
        /// Updates the value by the required value.
        /// </summary>
        /// <param name="adjustment">The adjustment to make.</param>
        /// <param name="notify">Should it notify the change?</param>
        /// <returns>The updated value.</returns>
        protected override double UpdateValue(double adjustment, bool notify = true)
        {
            currentValue += adjustment;
            return KeepValueInRange(notify);
        }


        /// <summary>
        /// Logic to loop the value correctly.
        /// </summary>
        /// <param name="notify">Should it notify the change?</param>
        /// <returns>The looped position if applicable.</returns>
        private double KeepValueInRange(bool notify)
        {
            var remainder = 0d;

            if (currentValue > maxValue)
            {
                remainder = currentValue - Difference;
                
                while (remainder > maxValue)
                {
                    currentValue -= currentValue - Difference;
                    remainder = currentValue - Difference;
                }
            }
            else if (currentValue < minValue)
            {
                remainder = currentValue + Difference;

                while (remainder < minValue)
                {
                    currentValue = maxValue - Difference;
                    remainder = (currentValue + Difference);
                }
            }
            
            if (notify)
            {
                ValueChanged.Raise();
            }
            
            return currentValue;
        }
    }
}

#endif