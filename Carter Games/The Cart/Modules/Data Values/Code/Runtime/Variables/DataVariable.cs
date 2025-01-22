#if CARTERGAMES_CART_MODULE_DATAVALUES

/*
 * Copyright (c) 2024 Carter Games
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

using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Modules.DataValues.Events;
using UnityEngine;

namespace CarterGames.Cart.Modules.DataValues
{
    /// <summary>
    /// A base class for any data value (as a single variable).
    /// </summary>
    /// <typeparam name="T">The type the value is.</typeparam>
    public abstract class DataVariable<T> : DataValueAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField, TextArea] private string devDescription;

        [SerializeField] private string key;
        [SerializeField] private T value;

        [SerializeField] private bool canReset;
        [SerializeField] private T defaultValue;
        [SerializeField] private DataValueResetState resetStates;

        [SerializeField] private bool useDataValueEvents;
        [SerializeField] private DataValueEventBase onChanged;
        [SerializeField] private DataValueEventBase onReset;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The key for the value.
        /// </summary>
        public override string Key => key;


        /// <summary>
        /// The value of the asset.
        /// </summary>
        public T Value
        {
            get => value;
            private set => this.value = value;
        }


        /// <summary>
        /// The default value for the asset.
        /// </summary>
        public T DefaultValue => defaultValue;


        /// <summary>
        /// The valid reset states for the asset.
        /// </summary>
        public override DataValueResetState ValidStates => resetStates;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Raises when the value is changed. Raises regardless of the data value event usage.
        /// </summary>
        public Evt Changed { get; } = new Evt();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Sets the value to the entered value
        /// </summary>
        /// <param name="input">The value to set to.</param>
        public void SetValue(T input)
        {
            Value = input;
            Changed.Raise();
            
            if (!useDataValueEvents) return;
            onChanged.Raise();
        }


        /// <summary>
        /// Sets the value to the entered value without sending the changed event.
        /// </summary>
        /// <param name="input">The value to set to.</param>
        public void SetValueWithoutNotify(T input)
        {
            Value = input;
        }


        /// <summary>
        /// Forces the asset to reset to default value.
        /// </summary>
        public void ResetValue()
        {
            value = defaultValue;
            Changed.Raise();

            if (!useDataValueEvents) return;
            onChanged.Raise();
            onReset.Raise();
        }


        /// <summary>
        /// Resets the asset when called.
        /// Only works if the asset can reset.
        /// </summary>
        public override void ResetAsset()
        {
            if (!canReset) return;
            ResetValue();
        }
    }
}

#endif