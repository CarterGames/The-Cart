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

using CarterGames.Cart.Core.Data;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
	/// <summary>
	/// Holds any settings for the localization system needed at runtime.
	/// </summary>
	public sealed class DataAssetSettingsLocalization : DataAsset
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		[SerializeField] private Language currentLanguage;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// The current language set for the game.
		/// </summary>
		public Language CurrentLanguage
		{
			get => currentLanguage;
			set => currentLanguage = value;
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Unity Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private void OnValidate()
		{
			// Sets the default language if one is not defined.
			if (!string.IsNullOrEmpty(currentLanguage.DisplayName) && !string.IsNullOrEmpty(currentLanguage.Code)) return;
			if (DataAccess.GetAsset<DataAssetDefinedLanguages>() == null) return;
			if (DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages.Count <= 0) return;
			currentLanguage = DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages[0];
		}
	}
}

#endif