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
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
    /// <summary>
    /// Defines a font that localization can use for a specific language.
    /// </summary>
    [Serializable]
    public sealed class LocalizationFontDefinition
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] [LanguageSelectable] private Language language;
        [SerializeField] private TMP_FontAsset fontAsset;
        [SerializeField] private bool usesMaterial;
        [SerializeField] private Material fontMaterial;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The language the font applies to.
        /// </summary>
        public Language Language => language;
        
        /// <summary>
        /// The font asset to apply.
        /// </summary>
        public TMP_FontAsset Font => fontAsset;
        
        /// <summary>
        /// Gets if a material is used for this font.
        /// </summary>
        public bool UsesMaterial => usesMaterial;
        
        /// <summary>
        /// The font material used for the font asset.
        /// </summary>
        public Material FontMaterial => fontMaterial;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Applies to font to the label entered.
        /// </summary>
        /// <param name="label">The label to edit.</param>
        public void ApplyToLabel(TMP_Text label)
        {
            label.font = Font;
            if (!UsesMaterial) return;
            label.fontMaterial = FontMaterial;
        }
    }
}

#endif