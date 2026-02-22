/*
 * Copyright (c) 2025 Carter Games
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

using CarterGames.Cart.Editor;
using CarterGames.Cart.Logs.Editor.Windows;
using CarterGames.Cart.Management;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Logs.Editor
{
    /// <summary>
    /// Handles the settings GUI for the logging system.
    /// </summary>
    public class SettingsProviderLogging : ISettingsProvider
    {
        private static readonly string LoggingEditorWindowScrollPos = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_Settings_Core_Logging_ScrollPos";
        
        
        public static Vector2 ScrollPos
        {
            get => PerUserSettings.GetValue<Vector2>(LoggingEditorWindowScrollPos, SettingType.EditorPref, Vector2.zero);
            set => PerUserSettings.SetValue<Vector2>(LoggingEditorWindowScrollPos, SettingType.EditorPref, value);
        }


        private static SerializedObject ObjectRef =>
            ScriptableRef.GetAssetDef<DataAssetCoreRuntimeSettings>().ObjectRef;
        


        public void OnProjectSettingsGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Logs", EditorStyles.boldLabel);

            // Draw the provider enum field on the GUI...
            EditorGUILayout.PropertyField(ObjectRef.Fp("loggingUseCartLogs"), new GUIContent("Use logs", "Defines if the logs are shown at all."));
            EditorGUILayout.PropertyField(ObjectRef.Fp("useLogsInProductionBuilds"), new GUIContent("Production build logs", "Defines if the logs will show in production builds or not."));
            EditorGUILayout.PropertyField(ObjectRef.Fp("forceShowErrors"), new GUIContent("Force Show Error Logs", "Defines if error logs will always show regardless of other settings."));
            
            EditorGUILayout.BeginHorizontal();
            
            GUILayout.Space(17.5f);

            if (GUILayout.Button("Toggle Categories"))
            {
                UtilityEditorWindow.Open<LogCategoriesEditor>("Log Category Statuses Window");
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
            }

            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}