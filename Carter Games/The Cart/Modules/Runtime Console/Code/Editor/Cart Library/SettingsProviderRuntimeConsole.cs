#if CARTERGAMES_CART_MODULE_RUNTIMECONSOLE && UNITY_EDITOR

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

namespace CarterGames.Cart.Modules.RuntimeConsole
{
	public sealed class SettingsProviderRuntimeConsole : ISettingsProvider
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static readonly string ExpandedId = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_Modules_RuntimeConsole_IsExpanded";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Should the game ticks dropdown be active?
        /// </summary>
        private static bool IsExpanded
        {
            get => (bool)PerUserSettings.GetOrCreateValue<bool>(ExpandedId, SettingType.EditorPref);
            set => PerUserSettings.SetValue<bool>(ExpandedId, SettingType.EditorPref, value);
        }
        
        /// <summary>
        /// The settings asset reference to use.
        /// </summary>
        private IScriptableAssetDef<DataAssetRuntimeConsole> SettingsDef => ScriptableRef.GetAssetDef<DataAssetRuntimeConsole>();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   ISettingsProvider Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Draws the inspector version of the settings.
        /// </summary>
        public void OnInspectorSettingsGUI()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField(AssetMeta.GetData("RuntimeConsoleMeta").Labels[AssetMeta.SectionTitle], EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("displayPrefab"), AssetMeta.GetData("RuntimeConsoleMeta").Content("prefab"));
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("showUnityConsoleMessages"), AssetMeta.GetData("RuntimeConsoleMeta").Content("showUnityLogs"));
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("showCartLogs"), AssetMeta.GetData("RuntimeConsoleMeta").Content("showCartLogs"));

            EditorGUILayout.EndVertical();
        }

        
        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public void OnProjectSettingsGUI()
        {
            IsExpanded = EditorGUILayout.Foldout(IsExpanded, AssetMeta.GetData("RuntimeConsoleMeta").Content(AssetMeta.SectionTitle));
            
            if (!IsExpanded) return;
            
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(1.5f);
            EditorGUI.indentLevel++;
            
            
            // Draw the provider enum field on the GUI...
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("displayPrefab"), AssetMeta.GetData("RuntimeConsoleMeta").Content("prefab"));
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("showUnityConsoleMessages"), AssetMeta.GetData("RuntimeConsoleMeta").Content("showUnityLogs"));
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("showCartLogs"), AssetMeta.GetData("RuntimeConsoleMeta").Content("showCartLogs"));

            
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
	}
}

#endif