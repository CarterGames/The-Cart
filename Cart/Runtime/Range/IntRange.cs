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
using CarterGames.Cart.Random;

namespace CarterGames.Cart
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