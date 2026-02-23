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
using CarterGames.Cart.Events;
using UnityEngine;

namespace CarterGames.Cart.Crates.DataValues.Events
{
	/// <summary>
	/// An event data value for the cart evt class.
	/// </summary>
	[CreateAssetMenu(fileName = "Data Value Cart Evt", menuName = "Carter Games/The Cart/Crates/Data Values/Events/Data Value Cart Evt", order = 0)]
	[Serializable]
	public class DataValueEvt : DataValueEventBase
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private readonly Evt evt = new Evt();

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Adds a listener when called.
		/// </summary>
		/// <param name="action">The action to add.</param>
		public override void AddListener(Action action)
		{
			evt.Add(action);
		}


		/// <summary>
		/// Removes a listener when called.
		/// </summary>
		/// <param name="action">The action to remove.</param>
		public override void RemoveListener(Action action)
		{
			evt.Remove(action);
		}


		/// <summary>
		/// Raises the event when called.
		/// </summary>
		public override void Raise()
		{
			evt.Raise();
		}


		/// <summary>
		/// Resets the data value when called.
		/// </summary>
		public override void ResetAsset()
		{
			evt.Clear();
		}
	}
}

#endif