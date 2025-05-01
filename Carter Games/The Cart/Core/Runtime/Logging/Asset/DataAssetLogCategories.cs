/*
 * Copyright (c) 2025 Carter Games
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
using System.Collections.Generic;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Logs;
using UnityEngine;

namespace CarterGames.Cart.Core
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
                Debug.LogWarning($"Cannot find category of: {key}");
                return false;
            }
            
            return categories[key];
        }
    }
}