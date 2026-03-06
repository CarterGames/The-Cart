#if CARTERGAMES_CART_CRATE_CONDITIONS && CARTERGAMES_CART_CRATE_PANELS

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
using CarterGames.Cart.Crates.Panels;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
{
	/// <summary>
	/// A criteria for checking the state of a panel.
	/// </summary>
	[Serializable]
	public sealed class CriteriaPanelState : Criteria
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField] private string panelId;
		[SerializeField] private bool panelOpen;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		protected override bool Valid
		{
			get
			{
				if (string.IsNullOrEmpty(panelId)) return false;
				if (!PanelTracker.IsTrackingPanel(panelId)) return false;
				return PanelTracker.GetPanel(panelId).IsOpen == panelOpen;
			}
		}

		protected override string DisplayName
		{
			get
			{
				if (string.IsNullOrEmpty(panelId)) return base.DisplayName;
				return $"{panelId} {IsString} {(panelOpen ? "open" : "closed")}";
			}
		}
		
		public override string SearchProviderGroup => "Panels";

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public override void OnInitialize(Evt stateChanged)
		{
			panelOpen = false;
			
			PanelTracker.GetPanel(panelId).OpenStartedEvt.Add(stateChanged.Raise);
			PanelTracker.GetPanel(panelId).CloseCompletedEvt.Add(stateChanged.Raise);
		}
	}
}

#endif