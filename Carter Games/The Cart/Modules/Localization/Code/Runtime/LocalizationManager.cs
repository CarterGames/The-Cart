#if CARTERGAMES_CART_MODULE_LOCALIZATION

/*
 * Copyright (c) 2024 Carter Games
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
		private static Dictionary<string, LocalizationData> lookupCache;

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

		private static List<DataAssetLocalizedText> NormalAssets => DataAccess.GetAssets<DataAssetLocalizedText>();

#if CARTERGAMES_CART_MODULE_NOTIONDATA
		private static List<NotionDataAssetLocalizedText> NotionAssets => DataAccess.GetAssets<NotionDataAssetLocalizedText>();
#endif

		private static Dictionary<string, LocalizationData> Lookup => CacheRef.GetOrAssign(ref lookupCache, GetAssetsLookup);

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
					SetLanguage(Language.None);
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

		private static Dictionary<string, LocalizationData> GetAssetsLookup()
		{
			var dic = new Dictionary<string, LocalizationData>();

			if (NormalAssets != null)
			{
				foreach (var asset in NormalAssets)
				{
					foreach (var data in asset.Data)
					{
						if (dic.ContainsKey(data.LocId)) continue;
						dic.Add(data.LocId, data);
					}
				}
			}
			
#if CARTERGAMES_CART_MODULE_NOTIONDATA
			if (NotionAssets != null)
			{
				foreach (var asset in NotionAssets)
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
		private static LocalizationData GetCopyData(string id)
		{
			return Lookup[id];
		}


		/// <summary>
		/// Tries to get the copy for a particular id in the currently set language.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <param name="copy">The copy for the id in the current language.</param>
		/// <returns>If it was successful or not.</returns>
		public static bool TryGetCopy(string id, out string copy)
		{
			copy = string.Empty;
			
			var data = GetCopyData(id).Entries
				.FirstOrDefault(t => t.LanguageCode.Equals(CurrentLanguage.Code));

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
		public static string GetCopy(string id)
		{
			var copy = GetCopyData(id).Entries.FirstOrDefault(t =>
				t.LanguageCode.Equals(CurrentLanguage.Code))
				?.Copy;

			if (copy == null)
			{
				CartLogger.LogError<LogCategoryModules>(
					$"[GetCopy] Unable to find copy for id {id} in {CurrentLanguage.DisplayName} in the system, please make sure it exists. Returning null",
					typeof(LocalizationManager));
			}

			return copy;
		}


		public static LocalizationData GetRawData(string id)
		{
			return GetCopyData(id);
		}
	}
}

#endif