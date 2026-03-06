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
using CarterGames.Cart.Data;
using CarterGames.Cart.Events;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
{
	/// <summary>
	/// The base class for a criteria to add to conditions.
	/// </summary>
	[Serializable]
	public abstract class Criteria : DataAsset
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		[SerializeField] [HideInInspector] protected Condition targetCondition;
		[SerializeField] private bool isExpanded;
		[SerializeField] protected bool readInverted;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Gets if the criteria is valid (includes the read inverted state).
		/// </summary>
		public bool IsCriteriaValid => readInverted ? !Valid : Valid;
		
		
		/// <summary>
		/// Used to define if the criteria is valid (assume read inverted is false).
		/// </summary>
		protected abstract bool Valid { get; }


		/// <summary>
		/// Gets the name used when displaying the criteria.
		/// </summary>
		protected virtual string DisplayName => GetType().Name.Replace("Criteria", string.Empty);
		
		
		public virtual string SearchProviderGroup => string.Empty;


		protected string IsString => readInverted ? "is not" : "is";

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Constructors
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Makes a new criteria that is not included in the data asset index.
		/// </summary>
		protected Criteria()
		{
			excludeFromAssetIndex = true;
		}
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Implement to run logic when the condition is initialized.
		/// </summary>
		/// <param name="stateChanged">The condition state changed event.</param>
		public abstract void OnInitialize(Evt stateChanged);
	}
}

#endif