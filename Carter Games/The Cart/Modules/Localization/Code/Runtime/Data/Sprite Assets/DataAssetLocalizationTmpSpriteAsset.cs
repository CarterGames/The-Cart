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
    [CreateAssetMenu(fileName = "Data Asset Localization TMP Sprite Asset", menuName = "Carter Games/The Cart/Modules/Localization/TMP/Data Asset Localization TMP Sprite Asset")]
    [Serializable]
    public sealed class DataAssetLocalizationTmpSpriteAsset : DataAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        [SerializeField] private TmpSpriteAsset[] data;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 

        /// <summary>
        /// Gets the current font asset to use from the style.
        /// </summary>
        public TMP_SpriteAsset CurrentLanguageSpriteAsset =>
            data.FirstOrDefault(t => t.LanguageCode == LocalizationManager.CurrentLanguage.Code)?.SpriteAsset;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets the language info for the entered language code.
        /// </summary>
        /// <param name="languageCode">The language to get the font asset for.</param>
        /// <returns>SpriteAssetDefinition</returns>
        public TmpSpriteAsset GetSpriteAssetInfoForLanguage(string languageCode)
        {
            return data.FirstOrDefault(t => t.LanguageCode == languageCode);
        }
        
        
        /// <summary>
        /// Gets the current font asset to use from the style for the requested language.
        /// </summary>
        /// <param name="languageCode">The language to get the font asset for.</param>
        /// <returns>TMP_FontAsset</returns>
        public TMP_SpriteAsset GetSpriteAssetForLanguage(string languageCode)
        {
            return GetSpriteAssetInfoForLanguage(languageCode)?.SpriteAsset;
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

            if (tmpText.spriteAsset == GetSpriteAssetForLanguage(targetLanguageCode)) return;
            tmpText.spriteAsset = GetSpriteAssetForLanguage(targetLanguageCode);
        }
    }
}

#endif