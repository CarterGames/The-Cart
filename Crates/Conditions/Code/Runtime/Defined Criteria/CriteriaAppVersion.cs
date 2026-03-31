#if CARTERGAMES_CART_CRATE_CONDITIONS

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
using CarterGames.Cart.Events;
using CarterGames.Cart.Management;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
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


		public override string DisplayName
		{
			get
			{
				switch (comparisonType)
				{
					case NumericalComparisonType.LessThan:
						return $"App Version < {version}";
					case NumericalComparisonType.LessThanOrEqual:
						return $"App Version <= {version}";
					case NumericalComparisonType.Equals:
						return $"App Version == {version}";
					case NumericalComparisonType.GreaterThanOrEqual:
						return $"App Version >= {version}";
					case NumericalComparisonType.GreaterThan:
						return $"App Version > {version}";
					case NumericalComparisonType.Unassigned:
					default:
						return "App Version";
				}
			}
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public override void OnInitialize(Evt stateChanged)
		{
			isValid = comparisonType switch
			{
				NumericalComparisonType.LessThan => CheckVersion.CompareTo(AppVersionNumber) > 0,
				NumericalComparisonType.LessThanOrEqual => CheckVersion.CompareTo(AppVersionNumber) > 0 || AppVersionNumber.Equals(CheckVersion),
				NumericalComparisonType.Equals => AppVersionNumber.Equals(CheckVersion),
				NumericalComparisonType.GreaterThanOrEqual => CheckVersion.CompareTo(AppVersionNumber) < 0 || AppVersionNumber.Equals(CheckVersion),
				NumericalComparisonType.GreaterThan => CheckVersion.CompareTo(AppVersionNumber) < 0,
				NumericalComparisonType.Unassigned => false,
				_ => false
			};

			stateChanged.Raise();
		}
	}
}

#endif