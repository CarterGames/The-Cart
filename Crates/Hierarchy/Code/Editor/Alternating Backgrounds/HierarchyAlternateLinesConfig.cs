#if CARTERGAMES_CART_CRATE_HIERARCHY && UNITY_EDITOR

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

using System;
using CarterGames.Cart.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Hierarchy.Editor
{
    [Serializable]
    public sealed class HierarchyAlternateLinesConfig : IHierarchyConfig
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
#pragma warning disable 0414
        [SerializeField] private bool isEnabled = true;
#pragma warning restore
        [SerializeField] private bool fullWidth = false;
        [SerializeField] private bool startAlternate = false;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public string OptionLabel => "Alternate Colors";
        public bool FullWidth => fullWidth;
        public bool StartAlternate => startAlternate;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public void DrawConfig(SerializedProperty serializedProperty)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Alternate Colors", EditorStyles.boldLabel);

            var content = !serializedProperty.Fpr("isEnabled").boolValue
                ? new GUIContent("Enable", EditorGUIUtility.IconContent("Valid").image)
                : new GUIContent("Disable", EditorGUIUtility.IconContent("CrossIcon").image);

            var col = !serializedProperty.Fpr("isEnabled").boolValue
                ? Color.green
                : Color.red;

            GUI.backgroundColor = col;
            
            if (GUILayout.Button(content, GUILayout.Width(75), GUILayout.Height(20f)))
            {
                serializedProperty.Fpr("isEnabled").boolValue = !serializedProperty.Fpr("isEnabled").boolValue;
                EditorApplication.RepaintHierarchyWindow();
                
                serializedProperty.serializedObject.ApplyModifiedProperties();
                serializedProperty.serializedObject.Update();
            }
            
            GUI.backgroundColor = Color.white;
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(17.5f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.EndHorizontal();
            
            EditorGUI.BeginChangeCheck();
            
            EditorGUI.BeginDisabledGroup(!serializedProperty.Fpr("isEnabled").boolValue);
            
            EditorGUILayout.PropertyField(serializedProperty.Fpr("fullWidth"));
            EditorGUILayout.PropertyField(serializedProperty.Fpr("startAlternate"));

            if (EditorGUI.EndChangeCheck())
            {
                EditorApplication.RepaintHierarchyWindow();
                
                serializedProperty.serializedObject.ApplyModifiedProperties();
                serializedProperty.serializedObject.Update();
            }
            
            EditorGUI.EndDisabledGroup();
        }
    }
}

#endif