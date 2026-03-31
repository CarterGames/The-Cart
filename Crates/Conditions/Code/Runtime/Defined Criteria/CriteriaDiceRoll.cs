#if CARTERGAMES_CART_CRATE_CONDITIONS && CARTERGAMES_CART_CRATE_DICE

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
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
{
	/// <summary>
	/// A criteria that rolls a custom dice.
	/// </summary>
	[Serializable]
	public sealed class CriteriaDiceRoll : Criteria
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField] private int numberOfSides;
		[SerializeField] private IntRange validBounds;

		[NonSerialized] private bool rolledSuccess;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		protected override bool Valid => rolledSuccess;

		public override string DisplayName
		{
			get
			{
				if (numberOfSides <= 0) return base.DisplayName;
				return $"Roll of a D{numberOfSides} result {IsString} within {validBounds.min} - {validBounds.max}";
			}
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public override void OnInitialize(Evt stateChanged)
		{ 
			var roll = Dice.Dice.Custom(numberOfSides);
			rolledSuccess = roll >= validBounds.min && roll <= validBounds.max;
			stateChanged.Raise();
		}
	}
}

#endif