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
using CarterGames.Cart;
using CarterGames.Cart.Data;
using CarterGames.Cart.Logs;
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization
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
		
		/// <summary>
		/// The loc id to use.
		/// </summary>
		public override string LocId
		{
			get => locId;
			protected set => locId = value;
		}
		

		/// <summary>
		/// Gets the text from the loc id.
		/// </summary>
		/// <param name="requestLocId">The id to use.</param>
		/// <returns>The text for the loc id.</returns>
		protected override string GetValueFromId(string requestLocId)
		{
			return LocalizationManager.GetText(requestLocId);
		}

		
		/// <summary>
		/// Localizes the element with the current loc id and uses the parameters entered to format the string.
		/// </summary>
		/// <param name="parameters">The parameters to use.</param>
		public void Localize(params object[] parameters)
		{
			if (string.IsNullOrEmpty(locId))
			{
				CartLogger.LogWarning<LogCategoryCrateLocalization>("Cannot apply just parameters to this localized text as the loc id is not set!");
				return;
			}
			
			formatParameters = parameters;
			base.Localize(locId);
		}
		

		/// <summary>
		/// Localizes the element with the entered loc id and uses the parameters entered to format the string.
		/// </summary>
		/// <param name="customLocId">The loc id to use.</param>
		/// <param name="parameters">The parameters to use.</param>
		public void Localize(string customLocId, params object[] parameters)
		{
			formatParameters = parameters;
			base.Localize(customLocId);
		}

		
		/// <summary>
		/// Assigns the value based on the data assigned to the component.
		/// </summary>
		/// <param name="localizedValue">The localized value to show.</param>
		protected override void AssignValue(string localizedValue)
		{
			if (formatParameters.IsEmptyOrNull())
			{
				// Just set..
				LabelRef.SetText(localizedValue);
			}
			else
			{
				// Try set with params entered..
				try
				{
					LabelRef.SetText(string.Format(localizedValue, formatParameters));
				}
				catch (Exception e)
				{
					CartLogger.LogWarning<LogCategoryCrateLocalization>($"Failed to format copy with parameters: {e.Message}.");
				}
			}
			
			// Update font.
			TryAssignFont();
		}


		/// <summary>
		/// Tries to update the font on the label to the relevant localization font.
		/// </summary>
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