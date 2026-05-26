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

namespace CarterGames.Cart.Crates.Panels.Editor
{
    [CustomEditor(typeof(PanelTransitionCanvasGroup))]
    public class PanelTransitionCanvasGroupEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(2.5f);
            GeneralUtilEditor.DrawMonoScriptSection((PanelTransitionCanvasGroup) target);
            
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
            
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

            var isValid = IsValidSetup();

            GUI.backgroundColor = isValid ? Color.green : Color.red;
            GUILayout.Label(isValid ? GeneralUtilEditor.TickIcon : GeneralUtilEditor.CrossIcon, new GUIStyle("minibutton"), GUILayout.Width(25));
            GUI.backgroundColor = Color.white;
            
            EditorGUILayout.EndHorizontal();
            
            
            GeneralUtilEditor.DrawHorizontalGUILine();
            

            EditorGUILayout.PropertyField(serializedObject.Fp("useUnscaledTime"));
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.PropertyField(serializedObject.Fp("canvasGroup"));
            EditorGUILayout.PropertyField(serializedObject.Fp("fadeSpeed"));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        private bool IsValidSetup()
        {
            return serializedObject.Fp("canvasGroup").objectReferenceValue != null &&
                   serializedObject.Fp("fadeSpeed").floatValue > 0f;
        }
    }
}

#endif