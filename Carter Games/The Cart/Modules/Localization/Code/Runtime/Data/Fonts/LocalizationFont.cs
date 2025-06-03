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
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
    /// <summary>
    /// Defines a font that localization can use.
    /// </summary>
    [Serializable]
    public struct LocalizationFont
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private string displayName;
        [SerializeField] private LocalizationFontDefinition[] data;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets if the default setup has been assigned.
        /// </summary>
        public bool HasDefaultSetup => data.Any(t => t.Language.Code == LocalizationManager.CurrentLanguage.Code);
		
        
        /// <summary>
        /// Gets the default setup.
        /// </summary>
        public LocalizationFontDefinition DefaultLanguageData =>
            data.FirstOrDefault(t => t.Language.Code == Language.Default.Code);
        
        
        /// <summary>
        /// Gets the data to read from.
        /// </summary>
        public IReadOnlyCollection<LocalizationFontDefinition> Data => data;
    }
}

#endif