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

using CarterGames.Cart.Data;
using CarterGames.Cart.Data.Editor;
using CarterGames.Cart.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Management.Editor
{
    public static class UtilEditor
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Paths
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        public const string SettingsWindowPath = "Carter Games/The Cart";
        
        
        // Filters
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private const string BannerGraphicFilter = "T_TheCart_Logo";
        
        
        // Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static Texture2D scarletRoseGraphicCache;
        private static Texture2D scarletBannerGraphicCache;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets/Sets the settings asset.
        /// </summary>
        public static DataAssetCoreRuntimeSettings Settings => AutoMakeDataAssetManager.GetDefine<DataAssetCoreRuntimeSettings>().AssetRef;


        /// <summary>
        /// Gets/Sets the asset index.
        /// </summary>
        public static DataAssetIndex AssetIndex => AutoMakeDataAssetManager.GetDefine<DataAssetIndex>().AssetRef;



        /// <summary>
        /// Gets/Sets the settings asset.
        /// </summary>
        public static SerializedObject SettingsObject => AutoMakeDataAssetManager.GetDefine<DataAssetCoreRuntimeSettings>().ObjectRef;
        
        
        /// <summary>
        /// The banner graphic for the settings provider.
        /// </summary>
        public static Texture2D BannerGraphic => FileEditorUtil.GetOrAssignCache(ref scarletBannerGraphicCache, BannerGraphicFilter);
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Initializes the library.
        /// </summary>
        public static void Initialize()
        {
            AssetDatabase.Refresh();

            var index = AssetIndex;
            var runtimeSettings = Settings;

            DataAssetIndexHandler.UpdateIndex();
            EditorUtility.SetDirty(AssetIndex);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}