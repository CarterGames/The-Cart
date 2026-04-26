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
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Logs.Editor.Windows
{
    public class LogCategoriesEditor : UtilityEditorWindow
    {
        private IReadOnlyDictionary<string, SerializedProperty> cacheCategoriesLookup;
        private List<string> cacheBuiltInCategories;


        private IReadOnlyDictionary<string, SerializedProperty> CategoriesLookup =>
            CacheRef.GetOrAssign(ref cacheCategoriesLookup, GetCategoriesLookup);
        
        private List<string> BuiltInCategories => CacheRef.GetOrAssign(ref cacheBuiltInCategories, AutoMakeDataAssetManager
            .GetDefine<DataAssetLogCategories>()
            .AssetRef.CartCategories);
        
            
        [MenuItem("Tools/Carter Games/The Cart/[Logging] Category Window", priority = 201)]
        private static void OpenEditor()
        {
            Open<LogCategoriesEditor>("Log Category Statuses");
        }
        

        private void OnGUI()
        {
            SettingsProviderLogging.ScrollPos = EditorGUILayout.BeginScrollView(SettingsProviderLogging.ScrollPos);
            DrawCategories(false);
            DrawCategories(true);
            EditorGUILayout.EndScrollView();
        }


        private IReadOnlyDictionary<string, SerializedProperty> GetCategoriesLookup()
        {
            var lookup = new Dictionary<string, SerializedProperty>();
            var data = AutoMakeDataAssetManager.GetDefine<DataAssetLogCategories>()
                .ObjectRef
                .Fp("categories")
                .Fpr("list");

            for (var i = 0; i < data.arraySize; i++)
            {
                var entry = data.GetIndex(i);
                lookup.Add(entry.Fpr("key").stringValue, entry);
            }
            
            return lookup;
        }


        private void DrawCategories(bool builtIn)
        {
            GUILayout.Space(5f);
            EditorGUILayout.LabelField((builtIn ? "Built-In" : "Categories"), EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.BeginVertical();
            GUILayout.Space(2.5f);

            var index = 0;

            foreach (var entry in CategoriesLookup)
            {
                if (builtIn)
                {
                    if (!BuiltInCategories.Contains(entry.Key)) continue;
                }
                else
                {
                    if (BuiltInCategories.Contains(entry.Key)) continue;
                }
                
                var style = new GUIStyle("Box")
                {
                    normal =
                    {
                        background = TextureHelper.SolidColorTexture2D(1, 1, index % 2 == 1
                            ? new Color32(50, 50, 50, 0)
                            : new Color32(50, 50, 50, 255))
                    }
                };

                EditorGUILayout.BeginHorizontal(style);

                EditorGUILayout.LabelField(entry.Key.SplitAndGetLastElement('.')
                    .Replace("Logs", string.Empty)
                    .Replace("Log", string.Empty)
                    .Replace("Category", string.Empty));
                
                CustomEditorStyling.DrawToggleStatusButton(entry.Value.Fpr("value"));
                
                EditorGUILayout.EndHorizontal();

                index++;
            }
            
            GUILayout.Space(1f);
            EditorGUILayout.EndVertical();
        }
    }
}