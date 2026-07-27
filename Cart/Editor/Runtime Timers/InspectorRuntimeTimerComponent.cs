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

using CarterGames.Cart.Components;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    [CustomEditor(typeof(RuntimeTimerComponent))]
    public class InspectorRuntimeTimerComponent : CustomInspector
    {
        protected override string[] HideProperties { get; }
        
        
        protected override void DrawInspectorGUI()
        {
            GUILayout.Space(5f);
            
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.PropertyField(serializedObject.Fp("timerType"));

            if (serializedObject.Fp("timerType").enumValueIndex == (int)TimerType.Unassigned)
            {
                EditorGUILayout.EndVertical();
                return;
            }
            
            EditorGUILayout.PropertyField(serializedObject.Fp("startOnEnable"));

            switch (serializedObject.Fp("timerType").enumValueIndex)
            {
                case (int) TimerType.Countdown:
                    
                    EditorGUILayout.PropertyField(serializedObject.Fp("timerDuration"));
                    
                    EditorGUILayout.PropertyField(serializedObject.Fp("loop"));
                    
                    if (serializedObject.Fp("loop").boolValue)
                    {
                        EditorGUILayout.PropertyField(serializedObject.Fp("infiniteLoop"));
                        
                        if (!serializedObject.Fp("infiniteLoop").boolValue)
                        {
                            EditorGUILayout.PropertyField(serializedObject.Fp("loops"));
                        }
                    }
          
                    break;
                case (int) TimerType.Stopwatch:
                    break;
            }
            
            EditorGUILayout.EndVertical();

            GUILayout.Space(5f);

            if (serializedObject.Fp("timerType").enumValueIndex != (int) TimerType.Unassigned)
            {
                EditorGUILayout.BeginVertical("HelpBox");

                EditorGUILayout.PropertyField(serializedObject.Fp("showUnityEvents"));

                GUILayout.Space(2.5f);

                if (serializedObject.Fp("showUnityEvents").boolValue)
                {
                    EditorGUILayout.PropertyField(serializedObject.Fp("timerStartedUnityEvt"));
                    EditorGUILayout.PropertyField(serializedObject.Fp("timerTickedUnityEvt"));
                    EditorGUILayout.PropertyField(serializedObject.Fp("timerSecondPassedUnityEvt"));

                    if (serializedObject.Fp("loop").boolValue && serializedObject.Fp("timerType").enumValueIndex == (int)TimerType.Countdown)
                    {
                        EditorGUILayout.PropertyField(serializedObject.Fp("timerLoopedUnityEvt"));
                    }

                    if (serializedObject.Fp("timerType").enumValueIndex == (int)TimerType.Countdown)
                    {
                        EditorGUILayout.PropertyField(serializedObject.Fp("timerCompleteUnityEvt"));
                    }
                }
                
                EditorGUILayout.EndVertical();
            }
        }
    }
}