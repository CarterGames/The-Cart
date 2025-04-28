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

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;

namespace CarterGames.Cart.Core.Logs.Editor
{
    /// <summary>
    /// Handles log category settings on editor reload.
    /// </summary>
    public sealed class LogCategoryHandler : IAssetEditorReload
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IAssetEditorReload Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Runs on editor reload after editor delay call.
        /// </summary>
        public void OnEditorReloaded()
        {
            UpdateCache();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets all the categories in the project currently.
        /// </summary>
        /// <returns>The found categories.</returns>
        private static IEnumerable<LogCategory> GetCategories()
        {
            return AssemblyHelper.GetClassesOfType<LogCategory>(false).OrderBy(t => t.GetType().FullName);
        }


        /// <summary>
        /// Updates the cache of categories and their settings.
        /// </summary>
        public static void UpdateCache()
        {
            var data = GetCategories();
            var states = SerializableDictionary<string, bool>.FromDictionary(LogCategoryStates.CategoryStates);

            foreach (var entry in data)
            {
                if (states.ContainsKey(entry.GetType().FullName)) continue;
                states.Add(entry.GetType().FullName, false);
            }

            LogCategoryStates.CategoryStates = states;
        }


        [MenuItem("Tools/Carter Games/The Cart/[Logging] Reset Categories")]
        private static void ResetCategories()
        {
            LogCategoryStates.CategoryStates = new SerializableDictionary<string, bool>();
        }
    }
}