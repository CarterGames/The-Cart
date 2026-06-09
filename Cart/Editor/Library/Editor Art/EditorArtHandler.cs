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
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Management.Editor
{
    /// <summary>
    /// Handles the art for the editor setup. Without needing exact references.
    /// </summary>
    public static class EditorArtHandler
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static string[] AllPaths = Array.Empty<string>();
        private static readonly Dictionary<string, string> CachePathsLookup = new Dictionary<string, string>();
        private static readonly Dictionary<string, Texture2D> CacheLookup = new Dictionary<string, Texture2D>();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static string GetPathToAsset(string assetEndPath)
        {
            if (CachePathsLookup.ContainsKey(assetEndPath))
            {
                return CachePathsLookup[assetEndPath];
            }
            
            AllPaths = AssetDatabase.GetAllAssetPaths();
            return AllPaths.FirstOrDefault(t => t.EndsWith(assetEndPath));
        }


        /// <summary>
        /// Gets an art icon from its constant id.
        /// </summary>
        /// <param name="assetEndPath">The end of the path to the asset.</param>
        /// <returns>Texture2D</returns>
        public static Texture2D GetIcon(string assetEndPath)
        {
            if (CacheLookup.ContainsKey(assetEndPath))
            {
                return CacheLookup[assetEndPath];
            }
            
            CachePathsLookup.Add(assetEndPath, GetPathToAsset(assetEndPath));
            CacheLookup.Add(assetEndPath, AssetDatabase.LoadAssetAtPath<Texture2D>(CachePathsLookup[assetEndPath]));
            return CacheLookup[assetEndPath];
        }
    }
}