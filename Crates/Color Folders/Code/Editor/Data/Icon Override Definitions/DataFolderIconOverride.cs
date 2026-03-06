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
	/// Used to define folder icon overrides in the project.
	/// </summary>
	[Serializable]
	public sealed class DataFolderIconOverride
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		[SerializeField] private string folderPath;
		[SerializeField] private bool isRecursive;
		[SerializeField] private string folderSetId;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Constructors
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Makes a new override with the entered params.
		/// </summary>
		/// <param name="path">The path to apply to.</param>
		/// <param name="setKey">The icon set key to use.</param>
		/// <param name="isRecursive">If the override is recursive or not.</param>
		public DataFolderIconOverride(string path, string setKey, bool isRecursive = true)
		{
			folderPath = path;
			this.isRecursive = isRecursive;
			folderSetId = setKey;
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// The folder path to apply to.
		/// </summary>
		public string FolderPath => folderPath;


		/// <summary>
		/// If the path should apply recursively where no other override exists.
		/// </summary>
		public bool IsRecursive => isRecursive;


		/// <summary>
		/// The folder icon set to apply.
		/// </summary>
		public string FolderSetId => folderSetId;
	}
}

#endif