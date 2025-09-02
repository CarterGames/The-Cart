#if CARTERGAMES_CART_MODULE_CONDITIONS

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Core.Management;
using UnityEngine;

namespace CarterGames.Cart.Modules.Conditions
{
	/// <summary>
	/// A pre-defined criteria for the app version number.
	/// </summary>
	[Serializable]
	public sealed class CriteriaAppVersion : Criteria
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField] private NumericalComparisonType comparisonType;
		[SerializeField] private string version;

		[NonSerialized] private Version cachedAppVersionNumber;
		[NonSerialized] private Version cachedCheckVersionNumber;
		[NonSerialized] private bool isValid;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Gets the converted app version to the AppVersion class type for comparison.
		/// </summary>
		private Version AppVersionNumber
		{
			get
			{
				if (cachedAppVersionNumber != null) return cachedAppVersionNumber;
				cachedAppVersionNumber = new Version(Application.version);
				return cachedAppVersionNumber;
			}
		}
		
		
		private Version CheckVersion
		{
			get
			{
				if (cachedCheckVersionNumber != null) return cachedCheckVersionNumber;
				cachedCheckVersionNumber = new Version(version);
				return cachedCheckVersionNumber;
			}
		}

		
		protected override bool Valid => isValid;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public override void OnInitialize(Evt stateChanged)
		{
			isValid = comparisonType switch
			{
				NumericalComparisonType.LessThan => CheckVersion.CompareTo(AppVersionNumber) < 0,
				NumericalComparisonType.LessThanOrEqual => CheckVersion.CompareTo(AppVersionNumber) < 0 || AppVersionNumber.Equals(CheckVersion),
				NumericalComparisonType.Equals => AppVersionNumber.Equals(CheckVersion),
				NumericalComparisonType.GreaterThanOrEqual => CheckVersion.CompareTo(AppVersionNumber) > 0 || AppVersionNumber.Equals(CheckVersion),
				NumericalComparisonType.GreaterThan => CheckVersion.CompareTo(AppVersionNumber) > 0,
				NumericalComparisonType.Unassigned => false,
				_ => false
			};

			stateChanged.Raise();
		}
	}
}

#endif