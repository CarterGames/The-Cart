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

using Scarlet.Management;
using UnityEditor;
using UnityEngine;

namespace Scarlet.Editor.Utility
{
    public static class UtilEditor
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public const string SettingsWindowPath = "Project/Scarlet Library";
        private static readonly string SettingsAssetPath = $"{AssetBasePath}/Data/Runtime Settings.asset";
        private const string AssetIndexPath = "Assets/Resources/Scarlet Library/Asset Index.asset";
        
        
        private const string LibrarySettingsFilter = "t:scarletlibraryruntimesettings";
        private const string AssetIndexFilter = "t:assetindex";
        private const string ScarletBannerFilter = "ScarletBanner";
        
        
        private static ScarletLibraryRuntimeSettings settingsCache;
        private static SerializedObject settingsObjectCache;
        private static ScarletLibraryAssetIndex assetIndexCache;
        private static Texture2D scarletBannerGraphicCache;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the path where the asset code is located.
        /// </summary>
        private static string AssetBasePath
        {
            get
            {
                var script = AssetDatabase.FindAssets($"t:Script {nameof(UtilEditor)}")[0];
                var path = AssetDatabase.GUIDToAssetPath(script);

                path = path.Replace("Code/Editor/Management/Utility/UtilEditor.cs", "");
                return path;
            }
        }
        
        
        /// <summary>
        /// Gets/Sets the save manager settings asset.
        /// </summary>
        public static ScarletLibraryRuntimeSettings Settings
            => FileEditorUtil.CreateSoGetOrAssignCache(ref settingsCache, SettingsAssetPath, LibrarySettingsFilter);
        
        
        /// <summary>
        /// Gets/Sets the save manager settings asset.
        /// </summary>
        public static ScarletLibraryAssetIndex AssetIndex
            => FileEditorUtil.CreateSoGetOrAssignCache(ref assetIndexCache, AssetIndexPath, AssetIndexFilter);

        
        
        /// <summary>
        /// Gets/Sets the save manager editor settings asset.
        /// </summary>
        public static SerializedObject SettingsObject
        {
            get
            {
                if (settingsObjectCache != null) return settingsObjectCache;
                settingsObjectCache = new SerializedObject(Settings);
                return settingsObjectCache;
            }
        }


        /// <summary>
        /// The banner graphic for the settings provider.
        /// </summary>
        public static Texture2D ScarletBanner => FileEditorUtil.GetOrAssignCache(ref scarletBannerGraphicCache, ScarletBannerFilter);
        
        
        
        
        
        public static void Initialize()
        {
            AssetDatabase.Refresh();
            
            if (assetIndexCache == null)
            {
                assetIndexCache = AssetIndex;
            }
            
            if (settingsCache == null)
            {
                settingsCache = Settings;
            }

            AssetIndexHandler.UpdateIndex();
            EditorUtility.SetDirty(AssetIndex);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}