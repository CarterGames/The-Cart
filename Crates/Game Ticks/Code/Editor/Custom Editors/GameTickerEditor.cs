#if CARTERGAMES_CART_CRATE_GAMETICKER && UNITY_EDITOR

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

namespace CarterGames.Cart.Crates.GameTicks.Editor
{
    [CustomEditor(typeof(GameTicker))]
    public class GameTickerEditor : CustomInspector
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Custom editor
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected override string[] HideProperties { get; }
        

        protected override void DrawInspectorGUI()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1.5f);
            
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.Fp("syncState"), new GUIContent("Sync state", "Defines the sync state for the ticker."));
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }
            
            
            if (serializedObject.Fp("syncState").intValue == 0)
            {
                EditorGUILayout.PropertyField(serializedObject.Fp("ticksPerSecond"), new GUIContent("Tick rate", "Defines the tick rate for the ticker."));
            }
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}

#endif