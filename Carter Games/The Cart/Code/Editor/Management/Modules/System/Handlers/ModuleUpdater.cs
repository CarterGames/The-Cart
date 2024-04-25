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
using System.Linq;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Modules.Window;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules
{
    /// <summary>
    /// Handles updating modules.
    /// </summary>
    public sealed class ModuleUpdater : IAssetEditorReload
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Updates the selected module when called.
        /// </summary>
        /// <param name="module">The module to update.</param>
        public static void UpdateModule(IModule module)
        {
            ModuleManager.CurrentProcess = ModuleOperations.Updating;
            ModuleManager.IsProcessing = true;
            ModuleManager.UpdatingModule = module.ModuleName;
            
            AssetDatabase.StartAssetEditing();
            
            ModuleManager.ProcessQueue = new List<string>()
            {
                module.ModulePackagePath
            };
            
            AssetDatabase.DeleteAsset(module.ModuleInstallPath);
            ModuleInstaller.Install(ModuleManager.AllModules.FirstOrDefault(t => t.ModulePackagePath.Equals(ModuleManager.ProcessQueue[0])));
            
            CartSoAssetAccessor.GetAsset<ModuleCache>().AddInstalledModuleInfo(module, AssetDatabase.LoadAssetAtPath<TextAsset>(module.ModuleInstallPath + "/Installation.json"));
            ModuleManager.UpdatingModule = string.Empty;
            
            AssetDatabase.StopAssetEditing();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IAssetEditorReload Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Is raised when the editor reloads.
        /// </summary>
        public void OnEditorReloaded()
        {
            ModuleManager.RefreshNamespaceCache();
            
            if (!ModuleManager.CurrentProcess.Equals(ModuleOperations.Updating)) return;
            
            var module = ModuleManager.AllModules.First(t => t.ModulePackagePath.Equals(ModuleManager.ProcessQueue.First()));
            CartSoAssetAccessor.GetAsset<ModuleCache>().AddInstalledModuleInfo(module, AssetDatabase.LoadAssetAtPath<TextAsset>(module.ModuleInstallPath + "/Installation.json"));
            
            ModuleManager.ProcessQueue = new List<string>();
            ModuleManager.RefreshNamespaceCache();
            ModulesWindow.RepaintWindow();
            
            ModuleManager.CurrentProcess = string.Empty;
            ModuleManager.IsProcessing = false;
        }
    }
}