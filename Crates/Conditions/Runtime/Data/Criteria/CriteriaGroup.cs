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
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
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
		
		[SerializeField] private CriteriaGroupCheckType groupCheckType = CriteriaGroupCheckType.All;
		[SerializeField] private string groupId;
		[SerializeField] private string groupUuid;
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
		public bool IsTrue
		{
			get
			{
				switch (CheckType)
				{
					case CriteriaGroupCheckType.Any:
						return criteria.Any(t => t.IsCriteriaValid);
					case CriteriaGroupCheckType.None:
						return criteria.All(t => !t.IsCriteriaValid);
					case CriteriaGroupCheckType.All:
						return criteria.All(t => t.IsCriteriaValid);
					case CriteriaGroupCheckType.AllOrNone:
						return criteria.All(t => !t.IsCriteriaValid) || criteria.All(t => t.IsCriteriaValid);
					default:
						return false;
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