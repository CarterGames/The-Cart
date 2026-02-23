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

using System.Collections.Generic;
using CarterGames.Cart.Management.Editor;
using Newtonsoft.Json.Linq;

namespace CarterGames.Cart.Crates.Window
{
    /// <summary>
    /// Handles any editor settings for the crate setup.
    /// </summary>
    public static class EditorSettingsCrateWindow
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static readonly string SelectedCrateId  = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_CrateWindow_SelectedName";
        private static readonly string MultiSelectCrateId  = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_CrateWindow_MultiSelectedCrateNames";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Should the data notion section be shown?
        /// </summary>
        public static string SelectedCrateName
        {
            get => (string)PerUserSettings.GetOrCreateValue<string>(SelectedCrateId, SettingType.EditorPref);
            set => PerUserSettings.SetValue<string>(SelectedCrateId, SettingType.EditorPref, value);
        }
        
        
        private static string MultiSelectedCrateNames
        {
            get => (string)PerUserSettings.GetOrCreateValue<string>(MultiSelectCrateId, SettingType.EditorPref);
            set => PerUserSettings.SetValue<string>(MultiSelectCrateId, SettingType.EditorPref, value);
        }


        private static JArray MultiSelectCratesContainer
        {
            get => string.IsNullOrEmpty(MultiSelectedCrateNames) ? new JArray() : JArray.Parse(MultiSelectedCrateNames);
            set => MultiSelectedCrateNames = value.ToString();
        }


        /// <summary>
        /// All the selected crates in the multi-select.
        /// </summary>
        public static List<Crate> MultiSelectCrates
        {
            get
            {
                var crates = new List<Crate>();

                for (var i = 0; i < MultiSelectCratesContainer.Count; i++)
                {
                    crates.Add(CrateManager.GetCrateByName(MultiSelectCratesContainer[i].ToString()));
                }

                return crates;
            }
            set
            {
                var data = new JArray();

                for (var j = 0; j < value.Count; j++)
                {
                    if (value[j] == null) continue;
                    data.Add(value[j].CrateName);
                }

                MultiSelectCratesContainer = data;
            }
        }
    }
}