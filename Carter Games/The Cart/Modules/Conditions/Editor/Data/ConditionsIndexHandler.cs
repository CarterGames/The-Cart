#if CARTERGAMES_CART_MODULE_CONDITIONS && UNITY_EDITOR

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

using System.Collections.Generic;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Logs;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace CarterGames.Cart.Modules.Conditions.Editor
{
	public class ConditionsIndexHandler : IPreprocessBuildWithReport
	{
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static readonly string AssetFilter = typeof(Condition).FullName;

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
        [MenuItem("Tools/Carter Games/The Cart/Modules/Conditions/Update Index", priority = 1201)]
        public static void UpdateIndex()
        {
            var foundAssets = new List<Condition>();
            var asset = AssetDatabase.FindAssets($"t:{AssetFilter}", null);
            
            if (asset == null || asset.Length <= 0) return;

            foreach (var assetInstance in asset)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetInstance);
                var assetObj = (Condition) AssetDatabase.LoadAssetAtPath(assetPath, typeof(Condition));
                
                // Doesn't include editor only or the index itself.
                if (assetObj == null) continue;
                if (string.IsNullOrEmpty(assetObj.VariantId)) continue;
                
                foundAssets.Add((Condition) AssetDatabase.LoadAssetAtPath(assetPath, typeof(Condition)));
            }
            
            UpdateIndexReferences(foundAssets);
            
            ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.ApplyModifiedProperties();
            ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.Update();
        }


        /// <summary>
        /// Updates any references when called with the latest in the project. 
        /// </summary>
        /// <param name="foundAssets">The found assets to update.</param>
        private static void UpdateIndexReferences(IReadOnlyList<Condition> foundAssets)
        {
            var dicRef = ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.Fp("assets").Fpr("list");
            
            dicRef.ClearArray();
            
            for (var i = 0; i < foundAssets.Count; i++)
            {
                for (var j = 0; j < dicRef.arraySize; j++)
                {
                    var entry = dicRef.GetIndex(j);
                    
                    if (entry.Fpr("key").stringValue.Equals(foundAssets[i].VariantId))
                    {
                        CartLogger.LogWarning<LogCategoryModules>(
                            $"[Conditions]: Cannot assign {foundAssets[i].VariantId} as it already exists.",
                            typeof(ConditionsIndexHandler));
                        
                        goto AlreadyExists;
                    }
                }
                
                dicRef.InsertIndex(dicRef.arraySize);
                dicRef.GetIndex(dicRef.arraySize - 1).Fpr("key").stringValue = foundAssets[i].VariantId;
                dicRef.GetIndex(dicRef.arraySize - 1).Fpr("value").objectReferenceValue = foundAssets[i];

                AlreadyExists: ;
            }
        }
	}
}

#endif