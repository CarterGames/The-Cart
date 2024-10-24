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

using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Data.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Management.Editor
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
        /// Gets/Sets the save manager settings asset.
        /// </summary>
        public static DataAssetCoreRuntimeSettings Settings => ScriptableRef.GetAssetDef<DataAssetCoreRuntimeSettings>().AssetRef;


        /// <summary>
        /// Gets/Sets the save manager settings asset.
        /// </summary>
        public static DataAssetIndex AssetIndex => ScriptableRef.GetAssetDef<DataAssetIndex>().AssetRef;



        /// <summary>
        /// Gets/Sets the save manager editor settings asset.
        /// </summary>
        public static SerializedObject SettingsObject => ScriptableRef.GetAssetDef<DataAssetCoreRuntimeSettings>().ObjectRef;
        
        
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