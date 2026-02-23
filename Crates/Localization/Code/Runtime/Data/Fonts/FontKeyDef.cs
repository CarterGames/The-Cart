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
    /// Defines a key to use to lookup fonts from assigned assets + material combo's
    /// </summary>
    [Serializable]
    public struct FontKeyDef
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private TMP_FontAsset fontAsset;
        [SerializeField] private Material fontMaterial;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The font asset.
        /// </summary>
        public TMP_FontAsset Font => fontAsset;
        
        
        /// <summary>
        /// The material if present.
        /// </summary>
        public Material Material => fontMaterial;
        
        
        /// <summary>
        /// Gets if the material is not null.
        /// </summary>
        public bool HasMaterial => fontMaterial != null;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Makes a font key from the loc font definition data type.
        /// </summary>
        /// <param name="data">The data to read</param>
        /// <returns>The font key</returns>
        public static FontKeyDef FromFontDef(LocalizationFontDefinition data)
        {
            return new FontKeyDef()
            {
                fontAsset = data.Font,
                fontMaterial = data.FontMaterial
            };
        }
        
        
        /// <summary>
        /// Makes a font key from the loc font definition data type.
        /// </summary>
        /// <param name="fontAsset">The font asset to use</param>
        /// <param name="material">The font material to use</param>
        /// <returns>The font key</returns>
        public static FontKeyDef FromElements(TMP_FontAsset fontAsset, Material material)
        {
            return new FontKeyDef()
            {
                fontAsset = fontAsset,
                fontMaterial = material
            };
        }


        /// <summary>
        /// Makes a font key from the loc font definition data type.
        /// </summary>
        /// <param name="component">The TMP text component to use</param>
        /// <returns>The font key</returns>
        public static FontKeyDef FromTMPComponent(TMP_Text component)
        {
            var data = new FontKeyDef()
            {
                fontAsset = component.font,
            };

            if (component.defaultMaterial != component.fontMaterial)
            {
                data.fontMaterial = component.fontMaterial;
            }

            return data;
        }
    }
}

#endif