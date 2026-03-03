#if CARTERGAMES_CART_CRATE_HIERARCHY && UNITY_EDITOR

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
    [CustomEditor(typeof(HierarchySeparatorSettings))]
    public sealed class HierarchySeparatorSettingsEditor : UnityEditor.Editor
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Overrides
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public override void OnInspectorGUI()
        {
            GUILayout.Space(4f);
            GeneralUtilEditor.DrawMonoScriptSection((HierarchySeparatorSettings)target);
            GUILayout.Space(2f);
            
            DrawHeaderBox();

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
            EditorGUILayout.LabelField("Separator", EditorStyles.boldLabel);
            GUILayout.Space(1f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            GUILayout.Space(1f);

            EditorGUILayout.BeginHorizontal();
            
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("backgroundColor")); 

            if (GUILayout.Button("R", GUILayout.Width(25)))
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
    }
}

#endif