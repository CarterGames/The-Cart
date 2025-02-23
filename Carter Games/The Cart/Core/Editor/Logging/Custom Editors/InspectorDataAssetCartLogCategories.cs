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

namespace CarterGames.Cart.Core.Logs.Editor
{
    /// <summary>
    /// Handles the custom editor of the log categories data asset.
    /// </summary>
    [CustomEditor(typeof(DataAssetCartLogCategories))]
    public sealed class InspectorDataAssetCartLogCategories : CustomInspector
    {
        protected override string[] HideProperties { get; }

        
        protected override void DrawInspectorGUI()
        {
            EditorGUILayout.Space(2.5f);

            DrawVariantId();
            
            EditorGUILayout.Space(2.5f);
            
            DrawCategories();
        }


        private void DrawVariantId()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);

            EditorGUILayout.LabelField("Data Asset", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("variantId"));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
        
        

        private void DrawCategories()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Categories", EditorStyles.boldLabel);
            
            GUI.backgroundColor = Color.yellow;
            
            if (GUILayout.Button("Edit", GUILayout.Width(65)))
            {
                LibrarySettingsProvider.OpenSettings();
                SettingsProviderLogging.ExpandSection(false, true);
            }
            
            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndHorizontal();
            
            
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            
            for (var i = 0; i < serializedObject.Fp("lookup").Fpr("list").arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginDisabledGroup(true);
                
                EditorGUILayout.LabelField(serializedObject.Fp("lookup").Fpr("list").GetIndex(i).Fpr("key").stringValue);
                EditorGUILayout.Toggle(serializedObject.Fp("lookup").Fpr("list").GetIndex(i).Fpr("value").boolValue, GUILayout.Width(15));
                
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndHorizontal();
            }
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}