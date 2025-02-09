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

using System.Linq;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Management.Editor;
using UnityEngine;

namespace CarterGames.Cart.Modules
{
    public static class ModuleManager
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Settings Keys
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static readonly string IsProcessingKey = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_ModuleManager_IsProcessing";
        private static readonly string HasPromptedKey = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_ModuleManager_HasPrompted";
        
        
        // Icons
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private const string CrossIconColourised = "<color=#ff9494>\u2718</color>";
        public const string CrossIcon = "\u2718";

        private const string TickIconColourised = "<color=#71ff50>\u2714</color>";
        public const string TickIcon = "\u2714";
        
        
        // Colours
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        public static readonly Color UninstallCol = ColorExtensions.HtmlStringToColor("#ff9494");
        public static readonly Color InstallCol = ColorExtensions.HtmlStringToColor("#71ff50");


        // Modules
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static IModule[] allModulesCache;

        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// All the modules found in the project.
        /// </summary>
        public static IModule[] AllModules
        {
            get
            {
                if (!allModulesCache.IsEmptyOrNull()) return allModulesCache;
                allModulesCache = AssemblyHelper.GetClassesOfType<IModule>().ToArray();
                return allModulesCache;
            }
        }
        
        
        /// <summary>
        /// Gets if the modules are currently being processed.
        /// </summary>
        public static bool IsProcessing
        {
            get => (bool) PerUserSettings.GetOrCreateValue<bool>(IsProcessingKey, SettingType.SessionState, false);
            set => PerUserSettings.SetValue<bool>(IsProcessingKey, SettingType.SessionState, value);
        }
        
        
        /// <summary>
        /// Gets if the user has been prompted or not.
        /// </summary>
        public static bool HasPrompted
        {
            get => (bool) PerUserSettings.GetOrCreateValue<bool>(HasPromptedKey, SettingType.SessionState, false);
            set => PerUserSettings.SetValue<bool>(HasPromptedKey, SettingType.SessionState, value);
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets if the module is installed.
        /// </summary>
        /// <param name="module">The module to check.</param>
        /// <returns>Bool</returns>
        public static bool IsEnabled(IModule module)
        {
            return CscFileHandler.HasDefine(module);
        }
        
        
        /// <summary>
        /// Gets the colourised status icon for the module's status.
        /// </summary>
        /// <param name="module">The module to get the icon for.</param>
        /// <returns>The rich text string for the icon.</returns>
        public static string GetModuleStatusIcon(IModule module)
        {
            if (!IsEnabled(module)) return CrossIconColourised;
            return TickIconColourised;
        }



        public static IModule GetModuleFromName(string moduleName)
        {
            return AllModules.FirstOrDefault(t => t.ModuleName.Equals(moduleName));
        }
        
        
        public static IModule GetModuleFromDefine(string moduleDefine)
        {
            return AllModules.FirstOrDefault(t => t.ModuleDefine.Equals(moduleDefine));
        }
    }
}