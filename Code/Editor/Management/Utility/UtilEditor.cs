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
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using CarterGames.Common.Management;
using CarterGames.Common.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Common.Editor
{
    public static class UtilEditor
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Paths
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        public const string SettingsWindowPath = "Project/Carter Games/Common";
        
        
        // Filters
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private const string BannerGraphicFilter = "LibraryBannerGraphic";
        
        
        // Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static Texture2D scarletRoseGraphicCache;
        private static Texture2D scarletBannerGraphicCache;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets if there is a settings asset in the project.
        /// </summary>
        public static bool HasInitialized
        {
            get
            {
                AssetIndexHandler.UpdateIndex();
                return ScriptableRef.HasAllAssets;
            }
        }
        
        
        /// <summary>
        /// Gets/Sets the save manager settings asset.
        /// </summary>
        public static CommonLibraryRuntimeSettings Settings => ScriptableRef.RuntimeSettings;


        /// <summary>
        /// Gets/Sets the save manager settings asset.
        /// </summary>
        public static CommonLibraryEditorSettings EditorSettings => ScriptableRef.EditorSettings;


        /// <summary>
        /// Gets/Sets the save manager settings asset.
        /// </summary>
        public static CommonLibraryAssetIndex AssetIndex => ScriptableRef.AssetIndex;



        /// <summary>
        /// Gets/Sets the save manager editor settings asset.
        /// </summary>
        public static SerializedObject SettingsObject => ScriptableRef.RuntimeSettingsObject;
        
        
        /// <summary>
        /// Gets/Sets the save manager editor settings asset.
        /// </summary>
        public static SerializedObject EditorSettingsObject => ScriptableRef.EditorSettingsObject;
        
        
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
            var editorSettings = EditorSettings;
            var dataAssetIndex = ScriptableRef.DataAssetIndex;

            AssetIndexHandler.UpdateIndex();
            EditorUtility.SetDirty(AssetIndex);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}