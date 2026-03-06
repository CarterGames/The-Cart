#if CARTERGAMES_CART_CRATE_COLORFOLDERS && UNITY_EDITOR

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

namespace CarterGames.Cart.Crates.ColourFolders.Editor
{
	/// <summary>
	/// A class to define a folder icon set that can be applied to folders in the project.
	/// </summary>
	[Serializable]
	public sealed class DataFolderIconSet
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		[SerializeField] private string id;
		[SerializeField] private Texture2D large;
		[SerializeField] private Texture2D small;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// The id of the set.
		/// </summary>
		public string Id => id;


		/// <summary>
		/// The large icon for the set.
		/// </summary>
		public Texture2D Large => large;


		/// <summary>
		/// The small icon for the set.
		/// </summary>
		public Texture2D Small => small;
	}
}

#endif