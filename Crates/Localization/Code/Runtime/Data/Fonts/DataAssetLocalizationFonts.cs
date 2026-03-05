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
using System.Collections.Generic;
using CarterGames.Cart;
using CarterGames.Cart.Data;
using CarterGames.Cart.Logs;
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization
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


        /// <summary>
        /// Updates the label to the entered font asset.
        /// </summary>
        /// <param name="label">The label to apply to.</param>
        /// <param name="fontAsset">The asset to apply.</param>
        /// <param name="overrideLanguageCode">The language code to get for. If not overriden it'll be the current language.</param>
        public void UpdateLabelToFont(TMP_Text label, TMP_FontAsset fontAsset, string overrideLanguageCode = "")
        {
	        UpdateLabelToFont(label, fontAsset, null, overrideLanguageCode);
        }
        
        
        /// <summary>
        /// Updates the font on a label to the current language or a specific language code if requested.
        /// </summary>
        /// <param name="label">The label to apply to.</param>
        /// <param name="fontAsset">The asset to apply.</param>
        /// <param name="material">The material to apply for the font.</param>
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