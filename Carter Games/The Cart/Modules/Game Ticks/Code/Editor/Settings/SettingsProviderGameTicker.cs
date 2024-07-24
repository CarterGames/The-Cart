#if CARTERGAMES_CART_MODULE_GAMETICKER

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

using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Core.MetaData.Editor;
using UnityEditor;

namespace CarterGames.Cart.Modules.GameTicks.Editor
{
    /// <summary>
    /// Handles the settings GUI for the game ticker module.
    /// </summary>
    public sealed class SettingsProviderGameTicker : ISettingsProvider, IMeta
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private SerializedObject settingsObj;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the settings obj.
        /// </summary>
        private SerializedObject SettingsObj
        {
            get
            {
                if (settingsObj != null) return settingsObj;
                settingsObj = new SerializedObject(DataAccess.GetAsset<DataAssetRuntimeSettingsGameTicks>());
                return settingsObj;
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IMeta Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the path for the metadata of the module.
        /// </summary>
        public string MetaDataPath => $"{ScriptableRef.AssetBasePath}/Carter Games/The Cart/Modules/Game Ticks/Data/Meta Data/";
        
        
        /// <summary>
        /// Gets the metadata of the module.
        /// </summary>
        public MetaData MetaData => Meta.GetData(MetaDataPath, "GameTickerMeta");
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   ISettingsProvider Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Draws the inspector version of the settings.
        /// </summary>
        /// <param name="serializedObject">The target object</param>
        public void OnInspectorSettingsGUI(SerializedObject serializedObject)
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField(MetaData.Labels[Meta.SectionTitle], EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(SettingsObj.Fp("gameTickUseGlobalTicker"), MetaData.Content("useGlobalTicker"));
            EditorGUILayout.PropertyField(SettingsObj.Fp("gameTickTicksPerSecond"), MetaData.Content("globalTicksPerSecond"));
            EditorGUILayout.PropertyField(SettingsObj.Fp("gameTickUseUnscaledTime"), MetaData.Content("globalUseUnscaledTime"));

            EditorGUILayout.EndVertical();
        }

        
        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public void OnProjectSettingsGUI()
        {
            EditorSettingsGameTicker.RuntimeSettingsTicksExpandedId =
                EditorGUILayout.Foldout(EditorSettingsGameTicker.RuntimeSettingsTicksExpandedId, MetaData.Content(Meta.SectionTitle));

            
            if (!EditorSettingsGameTicker.RuntimeSettingsTicksExpandedId) return;


            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(1.5f);
            EditorGUI.indentLevel++;
            
            
            // Draw the provider enum field on the GUI...
            EditorGUILayout.PropertyField(SettingsObj.Fp("gameTickUseGlobalTicker"), MetaData.Content("useGlobalTicker"));
            EditorGUILayout.PropertyField(SettingsObj.Fp("globalSyncState"), MetaData.Content("globalSyncState"));
            
            if (SettingsObj.Fp("globalSyncState").intValue == 0)
            {
                EditorGUILayout.PropertyField(SettingsObj.Fp("gameTickTicksPerSecond"),
                    MetaData.Content("globalTicksPerSecond"));
            }
            
            EditorGUILayout.PropertyField(SettingsObj.Fp("gameTickUseUnscaledTime"), MetaData.Content("globalUseUnscaledTime"));


            EditorGUI.indentLevel--;
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif