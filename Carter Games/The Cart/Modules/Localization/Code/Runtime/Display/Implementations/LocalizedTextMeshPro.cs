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
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Logs;
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
	/// <summary>
	/// A simple component for a localized TMP_Text element.
	/// </summary>
	public sealed class LocalizedTextMeshPro : LocalizedComponent<string>
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		[SerializeField] [LocalizationId] private string locId;
		[SerializeField] private TMP_Text displayLabel;

		private TMP_FontAsset defaultFontAsset;
		private Material defaultFontMaterial;
		private object[] formatParameters;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// A reference to the component, assumed on same object or child if auto-finding.
		/// </summary>
		public TMP_Text LabelRef => CacheRef.GetOrAssign(ref displayLabel, GetComponentInChildren<TMP_Text>);

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public override string LocId
		{
			get => locId;
			protected set => locId = value;
		}
		

		protected override string GetValueFromId(string locId)
		{
			return LocalizationManager.GetText(locId);
		}

		
		public void Localize(params object[] parameters)
		{
			if (string.IsNullOrEmpty(locId))
			{
				CartLogger.LogWarning<LogCategoryLocalization>("Cannot apply just parameters to this localized text as the loc id is not set!");
				return;
			}
			
			formatParameters = parameters;
			base.Localize(locId);
		}
		

		public void Localize(string locId, params object[] parameters)
		{
			formatParameters = parameters;
			base.Localize(locId);
		}

		
		protected override void AssignValue(string localizedValue)
		{
			if (formatParameters.IsEmptyOrNull())
			{
				LabelRef.SetText(localizedValue);
			}
			else
			{
				try
				{
					LabelRef.SetText(string.Format(localizedValue, formatParameters));
				}
				catch (Exception e)
				{
					CartLogger.LogWarning<LogCategoryLocalization>($"Failed to format copy with parameters: {e.Message}.");
				}
			}
			
			TryAssignFont();
		}


		private void TryAssignFont()
		{
			if (defaultFontAsset == null)
			{
				defaultFontAsset = LabelRef.font;
				
				if (LabelRef.defaultMaterial != LabelRef.fontMaterial)
				{
					defaultFontMaterial = LabelRef.fontMaterial;
				}
			}

			var data = DataAccess.GetAsset<DataAssetLocalizationFonts>()
				.GetFontDataFromFont(defaultFontAsset, defaultFontMaterial);

			if (data.Font == LabelRef.font) return;

			LabelRef.font = data.Font;

			if (!data.UsesMaterial) return;
			LabelRef.fontMaterial = data.FontMaterial;
		}
	}
}

#endif