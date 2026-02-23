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
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization
{
    /// <summary>
    /// The data structure for an entry for a localized element. ONe per language supported.
    /// </summary>
    /// <typeparam name="T">The type to contain</typeparam>
    [Serializable]
    public class LocalizationEntry<T>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] private string languageCode;
        [SerializeField] private T copy;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Creates a new entry when called with the requested values.
        /// </summary>
        /// <remarks>Used in Notion Data crate.</remarks>
        /// <param name="languageCode">The language code to set.</param>
        /// <param name="copy">The data to set.</param>
        public LocalizationEntry(string languageCode, T copy)
        {
            this.languageCode = languageCode;
            this.copy = copy;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The language code for the copy.
        /// </summary>
        public string LanguageCode => languageCode;


        /// <summary>
        /// The sprite for the language.
        /// </summary>
        public T Copy => copy;
    }
}

#endif