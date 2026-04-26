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
	/// <summary>
	/// Handles any TMP_SpriteAsset usage in the localization setup.
	/// </summary>
    [CreateAssetMenu(fileName = "Data Asset Localization Sprite Assets", menuName = "Carter Games/The Cart/Crates/Localization/TMP/Data Asset Localization Sprite Assets")]
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
                    CartLogger.LogWarning<LocalizationLogs>($"Unable to set the entry for {entry} as it doesn't have a default language setup.");
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
				CartLogger.LogWarning<LocalizationLogs>($"Sprite asset ref is null!");
				return null;
			}

			if (!spritesLookup.ContainsKey(spriteAsset))
			{
				CartLogger.LogWarning<LocalizationLogs>($"Couldn't find entry for {spriteAsset.name} in setup!");
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
				CartLogger.LogWarning<LocalizationLogs>($"Couldn't find an entry for the language code {targetLanguageCode}.");
				return;
			}
			
			label.spriteAsset = foundSpriteData[targetLanguageCode].SpriteAsset;
		}
    }
}

#endif