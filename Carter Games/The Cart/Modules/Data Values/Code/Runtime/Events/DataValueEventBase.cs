#if CARTERGAMES_CART_MODULE_DATAVALUES

/*
 * Copyright (c) 2024 Carter Games
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
	/// A base class for event data values.
	/// </summary>
	public abstract class DataValueEventBase : DataValueAsset
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField, TextArea] private string devDescription;
		[SerializeField] private string key;
		
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