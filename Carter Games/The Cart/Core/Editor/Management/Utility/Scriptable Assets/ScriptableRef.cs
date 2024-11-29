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

using System;
using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Modules.Settings;
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
        public static readonly string Root = $"Assets/Plugins/Carter Games";
        public static readonly string PathResources = $"{AssetName}/Resources";
        public static readonly string PathData = $"{AssetName}/Data";

        public static readonly string FullPathResources = $"{Root}/{PathResources}";
        public static readonly string FullPathData = $"{Root}/{PathData}";


        // Assets Initialized Check
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */


        private static Dictionary<Type, IScriptableAssetDef<DataAsset>> cacheLookup;

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

        private static Dictionary<Type, IScriptableAssetDef<DataAsset>> AssetLookup
        {
            get
            {
                if (!cacheLookup.IsEmptyOrNull()) return cacheLookup;
                cacheLookup = new Dictionary<Type, IScriptableAssetDef<DataAsset>>();

                foreach (var elly in AssemblyHelper.GetClassesOfType<IScriptableAssetDef<DataAsset>>())
                {
                    cacheLookup.Add(elly.AssetType, elly);
                }
                
                return cacheLookup;
            }   
        }


        /// <summary>
        /// Gets if all the assets needed for the asset to function are in the project at the expected paths.
        /// </summary>
        public static bool HasAllAssets()
        {
            return AssetLookup.All(t => HasAsset(t.Value));
        }


        /// <summary>
        /// Tries to create any missing assets when called.
        /// </summary>
        public static void TryCreateAssets()
        {
            foreach (var entry in AssetLookup)
            {
                entry.Value.TryCreate();
            }
        }


        public static IScriptableAssetDef<T> GetAssetDef<T>() where T : DataAsset
        {
            if (AssetLookup.ContainsKey(typeof(T)))
            {
                return (IScriptableAssetDef<T>) AssetLookup[typeof(T)];
            }

            return null;
        }


        public static bool HasAsset<T>(IScriptableAssetDef<T> def) where T : DataAsset
        {
            return AssetDatabaseHelper.FileIsInProject<T>(def.DataAssetPath);
        }


        public static void TryCreateAsset<T>(IScriptableAssetDef<T> def, ref T cache) where T : DataAsset
        {
            if (cache != null) return;
            GetOrCreateAsset<T>(def, ref cache);
        }


        public static T GetOrCreateAsset<T>(IScriptableAssetDef<T> def, ref T cache) where T : DataAsset
        {
            return FileEditorUtil.CreateSoGetOrAssignAssetCache(
                ref cache, 
                def,
                AssetName, $"{PathData}{def.DataAssetFileName}");
        }


        public static SerializedObject GetOrCreateAssetObject<T>(IScriptableAssetDef<T> def, ref SerializedObject objCache) where T : DataAsset
        {
            return FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref objCache, def.AssetRef);
        }
    }
}