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
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace CarterGames.Cart.Data.Editor
{
    /// <summary>
    /// Handles the setup of the asset index for runtime references to scriptable objects used for the asset.
    /// </summary>
    public sealed class DataAssetIndexHandler : IPreprocessBuildWithReport
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static readonly string AssetFilter = typeof(DataAsset).FullName;
        
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
        /// Updates the index with all the asset scriptable objects in the project.
        /// </summary>
        [MenuItem("Tools/Carter Games/The Cart/[Data] Update Data Asset Index", priority = 122)]
        public static void UpdateIndex()
        {
            var foundAssets = new List<DataAsset>();
            var asset = AssetDatabase.FindAssets($"t:{AssetFilter}", null);
            
            if (asset == null || asset.Length <= 0) return;

            foreach (var assetInstance in asset)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetInstance);
                var assetObj = (DataAsset) AssetDatabase.LoadAssetAtPath(assetPath, typeof(DataAsset));
                
                // Doesn't include editor only or the index itself.
                if (assetObj == null) continue;
                if (assetObj is DataAssetIndex) continue;
                if (assetObj is EditorOnlyDataAsset) continue;
                if (assetObj.ExcludeFromAssetIndex) continue;
                
                foundAssets.Add((DataAsset) AssetDatabase.LoadAssetAtPath(assetPath, typeof(DataAsset)));
            }

            foreach (var dataAsset in foundAssets)
            {
                if (dataAsset == null) continue;
                dataAsset.Initialize();
            }
            
            UpdateIndexReferences(foundAssets);
            
            AutoMakeDataAssetManager.GetDefine<DataAssetIndex>().ObjectRef.ApplyModifiedProperties();
            AutoMakeDataAssetManager.GetDefine<DataAssetIndex>().ObjectRef.Update();
        }


        /// <summary>
        /// Updates any references when called with the latest in the project. 
        /// </summary>
        /// <param name="foundAssets">The found assets to update.</param>
        private static void UpdateIndexReferences(IReadOnlyList<DataAsset> foundAssets)
        {
            var dicRef = AutoMakeDataAssetManager.GetDefine<DataAssetIndex>().ObjectRef.Fp("assets").Fpr("list");
            dicRef.ClearArray();
            
            for (var i = 0; i < foundAssets.Count; i++)
            {
                for (var j = 0; j < dicRef.arraySize; j++)
                {
                    var entry = dicRef.GetIndex(j);
                    
                    if (entry.Fpr("key").stringValue.Equals(foundAssets[i].GetType().ToString()))
                    {
                        for (var k = 0; k < entry.Fpr("value").arraySize; k++)
                        {
                            if (entry.Fpr("value").GetIndex(k).objectReferenceValue == foundAssets[i]) goto AlreadyExists;
                        }
                        
                        entry.Fpr("value").InsertIndex(entry.Fpr("value").arraySize);
                        entry.Fpr("value").GetIndex(entry.Fpr("value").arraySize - 1).objectReferenceValue = foundAssets[i];
                        goto AlreadyExists;
                    }
                }
                
                dicRef.InsertIndex(dicRef.arraySize);
                dicRef.GetIndex(dicRef.arraySize - 1).Fpr("key").stringValue = foundAssets[i].GetType().ToString();
                
                if (dicRef.GetIndex(dicRef.arraySize - 1).Fpr("value").arraySize > 0)
                {
                    dicRef.GetIndex(dicRef.arraySize - 1).Fpr("value").ClearArray();
                }
                
                dicRef.GetIndex(dicRef.arraySize - 1).Fpr("value").InsertIndex(0);
                dicRef.GetIndex(dicRef.arraySize - 1).Fpr("value").GetIndex(0).objectReferenceValue = foundAssets[i];

                AlreadyExists: ;
            }
        }
    }
}