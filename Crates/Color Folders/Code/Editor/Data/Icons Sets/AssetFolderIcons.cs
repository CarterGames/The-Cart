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
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Cart.Crates.ColourFolders.Editor
{
	/// <summary>
	/// A non-data asset scriptable object for asset folder sets as they are pre-defined.
	/// </summary>
	[Serializable]
	public sealed class AssetFolderIcons : ScriptableObject
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		[SerializeField] private List<DataFolderIconSet> folderGraphics;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Gets the graphic groups defined in the asset.
		/// </summary>
		public List<DataFolderIconSet> FolderGraphics => folderGraphics;
	}
}

#endif