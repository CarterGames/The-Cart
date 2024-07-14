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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.IO;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Logs;
using UnityEditor;

namespace CarterGames.Cart.Core.Management.Editor
{
    /// <summary>
    /// Handles references to scriptable objects in the asset that need generating without user input etc.
    /// </summary>
    public static class ScriptableRef
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Asset Paths
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static readonly string AssetIndexPath = $"{AssetBasePath}/Carter Games/{AssetName}/Resources/Data Asset Index.asset";
        private static readonly string SettingsAssetPath = $"{AssetBasePath}/Carter Games/{AssetName}/Data/Runtime Settings.asset";
        private static readonly string LogCategoriesAssetPath = $"{AssetBasePath}/Carter Games/{AssetName}/Data/Log Category Statuses.asset";

        // Asset Filters
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static readonly string RuntimeSettingsFilter = $"t:{typeof(DataAssetCartGlobalRuntimeSettings).FullName}";
        private static readonly string AssetIndexFilter = $"t:{typeof(DataAssetIndex).FullName}";
        private static readonly string LogFilter = $"t:{typeof(DataAssetCartLogCategories).FullName}";
        
        
        // Asset Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static DataAssetCartGlobalRuntimeSettings settingsAssetRuntimeCache;
        private static DataAssetIndex assetIndexCache;
        private static DataAssetCartLogCategories logCategoriesAssetCache;

        
        
        // SerializedObject Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static SerializedObject settingsAssetRuntimeObjectCache;
        private static SerializedObject settingsAssetEditorObjectCache;
        private static SerializedObject logCategoriesObjectCache;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Helper Properties
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the path where the asset code is located.
        /// </summary>
        internal static string AssetBasePath => FileEditorUtil.AssetBasePath;
        
        
        /// <summary>
        /// Gets the asset name stored in the file util editor class.
        /// </summary>
        private static string AssetName => FileEditorUtil.AssetName;

        
        // Asset Properties
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The asset index for the asset.
        /// </summary>
        public static DataAssetIndex AssetIndex =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref assetIndexCache, AssetIndexFilter, AssetIndexPath, AssetName, $"{AssetName}/Resources/Data Asset Index.asset");
        
        
        /// <summary>
        /// The runtime settings for the asset.
        /// </summary>
        public static DataAssetCartGlobalRuntimeSettings RuntimeSettings =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref settingsAssetRuntimeCache, RuntimeSettingsFilter, SettingsAssetPath, AssetName, $"{AssetName}/Data/Runtime Settings.asset");

        
        private static DataAssetCartLogCategories LogCategories =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref logCategoriesAssetCache, LogFilter, LogCategoriesAssetPath, AssetName, $"{AssetName}/Data/Log Category Statuses.asset");

        
        // Object Properties
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The runtime SerializedObject for the asset.
        /// </summary>
        public static SerializedObject RuntimeSettingsObject =>
            FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref settingsAssetRuntimeObjectCache, RuntimeSettings);


        /// <summary>
        /// The log categories asset.
        /// </summary>
        public static SerializedObject LogCategoriesObject =>
            FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref logCategoriesObjectCache, LogCategories);
        
        
        // Assets Initialized Check
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets if all the assets needed for the asset to function are in the project at the expected paths.
        /// </summary>
        public static bool HasAllAssets =>
            File.Exists(AssetIndexPath) && File.Exists(SettingsAssetPath);
        
        
        /// <summary>
        /// Tries to create any missing assets when called.
        /// </summary>
        public static void TryCreateAssets()
        {
            if (assetIndexCache == null)
            {
                FileEditorUtil.CreateSoGetOrAssignAssetCache(
                    ref assetIndexCache, 
                    AssetIndexFilter, 
                    AssetIndexPath,
                    AssetName, $"{AssetName}/Resources/Data Asset Index.asset");
            }

            
            if (settingsAssetRuntimeCache == null)
            {
                FileEditorUtil.CreateSoGetOrAssignAssetCache(
                    ref settingsAssetRuntimeCache, 
                    RuntimeSettingsFilter, 
                    SettingsAssetPath, 
                    AssetName, $"{AssetName}/Data/Runtime Settings.asset");
            }
        }
    }
}