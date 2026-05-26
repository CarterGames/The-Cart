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

using CarterGames.Cart.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Hierarchy.Editor
{
    /// <summary>
    /// Handles the custom inspector for the hierarchy header settings script.
    /// </summary>
    [CustomEditor(typeof(HierarchyHeaderSettings))]
    public sealed class HierarchyHeaderSettingsEditor : UnityEditor.Editor
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Overrides
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public override void OnInspectorGUI()
        {
            GUILayout.Space(4f);
            GeneralUtilEditor.DrawMonoScriptSection((HierarchyHeaderSettings)target);
            GUILayout.Space(2f);
            
            DrawHeaderBox();
            GUILayout.Space(2f);
            DrawLabelBox();

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Draws the header settings box & its options.
        /// </summary>
        private void DrawHeaderBox()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Header", EditorStyles.boldLabel);
            GUILayout.Space(1f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            GUILayout.Space(1f);

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("backgroundColor")); 

            if (GUILayout.Button("R", GUILayout.Width("  R  ".GUIWidth())))
            {
                serializedObject.Fp("backgroundColor").colorValue = Color.gray;
            }
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(serializedObject.Fp("fullWidth"));
            
            if (EditorGUI.EndChangeCheck())
            {
                EditorApplication.RepaintHierarchyWindow();
            }
           
            GUILayout.Space(2f);
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the label settings box & its options.
        /// </summary>
        private void DrawLabelBox()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Label", EditorStyles.boldLabel);
            GUILayout.Space(1f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            GUILayout.Space(1f);
            
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("label"));
            EditorGUILayout.PropertyField(serializedObject.Fp("labelColor"));
            EditorGUILayout.PropertyField(serializedObject.Fp("boldLabel"));
            EditorGUILayout.PropertyField(serializedObject.Fp("textAlign"));
          
            if (EditorGUI.EndChangeCheck())
            {
                EditorApplication.RepaintHierarchyWindow();
            }
            
            GUILayout.Space(2f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif