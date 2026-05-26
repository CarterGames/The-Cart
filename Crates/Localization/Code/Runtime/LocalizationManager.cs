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
using System.Globalization;
using System.Linq;
using CarterGames.Cart.Data;
using CarterGames.Cart.Events;
using CarterGames.Cart.Logs;
using CarterGames.Cart.Save;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization
{
	public static class LocalizationManager
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private const string SaveKeyCurrentLanguage = "CarterGames_Cart_Crate_Localization_Language";

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static DataAssetLocalizationLookup Lookup =>
			DataAccess.GetAsset<DataAssetLocalizationLookup>();

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
		public static IReadOnlyCollection<Language> GetLanguages => DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages;

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
					CartLogger.LogWarning<LocalizationLogs>(
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
				CartLogger.LogError<LocalizationLogs>(
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
				CartLogger.LogError<LocalizationLogs>(
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
				CartLogger.LogError<LocalizationLogs>(
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
				CartLogger.LogError<LocalizationLogs>(
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
				CartLogger.LogError<LocalizationLogs>(
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
				CartLogger.LogError<LocalizationLogs>(
					$"[GetCopy] Unable to find copy for id {id} in {language.DisplayName} in the system, please make sure it exists. Returning null",
					typeof(LocalizationManager));
			}

			return copy;
		}
	}
}

#endif