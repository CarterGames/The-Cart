#if CARTERGAMES_CART_CRATE_CONDITIONS && CARTERGAMES_CART_CRATE_DATAVALUES

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
using CarterGames.Cart.Events;
using CarterGames.Cart.Crates.DataValues.Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace CarterGames.Cart.Crates.Conditions
{
	/// <summary>
	/// A criteria for checking is a Data value evt has been raised.
	/// </summary>
	[Serializable]
	public sealed class CriteriaDataValueEvent : Criteria
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		[SerializeField] private DataValueEventBase dataValueEvent;

		[NonSerialized] private bool hasRaised;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		protected override bool Valid => hasRaised;

		public override string DisplayName
		{
			get
			{
				if (dataValueEvent == null) return base.DisplayName;
				return $"{dataValueEvent.Key} {IsString} raised";
			}
		}
		
		public override string SearchProviderGroup => "Data Values";

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public override void OnInitialize(Evt stateChanged)
		{
			hasRaised = false;
			if (dataValueEvent == null) return;
			dataValueEvent.AddListener(UpdateHasRaised);
			return;
			
			void UpdateHasRaised()
			{
				hasRaised = true;
				stateChanged.Raise();
			}
		}
	}
}

#endif