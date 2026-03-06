/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using System;
using UnityEngine;

namespace CarterGames.Cart
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