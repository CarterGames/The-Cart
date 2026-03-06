#if CARTERGAMES_CART_CRATE_LOOPINGVALUES

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

namespace CarterGames.Cart.Crates.LoopingValues
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
        /// The value currently at.
        /// </summary>
        public override double Value => currentValue % ModValue;
        
        
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
            currentValue += value;
            
            if (!notify) return Value;
            ValueChangedEvt.Raise();
            return Value;
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
            
            if (!notify) return Value;
            ValueChangedEvt.Raise();
            return Value;
        }
    }
}

#endif