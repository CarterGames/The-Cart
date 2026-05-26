#if CARTERGAMES_CART_CRATE_LOCALIZATION

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
using System.Linq;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization
{
    /// <summary>
    /// The base class for localization data.
    /// </summary>
    /// <typeparam name="T">The type to contain.</typeparam>
    [Serializable]
    public class LocalizationData<T>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] private string id;
        [SerializeField] private LocalizationEntry<T>[] entries;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The id for the data to use.
        /// </summary>
        public string LocId => id;


        /// <summary>
        /// The entries for the data to use.
        /// </summary>
        public LocalizationEntry<T>[] Entries => entries;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Blank constructor needed for generic new T() to compile mainly.
        /// </summary>
        public LocalizationData() {}


        /// <summary>
        /// Creates a new data with the entered values.
        /// </summary>
        /// <remarks>Used in Notion Data crate.</remarks>
        /// <param name="id">The id for the data.</param>
        /// <param name="entries">The entries for this id.</param>
        public LocalizationData(string id, IEnumerable<LocalizationEntry<T>> entries)
        {
            this.id = id;
            this.entries = entries.ToArray();
        }
    }
}

#endif