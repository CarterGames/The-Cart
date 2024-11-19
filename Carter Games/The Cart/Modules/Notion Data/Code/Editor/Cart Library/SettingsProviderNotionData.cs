#if CARTERGAMES_CART_MODULE_NOTIONDATA && UNITY_EDITOR

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

using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Core.MetaData.Editor;
using CarterGames.Cart.Modules.Settings;
using UnityEditor;

namespace CarterGames.Cart.Modules.NotionData.Editor
{
	public sealed class SettingsProviderNotionData : ISettingsProvider
	{
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static readonly string ExpandedId = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_Modules_NotionData_IsExpanded";

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Should the data notion section be shown?
        /// </summary>
        private static bool IsExpanded
        {
            get => (bool)PerUserSettings.GetOrCreateValue<bool>(ExpandedId, SettingType.EditorPref);
            set => PerUserSettings.SetValue<bool>(ExpandedId, SettingType.EditorPref, value);
        }

        private IScriptableAssetDef<DataAssetSettingsNotionData> SettingsDef => ScriptableRef.GetAssetDef<DataAssetSettingsNotionData>();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   ISettingsProvider Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Draws the inspector version of the settings.
        /// </summary>
        public void OnInspectorSettingsGUI()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField(AssetMeta.GetData("NotionData").Labels["sectionTitle"], EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("apiVersion"));
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("apiReleaseVersion"));

            if (EditorGUI.EndChangeCheck())
            {
                SettingsDef.ObjectRef.ApplyModifiedProperties();
                SettingsDef.ObjectRef.Update();
            }

            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public void OnProjectSettingsGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(1.5f);
            
            IsExpanded = EditorGUILayout.Foldout(IsExpanded, AssetMeta.GetData("NotionData").Content("notionTitle"));
            
            if (IsExpanded)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.Space(1.5f);
                EditorGUI.indentLevel++;
                
                // Draw the provider enum field on the GUI...
                EditorGUI.BeginChangeCheck();
                
                EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("apiVersion"));
                EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("apiReleaseVersion"));
                
                if (EditorGUI.EndChangeCheck())
                {
                    SettingsDef.ObjectRef.ApplyModifiedProperties();
                    SettingsDef.ObjectRef.Update();
                }
                
                EditorGUI.indentLevel--;
                EditorGUILayout.Space(1.5f);
                EditorGUILayout.EndVertical();
            }
            
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif