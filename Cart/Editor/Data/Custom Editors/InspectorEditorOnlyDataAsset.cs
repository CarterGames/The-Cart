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

namespace CarterGames.Cart.Data.Editor
{
    [CustomEditor(typeof(EditorOnlyDataAsset), true)]
    public class InspectorEditorOnlyDataAsset : CustomInspector
    {
        protected override string[] HideProperties => new string[]
        {
            "m_Script", "variantId", "excludeFromAssetIndex"
        };

        
        protected override void DrawInspectorGUI()
        {
            GUILayout.Space(1.5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            EditorGUILayout.PropertyField(serializedObject.Fp("variantId"));

            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            
            GUILayout.Space(5f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            GUILayout.Space(5f);

            DrawBaseInspectorGUI();
        }
    }
}