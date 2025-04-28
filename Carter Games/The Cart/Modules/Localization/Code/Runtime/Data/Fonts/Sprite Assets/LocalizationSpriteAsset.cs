using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
	[Serializable]
	public class LocalizationSpriteAsset
	{
		[SerializeField] private string displayName;
		[SerializeField] private SpriteAssetDef[] data;


		public bool HasDefaultSetup => data.Any(t => t.LanguageCode == Language.Default.Code);
		
		public SpriteAssetDef DefaultLanguageData =>
			data.FirstOrDefault(t => t.LanguageCode == Language.Default.Code);
		
		public TMP_SpriteAsset DefaultSpriteAsset =>
			data.FirstOrDefault(t => t.LanguageCode == Language.Default.Code)?.SpriteAsset;
		
		public SpriteAssetDef[] Data => data;
	}
}