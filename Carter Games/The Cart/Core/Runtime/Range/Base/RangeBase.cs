﻿/*
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
using UnityEngine;

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// A base class for the min/max value system.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    [Serializable]
    public abstract class RangeBase<T>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The min value the range allows for...
        /// </summary>
        [HideInInspector] public T min;
        
        /// <summary>
        /// The max value the range allows for...
        /// </summary>
        [HideInInspector] public T max;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Default Constructor...
        /// </summary>
        /// <remarks>Min = 0, Max = 1</remarks>
        protected RangeBase() { }


        /// <summary>
        /// Constructor... Makes a new min/max with the enter values...
        /// </summary>
        /// <param name="min">The min value for the range...</param>
        /// <param name="max">The max value for the range...</param>
        protected RangeBase(T min, T max)
        {
            this.min = min;
            this.max = max;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Increases the size of the min/max by the amount entered...
        /// </summary>
        /// <param name="amount">Amount to increase by...</param>
        public virtual void IncreaseMinMax(T amount)
        {
            if (min.Equals(0) && max.Equals(0))
            {
                max = amount;
                return;
            }
        }


        /// <summary>
        /// Reset the min/max to default values...
        /// </summary>
        public abstract void ResetMinMax();
    }
}