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

using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
{
	/// <summary>
	/// Defines the different types of criteria group checks can be assigned to.
	/// </summary>
	public enum CriteriaGroupCheckType
	{
		/// <summary>
		/// All in the group must pass to be valid.
		/// </summary>
		All = 1,
		
		
		/// <summary>
		/// Any can pass to be valid.
		/// </summary>
		Any = 2,
		
		
		/// <summary>
		/// None can pass to be valid.
		/// </summary>
		None = 3,
		
		
		/// <summary>
		/// All or none can pass to be valid.
		/// </summary>
		[InspectorName("All or None")] AllOrNone = 4,
	}
}

#endif