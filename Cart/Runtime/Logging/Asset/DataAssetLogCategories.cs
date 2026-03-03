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
using CarterGames.Cart.Data;
using CarterGames.Cart.Logs;
using UnityEngine;

namespace CarterGames.Cart
{
    /// <summary>
    /// A data asset for the cart log category states.
    /// </summary>
    [Serializable]
    public class DataAssetLogCategories : DataAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        [SerializeField] private SerializableDictionary<string, bool> categories;
        [SerializeField] private List<string> cartCategories;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// The categories stored in the asset.
        /// </summary>
        public SerializableDictionary<string, bool> Categories => categories;
        
        
        /// <summary>
        /// The categories stored in the asset that are cart defined.
        /// </summary>
        public List<string> CartCategories => cartCategories;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// Gets if the categories is enabled, if it exists. 
        /// </summary>
        /// <typeparam name="T">The type to check for.</typeparam>
        /// <returns></returns>
        public bool IsEnabled<T>() where T : LogCategory
        {
            var key = typeof(T).FullName;

            if (!categories.ContainsKey(key))
            {
                CartLogger.LogWarning<LogCategoryCart>($"Cannot find category of: {key}");
                return false;
            }
            
            return categories[key];
        }
    }
}