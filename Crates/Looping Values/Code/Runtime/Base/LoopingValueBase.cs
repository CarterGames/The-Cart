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
using CarterGames.Cart.Events;
using UnityEngine;

namespace CarterGames.Cart.Crates.LoopingValues
{
    /// <summary>
    /// Implement to make a looping value.
    /// </summary>
    /// <typeparam name="T">The type to loop with.</typeparam>
    [Serializable]
    public abstract class LoopingValueBase<T>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] protected T minValue;
        [SerializeField] protected T maxValue;
        [SerializeField] protected T currentValue;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// The value currently at.
        /// </summary>
        public abstract T Value { get; }

        
        /// <summary>
        /// The min value in this value.
        /// </summary>
        public T MinValue => minValue;
        
        
        /// <summary>
        /// The max value in this value.
        /// </summary>
        public T MaxValue => maxValue;


        /// <summary>
        /// Gets the mod value for the number
        /// </summary>
        protected T ModValue => Difference;
        

        /// <summary>
        /// Gets the difference between the min & max values.
        /// </summary>
        protected abstract T Difference { get; }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Is raised when the value is changed by normal means.
        /// </summary>
        public readonly Evt ValueChangedEvt = new Evt();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Set the value to the entered value, it will adjust if no in range.
        /// </summary>
        /// <param name="value">The value to set to.</param>
        public void SetValue(T value)
        {
            currentValue = SetToValue(value);
        }
        
        
        /// <summary>
        /// Sets the value without raising the ValueChanged Evt.
        /// </summary>
        /// <param name="value">The value to set to.</param>
        public void SetValueWithoutNotify(T value)
        {
            currentValue = SetToValue(value, false);
        }
        
        
        /// <summary>
        /// Increments the value by the requested amount.
        /// </summary>
        /// <param name="increment">The amount to increment by.</param>
        public void IncrementValue(T increment)
        {
            currentValue = UpdateValue(increment);
        }


        /// <summary>
        /// Resets the value to the min defined.
        /// </summary>
        /// <param name="notify">Should the event be raised on the change?</param>
        public void ResetToMinValue(bool notify = true)
        {
            currentValue = minValue;
            if (!notify) return;
            ValueChangedEvt.Raise();
        }
        
        
        /// <summary>
        /// Resets the value to the max defined.
        /// </summary>
        /// <param name="notify">Should the event be raised on the change?</param>
        public void ResetToMaxValue(bool notify = true)
        {
            currentValue = maxValue;
            if (!notify) return;
            ValueChangedEvt.Raise();
        }

        
        /// <summary>
        /// Implement to set the value to the requested, updating its loop placement if invalid.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">Should the event be raised on the change?</param>
        /// <returns>The newly adjusted value</returns>
        protected abstract T SetToValue(T value, bool notify = true);
        
        
        /// <summary>
        /// Implement to update the value by the requested increment.
        /// </summary>
        /// <param name="increment">The amount to increment by.</param>
        /// <param name="notify">Should the event be raised on the change?</param>
        /// <returns>The newly adjusted value</returns>
        protected abstract T UpdateValue(T increment, bool notify = true);
    }
}

#endif