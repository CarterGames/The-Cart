/*
 * Copyright (c) 2018-Present Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEditor;
using UnityEngine;

namespace CarterGames.Common.Editor
{
    /// <summary>
    /// Handles the settings drawing for the hierarchy block system.
    /// </summary>
    public static class HierarchySeparatorSettingsDrawer
    {
        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public static void DrawSettings()
        {
            UtilEditor.EditorSettingsObject.FindProperty("isHierarchySeparatorExpanded").boolValue =
                EditorGUILayout.Foldout(UtilEditor.EditorSettingsObject.FindProperty("isHierarchySeparatorExpanded").boolValue, "Hierarchy");

            
            if (!UtilEditor.EditorSettingsObject.FindProperty("isHierarchySeparatorExpanded").boolValue) return;


            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(1.5f);
            EditorGUI.indentLevel++;
            
            
            // Draw the provider enum field on the GUI...
            EditorGUILayout.PropertyField(UtilEditor.EditorSettingsObject.FindProperty("hierarchyHeaderPrefix"), new GUIContent("Header Prefix"));
            EditorGUILayout.PropertyField(UtilEditor.EditorSettingsObject.FindProperty("hierarchySeparatorPrefix"), new GUIContent("Separator Prefix"));
            EditorGUILayout.PropertyField(UtilEditor.EditorSettingsObject.FindProperty("hierarchyAlwaysFullWidth"), new GUIContent("Always Full Width?"));
            EditorGUILayout.PropertyField(UtilEditor.EditorSettingsObject.FindProperty("hierarchyTextAlign"), new GUIContent("Header Text Alignment"));
            EditorGUILayout.PropertyField(UtilEditor.EditorSettingsObject.FindProperty("hierarchyHeaderBackgroundColor"), new GUIContent("Background Color"));
            EditorGUILayout.PropertyField(UtilEditor.EditorSettingsObject.FindProperty("hierarchyHeaderTextColor"), new GUIContent("Label Color"));

            
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the inspector version of the settings.
        /// </summary>
        /// <param name="serializedObject">The target object</param>
        public static void DrawInspector(SerializedObject serializedObject)
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Hierarchy", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isHierarchySeparatorExpanded"), new GUIContent("Is Hierarchy Expanded"));
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hierarchyHeaderPrefix"), new GUIContent("Header Prefix"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hierarchySeparatorPrefix"), new GUIContent("Separator Prefix"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hierarchyAlwaysFullWidth"), new GUIContent("Always Full Width?"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hierarchyTextAlign"), new GUIContent("Header Text Alignment"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hierarchyHeaderBackgroundColor"), new GUIContent("Header Background Color"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hierarchyHeaderTextColor"), new GUIContent("Header Text Color"));
            
            EditorGUILayout.EndVertical();
        }
    }
}