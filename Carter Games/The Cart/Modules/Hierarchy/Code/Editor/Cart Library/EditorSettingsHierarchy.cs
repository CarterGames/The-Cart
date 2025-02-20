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

using CarterGames.Cart.Core.Management.Editor;

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
        private static readonly string LastSelectedId = $"{UniqueId}_CarterGames_TheCart_HierarchySettings_LastSelected";

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public static bool EditorSettingsSectionExpanded
        {
            get => (bool) PerUserSettings.GetOrCreateValue<bool>(IsSeparatorExpandedId, SettingType.EditorPref, false);
            set => PerUserSettings.SetValue<bool>(IsSeparatorExpandedId, SettingType.EditorPref, value);
        }
        
        
        public static int EditorSettingsLastSelected
        {
            get => (int) PerUserSettings.GetOrCreateValue<int>(LastSelectedId, SettingType.EditorPref, 0);
            set => PerUserSettings.SetValue<int>(LastSelectedId, SettingType.EditorPref, value);
        }
    }
}

#endif