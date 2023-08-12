/*
 * Copyright (c) 2018-Present Carter Games
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
using CarterGames.Common.Data;
using CarterGames.Common.Editor;
using UnityEditor;

namespace CarterGames.Common.Management.Editor
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
        private static readonly string AssetIndexPath = $"{AssetBasePath}/Carter Games/{AssetName}/Resources/Asset Index.asset";
        private static readonly string SettingsAssetPath = $"{AssetBasePath}/Carter Games/{AssetName}/Data/Runtime Settings.asset";
        private static readonly string EditorSettingsAssetPath = $"{AssetBasePath}/Carter Games/{AssetName}/Data/Editor Settings.asset";
        private static readonly string DataAssetIndexPath = $"{AssetBasePath}/Carter Games/{AssetName}/Data/Data Asset Index.asset";
        

        // Asset Filters
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static readonly string RuntimeSettingsFilter = $"t:{typeof(CommonLibraryRuntimeSettings).FullName}";
        private static readonly string EditorSettingsFilter = $"t:{typeof(CommonLibraryEditorSettings).FullName}";
        private static readonly string AssetIndexFilter = $"t:{typeof(CommonLibraryAssetIndex).FullName}";
        private static readonly string DataAssetIndexFilter = $"t:{typeof(DataAssetIndex).FullName}";
        
        
        // Asset Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static CommonLibraryRuntimeSettings settingsAssetRuntimeCache;
        private static CommonLibraryEditorSettings settingsAssetEditorCache;
        private static CommonLibraryAssetIndex assetIndexCache;
        private static DataAssetIndex dataAssetIndexCache;
        
        
        // SerializedObject Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static SerializedObject settingsAssetRuntimeObjectCache;
        private static SerializedObject settingsAssetEditorObjectCache;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Helper Properties
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the path where the asset code is located.
        /// </summary>
        private static string AssetBasePath => FileEditorUtil.AssetBasePath;
        
        
        /// <summary>
        /// Gets the asset name stored in the file util editor class.
        /// </summary>
        private static string AssetName => FileEditorUtil.AssetName;

        
        // Asset Properties
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The asset index for the asset.
        /// </summary>
        public static CommonLibraryAssetIndex AssetIndex =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref assetIndexCache, AssetIndexFilter, AssetIndexPath, AssetName, $"{AssetName}/Resources/Asset Index.asset");
        
        
        /// <summary>
        /// The editor settings for the asset.
        /// </summary>
        public static CommonLibraryEditorSettings EditorSettings =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref settingsAssetEditorCache, EditorSettingsFilter, EditorSettingsAssetPath, AssetName, $"{AssetName}/Data/Editor Settings.asset");
        
        
        /// <summary>
        /// The runtime settings for the asset.
        /// </summary>
        public static CommonLibraryRuntimeSettings RuntimeSettings =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref settingsAssetRuntimeCache, RuntimeSettingsFilter, SettingsAssetPath, AssetName, $"{AssetName}/Data/Runtime Settings.asset");
                
        
        /// <summary>
        /// The data asset index for the asset.
        /// </summary>
        public static DataAssetIndex DataAssetIndex =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref dataAssetIndexCache, DataAssetIndexFilter, DataAssetIndexPath, AssetName, $"{AssetName}/Data/Data Asset Index.asset");
        
        // Object Properties
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The editor SerializedObject for the asset.
        /// </summary>
        public static SerializedObject EditorSettingsObject =>
            FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref settingsAssetEditorObjectCache, EditorSettings);
        
        
        /// <summary>
        /// The runtime SerializedObject for the asset.
        /// </summary>
        public static SerializedObject RuntimeSettingsObject =>
            FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref settingsAssetRuntimeObjectCache, RuntimeSettings);
        
        // Assets Initialized Check
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets if all the assets needed for the asset to function are in the project at the expected paths.
        /// </summary>
        public static bool HasAllAssets =>
            File.Exists(AssetIndexPath) && File.Exists(SettingsAssetPath) &&
            File.Exists(EditorSettingsAssetPath) && File.Exists(DataAssetIndexPath);
    }
}