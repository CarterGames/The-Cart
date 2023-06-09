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
using Scarlet.Random;

namespace Scarlet.General
{
    /// <summary>
    /// A int variant of the min/max value system.
    /// </summary>
    [Serializable]
    public class IntRange : RangeBase<int>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Creates a new int min/max between 0-1.
        /// </summary>
        public IntRange()
        {
            min = 0;
            max = 1;
        }
        
        
        /// <summary>
        /// Creates a new int min/max between the entered values.
        /// </summary>
        /// <param name="min">The mix value.</param>
        /// <param name="max">The max value.</param>
        public IntRange(int min, int max) : base(min, max) { }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Override Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Increases the min/max by the amount entered.
        /// </summary>
        /// <param name="amount">The amount to increase by.</param>
        public override void IncreaseMinMax(int amount)
        {
            base.IncreaseMinMax(amount);

            min += amount;
            max += amount;
        }

        
        /// <summary>
        /// Resets the min/max to 0.
        /// </summary>
        public override void ResetMinMax()
        {
            min = 0;
            max = 0;
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Returns a random value in the min/max range...
        /// </summary>
        public virtual int Random()
        {
            return Rng.Int(min, max);
        }
    }
}