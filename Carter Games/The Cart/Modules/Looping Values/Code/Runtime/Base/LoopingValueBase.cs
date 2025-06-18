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
using CarterGames.Cart.Core.Events;
using UnityEngine;

namespace CarterGames.Cart.Modules.LoopingValues
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