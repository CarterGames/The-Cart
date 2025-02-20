#if CARTERGAMES_CART_MODULE_HIERARCHY && UNITY_EDITOR

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
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Core.MetaData.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    /// <summary>
    /// Handles the settings drawing for the hierarchy block system.
    /// </summary>
    public sealed class SettingsProviderHierarchy : ISettingsProvider
    {
        private static readonly string[] OptionLabels = new string[2]
        {
            "Headers & Separators", "Alternate Colors"
        };
        
        private static SerializedObject Config =>
            ScriptableRef.GetAssetDef<DataAssetHierarchySettings>().ObjectRef;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   ISettingsProvider Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Draws the inspector version of the settings.
        /// </summary>
        public void OnInspectorSettingsGUI()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Hierarchy (Header/Separator)", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.BeginChangeCheck();
            
            ScriptableRef.GetAssetDef<DataAssetHierarchySettings>().AssetRef.HeaderSeparatorConfig.DrawConfig(ScriptableRef.GetAssetDef<DataAssetHierarchySettings>().ObjectRef.Fp("headerSeparatorConfig"));

            EditorGUILayout.Space(2.5f);
            
            EditorGUILayout.LabelField("Hierarchy (Alternate Colors)", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            ScriptableRef.GetAssetDef<DataAssetHierarchySettings>().AssetRef.AlternateLinesConfig.DrawConfig(ScriptableRef.GetAssetDef<DataAssetHierarchySettings>().ObjectRef.Fp("alternateLinesConfig"));
            
            if (EditorGUI.EndChangeCheck())
            {
                Config.ApplyModifiedProperties();
                Config.Update();
            }
            
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public void OnProjectSettingsGUI()
        {
            EditorSettingsHierarchy.EditorSettingsSectionExpanded = EditorGUILayout.Foldout(EditorSettingsHierarchy.EditorSettingsSectionExpanded, "Hierarchy");
            
            if (!EditorSettingsHierarchy.EditorSettingsSectionExpanded) return;

            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(1.5f);

            EditorGUILayout.BeginHorizontal();
            for (var i = 0; i < OptionLabels.Length; i++)
            {
                GUI.backgroundColor = EditorSettingsHierarchy.EditorSettingsLastSelected == i ? Color.gray : Color.white;
                
                if (GUILayout.Button(OptionLabels[i]))
                {
                    EditorSettingsHierarchy.EditorSettingsLastSelected = i;
                }
                
                GUI.backgroundColor = Color.white;
            }
            EditorGUILayout.EndHorizontal();
            
            switch (EditorSettingsHierarchy.EditorSettingsLastSelected)
            {
                case 0:
                    ScriptableRef.GetAssetDef<DataAssetHierarchySettings>().AssetRef.HeaderSeparatorConfig.DrawConfig(ScriptableRef.GetAssetDef<DataAssetHierarchySettings>().ObjectRef.Fp("headerSeparatorConfig"));
                    break;
                case 1:
                    ScriptableRef.GetAssetDef<DataAssetHierarchySettings>().AssetRef.AlternateLinesConfig.DrawConfig(ScriptableRef.GetAssetDef<DataAssetHierarchySettings>().ObjectRef.Fp("alternateLinesConfig"));
                    break;
            }
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif