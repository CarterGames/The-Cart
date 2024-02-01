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

using CarterGames.Cart.Hierarchy;
using UnityEditor;

namespace CarterGames.Cart.Editor.Hierarchy
{
    /// <summary>
    /// Handles the settings drawing for the hierarchy block system.
    /// </summary>
    public static class HierarchySettingsDrawer
    {
        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public static void DrawSettings()
        {
            HierarchySettings.EditorSettingsSectionExpanded = EditorGUILayout.Foldout(HierarchySettings.EditorSettingsSectionExpanded, "Hierarchy");
            
            if (!HierarchySettings.EditorSettingsSectionExpanded) return;


            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(1.5f);
            EditorGUI.indentLevel++;
            
            
            // Draw the provider enum field on the GUI...
            HierarchySettings.HeaderPrefix = EditorGUILayout.TextField(HierarchyMeta.Settings.HeaderPrefix, HierarchySettings.HeaderPrefix);
            HierarchySettings.SeparatorPrefix = EditorGUILayout.TextField(HierarchyMeta.Settings.SeparatorPrefix, HierarchySettings.SeparatorPrefix);
            HierarchySettings.TextAlign = (HierarchyTitleTextAlign) EditorGUILayout.EnumPopup(HierarchyMeta.Settings.TextAlign, HierarchySettings.TextAlign);
            HierarchySettings.FullWidth = EditorGUILayout.Toggle(HierarchyMeta.Settings.FullWidth, HierarchySettings.FullWidth);
            HierarchySettings.HeaderBackgroundColor = EditorGUILayout.ColorField(HierarchyMeta.Settings.HeaderBackgroundColor, HierarchySettings.HeaderBackgroundColor);
            HierarchySettings.TextColor = EditorGUILayout.ColorField(HierarchyMeta.Settings.TextColor, HierarchySettings.TextColor);
            
            
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}