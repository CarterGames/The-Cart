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

using System.Collections.Generic;
using CarterGames.Cart.Core.Data;
using UnityEngine;

namespace CarterGames.Cart.Modules.ColourFolders.Editor
{
	/// <summary>
	/// Stores the overrides the user has applied to the project.
	/// </summary>
	public class DataAssetFolderIconOverrides : DataAsset
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		[SerializeField] private List<DataFolderIconOverride> folderOverrides = new List<DataFolderIconOverride>()
		{
			// Defaults from project structure used from library.
			new DataFolderIconOverride("Assets/_Project/Art", "Yellow"),
			new DataFolderIconOverride("Assets/_Project/Audio", "Orange"),
			new DataFolderIconOverride("Assets/_Project/Data", "Green"),
			new DataFolderIconOverride("Assets/_Project/Scenes", "Blue"),
			new DataFolderIconOverride("Assets/_Project/Code", "Red"),
			new DataFolderIconOverride("Assets/_Project/Prefabs", "Cyan"),
		};

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public List<DataFolderIconOverride> FolderOverrides => folderOverrides;
	}
}

#endif