/*
 * Copyright (c) 2025 Carter Games
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

using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Logs.Editor.Windows
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

            var lookup = ScriptableRef.GetAssetDef<DataAssetLogCategories>()
                .ObjectRef
                .Fp("categories")
                .Fpr("list");
            
            var cartCategories = ScriptableRef.GetAssetDef<DataAssetLogCategories>()
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
                    ScriptableRef.GetAssetDef<DataAssetLogCategories>().ObjectRef.ApplyModifiedProperties();
                    ScriptableRef.GetAssetDef<DataAssetLogCategories>().ObjectRef.Update();
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
            
            var lookup = ScriptableRef.GetAssetDef<DataAssetLogCategories>()
                .ObjectRef
                .Fp("categories")
                .Fpr("list");

            var cartCategories = ScriptableRef.GetAssetDef<DataAssetLogCategories>()
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
                    ScriptableRef.GetAssetDef<DataAssetLogCategories>().ObjectRef.ApplyModifiedProperties();
                    ScriptableRef.GetAssetDef<DataAssetLogCategories>().ObjectRef.Update();
                }
                
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(1f);
            EditorGUILayout.EndVertical();
        }
    }
}