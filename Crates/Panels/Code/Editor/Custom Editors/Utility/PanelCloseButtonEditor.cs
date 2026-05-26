#if CARTERGAMES_CART_CRATE_PANELS && UNITY_EDITOR

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
using UnityEngine.UI;

namespace CarterGames.Cart.Crates.Panels.Editor
{
    [CustomEditor(typeof(PanelCloseButton), true)]
    public class PanelCloseButtonEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(2.5f);
            GeneralUtilEditor.DrawMonoScriptSection((PanelCloseButton) target);
            
            EditorGUILayout.Space(2.5f);
            DrawSettings();


            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }


        private void DrawSettings()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);
            

            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField("References", EditorStyles.boldLabel);

            GUI.backgroundColor = Color.yellow;
            
            if (GUILayout.Button("Try Get References", GUILayout.Width(140)))
            {
                serializedObject.Fp("panel").objectReferenceValue ??= ((PanelCloseButton)target).GetComponentInParent<Panel>();
                serializedObject.Fp("button").objectReferenceValue ??= ((PanelCloseButton)target).GetComponentInChildren<Button>();

                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }
            
            GUI.backgroundColor = Color.white;
            
            EditorGUILayout.EndHorizontal();
            
            
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("panel"));
            EditorGUILayout.PropertyField(serializedObject.Fp("button"));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif