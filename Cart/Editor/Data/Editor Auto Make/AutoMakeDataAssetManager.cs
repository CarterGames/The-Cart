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

using System;
using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Data;
using CarterGames.Cart.Management;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    public static class AutoMakeDataAssetManager
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Base Asset Paths
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static readonly string Root = $"Assets/Plugins/Carter Games";
        private static readonly string PathResources = $"{AssetName}/Resources";
        private static readonly string PathData = $"{AssetName}/Data";
        
        public static readonly string FullPathResources = $"{Root}/{PathResources}";
        public static readonly string FullPathData = $"{Root}/{PathData}";
        
        // Assets Initialized Check
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        
        private static Dictionary<Type, IAutoMakeDataAssetDefine<DataAsset>> cacheLookup;

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

        private static Dictionary<Type, IAutoMakeDataAssetDefine<DataAsset>> AssetLookup
        {
            get
            {
                if (!cacheLookup.IsEmptyOrNull()) return cacheLookup;
                cacheLookup = new Dictionary<Type, IAutoMakeDataAssetDefine<DataAsset>>();
                
                foreach (var elly in AssemblyHelper.GetClassesOfType<IAutoMakeDataAssetDefine<DataAsset>>(false))
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


        public static IAutoMakeDataAssetDefine<T> GetDefine<T>() where T : DataAsset
        {
            if (AssetLookup.ContainsKey(typeof(T)))
            {
                return AssetLookup[typeof(T)] as IAutoMakeDataAssetDefine<T>;
            }

            return null;
        }


        public static bool HasAsset<T>(IAutoMakeDataAssetDefine<T> def) where T : DataAsset
        {
            return AssetDatabaseHelper.FileIsInProject<T>(def.CompleteDataAssetPath);
        }


        public static void TryCreateAsset<T>(IAutoMakeDataAssetDefine<T> def, ref T cache) where T : DataAsset
        {
            if (cache != null) return;
            GetOrCreateAsset<T>(def, ref cache);
        }
        
        
        public static T GetOrCreateAsset<T>(IAutoMakeDataAssetDefine<T> def, ref T cache) where T : DataAsset
        {
            return FileEditorUtil.CreateSoGetOrAssignAssetCache(
                ref cache, 
                def,
                AssetName, def.CompleteDataAssetPath);
        }


        public static SerializedObject GetOrCreateAssetObject<T>(IAutoMakeDataAssetDefine<T> def, ref SerializedObject objCache) where T : DataAsset
        {
            return FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref objCache, def.AssetRef);
        }
    }
}