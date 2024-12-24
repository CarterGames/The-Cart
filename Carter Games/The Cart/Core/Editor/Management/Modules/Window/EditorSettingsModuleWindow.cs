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

using System.Collections.Generic;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.ThirdParty;
using UnityEngine;

namespace CarterGames.Cart.Modules.Window
{
    /// <summary>
    /// Handles any editor settings for the module setup.
    /// </summary>
    public static class EditorSettingsModuleWindow
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static readonly string SelectedModuleId  = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_ModuleWindow_SelectedName";
        private static readonly string MultiSelectModuleId  = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_ModuleWindow_MultiSelectedModuleNames";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Should the data notion section be shown?
        /// </summary>
        public static string SelectedModuleName
        {
            get => (string)PerUserSettings.GetOrCreateValue<string>(SelectedModuleId, SettingType.EditorPref);
            set => PerUserSettings.SetValue<string>(SelectedModuleId, SettingType.EditorPref, value);
        }
        
        
        private static string MultiSelectedModuleNames
        {
            get => (string)PerUserSettings.GetOrCreateValue<string>(MultiSelectModuleId, SettingType.EditorPref);
            set => PerUserSettings.SetValue<string>(MultiSelectModuleId, SettingType.EditorPref, value);
        }


        private static JSONNode MultiSelectModulesContainer
        {
            get => string.IsNullOrEmpty(MultiSelectedModuleNames) ? new JSONObject() : JSON.Parse(MultiSelectedModuleNames);
            set => MultiSelectedModuleNames = value.ToString();
        }


        /// <summary>
        /// All the selected modules in the multi-select.
        /// </summary>
        public static List<IModule> MultiSelectModules
        {
            get
            {
                var modules = new List<IModule>();

                for (var i = 0; i < MultiSelectModulesContainer.Count; i++)
                {
                    modules.Add(ModuleManager.GetModuleFromName(MultiSelectModulesContainer[i]));
                }

                return modules;
            }
            set
            {
                var data = new JSONArray();

                for (var j = 0; j < value.Count; j++)
                {
                    if (value[j] == null) continue;
                    data.Add(value[j].ModuleName);
                }

                MultiSelectModulesContainer = data;
            }
        }
    }
}