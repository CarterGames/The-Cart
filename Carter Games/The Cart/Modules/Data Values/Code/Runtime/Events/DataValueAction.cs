﻿#if CARTERGAMES_CART_MODULE_DATAVALUES

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using UnityEngine;

namespace CarterGames.Cart.Modules.DataValues.Events
{
	/// <summary>
	/// An event data value for normal system action.
	/// </summary>
	[CreateAssetMenu(fileName = "Data Value Action", menuName = "Carter Games/The Cart/Modules/Data Values/Events/Data Value Action", order = 1)]
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