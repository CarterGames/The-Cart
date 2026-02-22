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

using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Logs.Editor.Windows;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Core.MetaData.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Logs.Editor
{
    /// <summary>
    /// Handles the settings GUI for the logging system.
    /// </summary>
    public class SettingsProviderLogging : ISettingsProvider
    {
        private static readonly string LoggingExpanded = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_Settings_Core_Logging_Expanded";
        private static readonly string LoggingSettingsExpanded = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_Settings_Core_Logging_SettingsExpanded";
        private static readonly string LoggingCategoriesExpanded = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_Settings_Core_Logging_CategoriesExpanded";
        private static readonly string LoggingEditorWindowScrollPos = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_Settings_Core_Logging_ScrollPos";


        private static bool IsExpanded
        {
            get => PerUserSettings.GetValue<bool>(LoggingExpanded, SettingType.EditorPref);
            set => PerUserSettings.SetValue<bool>(LoggingExpanded, SettingType.EditorPref, value);
        }
        
        
        private static bool IsSettingsExpanded
        {
            get => PerUserSettings.GetValue<bool>(LoggingSettingsExpanded, SettingType.EditorPref, false);
            set => PerUserSettings.SetValue<bool>(LoggingSettingsExpanded, SettingType.EditorPref, value);
        }
        
        
        private static bool IsCategoriesExpanded
        {
            get => PerUserSettings.GetValue<bool>(LoggingCategoriesExpanded, SettingType.EditorPref, false);
            set => PerUserSettings.SetValue<bool>(LoggingCategoriesExpanded, SettingType.EditorPref, value);
        }
        
        
        public static Vector2 ScrollPos
        {
            get => PerUserSettings.GetValue<Vector2>(LoggingEditorWindowScrollPos, SettingType.EditorPref, Vector2.zero);
            set => PerUserSettings.SetValue<Vector2>(LoggingEditorWindowScrollPos, SettingType.EditorPref, value);
        }


        private static SerializedObject ObjectRef =>
            ScriptableRef.GetAssetDef<DataAssetCoreRuntimeSettings>().ObjectRef;
        
        private static DataAssetCoreRuntimeSettings AssetRef =>
            ScriptableRef.GetAssetDef<DataAssetCoreRuntimeSettings>().AssetRef;
        
        
        
        public void OnInspectorSettingsGUI()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField(AssetMeta.GetData("CartLogs").Labels[AssetMeta.SectionTitle], EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(ObjectRef.Fp("loggingUseCartLogs"), AssetMeta.GetData("CartLogs").Content("useLogs"));
            EditorGUILayout.PropertyField(ObjectRef.Fp("useLogsInProductionBuilds"), AssetMeta.GetData("CartLogs").Content("useInProduction"));
            EditorGUILayout.PropertyField(ObjectRef.Fp("forceShowErrors"), AssetMeta.GetData("CartLogs").Content("forceShowErrors"));
            
            EditorGUILayout.EndVertical();
        }


        public void OnProjectSettingsGUI()
        {
            EditorGUI.BeginChangeCheck();

            IsExpanded =
                EditorGUILayout.Foldout(IsExpanded, AssetMeta.GetData("CartLogs").Content(AssetMeta.SectionTitle));

            if (!IsExpanded) return;


            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(1.5f);
            EditorGUI.indentLevel++;
            

            // Draw the provider enum field on the GUI...
            EditorGUILayout.PropertyField(ObjectRef.Fp("loggingUseCartLogs"), AssetMeta.GetData("CartLogs").Content("useLogs"));
            EditorGUILayout.PropertyField(ObjectRef.Fp("useLogsInProductionBuilds"), AssetMeta.GetData("CartLogs").Content("useInProduction"));
            EditorGUILayout.PropertyField(ObjectRef.Fp("forceShowErrors"), AssetMeta.GetData("CartLogs").Content("forceShowErrors"));
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15f);

            if (GUILayout.Button("Edit Category States"))
            {
                UtilityEditorWindow.Open<LogCategoriesEditor>("Log Category Statuses Window");
            }

            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel--;
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}