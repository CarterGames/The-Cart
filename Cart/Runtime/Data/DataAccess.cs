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
using System.Linq;
using UnityEngine;

namespace CarterGames.Cart.Data
{
    /// <summary>
    /// Helper class to access data assets at runtime.
    /// </summary>
    public static class DataAccess
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // A cache of all the assets found...
        private static DataAssetIndex indexCache;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets all the assets from the library...
        /// </summary>
        private static DataAssetIndex Index
        {
            get
            {
                if (indexCache != null) return indexCache;
                indexCache = Resources.Load<DataAssetIndex>("[Cart] Data Asset Index");
                return indexCache;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the Data Asset requested.
        /// </summary>
        /// <typeparam name="T">The asset to get.</typeparam>
        /// <returns>The asset if it exists.</returns>
        public static T GetAsset<T>() where T : DataAsset
        {
            if (Index.Lookup.ContainsKey(typeof(T).ToString()))
            {
                return (T)Index.Lookup[typeof(T).ToString()][0];
            }

            return null;
        }
        
        
        /// <summary>
        /// Gets the Data Asset requested.
        /// </summary>
        /// <typeparam name="T">The asset to get.</typeparam>
        /// <returns>The asset if it exists.</returns>
        public static T GetAsset<T>(string id) where T : DataAsset
        {
            if (Index.Lookup.ContainsKey(typeof(T).ToString()))
            {
                return (T)Index.Lookup[typeof(T).ToString()].FirstOrDefault(t => t.VariantId.Equals(id));
            }

            return null;
        }
        
        
        /// <summary>
        /// Gets the Data Asset requested.
        /// </summary>
        /// <typeparam name="T">The asset to get.</typeparam>
        /// <returns>The asset if it exists.</returns>
        public static List<T> GetAssets<T>() where T : DataAsset
        {
            if (Index.Lookup.ContainsKey(typeof(T).ToString()))
            {
                return Index.Lookup[typeof(T).ToString()].Cast<T>().ToList();
            }

            return null;
        }

        
        /// <summary>
        /// Gets all the data assets stored in the index.
        /// </summary>
        /// <returns>All the assets stored.</returns>
        public static List<DataAsset> GetAllAssets()
        {
            var list = new List<DataAsset>();
            
            foreach (var entry in Index.Lookup.Values)
            {
                list.AddRange(entry);
            }

            return list;
        }
    }
}