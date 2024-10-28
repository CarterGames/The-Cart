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

using CarterGames.Cart.Core.Management.Editor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    /// <summary>
    /// Handles the editor settings for the hierarchy.
    /// </summary>
    public static class EditorSettingsHierarchy
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static readonly string UniqueId = PerUserSettings.UniqueId;
        
        private static readonly string IsSeparatorExpandedId = $"{UniqueId}_CarterGames_TheCart_HierarchySettings_SectionExpanded";
        
        private static readonly string HeaderPrefixId = $"{UniqueId}_CarterGames_TheCart_HierarchySettings_HierarchyHeaderPrefix";
        private static readonly string SeparatorPrefixId = $"{UniqueId}_CarterGames_TheCart_HierarchySettings_SeparatorHeaderPrefix";
        private static readonly string TextAlignId = $"{UniqueId}_CarterGames_TheCart_HierarchySettings_TextAlign";
        private static readonly string FullWidthId = $"{UniqueId}_CarterGames_TheCart_HierarchySettings_FullWidth";
        private static readonly string HeaderBackgroundColorId = $"{UniqueId}_CarterGames_TheCart_HierarchySettings_HeaderBackgroundColor";
        private static readonly string HeaderTextId = $"{UniqueId}_CarterGames_TheCart_HierarchySettings_HeaderText";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public static bool EditorSettingsSectionExpanded
        {
            get => (bool) PerUserSettings.GetOrCreateValue<bool>(IsSeparatorExpandedId, SettingType.EditorPref, false);
            set => PerUserSettings.SetValue<bool>(IsSeparatorExpandedId, SettingType.EditorPref, value);
        }
        
        
        public static string HeaderPrefix
        {
            get => (string) PerUserSettings.GetOrCreateValue<string>(HeaderPrefixId, SettingType.EditorPref, "<---");
            set => PerUserSettings.SetValue<string>(HeaderPrefixId, SettingType.EditorPref, value);
        }
        
        
        public static string SeparatorPrefix
        {
            get => (string) PerUserSettings.GetOrCreateValue<string>(SeparatorPrefixId, SettingType.EditorPref, "--->");
            set => PerUserSettings.SetValue<string>(SeparatorPrefixId, SettingType.EditorPref, value);
        }
        
        
        public static HierarchyTitleTextAlign TextAlign
        {
            get => (HierarchyTitleTextAlign) PerUserSettings.GetOrCreateValue<int>(TextAlignId, SettingType.EditorPref, HierarchyTitleTextAlign.Center);
            set => PerUserSettings.SetValue<int>(TextAlignId, SettingType.EditorPref, value);
        }
        
        
        public static bool FullWidth
        {
            get => (bool) PerUserSettings.GetOrCreateValue<bool>(FullWidthId, SettingType.EditorPref, false);
            set => PerUserSettings.SetValue<bool>(FullWidthId, SettingType.EditorPref, value);
        }
        
        
        public static Color HeaderBackgroundColor
        {
            get => (Color) PerUserSettings.GetOrCreateValue<Color>(HeaderBackgroundColorId, SettingType.EditorPref, Color.gray);
            set => PerUserSettings.SetValue<Color>(HeaderBackgroundColorId, SettingType.EditorPref, value);
        }
        
        
        public static Color TextColor
        {
            get => (Color) PerUserSettings.GetOrCreateValue<Color>(HeaderTextId, SettingType.EditorPref, Color.white);
            set => PerUserSettings.SetValue<Color>(HeaderTextId, SettingType.EditorPref, value);
        }
    }
}

#endif