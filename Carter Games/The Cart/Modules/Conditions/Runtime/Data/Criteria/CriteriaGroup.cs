#if CARTERGAMES_CART_MODULE_CONDITIONS

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
using UnityEngine;

namespace CarterGames.Cart.Modules.Conditions
{
	/// <summary>
	/// A group of criteria to be checked together as a group.
	/// </summary>
	[Serializable]
	public sealed class CriteriaGroup
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField] private CriteriaGroupCheckType groupCheckType = CriteriaGroupCheckType.And;
		[SerializeField] private string groupId;
		[SerializeField] private int groupUuid;
		[SerializeField] private List<Criteria> criteria;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// The type of check the group needs to pass.
		/// </summary>
		public CriteriaGroupCheckType CheckType => groupCheckType;
		
		
		/// <summary>
		/// Gets if the group is valid or not.
		/// </summary>
		public bool IsValid
		{
			get
			{
				if (CheckType == CriteriaGroupCheckType.And)
				{
					return criteria.All(t => t.IsCriteriaValid);
				}
				else
				{
					return criteria.Any(t => t.IsCriteriaValid);
				}
			}
		}

		
		/// <summary>
		/// Gets the criteria in the group.
		/// </summary>
		public IEnumerable<Criteria> Criteria => criteria;
	}
}

#endif