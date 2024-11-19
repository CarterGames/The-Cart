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
using System.Linq;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Modules;

namespace CarterGames.Cart.Core.Logs.Editor
{
    /// <summary>
    /// Handles log category settings on editor reload.
    /// </summary>
    public sealed class LogCategoryHandler : IAssetEditorReload
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The default categories for the setup.
        /// </summary>
        private static readonly List<string> DefaultEnabled = new List<string>()
        {
            typeof(LogCategoryCore).FullName,
            typeof(LogCategoryModules).FullName,
            "CarterGames.Cart.Modules.NotionData.LogCategoryNotionData",
            "CarterGames.Cart.Modules.Panels.LogCategoryPanels",
        };


        /// <summary>
        /// The last data that was set, used as a cache before a reload.
        /// </summary>
        private static Dictionary<string, bool> oldData = new Dictionary<string, bool>();
        
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
        private static IEnumerable<CartLogCategory> GetCategories()
        {
            return AssemblyHelper.GetClassesOfType<CartLogCategory>(false).OrderBy(t => t.GetType().Name);
        }


        /// <summary>
        /// Updates the cache of categories and their settings.
        /// </summary>
        private void UpdateCache()
        {
            var data = GetCategories();

            oldData = new Dictionary<string, bool>();

            for (var i = 0; i < ScriptableRef.GetAssetDef<DataAssetCartLogCategories>().ObjectRef.Fp("lookup").Fpr("list").arraySize; i++)
            {
                oldData.Add(
                    ScriptableRef.GetAssetDef<DataAssetCartLogCategories>().ObjectRef.Fp("lookup").Fpr("list").GetIndex(i).Fpr("key").stringValue,
                    ScriptableRef.GetAssetDef<DataAssetCartLogCategories>().ObjectRef.Fp("lookup").Fpr("list").GetIndex(i).Fpr("value").boolValue);
            }
            
            var lookup = ScriptableRef.GetAssetDef<DataAssetCartLogCategories>().ObjectRef.Fp("lookup").Fpr("list");
            
            lookup.ClearArray();

            foreach (var category in data)
            {
                lookup.InsertIndex(lookup.arraySize);

                var newEntry = lookup.GetIndex(lookup.arraySize - 1);

                newEntry.Fpr("key").stringValue = category.GetType().FullName;

                var wasSet = false;
                var toSetAs = false;

                foreach (var oldDataEntry in oldData)
                {
                    if (oldDataEntry.Key != category.GetType().FullName) continue;
                    toSetAs = oldDataEntry.Value;
                    wasSet = true;
                    break;
                }
                
                if (!wasSet)
                {
                    if (DefaultEnabled.Contains(category.GetType().FullName))
                    {
                        toSetAs = true;
                    }
                }


                newEntry.Fpr("value").boolValue = toSetAs;
            }

            ScriptableRef.GetAssetDef<DataAssetCartLogCategories>().ObjectRef.ApplyModifiedProperties();
            ScriptableRef.GetAssetDef<DataAssetCartLogCategories>().ObjectRef.Update();
        }
    }
}