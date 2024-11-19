#if CARTERGAMES_CART_MODULE_HIERARCHY && UNITY_EDITOR

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
using UnityEditor;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    /// <summary>
    /// Handles the settings drawing for the hierarchy block system.
    /// </summary>
    public sealed class SettingsProviderHierarchy : ISettingsProvider
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   ISettingsProvider Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Draws the inspector version of the settings.
        /// </summary>
        public void OnInspectorSettingsGUI()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Hierarchy", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorSettingsHierarchy.HeaderPrefix = EditorGUILayout.TextField(AssetMeta.GetData("Hierarchy").Content("headerPrefix"), EditorSettingsHierarchy.HeaderPrefix);
            EditorSettingsHierarchy.SeparatorPrefix = EditorGUILayout.TextField(AssetMeta.GetData("Hierarchy").Content("separatorPrefix"), EditorSettingsHierarchy.SeparatorPrefix);
            EditorSettingsHierarchy.TextAlign = (HierarchyTitleTextAlign) EditorGUILayout.EnumPopup(AssetMeta.GetData("Hierarchy").Content("textAlignment"), EditorSettingsHierarchy.TextAlign);
            EditorSettingsHierarchy.FullWidth = EditorGUILayout.Toggle(AssetMeta.GetData("Hierarchy").Content("useFullWidth"), EditorSettingsHierarchy.FullWidth);
            EditorSettingsHierarchy.HeaderBackgroundColor = EditorGUILayout.ColorField(AssetMeta.GetData("Hierarchy").Content("headerBackgroundColor"), EditorSettingsHierarchy.HeaderBackgroundColor);
            EditorSettingsHierarchy.TextColor = EditorGUILayout.ColorField(AssetMeta.GetData("Hierarchy").Content("headerTextColor"), EditorSettingsHierarchy.TextColor);
            
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public void OnProjectSettingsGUI()
        {
            EditorSettingsHierarchy.EditorSettingsSectionExpanded = EditorGUILayout.Foldout(EditorSettingsHierarchy.EditorSettingsSectionExpanded, AssetMeta.GetData("Hierarchy").Content(AssetMeta.SectionTitle));
            
            if (!EditorSettingsHierarchy.EditorSettingsSectionExpanded) return;


            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(1.5f);
            EditorGUI.indentLevel++;
            
            
            // Draw the provider enum field on the GUI...
            EditorSettingsHierarchy.HeaderPrefix = EditorGUILayout.TextField(AssetMeta.GetData("Hierarchy").Content("headerPrefix"), EditorSettingsHierarchy.HeaderPrefix);
            EditorSettingsHierarchy.SeparatorPrefix = EditorGUILayout.TextField(AssetMeta.GetData("Hierarchy").Content("separatorPrefix"), EditorSettingsHierarchy.SeparatorPrefix);
            EditorSettingsHierarchy.TextAlign = (HierarchyTitleTextAlign) EditorGUILayout.EnumPopup(AssetMeta.GetData("Hierarchy").Content("textAlignment"), EditorSettingsHierarchy.TextAlign);
            EditorSettingsHierarchy.FullWidth = EditorGUILayout.Toggle(AssetMeta.GetData("Hierarchy").Content("useFullWidth"), EditorSettingsHierarchy.FullWidth);
            EditorSettingsHierarchy.HeaderBackgroundColor = EditorGUILayout.ColorField(AssetMeta.GetData("Hierarchy").Content("headerBackgroundColor"), EditorSettingsHierarchy.HeaderBackgroundColor);
            EditorSettingsHierarchy.TextColor = EditorGUILayout.ColorField(AssetMeta.GetData("Hierarchy").Content("headerTextColor"), EditorSettingsHierarchy.TextColor);
            
            
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif