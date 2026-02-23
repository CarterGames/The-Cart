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
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization
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