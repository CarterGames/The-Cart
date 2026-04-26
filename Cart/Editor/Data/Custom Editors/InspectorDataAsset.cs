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
    [CustomEditor(typeof(DataAsset), true)]
    public class InspectorDataAsset : CustomInspector
    {
        protected override string[] HideProperties => new string[]
        {
            "m_Script", "variantId", "excludeFromAssetIndex"
        };


        protected virtual bool ShowVariantIdOption => true;
        protected virtual bool ShowAssetIndexOptions => true;

        
        protected override void DrawInspectorGUI()
        {
            DrawDataAssetInspector();
            DrawBaseInspectorGUI();
        }


        public void DrawDataAssetInspector()
        {
            GUILayout.Space(1.5f);

            if (ShowVariantIdOption || ShowAssetIndexOptions)
            {
                EditorGUILayout.BeginVertical("HelpBox");
                GUILayout.Space(1.5f);
            }
            
            if (ShowVariantIdOption)
            {
                EditorGUILayout.PropertyField(serializedObject.Fp("variantId"));
            }

            if (ShowAssetIndexOptions)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Store in index");

                if (serializedObject.Fp("excludeFromAssetIndex").boolValue)
                {
                    CustomEditorStyling.CrossMiniButton(35);

                    GUI.backgroundColor = Color.yellow;

                    if (GUILayout.Button("Toggle"))
                    {
                        serializedObject.Fp("excludeFromAssetIndex").boolValue = false;
                    }
                }
                else
                {
                    CustomEditorStyling.TickMiniButton(35);

                    GUI.backgroundColor = Color.yellow;

                    if (GUILayout.Button("Toggle"))
                    {
                        serializedObject.Fp("excludeFromAssetIndex").boolValue = true;
                    }
                }

                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndHorizontal();
            }

            if (ShowVariantIdOption || ShowAssetIndexOptions)
            {
                GUILayout.Space(1.5f);
                EditorGUILayout.EndVertical();
            }
            
            GUILayout.Space(5f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            GUILayout.Space(5f);
        }
    }
}