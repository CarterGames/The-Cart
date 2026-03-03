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

namespace CarterGames.Cart.Crates.Panels.Editor
{
    [CustomEditor(typeof(PanelUnityEvents), true)]
    public class PanelUnityEventsEditor : PanelEditor
    {
        protected override bool HasExtra => true;


        protected override void DrawExtraContent()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);

            EditorGUILayout.LabelField("Unity Events", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();

            EditorGUI.indentLevel++;
            
            serializedObject.Fp("eventsExpanded").boolValue =
                EditorGUILayout.Foldout(serializedObject.Fp("eventsExpanded").boolValue, "Show events");
            
            EditorGUI.indentLevel--;
            
            if (serializedObject.Fp("eventsExpanded").boolValue)
            {
                EditorGUILayout.PropertyField(serializedObject.Fp("onPanelOpenStart"));
                EditorGUILayout.PropertyField(serializedObject.Fp("onPanelOpenComplete"));
                EditorGUILayout.PropertyField(serializedObject.Fp("onPanelCloseStart"));
                EditorGUILayout.PropertyField(serializedObject.Fp("onPanelCloseComplete"));
            }
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif