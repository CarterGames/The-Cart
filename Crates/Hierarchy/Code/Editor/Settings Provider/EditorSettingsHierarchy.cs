#if CARTERGAMES_CART_CRATE_HIERARCHY && UNITY_EDITOR

/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using CarterGames.Cart.Management.Editor;

namespace CarterGames.Cart.Crates.Hierarchy.Editor
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