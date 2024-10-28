#if CARTERGAMES_CART_MODULE_GAMETICKER && UNITY_EDITOR

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

using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Core.MetaData.Editor;
using CarterGames.Cart.Modules.Settings;
using UnityEditor;

namespace CarterGames.Cart.Modules.GameTicks.Editor
{
    /// <summary>
    /// Handles the settings GUI for the game ticker module.
    /// </summary>
    public sealed class SettingsProviderGameTicker : ISettingsProvider
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static readonly string ExpandedId = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_Modules_GameTicks_IsExpanded";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Should the game ticks dropdown be active?
        /// </summary>
        public static bool IsExpanded
        {
            get => (bool)PerUserSettings.GetOrCreateValue<bool>(ExpandedId, SettingType.EditorPref);
            set => PerUserSettings.SetValue<bool>(ExpandedId, SettingType.EditorPref, value);
        }
        
        private IScriptableAssetDef<DataAssetSettingsGameTicker> SettingsDef => ScriptableRef.GetAssetDef<DataAssetSettingsGameTicker>();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   ISettingsProvider Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Draws the inspector version of the settings.
        /// </summary>
        public void OnInspectorSettingsGUI()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField(AssetMeta.GetData("GameTickerMeta").Labels[AssetMeta.SectionTitle], EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("gameTickUseGlobalTicker"), AssetMeta.GetData("GameTickerMeta").Content("useGlobalTicker"));
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("gameTickTicksPerSecond"), AssetMeta.GetData("GameTickerMeta").Content("globalTicksPerSecond"));
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("gameTickUseUnscaledTime"), AssetMeta.GetData("GameTickerMeta").Content("globalUseUnscaledTime"));

            EditorGUILayout.EndVertical();
        }

        
        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public void OnProjectSettingsGUI()
        {
            IsExpanded = EditorGUILayout.Foldout(IsExpanded, AssetMeta.GetData("GameTickerMeta").Content(AssetMeta.SectionTitle));
            
            if (!IsExpanded) return;


            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(1.5f);
            EditorGUI.indentLevel++;
            
            
            // Draw the provider enum field on the GUI...
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("gameTickUseGlobalTicker"), AssetMeta.GetData("GameTickerMeta").Content("useGlobalTicker"));
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("globalSyncState"), AssetMeta.GetData("GameTickerMeta").Content("globalSyncState"));
            
            if (SettingsDef.ObjectRef.Fp("globalSyncState").intValue == 0)
            {
                EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("gameTickTicksPerSecond"),
                    AssetMeta.GetData("GameTickerMeta").Content("globalTicksPerSecond"));
            }
            
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("gameTickUseUnscaledTime"), AssetMeta.GetData("GameTickerMeta").Content("globalUseUnscaledTime"));


            EditorGUI.indentLevel--;
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif