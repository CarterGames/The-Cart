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
using CarterGames.Cart.Core;

namespace CarterGames.Cart.Modules.ColourFolders.Editor
{
	/// <summary>
	/// Holds a cache for the icon sets to apply.
	/// </summary>
	public static class ColorFolderCache
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static Dictionary<string, DataFolderIconSet> setsLookupCache;
		private static Dictionary<string, DataFolderIconSet> folderResultCache = new Dictionary<string, DataFolderIconSet>();

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// A lookup of all the icon sets defined since last reload.
		/// </summary>
		public static Dictionary<string, DataFolderIconSet> SetsLookup
		{
			get
			{
				if (!setsLookupCache.IsEmptyOrNull()) return setsLookupCache;
				setsLookupCache = new Dictionary<string, DataFolderIconSet>();

				var data = ColourFolderManager.folderIconsAsset;

				foreach (var set in data.FolderGraphics)
				{
					setsLookupCache.Add(set.Id, set);
				}

				return setsLookupCache;
			}
		}
		
		
		public static Dictionary<string, DataFolderIconSet> FolderResult => folderResultCache;


		public static void AddFolderResult(string path, DataFolderIconSet set)
		{
			if (folderResultCache.ContainsKey(path))
			{
				folderResultCache[path] = set;
				return;
			}
			
			folderResultCache.Add(path, set);
		}
	}
}

#endif