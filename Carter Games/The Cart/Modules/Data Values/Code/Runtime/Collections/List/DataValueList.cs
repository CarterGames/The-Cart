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

using System.Collections.Generic;
using CarterGames.Cart.Core.Events;
using UnityEngine;

namespace CarterGames.Cart.Modules.DataValues
{
    /// <summary>
    /// The base class for a list data value.
    /// </summary>
    /// <typeparam name="T">The value type for the list.</typeparam>
    public abstract class DataValueList<T> : DataValueAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField, TextArea] private string devDescription;
        
        [SerializeField] private string key;
        [SerializeField] private List<T> value = new List<T>();

        [SerializeField] private bool canReset;
        [SerializeField] private List<T> defaultValue;
        [SerializeField] private DataValueResetState resetStates;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The key for the value.
        /// </summary>
        public override string Key => key;
        
        
        /// <summary>
        /// The value for the list.
        /// </summary>
        public List<T> Value => value;
        
        
        /// <summary>
        /// The default value for the asset.
        /// </summary>
        public List<T> DefaultValue => defaultValue;
        
        
        /// <summary>
        /// The valid reset states for the asset.
        /// </summary>
        public override DataValueResetState ValidStates => resetStates;
        
        
        /// <summary>
        /// Gets the count for the list.
        /// </summary>
        /// <returns>The count.</returns>
        public int Count => value.Count;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Raises when the value is changed.
        /// </summary>
        public Evt Changed { get; } = new Evt();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Adds an entry to the list.
        /// </summary>
        /// <param name="element">The element to add.</param>
        public void Add(T element)
        {
            if (value.Contains(element)) return;
            value.Add(element);
        }
        

        /// <summary>
        /// Removes an entry to the list.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        public void Remove(T element)
        {
            if (!value.Contains(element)) return;
            value.Remove(element);
        }


        /// <summary>
        /// Gets if an entry is in the list.
        /// </summary>
        /// <param name="element">The element to find.</param>
        /// <returns>If the element is in the list.</returns>
        public bool Contains(T element) => value.Contains(element);
        
        
        /// <summary>
        /// Sets the value to the entered value
        /// </summary>
        /// <param name="input">The value to set to.</param>
        public void SetValue(List<T> input)
        {
            value = input;
            Changed.Raise();
        }
        
        
        /// <summary>
        /// Sets the value to the entered value
        /// </summary>
        /// <param name="input">The value to set to.</param>
        public void SetValueWithoutNotify(List<T> input)
        {
            value = input;
        }

        
        /// <summary>
        /// Forces the asset to reset to default value.
        /// </summary>
        public void ResetValue()
        {
            value = defaultValue;
            Changed.Raise();
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