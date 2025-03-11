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

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Core.Logs;
using CarterGames.Cart.Core.Save;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
	public static class LocalizationManager
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private const string SaveKeyCurrentLanguage = "CarterGames_Cart_Module_Localization_Language";
		
		private static Dictionary<string, LocalizationData<string>> textLookupCache;
		private static Dictionary<string, LocalizationData<Sprite>> spriteLookupCache;
		private static Dictionary<string, LocalizationData<AudioClip>> audioLookupCache;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Events
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Raises when the language is changed by the API.
		/// </summary>
		public static readonly Evt LanguageChanged = new Evt();

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private static List<DataAssetLocalizedText> TextNormalAssets => DataAccess.GetAssets<DataAssetLocalizedText>();
		private static List<DataAssetLocalizedSprite> SpriteNormalAssets => DataAccess.GetAssets<DataAssetLocalizedSprite>();
		private static List<DataAssetLocalizedAudio> AudioNormalAssets => DataAccess.GetAssets<DataAssetLocalizedAudio>();

#if CARTERGAMES_CART_MODULE_NOTIONDATA
		private static List<NotionDataAssetLocalizedText> TextNotionAssets => DataAccess.GetAssets<NotionDataAssetLocalizedText>();
		private static List<NotionDataAssetLocalizedSprite> SpriteNotionAssets => DataAccess.GetAssets<NotionDataAssetLocalizedSprite>();
		private static List<NotionDataAssetLocalizedAudio> AudioNotionAssets => DataAccess.GetAssets<NotionDataAssetLocalizedAudio>();
#endif
		
		public static Dictionary<string, LocalizationData<string>> TextLookup => CacheRef.GetOrAssign(ref textLookupCache, GetTextAssetsLookup);
		public static Dictionary<string, LocalizationData<Sprite>> SpriteLookup => CacheRef.GetOrAssign(ref spriteLookupCache, GetSpriteAssetsLookup);
		public static Dictionary<string, LocalizationData<AudioClip>> AudioLookup => CacheRef.GetOrAssign(ref audioLookupCache, GetAudioAssetsLookup);

		/// <summary>
		/// The current language assigned.
		/// </summary>
		public static Language CurrentLanguage
		{
			get
			{
				var data = CartSaveHandler.Get<string>(SaveKeyCurrentLanguage);
				
				if (string.IsNullOrEmpty(data))
				{
					SetLanguage(Language.Default);
				}
				
				return JsonUtility.FromJson<Language>(CartSaveHandler.Get<string>(SaveKeyCurrentLanguage));
			}
		}


		/// <summary>
		/// Gets the languages in the system.
		/// </summary>
		public static List<Language> GetLanguages => DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private static Dictionary<string, LocalizationData<string>> GetTextAssetsLookup()
		{
			var dic = new Dictionary<string, LocalizationData<string>>();

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
		
		
		private static Dictionary<string, LocalizationData<Sprite>> GetSpriteAssetsLookup()
		{
			var dic = new Dictionary<string, LocalizationData<Sprite>>();

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
		
		
		private static Dictionary<string, LocalizationData<AudioClip>> GetAudioAssetsLookup()
		{
			var dic = new Dictionary<string, LocalizationData<AudioClip>>();

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


		/// <summary>
		/// Sets the language to the entered language when called.
		/// </summary>
		/// <param name="language">The language to assign.</param>
		public static void SetLanguage(Language language)
		{
			CartSaveHandler.Set(SaveKeyCurrentLanguage, JsonUtility.ToJson(language));
			LanguageChanged.Raise();
		}


		/// <summary>
		/// Gets the data for copy for a particular id.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <returns>The copy for the id.</returns>
		private static LocalizationData<string> GetTextCopyData(string id)
		{
			return TextLookup[id];
		}
		
		
		/// <summary>
		/// Gets the data for copy for a particular id.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <returns>The copy for the id.</returns>
		private static LocalizationData<Sprite> GetSpriteCopyData(string id)
		{
			return SpriteLookup[id];
		}
		
		
		/// <summary>
		/// Gets the data for copy for a particular id.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <returns>The copy for the id.</returns>
		private static LocalizationData<AudioClip> GetAudioCopyData(string id)
		{
			return AudioLookup[id];
		}


		/// <summary>
		/// Tries to get the copy for a particular id in the currently set language.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <param name="copy">The copy for the id in the current language.</param>
		/// <returns>If it was successful or not.</returns>
		public static bool TryGetTextCopy(string id, out string copy)
		{
			copy = string.Empty;
			
			var data = GetTextCopyData(id).Entries
				.FirstOrDefault(t => t.LanguageCode.ToLowerInvariant().Equals(CurrentLanguage.Code.ToLowerInvariant()));

			if (data == null)
			{
				CartLogger.LogError<LogCategoryModules>(
					$"[TryGetCopy] Unable to find copy for id {id} in {CurrentLanguage.DisplayName} in the system, please make sure it exists. Returning null",
					typeof(LocalizationManager));
				
				return false;
			}
			
			copy = data.Copy;
			return true;
		}
		
		
		/// <summary>
		/// Tries to get the copy for a particular id in the currently set language.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <param name="copy">The copy for the id in the current language.</param>
		/// <returns>If it was successful or not.</returns>
		public static bool TryGetSpriteCopy(string id, out Sprite copy)
		{
			copy = null;
			
			var data = GetSpriteCopyData(id).Entries
				.FirstOrDefault(t => t.LanguageCode.ToLowerInvariant().Equals(CurrentLanguage.Code.ToLowerInvariant()));

			if (data == null)
			{
				CartLogger.LogError<LogCategoryModules>(
					$"[TryGetCopy] Unable to find copy for id {id} in {CurrentLanguage.DisplayName} in the system, please make sure it exists. Returning null",
					typeof(LocalizationManager));
				
				return false;
			}
			
			copy = data.Copy;
			return true;
		}
		
		
		/// <summary>
		/// Tries to get the copy for a particular id in the currently set language.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <param name="copy">The copy for the id in the current language.</param>
		/// <returns>If it was successful or not.</returns>
		public static bool TryGetAudioCopy(string id, out AudioClip copy)
		{
			copy = null;
			
			var data = GetAudioCopyData(id).Entries
				.FirstOrDefault(t => t.LanguageCode.ToLowerInvariant().Equals(CurrentLanguage.Code.ToLowerInvariant()));

			if (data == null)
			{
				CartLogger.LogError<LogCategoryModules>(
					$"[TryGetCopy] Unable to find copy for id {id} in {CurrentLanguage.DisplayName} in the system, please make sure it exists. Returning null",
					typeof(LocalizationManager));
				
				return false;
			}
			
			copy = data.Copy;
			return true;
		}


		/// <summary>
		/// Gets the copy for a particular id in the currently set language.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <returns>The copy for the id in the current language.</returns>
		public static string GetText(string id)
		{
			var copy = GetTextCopyData(id).Entries.FirstOrDefault(t =>
				t.LanguageCode.ToLowerInvariant().Equals(CurrentLanguage.Code.ToLowerInvariant()))
				?.Copy;

			if (copy == null)
			{
				CartLogger.LogError<LogCategoryModules>(
					$"[GetCopy] Unable to find copy for id {id} in {CurrentLanguage.DisplayName} in the system, please make sure it exists. Returning null",
					typeof(LocalizationManager));
			}

			return copy;
		}
		
		
		/// <summary>
		/// Gets the copy for a particular id in the currently set language.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <returns>The copy for the id in the current language.</returns>
		public static Sprite GetSprite(string id)
		{
			var copy = GetSpriteCopyData(id).Entries.FirstOrDefault(t =>
					t.LanguageCode.ToLowerInvariant().Equals(CurrentLanguage.Code.ToLowerInvariant()))
				?.Copy;

			if (copy == null)
			{
				CartLogger.LogError<LogCategoryModules>(
					$"[GetCopy] Unable to find copy for id {id} in {CurrentLanguage.DisplayName} in the system, please make sure it exists. Returning null",
					typeof(LocalizationManager));
			}

			return copy;
		}
		
		
		/// <summary>
		/// Gets the copy for a particular id in the currently set language.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <returns>The copy for the id in the current language.</returns>
		public static AudioClip GetAudio(string id)
		{
			var copy = GetAudioCopyData(id).Entries.FirstOrDefault(t =>
					t.LanguageCode.ToLowerInvariant().Equals(CurrentLanguage.Code.ToLowerInvariant()))
				?.Copy;

			if (copy == null)
			{
				CartLogger.LogError<LogCategoryModules>(
					$"[GetCopy] Unable to find copy for id {id} in {CurrentLanguage.DisplayName} in the system, please make sure it exists. Returning null",
					typeof(LocalizationManager));
			}

			return copy;
		}
	}
}

#endif