#if CARTERGAMES_CART_MODULE_DATAVALUES

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
using System.Collections.Generic;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Modules.DataValues.Events;
using UnityEngine;

namespace CarterGames.Cart.Modules.DataValues
{
    /// <summary>
    /// The base class for a dictionary data value.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    [Serializable]
    public class DataValueDictionary<TKey, TValue> : DataValueAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField, TextArea] private string devDescription;

        [SerializeField] private string key;
        [SerializeField] private SerializableDictionary<TKey, TValue> value = new SerializableDictionary<TKey, TValue>();

        [SerializeField] private bool canReset;
        [SerializeField] private SerializableDictionary<TKey, TValue> defaultValue;
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
        /// The value of the dictionary.
        /// </summary>
        public SerializableDictionary<TKey, TValue> Value => value;


        /// <summary>
        /// The default value for the asset.
        /// </summary>
        public SerializableDictionary<TKey, TValue> DefaultValue => defaultValue;


        /// <summary>
        /// The valid reset states for the asset.
        /// </summary>
        public override DataValueResetState ValidStates => resetStates;


        /// <summary>
        /// Gets the count for the dictionary.
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
        /// Adds an entry to the dictionary.
        /// </summary>
        /// <param name="elementKey">The key to add.</param>
        /// <param name="elementValue">The value to add.</param>
        public void Add(TKey elementKey, TValue elementValue)
        {
            if (value.ContainsKey(elementKey)) return;
            value.Add(elementKey, elementValue);
            Changed.Raise();
            
            if (!useDataValueEvents) return;
            if (onChanged != null)
            {
                onChanged.Raise();
            }
        }


        /// <summary>
        /// Adds an entry to the dictionary.
        /// </summary>
        /// <param name="element">The element to add.</param>
        public void Add(SerializableKeyValuePair<TKey, TValue> element)
        {
            if (value.ContainsKey(element.Key)) return;
            value.Add(element.Key, element.Value);
            Changed.Raise();
            
            if (!useDataValueEvents) return;
            if (onChanged != null)
            {
                onChanged.Raise();
            }
        }


        /// <summary>
        /// Removes an entry from the dictionary.
        /// </summary>
        /// <param name="elementKey">The key to remove.</param>
        public void Remove(TKey elementKey)
        {
            if (!value.ContainsKey(elementKey)) return;
            value.Remove(elementKey);
            Changed.Raise();
            
            if (!useDataValueEvents) return;
            if (onChanged != null)
            {
                onChanged.Raise();
            }
        }


        /// <summary>
        /// Removes an entry from the dictionary.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        public void Remove(SerializableKeyValuePair<TKey, TValue> element)
        {
            if (!value.ContainsKey(element.Key)) return;
            value.Remove(element.Key);
            Changed.Raise();
            
            if (!useDataValueEvents) return;
            if (onChanged != null)
            {
                onChanged.Raise();
            }
        }


        /// <summary>
        /// Gets if the dictionary contains the entered key.
        /// </summary>
        /// <param name="elementKey"></param>
        /// <returns>If it exists in the dictionary.</returns>
        public bool ContainsKey(TKey elementKey) => value.ContainsKey(elementKey);


        /// <summary>
        /// Sets the dictionary to the entered value.
        /// </summary>
        /// <param name="input">The value to set to.</param>
        public void SetValue(Dictionary<TKey, TValue> input)
        {
            value = (SerializableDictionary<TKey, TValue>) input;
            Changed.Raise();
            
            if (!useDataValueEvents) return;
            if (onChanged != null)
            {
                onChanged.Raise();
            }
        }


        /// <summary>
        /// Sets the dictionary to the entered value.
        /// </summary>
        /// <param name="input">The value to set to.</param>
        public void SetValue(SerializableDictionary<TKey, TValue> input)
        {
            value = input;
            Changed.Raise();
            
            if (!useDataValueEvents) return;
            if (onChanged != null)
            {
                onChanged.Raise();
            }
        }


        /// <summary>
        /// Sets the dictionary to the entered value without raising the changed evt.
        /// </summary>
        /// <param name="input">The value to set to.</param>
        public void SetValueWithoutNotify(Dictionary<TKey, TValue> input)
        {
            value = (SerializableDictionary<TKey, TValue>) input;
        }


        /// <summary>
        /// Sets the dictionary to the entered value without raising the changed evt.
        /// </summary>
        /// <param name="input">The value to set to.</param>
        public void SetValueWithoutNotify(SerializableDictionary<TKey, TValue> input)
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
            
            if (!useDataValueEvents) return;
            if (onChanged != null)
            {
                onChanged.Raise();
            }
            
            if (onReset != null)
            {
                onReset.Raise();
            }
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