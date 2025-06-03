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