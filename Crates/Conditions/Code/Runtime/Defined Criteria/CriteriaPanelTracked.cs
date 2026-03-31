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
	/// A criteria for checking if a panel is being tracked or not. 
	/// </summary>
	[Serializable]
	public sealed class CriteriaPanelTracked : Criteria
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField] private string panelId;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		protected override bool Valid
		{
			get
			{
				if (string.IsNullOrEmpty(panelId)) return false;
				return PanelTracker.IsTrackingPanel(panelId);
			}
		}

		public override string DisplayName
		{
			get
			{
				if (string.IsNullOrEmpty(panelId)) return base.DisplayName;
				return $"{panelId} {IsString} tracked";
			}
		}
		
		public override string SearchProviderGroup => "Panels";

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public override void OnInitialize(Evt stateChanged)
		{
			PanelTracker.PanelTrackedEvt.Add(OnPanelTracked);
			PanelTracker.PanelUnTrackedEvt.Add(OnPanelTracked);
			return;
			
			void OnPanelTracked(Panel panel)
			{
				stateChanged.Raise();
			}
		}
	}
}

#endif