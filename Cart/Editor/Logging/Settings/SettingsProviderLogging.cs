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
            AutoMakeDataAssetManager.GetDefine<DataAssetCoreRuntimeSettings>().ObjectRef;


        public string MenuName => "Logs";

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