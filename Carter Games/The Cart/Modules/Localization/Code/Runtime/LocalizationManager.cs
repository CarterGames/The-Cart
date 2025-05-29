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
using System.Globalization;
using System.Linq;
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

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static DataAssetGlobalLocalizationLookup Lookup =>
			DataAccess.GetAsset<DataAssetGlobalLocalizationLookup>();

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

		/// <summary>
		/// Gets if the language has been set.
		/// </summary>
		private static bool HasSetLanguage => string.IsNullOrEmpty(CartSaveHandler.Get<string>(SaveKeyCurrentLanguage));
		
		
		/// <summary>
		/// The current language assigned.
		/// </summary>
		public static Language CurrentLanguage
		{
			get
			{
				try
				{
					var data = CartSaveHandler.Get<string>(SaveKeyCurrentLanguage);
					
					return string.IsNullOrEmpty(data) 
						? Language.Default 
						: JsonUtility.FromJson<Language>(CartSaveHandler.Get<string>(SaveKeyCurrentLanguage));
				}
#pragma warning disable 0168
				catch (Exception e)
#pragma warning restore
				{
					return Language.Default;
				}
			}
		}


		/// <summary>
		/// Gets the languages in the system.
		/// </summary>
		public static List<Language> GetLanguages => DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeAtRuntime()
		{
			if (HasSetLanguage) return;
			
			if (GetLanguages.Any(t => t.Code == CultureInfo.CurrentCulture.Name))
			{
				SetLanguage(GetLanguages.FirstOrDefault(t => t.Code == CultureInfo.CurrentCulture.Name));
			}
			else
			{
				SetLanguage(Language.Default);
			}
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


		private static bool TryGetLanguageFromCode(string languageCode, out Language language)
		{
			language = CurrentLanguage;
			
			if (!string.IsNullOrEmpty(languageCode))
			{
				if (DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages
				    .Any(t => t.Code == languageCode))
				{
					language = DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages
						.First(t => t.Code == languageCode);
					return true;
				}
				else
				{
					CartLogger.LogWarning<LogCategoryLocalization>(
						$"Unable to get override language from code {languageCode}. Using default.",
						typeof(LocalizationManager));
					
					language = Language.Default;
					return false;
				}
			}

			return true;
		}


		/// <summary>
		/// Gets the data for copy for a particular id.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <returns>The copy for the id.</returns>
		private static LocalizationData<string> GetTextCopyData(string id)
		{
			return Lookup.TextLocalizationLookup[id];
		}
		
		
		/// <summary>
		/// Gets the data for copy for a particular id.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <returns>The copy for the id.</returns>
		private static LocalizationData<Sprite> GetSpriteCopyData(string id)
		{
			return Lookup.SpriteLocalizationLookup[id];
		}
		
		
		/// <summary>
		/// Gets the data for copy for a particular id.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <returns>The copy for the id.</returns>
		private static LocalizationData<AudioClip> GetAudioCopyData(string id)
		{
			return Lookup.AudioLocalizationLookup[id];
		}


		/// <summary>
		/// Tries to get the copy for a particular id in the currently set language.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <param name="copy">The copy for the id in the current language.</param>
		/// <returns>If it was successful or not.</returns>
		public static bool TryGetTextCopy(string id, out string copy, string overrideLanguageCode = "")
		{
			TryGetLanguageFromCode(overrideLanguageCode, out var language);
			
			copy = string.Empty;
			
			var data = GetTextCopyData(id).Entries
				.FirstOrDefault(t => t.LanguageCode.Equals(language.Code));

			if (data == null)
			{
				CartLogger.LogError<LogCategoryLocalization>(
					$"[TryGetCopy] Unable to find copy for id {id} in {language.DisplayName} in the system, please make sure it exists. Returning null",
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
		public static bool TryGetSpriteCopy(string id, out Sprite copy, string overrideLanguageCode = "")
		{
			TryGetLanguageFromCode(overrideLanguageCode, out var language);
			
			copy = null;
			
			var data = GetSpriteCopyData(id).Entries
				.FirstOrDefault(t => t.LanguageCode.Equals(language.Code));

			if (data == null)
			{
				CartLogger.LogError<LogCategoryLocalization>(
					$"[TryGetCopy] Unable to find copy for id {id} in {language.DisplayName} in the system, please make sure it exists. Returning null",
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
		public static bool TryGetAudioCopy(string id, out AudioClip copy, string overrideLanguageCode = "")
		{
			TryGetLanguageFromCode(overrideLanguageCode, out var language);
			
			copy = null;
			
			var data = GetAudioCopyData(id).Entries
				.FirstOrDefault(t => t.LanguageCode.Equals(language.Code));

			if (data == null)
			{
				CartLogger.LogError<LogCategoryLocalization>(
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
		public static string GetText(string id, string overrideLanguageCode = "")
		{
			TryGetLanguageFromCode(overrideLanguageCode, out var language);
			
			var copy = GetTextCopyData(id).Entries.FirstOrDefault(t =>
				t.LanguageCode.Equals(language.Code))
				?.Copy;

			if (copy == null)
			{
				CartLogger.LogError<LogCategoryLocalization>(
					$"[GetCopy] Unable to find copy for id {id} in {language.DisplayName} in the system, please make sure it exists. Returning null",
					typeof(LocalizationManager));
			}

			return copy;
		}
		
		
		/// <summary>
		/// Gets the copy for a particular id in the currently set language.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <returns>The copy for the id in the current language.</returns>
		public static Sprite GetSprite(string id, string overrideLanguageCode = "")
		{
			TryGetLanguageFromCode(overrideLanguageCode, out var language);
			
			var copy = GetSpriteCopyData(id).Entries.FirstOrDefault(t =>
					t.LanguageCode.Equals(language.Code))
				?.Copy;

			if (copy == null)
			{
				CartLogger.LogError<LogCategoryLocalization>(
					$"[GetCopy] Unable to find copy for id {id} in {language.DisplayName} in the system, please make sure it exists. Returning null",
					typeof(LocalizationManager));
			}

			return copy;
		}
		
		
		/// <summary>
		/// Gets the copy for a particular id in the currently set language.
		/// </summary>
		/// <param name="id">The id to find.</param>
		/// <returns>The copy for the id in the current language.</returns>
		public static AudioClip GetAudio(string id, string overrideLanguageCode = "")
		{
			TryGetLanguageFromCode(overrideLanguageCode, out var language);
			
			var copy = GetAudioCopyData(id).Entries.FirstOrDefault(t =>
					t.LanguageCode.Equals(language.Code))
				?.Copy;

			if (copy == null)
			{
				CartLogger.LogError<LogCategoryLocalization>(
					$"[GetCopy] Unable to find copy for id {id} in {language.DisplayName} in the system, please make sure it exists. Returning null",
					typeof(LocalizationManager));
			}

			return copy;
		}
	}
}

#endif