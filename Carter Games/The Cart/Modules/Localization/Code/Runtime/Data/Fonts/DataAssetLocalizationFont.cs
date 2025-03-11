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
using System.Linq;
using CarterGames.Cart.Core.Data;
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
    [CreateAssetMenu(fileName = "Data Asset Localization Font", menuName = "Carter Games/The Cart/Modules/Localization/TMP/Data Asset Localization Font")]
    [Serializable]
    public sealed class DataAssetLocalizationFont : DataAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private LocalizationFont[] fontLookup;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the current font asset to use from the style.
        /// </summary>
        public TMP_FontAsset CurrentLanguageFont =>
            fontLookup.FirstOrDefault(t =>  t.Language.Code == LocalizationManager.CurrentLanguage.Code)?.Font;
        
        
        /// <summary>
        /// Gets if the font needs to apply a material.
        /// </summary>
        public bool RequiresFontMaterial =>
            fontLookup.FirstOrDefault(t => t.Language.Code == LocalizationManager.CurrentLanguage.Code) is { UsesMaterial: true };
        
        
        /// <summary>
        /// Gets the font material to apply.
        /// </summary>
        public Material CurrentLanguageFontMaterial =>
            fontLookup.FirstOrDefault(t => t.Language.Code == LocalizationManager.CurrentLanguage.Code)?.FontMaterial;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets the language info for the entered language code.
        /// </summary>
        /// <param name="languageCode">The language to get the font asset for.</param>
        /// <returns>LocalizationFont</returns>
        public LocalizationFont GetFontInfoForLanguage(string languageCode)
        {
            return fontLookup.FirstOrDefault(t => t.Language.Code == languageCode);
        }
        
        
        /// <summary>
        /// Gets the current font asset to use from the style for the requested language.
        /// </summary>
        /// <param name="languageCode">The language to get the font asset for.</param>
        /// <returns>TMP_FontAsset</returns>
        public TMP_FontAsset GetFontAssetForLanguage(string languageCode)
        {
            return GetFontInfoForLanguage(languageCode)?.Font;
        }
        
        
        /// <summary>
        /// Gets if the font needs to apply a material for the requested language.
        /// </summary>
        /// <param name="languageCode">The language to get check for.</param>
        /// <returns>bool</returns>
        public bool GetRequiresFontMaterialForLanguage(string languageCode)
        {
            return GetFontInfoForLanguage(languageCode) is { UsesMaterial: true };
        }
        
        
        /// <summary>
        /// Gets the font material to apply for the requested language.
        /// </summary>
        /// <param name="languageCode">The language to get the material for.</param>
        /// <returns>Material</returns>
        public Material GetFontMaterialForLanguage(string languageCode)
        {
            return GetFontInfoForLanguage(languageCode)?.FontMaterial;
        }

        
        /// <summary>
        /// Applies a font style to the TMP_Text element entered.
        /// </summary>
        /// <param name="tmpText">The component to apply to.</param>
        /// <param name="overrideLanguageCode">The language code to override to show instead of the current.</param>
        public void ApplyToComponent(TMP_Text tmpText, string overrideLanguageCode = "")
        {
            var targetLanguageCode = string.IsNullOrEmpty(overrideLanguageCode)
                ? LocalizationManager.CurrentLanguage.Code
                : overrideLanguageCode;
            
            if (tmpText.font != GetFontAssetForLanguage(targetLanguageCode))
            {
                tmpText.font = GetFontAssetForLanguage(targetLanguageCode);
            }

            if (!GetRequiresFontMaterialForLanguage(targetLanguageCode))
            {
                tmpText.fontMaterial = GetFontAssetForLanguage(targetLanguageCode).material;
                return;
            }

            tmpText.fontMaterial = GetFontMaterialForLanguage(targetLanguageCode);
        }
    }
}

#endif