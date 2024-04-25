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
using CarterGames.Cart.Core.Logs;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.MetaData.Editor
{
    /// <summary>
    /// A manager class for all meta data used in the editor of the asset.
    /// </summary>
    public static class Meta
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static readonly string DataBasePath =
            $"{FileEditorUtil.AssetBasePath}/Carter Games/{FileEditorUtil.AssetName}/Code/Editor/Management/Meta Data/Data/";
        
        public const string SectionTitle = "sectionTitle";
        
        private static Dictionary<string, MetaData> dataCache;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// A lookup of all the meta data elements that have been accessed.
        /// </summary>
        private static Dictionary<string, MetaData> Lookup
        {
            get
            {
                if (!dataCache.IsEmptyOrNull()) return dataCache;
                dataCache = new Dictionary<string, MetaData>();
                return dataCache;
            }
        }
        
        
        // Implementations
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public static MetaData Asset => GetData("Asset");
        public static MetaData Logs => GetData("CartLogs");
        public static MetaData Rng => GetData("Rng");

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Updates the meta data when called, in-case of edits without any script changes.
        /// </summary>
        [MenuItem("Tools/Carter Games/The Cart/Editor/Meta Data/Force Update")]
        private static void ForceUpdate()
        {
            dataCache.Clear();
        }
        
        
        /// <summary>
        /// Gets the data for a specific system.
        /// </summary>
        /// <param name="key">The key to find.</param>
        /// <returns>The found data.</returns>
        public static MetaData GetData(string key)
        {
            if (Lookup.ContainsKey(key))
            {
                return Lookup[key];
            }
            
            var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(DataBasePath + key + ".json");

            if (asset == null)
            {
                CartLogger.LogError(typeof(Meta), $"Unable to find JSON for {key}");
                return null;
            }
            
            Lookup.Add(key, JsonUtility.FromJson<MetaData>(asset.text));
            return Lookup[key];
        }


        /// <summary>
        /// Gets the data for a specific system.
        /// </summary>
        /// <param name="path">The path to find the asset at.</param>
        /// <param name="key">The key to find.</param>
        /// <returns>The found data.</returns>
        public static MetaData GetData(string path, string key)
        {
            if (Lookup.ContainsKey(key))
            {
                return Lookup[key];
            }
            
            var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(path + key + ".json");

            if (asset == null)
            {
                CartLogger.LogError(typeof(Meta), $"Unable to find JSON for {key}");
                return null;
            }
            
            Lookup.Add(key, JsonUtility.FromJson<MetaData>(asset.text));
            return Lookup[key];
        }
    }
}