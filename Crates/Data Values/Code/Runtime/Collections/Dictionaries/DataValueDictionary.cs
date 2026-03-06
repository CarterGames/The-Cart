#if CARTERGAMES_CART_CRATE_DATAVALUES

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
using System.Collections.Generic;
using CarterGames.Cart;
using CarterGames.Cart.Events;
using CarterGames.Cart.Crates.DataValues.Events;
using UnityEngine;

namespace CarterGames.Cart.Crates.DataValues
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

        [SerializeField] [HideInInspector] [TextArea] private string devDescription;

        [SerializeField] [HideInInspector] private string key;
        [SerializeField] [HideInInspector] private SerializableDictionary<TKey, TValue> value = new SerializableDictionary<TKey, TValue>();

        [SerializeField] [HideInInspector] private bool canReset;
        [SerializeField] [HideInInspector] private SerializableDictionary<TKey, TValue> defaultValue;
        [SerializeField] [HideInInspector] private DataValueResetState resetStates;
        
        [SerializeField] [HideInInspector] private bool useDataValueEvents;
        [SerializeField] [HideInInspector] private DataValueEventBase onChanged;
        [SerializeField] [HideInInspector] private DataValueEventBase onReset;

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