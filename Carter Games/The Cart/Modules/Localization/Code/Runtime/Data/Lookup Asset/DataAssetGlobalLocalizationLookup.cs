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
#if CARTERGAMES_NOTIONDATA
using CarterGames.Assets.Shared.Common;
#endif
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Data;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
	[Serializable]
    public class DataAssetGlobalLocalizationLookup : DataAsset
    {
        [SerializeField] private SerializableDictionary<string, LocalizationData<string>> textLookup;
        [SerializeField] private SerializableDictionary<string, LocalizationData<Sprite>> spriteLookup;
        [SerializeField] private SerializableDictionary<string, LocalizationData<AudioClip>> audioLookup;


        public IReadOnlyDictionary<string, LocalizationData<string>> TextLocalizationLookup => textLookup;
        public IReadOnlyDictionary<string, LocalizationData<Sprite>> SpriteLocalizationLookup => spriteLookup;
        public IReadOnlyDictionary<string, LocalizationData<AudioClip>> AudioLocalizationLookup => audioLookup;
        
        
        private static List<DataAssetLocalizedText> TextNormalAssets => DataAccess.GetAssets<DataAssetLocalizedText>();
        private static List<DataAssetLocalizedSprite> SpriteNormalAssets => DataAccess.GetAssets<DataAssetLocalizedSprite>();
        private static List<DataAssetLocalizedAudio> AudioNormalAssets => DataAccess.GetAssets<DataAssetLocalizedAudio>();

#if CARTERGAMES_NOTIONDATA
        private static List<NotionDataAssetLocalizedText> TextNotionAssets => NotionDataAccessor.GetAssets<NotionDataAssetLocalizedText>();
        private static List<NotionDataAssetLocalizedSprite> SpriteNotionAssets => NotionDataAccessor.GetAssets<NotionDataAssetLocalizedSprite>();
        private static List<NotionDataAssetLocalizedAudio> AudioNotionAssets => NotionDataAccessor.GetAssets<NotionDataAssetLocalizedAudio>();
#endif
        
        

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