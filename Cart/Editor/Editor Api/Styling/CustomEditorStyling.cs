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

using System;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    public static class CustomEditorStyling
    {
        public static void DrawToggleStatusButton(SerializedProperty property, bool showText = false, bool interactable = true)
        {
            GUIContent content;
            
            if (!interactable)
            {
                if (showText)
                {
                    content = property.boolValue
                        ? new GUIContent("Enable", EditorGUIUtility.IconContent("Valid@2x").image)
                        : new GUIContent("Disable", EditorGUIUtility.IconContent("CrossIcon").image);
                }
                else
                {
                    content = property.boolValue
                        ? new GUIContent(EditorGUIUtility.IconContent("Valid@2x").image)
                        : new GUIContent(EditorGUIUtility.IconContent("CrossIcon").image);
                }
                
                GUI.backgroundColor = property.boolValue ? Color.green : Color.red;
                
                EditorGUILayout.LabelField(content, "button", GUILayout.Height(20f), GUILayout.Width(25f));
                
                GUI.backgroundColor = Color.white;
            }
            else
            {
                if (showText)
                {
                    content = property.boolValue
                        ? new GUIContent("Enable", EditorGUIUtility.IconContent("Valid@2x").image)
                        : new GUIContent("Disable", EditorGUIUtility.IconContent("CrossIcon").image);
                }
                else
                {
                    content = property.boolValue
                        ? new GUIContent(EditorGUIUtility.IconContent("Valid@2x").image)
                        : new GUIContent(EditorGUIUtility.IconContent("CrossIcon").image);
                }
                
                GUI.backgroundColor = property.boolValue ? Color.green : Color.red;

                if (GUILayout.Button(content, GUILayout.Height(20f), GUILayout.Width(25f)))
                {
                    property.boolValue = !property.boolValue;
                    property.serializedObject.ApplyModifiedProperties();
                    property.serializedObject.Update();
                }
                
                GUI.backgroundColor = Color.white;
            }
        }
        
        
        public static void SmallCrossButton(Action onPress)
        {
            GUI.backgroundColor = Color.red;
            
            if (GUILayout.Button(GeneralUtilEditor.CrossIcon, GUILayout.Width(GeneralUtilEditor.CrossIcon.GUIWidth() + 7.5f)))
            {
                onPress?.Invoke();
            }
            
            GUI.backgroundColor = Color.white;
        }

        
        public static void CrossMiniButton(string extraLabel, float width = -1)
        {
            GUI.backgroundColor = Color.red;
            
            if (width > 0)
            {
                EditorGUILayout.LabelField(GeneralUtilEditor.CrossIcon + " " + extraLabel, new GUIStyle("minibutton"), GUILayout.Width(width));
            }
            else
            {
                EditorGUILayout.LabelField(GeneralUtilEditor.CrossIcon + " " + extraLabel, new GUIStyle("minibutton"));
            }
            
            GUI.backgroundColor = Color.white;
        }
        

        public static void CrossMiniButton(float width, string extraLabel = "")
        {
            GUI.backgroundColor = Color.red;

            if (string.IsNullOrEmpty(extraLabel))
            {
                EditorGUILayout.LabelField(GeneralUtilEditor.CrossIcon, new GUIStyle("minibutton"), GUILayout.Width(width));
            }
            else
            {
                CrossMiniButton(extraLabel, width);
            }
            
            GUI.backgroundColor = Color.white;
        }
        
        
        public static void TickMiniButton(string extraLabel, float width = -1)
        {
            GUI.backgroundColor = Color.green;
            
            if (width > 0)
            {
                EditorGUILayout.LabelField(GeneralUtilEditor.TickIcon + " " + extraLabel, new GUIStyle("minibutton"), GUILayout.Width(width));
            }
            else
            {
                EditorGUILayout.LabelField(GeneralUtilEditor.TickIcon + " " + extraLabel, new GUIStyle("minibutton"));
            }      
            
            GUI.backgroundColor = Color.white;
        }
        
        
        public static void TickMiniButton(float width, string extraLabel = "")
        {
            GUI.backgroundColor = Color.green;
            
            if (string.IsNullOrEmpty(extraLabel))
            {
                EditorGUILayout.LabelField(GeneralUtilEditor.TickIcon, new GUIStyle("minibutton"), GUILayout.Width(width));
            }
            else
            {
                TickMiniButton(extraLabel, width);
            }
            
            GUI.backgroundColor = Color.white;
        }
    }
}