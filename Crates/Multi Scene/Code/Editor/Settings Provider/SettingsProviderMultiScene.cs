#if CARTERGAMES_CART_CRATE_MULTISCENE && UNITY_EDITOR

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
using CarterGames.Cart.Events;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.MultiScene.Editor
{
    /// <summary>
    /// Handles the settings window for the asset.
    /// </summary>
    public class SettingsProviderMultiScene : ISettingsProvider
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static SettingsProvider Provider;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets the settings asset in the project as a SerializedObject.
        /// </summary>
        private static SerializedObject SettingsAssetObject => AutoMakeDataAssetManager.GetDefine<MultiSceneSettings>().ObjectRef;
        
        
        private static bool SettingsSceneManagementDropdown
        {
            get => (bool) PerUserSettings.GetOrCreateValue<bool>("cart_crate_multiscene_managementDropdownExpanded", SettingType.EditorPref);
            set => PerUserSettings.SetValue<bool>("cart_crate_multiscene_managementDropdownExpanded", SettingType.EditorPref, value);
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public static readonly Evt ToolbarSettingChangedEvt = new Evt();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Draws the scene group options section of the window.
        /// </summary>
        private static void DrawSceneManagementOptions()
        {
            EditorGUILayout.BeginVertical();

            GUILayout.Space(1.5f);

            // Toolbar Menu...
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(SettingsAssetObject.FindProperty("showToolbar"),
                new GUIContent("Show Toolbar",
                    "Toggles the toolbar menu for changing scene groups."));

            if (EditorGUI.EndChangeCheck())
            {
                ToolbarSettingChangedEvt.Raise();
            }
            
            // Listener Frequency...
            EditorGUILayout.PropertyField(SettingsAssetObject.FindProperty("listenerFrequency"),
                new GUIContent("Listener Frequency",
                    "Controls how many listeners execute per frame. The higher the number the more intensive the scene group loading can be."));

            // Use Unload Resources...
            EditorGUILayout.PropertyField(SettingsAssetObject.FindProperty("useUnloadResources"),
                new GUIContent("Use Unload Resources?", "Runs Resources.UnloadUnusedAssets() if enabled."));
            

            EditorGUILayout.PropertyField(SettingsAssetObject.FindProperty("sceneGroupLoadMode"),
                new GUIContent("Group Load Mode", "Defines which group will load on play mode."));

            EditorGUILayout.PropertyField(SettingsAssetObject.FindProperty("startGroup"),
                new GUIContent("Start Scene Group", "The scene group that is loaded first by the system."));

            GUI.enabled = false;

            EditorGUILayout.PropertyField(SettingsAssetObject.FindProperty("lastGroupLoaded"),
                new GUIContent("Last Scene Group Loaded", "The last scene group to be loaded in the editor."));

            GUI.enabled = true;

            EditorGUILayout.EndVertical();
        }


        public string MenuName => "Multi Scene";

        public void OnProjectSettingsGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(1.5f);
                    
            EditorGUI.BeginChangeCheck();
            
            DrawSceneManagementOptions();

            if (EditorGUI.EndChangeCheck())
            {
                // Debug.LogError("Setting Edit...");
                SettingsAssetObject.ApplyModifiedProperties();
                SettingsAssetObject.Update();
                MultiSceneEditorEvents.Settings.OnSettingChanged.Raise();
            }

            GUILayout.Space(2.5f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif