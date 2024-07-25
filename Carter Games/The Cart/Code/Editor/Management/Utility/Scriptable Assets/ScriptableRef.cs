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
using CarterGames.Cart.Modules;
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
        
        // Base Asset Paths
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        public static readonly string Root = $"Assets/Plugins/Carter Games/";
        public static readonly string PathResources = $"{AssetName}/Resources/";
        public static readonly string PathData = $"{AssetName}/Data/";

        public static readonly string FullPathResources = $"{Root}{PathResources}";
        public static readonly string FullPathData = $"{Root}{PathData}";
        
        
        // File Names
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static readonly string FileNameAssetIndex = $"[Cart] Data Asset Index.asset";
        private static readonly string FileNameSettingsAsset = $"[Cart] Runtime Settings Data Asset.asset";
        private static readonly string FileNameLogCategoriesAsset = $"[Cart] Log Category Statuses Data Asset.asset";
        private static readonly string FileNameCscAsset = $"[Cart] Csc Data Asset.asset";
        
        
        // Asset Paths
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static readonly string FullPathAssetIndex = $"{FullPathResources}{FileNameAssetIndex}";
        private static readonly string FullPathSettingsAsset = $"{FullPathData}{FileNameSettingsAsset}";
        private static readonly string FullPathLogCategoriesAsset = $"{FullPathData}{FileNameLogCategoriesAsset}";
        private static readonly string FullPathCscAsset = $"{FullPathData}{FileNameCscAsset}";
        
        
        // Asset Filters
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static readonly string FilterAssetIndex = $"t:{typeof(DataAssetIndex).FullName}";
        private static readonly string FilterRuntimeSettingsAsset = $"t:{typeof(DataAssetCartGlobalRuntimeSettings).FullName}";
        private static readonly string FilterLogCategoryAsset = $"t:{typeof(DataAssetCartLogCategories).FullName}";
        private static readonly string FilterCscAsset = $"t:{typeof(DataAssetCsc).FullName}";
        
        
        // Asset Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static DataAssetIndex cacheAssetIndex;
        private static DataAssetCartGlobalRuntimeSettings cacheSettingsAssetRuntime;
        private static DataAssetCartLogCategories cacheLogCategoriesAsset;
        private static DataAssetCsc cacheCscAsset;
        
        
        // SerializedObject Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static SerializedObject objectCacheAssetIndex;
        private static SerializedObject objectCacheSettingsAssetRuntime;
        private static SerializedObject objectCacheLogCategoriesAsset;
        private static SerializedObject objectCacheCscAsset;


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
        public static string AssetName => FileEditorUtil.AssetName;

        
        // Asset Properties
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The asset index for the asset.
        /// </summary>
        public static DataAssetIndex AssetIndex =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref cacheAssetIndex, FilterAssetIndex, FullPathAssetIndex, AssetName, $"{PathResources}{FileNameAssetIndex}");
        
        
        /// <summary>
        /// The runtime settings for the asset.
        /// </summary>
        public static DataAssetCartGlobalRuntimeSettings RuntimeSettings =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref cacheSettingsAssetRuntime, FilterRuntimeSettingsAsset, FullPathSettingsAsset, AssetName, $"{PathData}{FileNameSettingsAsset}");

        
        
        private static DataAssetCartLogCategories LogCategories =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref cacheLogCategoriesAsset, FilterLogCategoryAsset, FullPathLogCategoriesAsset, AssetName, $"{PathData}{FileNameLogCategoriesAsset}");
        
        
        public static DataAssetCsc CscAsset =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref cacheCscAsset, FilterCscAsset, FullPathCscAsset, AssetName, $"{PathData}{FileNameCscAsset}");

        
        // Object Properties
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The asset index.
        /// </summary>
        public static SerializedObject DataAssetIndexObject =>
            FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref objectCacheAssetIndex, AssetIndex);
        
        
        /// <summary>
        /// The runtime SerializedObject for the asset.
        /// </summary>
        public static SerializedObject RuntimeSettingsObject =>
            FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref objectCacheSettingsAssetRuntime, RuntimeSettings);


        /// <summary>
        /// The log categories asset.
        /// </summary>
        public static SerializedObject LogCategoriesObject =>
            FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref objectCacheLogCategoriesAsset, LogCategories);
        
        
        /// <summary>
        /// The csc asset.
        /// </summary>
        public static SerializedObject CscObject =>
            FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref objectCacheCscAsset, CscAsset);
        
        
        // Assets Initialized Check
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets if all the assets needed for the asset to function are in the project at the expected paths.
        /// </summary>
        public static bool HasAllAssets =>
            File.Exists(FullPathAssetIndex) &&
            File.Exists(FullPathSettingsAsset) &&
            File.Exists(FullPathLogCategoriesAsset) &&
            File.Exists(FullPathCscAsset);
        
        
        /// <summary>
        /// Tries to create any missing assets when called.
        /// </summary>
        public static void TryCreateAssets()
        {
            if (cacheAssetIndex == null)
            {
                FileEditorUtil.CreateSoGetOrAssignAssetCache(
                    ref cacheAssetIndex, 
                    FilterAssetIndex, 
                    FullPathAssetIndex,
                    AssetName, $"{PathResources}{FileNameAssetIndex}");
            }

            
            if (cacheSettingsAssetRuntime == null)
            {
                FileEditorUtil.CreateSoGetOrAssignAssetCache(
                    ref cacheSettingsAssetRuntime, 
                    FilterRuntimeSettingsAsset, 
                    FullPathSettingsAsset, 
                    AssetName, $"{PathData}{FileNameSettingsAsset}");
            }
            
            
            if (cacheLogCategoriesAsset == null)
            {
                FileEditorUtil.CreateSoGetOrAssignAssetCache(
                    ref cacheLogCategoriesAsset, 
                    FilterLogCategoryAsset, 
                    FullPathLogCategoriesAsset, 
                    AssetName, $"{PathData}{FileNameLogCategoriesAsset}");
            }
            
            
            if (cacheCscAsset == null)
            {
                FileEditorUtil.CreateSoGetOrAssignAssetCache(
                    ref cacheCscAsset, 
                    FilterCscAsset, 
                    FullPathCscAsset, 
                    AssetName, $"{PathData}{FileNameCscAsset}");
            }
        }
    }
}