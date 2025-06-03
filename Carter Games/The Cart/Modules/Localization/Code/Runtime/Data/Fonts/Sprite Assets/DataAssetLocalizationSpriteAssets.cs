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
	/// <summary>
	/// Handles any TMP_SpriteAsset usage in the localization setup.
	/// </summary>
    [CreateAssetMenu(fileName = "Data Asset Localization Sprite Assets", menuName = "Carter Games/The Cart/Modules/Localization/TMP/Data Asset Localization Sprite Assets")]
    [Serializable]
    public sealed class DataAssetLocalizationSpriteAssets : DataAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] private LocalizationSpriteAsset[] data;
        [SerializeField] private SerializableDictionary<TMP_SpriteAsset, SerializableDictionary<string, SpriteAssetDef>> spritesLookup;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected override void OnInitialize()
        {
            spritesLookup = new SerializableDictionary<TMP_SpriteAsset, SerializableDictionary<string, SpriteAssetDef>>();
			
            var currentSpriteAssetLookup = new Dictionary<string, SpriteAssetDef>();

            foreach (var entry in data)
            {
                if (!entry.HasDefaultSetup)
                {
                    CartLogger.LogWarning<LogCategoryModuleLocalization>($"Unable to set the entry for {entry} as it doesn't have a default language setup.");
                    continue;
                }
				
                currentSpriteAssetLookup.Clear();

                foreach (var entryData in entry.Data)
                {
                    if (currentSpriteAssetLookup.ContainsKey(entryData.LanguageCode)) continue;
                    currentSpriteAssetLookup.Add(entryData.LanguageCode, entryData);
                }
				
                spritesLookup.Add(entry.DefaultSpriteAsset, SerializableDictionary<string, SpriteAssetDef>.FromDictionary(currentSpriteAssetLookup));
            }
        }
        
        
		private SerializableDictionary<string, SpriteAssetDef> GetSpriteAssetData(TMP_Text label)
		{
			return GetSpriteAssetData(label.spriteAsset);
		}
		

		private SerializableDictionary<string, SpriteAssetDef> GetSpriteAssetData(TMP_SpriteAsset spriteAsset)
		{
			if (spriteAsset == null)
			{
				CartLogger.LogWarning<LogCategoryModuleLocalization>($"Sprite asset ref is null!");
				return null;
			}

			if (!spritesLookup.ContainsKey(spriteAsset))
			{
				CartLogger.LogWarning<LogCategoryModuleLocalization>($"Couldn't find entry for {spriteAsset.name} in setup!");
				return null;
			}

			return spritesLookup[spriteAsset];
		}
		
		
		/// <summary>
		/// Gets the sprite asset setup for the language required on the label.
		/// </summary>
		/// <param name="spriteAsset">The sprite asset to look for.</param>
		/// <param name="languageCode">The language to get the font asset for.</param>
		/// <returns>FontStyleDefinition</returns>
		public SpriteAssetDef GetFontDataFromSpriteAsset(TMP_SpriteAsset spriteAsset, string languageCode)
		{
			return GetSpriteAssetData(spriteAsset)[languageCode];
		}
		
		
		/// <summary>
		/// Gets the sprite asset setup for the language required on the label.
		/// </summary>
		/// <param name="label">The label to get for.</param>
		/// <param name="languageCode">The language to get the font asset for.</param>
		/// <returns>FontStyleDefinition</returns>
		public SpriteAssetDef GetSpriteDataFromLabel(TMP_Text label, string languageCode)
		{
			return GetSpriteAssetData(label)[languageCode];
		}
		
		
		/// <summary>
		/// Updates the sprite asset on a label to the current language or a specific language code if requested.
		/// </summary>
		/// <param name="label">The label to apply to.</param>
		/// <param name="overrideLanguageCode">The language code to get for. If not overriden it'll be the current language.</param>
		public void UpdateSpriteAssetOnLabel(TMP_Text label, string overrideLanguageCode = "")
		{
			if (label.spriteAsset == null) return;
			
			var targetLanguageCode = string.IsNullOrEmpty(overrideLanguageCode)
				? LocalizationManager.CurrentLanguage.Code
				: overrideLanguageCode;

			var foundSpriteData = GetSpriteAssetData(label);

			if (foundSpriteData == null) return;
			
			if (foundSpriteData.ContainsKey(targetLanguageCode))
			{
				CartLogger.LogWarning<LogCategoryModuleLocalization>($"Couldn't find an entry for the language code {targetLanguageCode}.");
				return;
			}
			
			label.spriteAsset = foundSpriteData[targetLanguageCode].SpriteAsset;
		}
    }
}

#endif