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
		[SerializeField] private VersionNumber version;

		[NonSerialized] private VersionNumber cachedAppVersionNumber;
		[NonSerialized] private bool isValid;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Gets the converted app version to the AppVersion class type for comparison.
		/// </summary>
		private VersionNumber AppVersionNumber
		{
			get
			{
				if (cachedAppVersionNumber != null) return cachedAppVersionNumber;
				cachedAppVersionNumber = new VersionNumber(Application.version);
				return cachedAppVersionNumber;
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
				NumericalComparisonType.LessThan => version < AppVersionNumber,
				NumericalComparisonType.LessThanOrEqual => version < AppVersionNumber || AppVersionNumber.Equals(version),
				NumericalComparisonType.Equals => AppVersionNumber.Equals(version),
				NumericalComparisonType.GreaterThanOrEqual => version > AppVersionNumber || AppVersionNumber.Equals(version),
				NumericalComparisonType.GreaterThan => version > AppVersionNumber,
				NumericalComparisonType.Unassigned => false,
				_ => false
			};

			stateChanged.Raise();
		}
	}
}

#endif