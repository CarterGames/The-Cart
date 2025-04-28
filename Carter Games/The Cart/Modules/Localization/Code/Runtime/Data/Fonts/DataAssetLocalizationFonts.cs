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
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Logs;
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
    [Serializable]
    public sealed class DataAssetLocalizationFonts : DataAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] private LocalizationFont[] fontsLookup;
        [SerializeField] private SerializableDictionary<string, LocalizationFontDefinition> fallbackFont;
        [SerializeField] private SerializableDictionary<FontKeyDef, SerializableDictionary<string, LocalizationFontDefinition>> fontAssetLookup;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected override void OnInitialize()
        {
	        fontAssetLookup = new SerializableDictionary<FontKeyDef, SerializableDictionary<string, LocalizationFontDefinition>>();

	        if (fontsLookup.IsEmptyOrNull()) return;
	        
	        var currentFontLookup = new Dictionary<string, LocalizationFontDefinition>();

	        foreach (var entry in fontsLookup)
	        {
		        if (!entry.HasDefaultSetup)
		        {
			        CartLogger.LogWarning<LogCategoryLocalization>(
				        $"Unable to set the entry for {entry} as it doesn't have a default language setup.");
			        continue;
		        }

		        var key = FontKeyDef.FromFontDef(entry.DefaultLanguageData);
		        currentFontLookup.Clear();

		        foreach (var entryData in entry.Data)
		        {
			        if (currentFontLookup.ContainsKey(entryData.Language.Code)) continue;
			        currentFontLookup.Add(entryData.Language.Code, entryData);
		        }

		        fontAssetLookup.Add(key,
			        SerializableDictionary<string, LocalizationFontDefinition>.FromDictionary(currentFontLookup));

		        foreach (var entryData in entry.Data)
		        {
			        key = FontKeyDef.FromElements(entryData.Font, entryData.FontMaterial);

			        if (fontAssetLookup.ContainsKey(key)) continue;
			        fontAssetLookup.Add(key,
				        SerializableDictionary<string, LocalizationFontDefinition>.FromDictionary(currentFontLookup));
		        }
	        }
        }


        private SerializableDictionary<string, LocalizationFontDefinition> GetFontData(TMP_Text label)
        {
	        return GetFontData(label.font, label.defaultMaterial == label.fontMaterial ? null : label.fontMaterial);
        }
        
        
        private SerializableDictionary<string, LocalizationFontDefinition> GetFontData(TMP_FontAsset fontAsset, Material fontMaterial = null)
        {
	        var defaultKey = FontKeyDef.FromElements(fontAsset, null);
	        var key = FontKeyDef.FromElements(fontAsset, fontMaterial);

	        if (fontAssetLookup.TryGetValue(key, out var fontData))
	        {
		        return fontData;
	        }
	        
	        if (fontAssetLookup.TryGetValue(defaultKey, out fontData))
	        {
		        return fontData;
	        }
	        
	        CartLogger.LogError<LogCategoryLocalization>($"Couldn't find default font setup for {fontAsset.name} | {fontMaterial}, using fallback!");
	        return fallbackFont;
        }


        private string GetLanguageCode(string overrideLanguageCode = "")
        {
	        return string.IsNullOrEmpty(overrideLanguageCode)
		        ? LocalizationManager.CurrentLanguage.Code
		        : overrideLanguageCode;
        }
        
        
        
        /// <summary>
        /// Gets the font setup for the language required on the label.
        /// </summary>
        /// <param name="fontAsset">The font to look for.</param>
        /// <param name="overrideLanguageCode">The language code to get for. If not overriden it'll be the current language.</param>
        /// <returns>LocalizationFontDefinition</returns>
        public LocalizationFontDefinition GetFontDataFromFont(TMP_FontAsset fontAsset, string overrideLanguageCode = "")
        {
	        return GetFontData(fontAsset)[GetLanguageCode(overrideLanguageCode)];
        }
        
                
        /// <summary>
        /// Gets the font setup for the language required on the label.
        /// </summary>
        /// <param name="fontAsset">The font to look for.</param>
        /// <param name="fontMaterial">The font material to look for.</param>
        /// <param name="overrideLanguageCode">The language code to get for. If not overriden it'll be the current language.</param>
        /// <returns>LocalizationFontDefinition</returns>
        public LocalizationFontDefinition GetFontDataFromFont(TMP_FontAsset fontAsset, Material fontMaterial, string overrideLanguageCode = "")
        {
	        return GetFontData(fontAsset, fontMaterial)[GetLanguageCode(overrideLanguageCode)];
        }
        
        
        /// <summary>
        /// Gets the font setup for the language required on the label.
        /// </summary>
        /// <param name="label">The label to get for.</param>
        /// <param name="overrideLanguageCode">The language code to get for. If not overriden it'll be the current language.</param>
        /// <returns>LocalizationFontDefinition</returns>
        public LocalizationFontDefinition GetFontDataFromLabel(TMP_Text label, string overrideLanguageCode = "")
        {
	        return GetFontData(label)[GetLanguageCode(overrideLanguageCode)];
        }
        
        
        /// <summary>
        /// Updates the font on a label to the current language or a specific language code if requested.
        /// </summary>
        /// <param name="label">The label to apply to.</param>
        /// <param name="overrideLanguageCode">The language code to get for. If not overriden it'll be the current language.</param>
        public void UpdateFontOnLabel(TMP_Text label, string overrideLanguageCode = "")
        {
	        var targetLanguageCode = GetLanguageCode(overrideLanguageCode);

	        if (!GetFontData(label).ContainsKey(targetLanguageCode))
	        {
		        CartLogger.LogWarning<LogCategoryLocalization>($"Couldn't find an entry for the language code {targetLanguageCode}.");
		        return;
	        }
	        
	        var fontData = GetFontData(label)[targetLanguageCode];

	        label.font = fontData.Font;

	        if (fontData.FontMaterial == null) return;
	        label.fontMaterial = fontData.FontMaterial;
        }


        public void UpdateLabelToFont(TMP_Text label, TMP_FontAsset fontAsset, string overrideLanguageCode = "")
        {
	        UpdateLabelToFont(label, fontAsset, null, overrideLanguageCode);
        }
        
        
        /// <summary>
        /// Updates the font on a label to the current language or a specific language code if requested.
        /// </summary>
        /// <param name="label">The label to apply to.</param>
        /// <param name="overrideLanguageCode">The language code to get for. If not overriden it'll be the current language.</param>
        public void UpdateLabelToFont(TMP_Text label, TMP_FontAsset fontAsset, Material material, string overrideLanguageCode = "")
        {
	        var targetLanguageCode = GetLanguageCode(overrideLanguageCode);

	        if (!GetFontData(fontAsset, material).ContainsKey(targetLanguageCode))
	        {
		        CartLogger.LogWarning<LogCategoryLocalization>($"Couldn't find an entry for the language code {targetLanguageCode}.");
		        return;
	        }
	        
	        var fontData = GetFontData(fontAsset, material)[targetLanguageCode];

	        label.font = fontData.Font;

	        if (!fontData.UsesMaterial) return;
	        label.fontMaterial = fontData.FontMaterial;
        }
    }
}

#endif