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

using System.Collections.Generic;
using CarterGames.Cart;
using UnityEditor;

namespace CarterGames.Cart.Crates.ColourFolders.Editor
{
	/// <summary>
	/// Holds a cache for the icon sets to apply.
	/// </summary>
	public static class ColorFolderCache
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private static Dictionary<string, DataFolderIconSet> setsLookupCache = new Dictionary<string, DataFolderIconSet>();
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


		public static Dictionary<string, DataFolderIconSet> FolderResult
		{
			get => folderResultCache;
			set => folderResultCache = value;
		}


		public static void AddFolderResult(string path, DataFolderIconSet set)
		{
			if (folderResultCache.ContainsKey(path))
			{
				folderResultCache[path] = set;
				return;
			}
			
			folderResultCache.Add(path, set);
		}


		public static void TryAddEntry(string folderPath, string folderColorId)
		{
			if (FolderResult.ContainsKey(folderPath))
			{
				FolderResult[folderPath] = SetsLookup[folderColorId];
			}
			else
			{
				if (string.IsNullOrEmpty(folderColorId))
				{
					FolderResult.Add(folderPath, null);
					return;
				}
				
				FolderResult.Add(folderPath, SetsLookup[folderColorId]);
			}
		}


		public static void ValidateCache()
		{
				var toCheck = new Dictionary<string, DataFolderIconSet>(FolderResult);

				foreach (var cacheEntry in toCheck)
				{
					if (AssetDatabase.IsValidFolder(cacheEntry.Key)) continue;
					FolderResult.Remove(cacheEntry.Key);
				}
		}
	}
}

#endif