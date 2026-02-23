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
using UnityEngine;

#if CARTERGAMES_NOTIONDATA
using CarterGames.Shared.NotionData;
#endif

namespace CarterGames.Cart.Crates.Localization
{
	/// <summary>
	/// The global localization lookup asset to search through all copy in the project with.
	/// </summary>
	[CreateAssetMenu(fileName = "Localization Lookup",
		menuName = "Carter Games/The Cart/Crates/Localization/Global Localization Lookup")]
	[Serializable]
    public sealed class DataAssetLocalizationLookup : DataAsset
    {
	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Fields
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
	    
        [SerializeField] private SerializableDictionary<string, LocalizationData<string>> textLookup;
        [SerializeField] private SerializableDictionary<string, LocalizationData<Sprite>> spriteLookup;
        [SerializeField] private SerializableDictionary<string, LocalizationData<AudioClip>> audioLookup;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// A readonly lookup of all text entries in the localization setup.
        /// </summary>
        public IReadOnlyDictionary<string, LocalizationData<string>> TextLocalizationLookup => textLookup;
        
        
        /// <summary>
        /// A readonly lookup of all sprite entries in the localization setup.
        /// </summary>
        public IReadOnlyDictionary<string, LocalizationData<Sprite>> SpriteLocalizationLookup => spriteLookup;
        
        
        /// <summary>
        /// A readonly lookup of all audio entries in the localization setup.
        /// </summary>
        public IReadOnlyDictionary<string, LocalizationData<AudioClip>> AudioLocalizationLookup => audioLookup;
        
        
        private static List<DataAssetLocalizedText> TextNormalAssets => DataAccess.GetAssets<DataAssetLocalizedText>();
        private static List<DataAssetLocalizedSprite> SpriteNormalAssets => DataAccess.GetAssets<DataAssetLocalizedSprite>();
        private static List<DataAssetLocalizedAudio> AudioNormalAssets => DataAccess.GetAssets<DataAssetLocalizedAudio>();

#if CARTERGAMES_NOTIONDATA
        private static List<NotionDataAssetLocalizedText> TextNotionAssets => NotionDataAccessor.GetAssets<NotionDataAssetLocalizedText>();
        private static List<NotionDataAssetLocalizedSprite> SpriteNotionAssets => NotionDataAccessor.GetAssets<NotionDataAssetLocalizedSprite>();
        private static List<NotionDataAssetLocalizedAudio> AudioNotionAssets => NotionDataAccessor.GetAssets<NotionDataAssetLocalizedAudio>();
#endif
        
	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Methods
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
	    
        protected override void OnInitialize()
        {
	        textLookup = GetTextAssetsLookup();
	        spriteLookup = GetSpriteAssetsLookup();
	        audioLookup = GetAudioAssetsLookup();
        }
        
        
	    private static SerializableDictionary<string, LocalizationData<string>> GetTextAssetsLookup()
		{
			var dic = new SerializableDictionary<string, LocalizationData<string>>();

			if (TextNormalAssets != null)
			{
				foreach (var asset in TextNormalAssets)
				{
					foreach (var data in asset.Data)
					{
						if (dic.ContainsKey(data.LocId)) continue;
						dic.Add(data.LocId, data);
					}
				}
			}
			
#if CARTERGAMES_NOTIONDATA
			if (TextNotionAssets != null)
			{
				foreach (var asset in TextNotionAssets)
				{
					foreach (var data in asset.Data)
					{
						if (dic.ContainsKey(data.LocId)) continue;
						dic.Add(data.LocId, data);
					}
				}
			}
#endif

			return dic;
		}
		
		
		private static SerializableDictionary<string, LocalizationData<Sprite>> GetSpriteAssetsLookup()
		{
			var dic = new SerializableDictionary<string, LocalizationData<Sprite>>();

			if (SpriteNormalAssets != null)
			{
				foreach (var asset in SpriteNormalAssets)
				{
					foreach (var data in asset.Data)
					{
						if (dic.ContainsKey(data.LocId)) continue;
						dic.Add(data.LocId, data);
					}
				}
			}
			
#if CARTERGAMES_NOTIONDATA
			if (SpriteNotionAssets != null)
			{
				foreach (var asset in SpriteNotionAssets)
				{
					foreach (var data in asset.Data)
					{
						if (dic.ContainsKey(data.LocId)) continue;
						dic.Add(data.LocId, data);
					}
				}
			}
#endif

			return dic;
		}
		
		
		private static SerializableDictionary<string, LocalizationData<AudioClip>> GetAudioAssetsLookup()
		{
			var dic = new SerializableDictionary<string, LocalizationData<AudioClip>>();

			if (AudioNormalAssets != null)
			{
				foreach (var asset in AudioNormalAssets)
				{
					foreach (var data in asset.Data)
					{
						if (dic.ContainsKey(data.LocId)) continue;
						dic.Add(data.LocId, data);
					}
				}
			}
			
#if CARTERGAMES_NOTIONDATA
			if (AudioNotionAssets != null)
			{
				foreach (var asset in AudioNotionAssets)
				{
					foreach (var data in asset.Data)
					{
						if (dic.ContainsKey(data.LocId)) continue;
						dic.Add(data.LocId, data);
					}
				}
			}
#endif

			return dic;
		}
    }
}

#endif