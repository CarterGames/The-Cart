#if CARTERGAMES_CART_MODULE_COLORFOLDERS

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

namespace CarterGames.Cart.Modules.ColourFolders.Editor
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