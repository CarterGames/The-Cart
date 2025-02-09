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

using CarterGames.Cart.Core;
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
	/// <summary>
	/// A simple component for a localized TMP_Text element.
	/// </summary>
	[AddComponentMenu("Carter Games/The Cart/Modules/Localization/Localized Text Mesh Pro")]
	public sealed class LocalizedTextMeshPro : MonoBehaviour
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		[SerializeField] private string locId;
		[SerializeField] private TMP_Text displayLabel;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// A reference to the component, assumed on same object or child if auto-finding.
		/// </summary>
		private TMP_Text LabelRef => CacheRef.GetOrAssign(ref displayLabel, GetComponentInChildren<TMP_Text>);

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Unity Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private void OnEnable()
		{
			LocalizationManager.LanguageChanged.Add(UpdateDisplay);
			UpdateDisplay();
		}


		private void OnDestroy()
		{
			LocalizationManager.LanguageChanged.Remove(UpdateDisplay);
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Updates the text when called with the latest copy.
		/// </summary>
		private void UpdateDisplay()
		{
			LabelRef.SetText(LocalizationManager.GetCopy(locId));
		}


		/// <summary>
		/// Forces the display to update when called.
		/// </summary>
		public void ForceUpdateDisplay()
		{
			UpdateDisplay();
		}
	}
}

#endif