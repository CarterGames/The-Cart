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
	/// An event data value for normal system action.
	/// </summary>
	[CreateAssetMenu(fileName = "Data Value Action", menuName = "Carter Games/The Cart/Crates/Data Values/Events/Data Value Action", order = 1)]
	[Serializable]
	public class DataValueAction : DataValueEventBase
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private event Action EventAction;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Adds a listener when called.
		/// </summary>
		/// <param name="action">The action to add.</param>
		public override void AddListener(Action action)
		{
			EventAction -= action;
			EventAction += action;
		}


		/// <summary>
		/// Removes a listener when called.
		/// </summary>
		/// <param name="action">The action to remove.</param>
		public override void RemoveListener(Action action)
		{
			EventAction -= action;
		}


		/// <summary>
		/// Raises the event when called.
		/// </summary>
		public override void Raise()
		{
			EventAction?.Invoke();
		}


		/// <summary>
		/// Resets the data value when called.
		/// </summary>
		public override void ResetAsset()
		{
			EventAction = delegate { };
		}
	}
}

#endif