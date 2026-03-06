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

using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    [CustomEditor(typeof(RuntimeTimer))]
    public class InspectorRuntimeTimer : CustomInspector
    {
        protected override string[] HideProperties { get; }
        
        
        protected override void DrawInspectorGUI()
        {
            GUILayout.Space(2.5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(serializedObject.Fp("timeRemaining"));
            EditorGUILayout.PropertyField(serializedObject.Fp("timerDuration"));
            EditorGUILayout.PropertyField(serializedObject.Fp("timerPaused"));
            EditorGUILayout.PropertyField(serializedObject.Fp("timerUnscaledTimer"));
            EditorGUILayout.PropertyField(serializedObject.Fp("timerLoops"));
            EditorGUI.EndDisabledGroup();
            
            EditorGUILayout.EndVertical();
        }
    }
}