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
using CarterGames.Cart.Events;
using CarterGames.Cart.Crates.DataValues.Events;
using UnityEngine;

namespace CarterGames.Cart.Crates.DataValues
{
    /// <summary>
    /// The base class for a list data value.
    /// </summary>
    /// <typeparam name="T">The value type for the list.</typeparam>
    [Serializable]
    public abstract class DataValueList<T> : DataValueAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] [HideInInspector] [TextArea] private string devDescription;

        [SerializeField] [HideInInspector] private string key;
        [SerializeField] [HideInInspector] private List<T> value = new List<T>();

        [SerializeField] [HideInInspector] private bool canReset;
        [SerializeField] [HideInInspector] private List<T> defaultValue;
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
            Changed.Raise();
            
            if (!useDataValueEvents) return;
            if (onChanged != null)
            {
                onChanged.Raise();
            }
        }


        /// <summary>
        /// Removes an entry to the list.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        public void Remove(T element)
        {
            if (!value.Contains(element)) return;
            value.Remove(element);
            Changed.Raise();
            
            if (!useDataValueEvents) return;
            if (onChanged != null)
            {
                onChanged.Raise();
            }
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
            
            if (!useDataValueEvents) return;
            if (onChanged != null)
            {
                onChanged.Raise();
            }
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