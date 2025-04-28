using System.Collections.Generic;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Data;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization.Data.Lookup
{
    public class DataAssetGlobalLocalizationLookup : DataAsset
    {
        public SerializableDictionary<string, LocalizationData<string>> textLookup;
        public SerializableDictionary<string, LocalizationData<Sprite>> spriteLookup;
        public SerializableDictionary<string, LocalizationData<AudioClip>> audioLookup;

        
        
        private static List<DataAssetLocalizedText> TextNormalAssets => DataAccess.GetAssets<DataAssetLocalizedText>();
        private static List<DataAssetLocalizedSprite> SpriteNormalAssets => DataAccess.GetAssets<DataAssetLocalizedSprite>();
        private static List<DataAssetLocalizedAudio> AudioNormalAssets => DataAccess.GetAssets<DataAssetLocalizedAudio>();

#if CARTERGAMES_CART_MODULE_NOTIONDATA
        private static List<NotionDataAssetLocalizedText> TextNotionAssets => DataAccess.GetAssets<NotionDataAssetLocalizedText>();
        private static List<NotionDataAssetLocalizedSprite> SpriteNotionAssets => DataAccess.GetAssets<NotionDataAssetLocalizedSprite>();
        private static List<NotionDataAssetLocalizedAudio> AudioNotionAssets => DataAccess.GetAssets<NotionDataAssetLocalizedAudio>();
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
			
#if CARTERGAMES_CART_MODULE_NOTIONDATA
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
			
#if CARTERGAMES_CART_MODULE_NOTIONDATA
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
			
#if CARTERGAMES_CART_MODULE_NOTIONDATA
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