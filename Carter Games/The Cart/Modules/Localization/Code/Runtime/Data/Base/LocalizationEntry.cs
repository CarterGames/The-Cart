#if CARTERGAMES_CART_MODULE_LOCALIZATION

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
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
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
        /// <remarks>Used in Notion Data module.</remarks>
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