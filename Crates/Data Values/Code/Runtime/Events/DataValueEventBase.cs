#if CARTERGAMES_CART_CRATE_DATAVALUES

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
using UnityEngine;

namespace CarterGames.Cart.Crates.DataValues.Events
{
	/// <summary>
	/// A base class for event data values.
	/// </summary>
	[Serializable]
	public abstract class DataValueEventBase : DataValueAsset
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		[SerializeField] [HideInInspector] [TextArea] private string devDescription;
		[SerializeField] [HideInInspector] private string key;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// The key for the value.
		/// </summary>
		public override string Key => key;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Implement to add a listener.
		/// </summary>
		/// <param name="action">The action to change.</param>
		public abstract void AddListener(Action action);


		/// <summary>
		/// Implement to remove a listener.
		/// </summary>
		/// <param name="action">The action to change.</param>
		public abstract void RemoveListener(Action action);


		/// <summary>
		/// Implement to raise the event.
		/// </summary>
		public abstract void Raise();
	}
}

#endif