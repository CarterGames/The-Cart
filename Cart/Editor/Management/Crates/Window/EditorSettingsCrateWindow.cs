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

using System.Collections.Generic;
using CarterGames.Cart.Management.Editor;
using CarterGames.Cart.ThirdParty;
using Newtonsoft.Json.Linq;
using UnityEngine;

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
                    crates.Add(CrateManager.GetCrateFromName(MultiSelectCratesContainer[i].ToString()));
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