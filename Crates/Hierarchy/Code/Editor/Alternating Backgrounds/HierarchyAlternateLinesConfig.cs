#if CARTERGAMES_CART_CRATE_HIERARCHYDECORATORS && UNITY_EDITOR

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