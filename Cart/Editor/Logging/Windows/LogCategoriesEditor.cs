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

using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Logs.Editor.Windows
{
    public class LogCategoriesEditor : UtilityEditorWindow
    {
        [MenuItem("Tools/Carter Games/The Cart/[Logging] Category Window")]
        private static void OpenEditor()
        {
            Open<LogCategoriesEditor>("Log Category Statuses Window");
        }
        

        private void OnGUI()
        {
            SettingsProviderLogging.ScrollPos = EditorGUILayout.BeginScrollView(SettingsProviderLogging.ScrollPos);
            DrawUserDefinedCategories();
            DrawCartDefinedCategories();
            EditorGUILayout.EndScrollView();
        }


        private void DrawUserDefinedCategories()
        {
            GUILayout.Space(5f);
            EditorGUILayout.LabelField("Categories", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.BeginVertical();
            GUILayout.Space(2.5f);

            var lookup = AutoMakeDataAssetManager.GetDefine<DataAssetLogCategories>()
                .ObjectRef
                .Fp("categories")
                .Fpr("list");
            
            var cartCategories = AutoMakeDataAssetManager.GetDefine<DataAssetLogCategories>()
                .AssetRef.CartCategories;
            
            for (var i = 0; i < lookup.arraySize; i++)
            {
                if (cartCategories.Contains(lookup.GetIndex(i).Fpr("key").stringValue)) continue;
                
                var style = new GUIStyle("Box")
                {
                    normal =
                    {
                        background = TextureHelper.SolidColorTexture2D(1, 1, i % 2 == 1
                            ? new Color32(50, 50, 50, 0)
                            : new Color32(50, 50, 50, 255))
                    }
                };

                EditorGUILayout.BeginHorizontal(style);

                EditorGUILayout.LabelField(lookup.GetIndex(i).Fpr("key").stringValue.SplitAndGetLastElement('.')
                    .Replace("Logs", string.Empty)
                    .Replace("Log", string.Empty)
                    .Replace("Category", string.Empty));
                
                EditorGUI.BeginChangeCheck();
                lookup.GetIndex(i).Fpr("value").boolValue = EditorGUILayout.Toggle(lookup.GetIndex(i).Fpr("value").boolValue, GUILayout.Width(15));
                if (EditorGUI.EndChangeCheck())
                {
                    AutoMakeDataAssetManager.GetDefine<DataAssetLogCategories>().ObjectRef.ApplyModifiedProperties();
                    AutoMakeDataAssetManager.GetDefine<DataAssetLogCategories>().ObjectRef.Update();
                }
                
                EditorGUILayout.EndHorizontal();
            }
            
            GUILayout.Space(1f);
            EditorGUILayout.EndVertical();
        }


        private void DrawCartDefinedCategories()
        {
            EditorGUILayout.BeginVertical("Box");
            GUILayout.Space(1f);

            EditorGUILayout.LabelField("Cart Log Categories", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            var lookup = AutoMakeDataAssetManager.GetDefine<DataAssetLogCategories>()
                .ObjectRef
                .Fp("categories")
                .Fpr("list");

            var cartCategories = AutoMakeDataAssetManager.GetDefine<DataAssetLogCategories>()
                .AssetRef.CartCategories;

            
            for (var i = 0; i < lookup.arraySize; i++)
            {
                if (!cartCategories.Contains(lookup.GetIndex(i).Fpr("key").stringValue)) continue;
                
                var style = new GUIStyle("Box")
                {
                    normal =
                    {
                        background = TextureHelper.SolidColorTexture2D(1, 1, i % 2 == 1
                            ? new Color32(50, 50, 50, 0)
                            : new Color32(50, 50, 50, 255))
                    }
                };

                EditorGUILayout.BeginHorizontal(style);

                EditorGUILayout.LabelField(lookup.GetIndex(i).Fpr("key").stringValue.SplitAndGetLastElement('.')
                    .Replace("Logs", string.Empty)
                    .Replace("Log", string.Empty)
                    .Replace("Category", string.Empty));
                
                EditorGUI.BeginChangeCheck();
                lookup.GetIndex(i).Fpr("value").boolValue = EditorGUILayout.Toggle(lookup.GetIndex(i).Fpr("value").boolValue, GUILayout.Width(15));
                if (EditorGUI.EndChangeCheck())
                {
                    AutoMakeDataAssetManager.GetDefine<DataAssetLogCategories>().ObjectRef.ApplyModifiedProperties();
                    AutoMakeDataAssetManager.GetDefine<DataAssetLogCategories>().ObjectRef.Update();
                }
                
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(1f);
            EditorGUILayout.EndVertical();
        }
    }
}