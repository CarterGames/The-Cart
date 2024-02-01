/*
 * Copyright (c) 2024 Carter Games
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

namespace CarterGames.Cart.Editor
{
    /// <summary>
    /// Handles the editor GUI for the game ticker system.
    /// </summary>
    public static class GameTickerSettingsDrawer
    {
        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public static void DrawSettings()
        {
            UtilEditor.SettingsObject.Fp("isGameTicksExpanded").boolValue =
                EditorGUILayout.Foldout(UtilEditor.SettingsObject.Fp("isGameTicksExpanded").boolValue, "Game Ticker");

            
            if (!UtilEditor.SettingsObject.Fp("isGameTicksExpanded").boolValue) return;


            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(1.5f);
            EditorGUI.indentLevel++;
            
            
            // Draw the provider enum field on the GUI...
            EditorGUILayout.PropertyField(UtilEditor.SettingsObject.Fp("gameTickUseGlobalTicker"), new GUIContent("Use Global Ticker?"));
            EditorGUILayout.PropertyField(UtilEditor.SettingsObject.Fp("gameTickTicksPerSecond"), new GUIContent("Global Ticks Per Sec"));
            EditorGUILayout.PropertyField(UtilEditor.SettingsObject.Fp("gameTickUseUnscaledTime"), new GUIContent("Use Unscaled Time?"));


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
            
            EditorGUILayout.LabelField("Game Ticks", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("gameTickUseGlobalTicker"), new GUIContent("Use Global Ticker?"));
            EditorGUILayout.PropertyField(serializedObject.Fp("gameTickTicksPerSecond"), new GUIContent("Global Ticks Per Sec"));
            EditorGUILayout.PropertyField(serializedObject.Fp("gameTickUseUnscaledTime"), new GUIContent("Use Unscaled Time?"));

            EditorGUILayout.EndVertical();
        }
    }
}