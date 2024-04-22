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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Json;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
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
        private static readonly string ModuleNamespacesSettingKey = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_ModuleManager_ProjectNamespacesList";
        private static readonly string CurrentlyUpdatingModuleNameKey = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_ModuleManager_UpdateModuleName";
        private static readonly string InstallModulesQueueKey = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_ModuleManager_InstallQueue";
        private static readonly string IsProcessingKey = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_ModuleManager_IsProcessing";
        private static readonly string CurrentProcessKey = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_ModuleManager_CurrentProcess";
        private static readonly string HasPromptedKey = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_ModuleManager_HasPrompted";
        
        
        // Icons
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private const string CrossIconColourised = "<color=#ff9494>\u2718</color>";
        public const string CrossIcon = "\u2718";

        private const string UpdateIconColourised = "<color=#ffda6a>\u25B2</color>";
        public const string UpdateIcon = "\u25B2";

        private const string TickIconColourised = "<color=#71ff50>\u2714</color>";
        public const string TickIcon = "\u2714";
        
        
        // Colours
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        public static readonly Color UninstallCol = ColorExtensions.HtmlStringToColor("#ff9494");
        public static readonly Color UpdateCol = ColorExtensions.HtmlStringToColor("#ffda6a");
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
                allModulesCache = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .Where(x => x.IsClass && typeof(IModule).IsAssignableFrom(x))
                    .Select(type => (IModule)Activator.CreateInstance(type)).ToArray();

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

        
        /// <summary>
        /// Gets the current process
        /// </summary>
        public static string CurrentProcess
        {
            get => (string) PerUserSettings.GetOrCreateValue<string>(CurrentProcessKey, SettingType.SessionState, string.Empty);
            set => PerUserSettings.SetValue<string>(CurrentProcessKey, SettingType.SessionState, value);
        }
        
        
        /// <summary>
        /// Gets the namespaces in the project
        /// </summary>
        public static List<string> NamespacesInProject => JsonUtility.FromJson<ListWrapper<string>>((string) PerUserSettings.GetOrCreateValue<string>(ModuleNamespacesSettingKey, SettingType.SessionState, JsonUtility.ToJson(new ListWrapper<string>()))).Data;


        /// <summary>
        /// Gets all the elements to process before the process is completed.
        /// </summary>
        public static List<string> ProcessQueue
        {
            get => JsonUtility.FromJson<ListWrapper<string>>((string) PerUserSettings.GetOrCreateValue<string>(InstallModulesQueueKey, SettingType.SessionState, JsonUtility.ToJson(new ListWrapper<string>()))).Data;
            set => PerUserSettings.SetValue<string>(InstallModulesQueueKey, SettingType.SessionState, JsonUtility.ToJson(new ListWrapper<string>(value)));
        }


        /// <summary>
        /// Gets the string of the module being updated.
        /// </summary>
        public static string UpdatingModule
        {
            get => (string) PerUserSettings.GetOrCreateValue<string>(CurrentlyUpdatingModuleNameKey, SettingType.SessionState, string.Empty);
            set => PerUserSettings.SetValue<string>(CurrentlyUpdatingModuleNameKey, SettingType.SessionState, value);
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets if the module is installed.
        /// </summary>
        /// <param name="module">The module to check.</param>
        /// <returns>Bool</returns>
        public static bool IsInstalled(IModule module)
        {
            if (NamespacesInProject.Count.Equals(0))
            {
                RefreshNamespaceCache();    
            }
            
            return NamespacesInProject.Contains(module.Namespace);
        }


        /// <summary>
        /// Gets if the package file exists in the project. 
        /// </summary>
        /// <param name="module">The module to check.</param>
        /// <returns>Bool</returns>
        public static bool HasPackage(IModule module)
        {
            return File.Exists(module.ModulePackagePath);
        }


        /// <summary>
        /// Gets the installed revision number for the module.
        /// </summary>
        /// <param name="module">The module to check.</param>
        /// <returns>Int</returns>
        public static int InstalledRevisionNumber(IModule module)
        {
            if (IsProcessing) return CartSoAssetAccessor.GetAsset<ModuleCache>().Manifest.GetData(module).Revision;
            if (!IsInstalled(module)) return -1;

            if (CartSoAssetAccessor.GetAsset<ModuleCache>().InstalledModulesInfo.ContainsKey(module.Namespace))
            {
                if (CartSoAssetAccessor.GetAsset<ModuleCache>().InstalledModulesInfo[module.Namespace] == null)
                {
                    CartSoAssetAccessor.GetAsset<ModuleCache>().InstalledModulesInfo.Remove(module.Namespace);
                    return -1;
                }
                
                return CartSoAssetAccessor.GetAsset<ModuleCache>().InstalledModulesInfo[module.Namespace].Revision;
            }
            else
            {
                CartSoAssetAccessor.GetAsset<ModuleCache>().AddInstalledModuleInfo(module, AssetDatabase.LoadAssetAtPath<TextAsset>(module.ModuleInstallPath + "/Installation.json"));
                return CartSoAssetAccessor.GetAsset<ModuleCache>().InstalledModulesInfo[module.Namespace].Revision;
            }
        }


        /// <summary>
        /// Gets if a module has an update or not.
        /// </summary>
        /// <param name="module">The module to check.</param>
        /// <returns>Bool</returns>
        public static bool HasUpdate(IModule module)
        {
            if (!IsInstalled(module)) return false;
            return InstalledRevisionNumber(module) < (CartSoAssetAccessor.GetAsset<ModuleCache>().Manifest.GetData(module).Revision);
        }
        
        
        /// <summary>
        /// Refreshes the cache of the namespaces found in the project.
        /// </summary>
        public static void RefreshNamespaceCache()
        {
            var found = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Select(x => x.Namespace).Where(t => t != null && t.Contains("CarterGames.Cart.Modules")).Distinct().ToList();

            PerUserSettings.SetValue<string>(ModuleNamespacesSettingKey, SettingType.SessionState, JsonUtility.ToJson(new ListWrapper<string>(found)));
        }
        
        
        /// <summary>
        /// Gets the colourised status icon for the module's status.
        /// </summary>
        /// <param name="module">The module to get the icon for.</param>
        /// <returns>The rach text string for the icon.</returns>
        public static string GetModuleStatusIcon(IModule module)
        {
            if (!IsInstalled(module)) return CrossIconColourised;
            if (HasUpdate(module)) return UpdateIconColourised;
            return TickIconColourised;
        }
    }
}