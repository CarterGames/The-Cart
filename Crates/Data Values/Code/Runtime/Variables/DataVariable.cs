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
using CarterGames.Cart.Events;
using CarterGames.Cart.Crates.DataValues.Events;
using UnityEngine;

namespace CarterGames.Cart.Crates.DataValues
{
    /// <summary>
    /// A base class for any data value (as a single variable).
    /// </summary>
    /// <typeparam name="T">The type the value is.</typeparam>
    [Serializable]
    public abstract class DataVariable<T> : DataValueAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] [HideInInspector] [TextArea] private string devDescription;

        [SerializeField] [HideInInspector] private string key;
        [SerializeField] [HideInInspector] private T value;

        [SerializeField] [HideInInspector] private bool canReset;
        [SerializeField] [HideInInspector] private T defaultValue;
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
            
            if (onChanged != null)
            {
                onChanged.Raise();
            }
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