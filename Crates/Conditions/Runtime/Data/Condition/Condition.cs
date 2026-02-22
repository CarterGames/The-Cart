#if CARTERGAMES_CART_CRATE_CONDITIONS

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Data;
using CarterGames.Cart.Events;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
{
	/// <summary>
	/// A container for a condition asset.
	/// </summary>
	[Serializable]
	public sealed class Condition : DataAsset
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField] [HideInInspector] private bool isExpanded;
		[SerializeField] private string uid;
		[SerializeField] private string id;
		[SerializeField] private List<CriteriaGroup> criteriaList;
		

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// The id of the condition.
		/// </summary>
		public string Id => id;

		
		/// <summary>
		/// Gets if the condition is valid or not.
		/// </summary>
		public bool IsTrue => criteriaList.All(t => t is {IsTrue: true});

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Events
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Raises when the condition is changed.
		/// </summary>
		public readonly Evt StateChanged = new Evt();
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Initializes the condition when called.
		/// </summary>
		public new void Initialize()
		{
			foreach (var group in criteriaList)
			{
				foreach (var criteria in group.Criteria)
				{
					if (criteria == null) continue;
					criteria.OnInitialize(StateChanged);
				}
			}
		}
	}
}

#endif