#if CARTERGAMES_CART_CRATE_DATAVALUES && UNITY_EDITOR

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

using System.Collections.Generic;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Logs;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace CarterGames.Cart.Crates.DataValues.Editor
{
    /// <summary>
    /// Handles the data value asset index.
    /// </summary>
	public sealed class DataValueIndexHandler : IPreprocessBuildWithReport
	{
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static readonly string AssetFilter = typeof(DataValueAsset).FullName;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IPreprocessBuildWithReport Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The order this script is processed in, in this case its the default.
        /// </summary>
        public int callbackOrder => 0;


        /// <summary>
        /// Runs before a build is executed.
        /// </summary>
        /// <param name="report">The report about the build (I don't need it, but its a param for the method).</param>
        public void OnPreprocessBuild(BuildReport report)
        {
            UpdateIndex();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Initializes the event subscription needed for this to work in editor.
        /// </summary>
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorApplication.update -= OnEditorUpdate;
            EditorApplication.update += OnEditorUpdate;
        }


        /// <summary>
        /// Runs when the editor has updated.
        /// </summary>
        private static void OnEditorUpdate()
        {
            // If the user is about to enter play-mode, update the index, otherwise leave it be. 
            if (!EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPlaying) return;
            UpdateIndex();
        }


        /// <summary>
        /// Updates the index with all the data asset scriptable objects in the project.
        /// </summary>
        [MenuItem("Tools/Carter Games/The Cart/[Data Values] Update Data Value Index", priority = 1302)]
        public static void UpdateIndex()
        {
            var foundAssets = new List<DataValueAsset>();
            var asset = AssetDatabase.FindAssets($"t:{AssetFilter}", null);
            
            if (asset == null || asset.Length <= 0) return;

            foreach (var assetInstance in asset)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetInstance);
                var assetObj = (DataValueAsset) AssetDatabase.LoadAssetAtPath(assetPath, typeof(DataValueAsset));
                
                // Doesn't include editor only or the index itself.
                if (assetObj == null) continue;
                if (string.IsNullOrEmpty(assetObj.Key)) continue;
                
                foundAssets.Add((DataValueAsset) AssetDatabase.LoadAssetAtPath(assetPath, typeof(DataValueAsset)));
            }
            
            UpdateIndexReferences(foundAssets);
            
            AutoMakeDataAssetManager.GetDefine<DataValueIndex>().ObjectRef.ApplyModifiedProperties();
            AutoMakeDataAssetManager.GetDefine<DataValueIndex>().ObjectRef.Update();
        }


        /// <summary>
        /// Updates any references when called with the latest in the project. 
        /// </summary>
        /// <param name="foundAssets">The found assets to update.</param>
        private static void UpdateIndexReferences(IReadOnlyList<DataValueAsset> foundAssets)
        {
            var dicRef = AutoMakeDataAssetManager.GetDefine<DataValueIndex>().ObjectRef.Fp("assets").Fpr("list");
            
            dicRef.ClearArray();
            
            for (var i = 0; i < foundAssets.Count; i++)
            {
                for (var j = 0; j < dicRef.arraySize; j++)
                {
                    var entry = dicRef.GetIndex(j);
                    
                    if (entry.Fpr("key").stringValue.Equals(foundAssets[i].Key))
                    {
                        CartLogger.LogWarning<LogCategoryDataValues>(
                            $"[Data Values]: Cannot assign {foundAssets[i].Key} as it already exists.",
                            typeof(DataValueIndexHandler));
                        
                        goto AlreadyExists;
                    }
                }
                
                dicRef.InsertIndex(dicRef.arraySize);
                dicRef.GetIndex(dicRef.arraySize - 1).Fpr("key").stringValue = foundAssets[i].Key;
                dicRef.GetIndex(dicRef.arraySize - 1).Fpr("value").objectReferenceValue = foundAssets[i];

                AlreadyExists: ;
            }
        }
    }
}

#endif