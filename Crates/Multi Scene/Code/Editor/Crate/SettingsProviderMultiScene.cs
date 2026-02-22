#if CARTERGAMES_CART_CRATE_MULTISCENE && UNITY_EDITOR

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

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
        private static SerializedObject SettingsAssetObject => ScriptableRef.GetAssetDef<MultiSceneSettings>().ObjectRef;
        
        
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