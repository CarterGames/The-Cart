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
using System.Linq;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Logs;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Modules.Window;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules
{
    /// <summary>
    /// Handles installing modules.
    /// </summary>
    public sealed class ModuleInstaller : IAssetEditorReload
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The module that is being installed.
        /// </summary>
        public static IModule InstallingModule { get; private set; }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Installs the module entered when called.
        /// </summary>
        /// <param name="module">The module to install.</param>
        public static void Install(IModule module)
        {
            InstallingModule = module;
            
            var toInstall = Array.Empty<IModule>();
            
            if (module.PreRequisites.Length > 0)
            {
                foreach (var preReq in module.PreRequisites)
                {
                    if (ModuleManager.IsInstalled(preReq)) continue;
                    
                    toInstall = toInstall.Add(preReq);
                    ModuleManager.AddModuleToQueue(new ModuleChangeStateElement(preReq, ModuleOperations.Install));
                }
            }
            
            ModuleManager.AddModuleToQueue(new ModuleChangeStateElement(module, ModuleOperations.Install));
            
            InstallNextInQueue();
        }


        public static void InstallNextInQueue()
        {
            try
            {
                AssetDatabase.StartAssetEditing();
                AssetDatabase.ImportPackage(ModuleManager.CurrentProcess.PackageFileLocation, false);
                AssetDatabase.StopAssetEditing();
            }
#pragma warning disable
            catch (Exception e)
#pragma warning restore
            {
                CartLogger.LogError<LogCategoryModules>("Failed to install a module. Stopping the operation.", typeof(ModuleInstaller), true);
                ModuleManager.RefreshNamespaceCache();
                ModuleManager.ClearProcessQueue();
                
                if (ModuleManager.IsUpdating)
                {
                    ModuleManager.IsUpdating = false;
                }
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Package Import Listeners
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static void OnImportPackageCompleted()
        {
            var module = ModuleManager.AllModules.First(t =>
                t.ModulePackagePath.Equals(ModuleManager.CurrentProcess.PackageFileLocation));

            if (!PostImportFileMover.IsInRightPath(module))
            {
                CartLogger.Log<LogCategoryModules>("Module not in the right path, moving to the correct path.", typeof(ModuleInstaller), true);
                PostImportFileMover.UpdateFileLocation(module);
            }
            
            ModuleManager.RefreshNamespaceCache();
            ModulesWindow.RepaintWindow();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IAssetEditorReload Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Is raised when the editor reloads.
        /// </summary>
        public void OnEditorReloaded()
        {
            if (ModuleManager.IsUpdating) return;
            if (ModuleManager.CurrentProcess == null) return;
            if (!ModuleManager.CurrentProcess.FlowInUse.Equals(ModuleOperations.Install)) return;

            OnImportPackageCompleted();
            
            ModuleManager.RefreshNamespaceCache();
            
            CartLogger.Log<LogCategoryModules>($"Module {ModuleManager.CurrentProcess.Package} Installed.", typeof(ModuleInstaller), true);
            
            if (ModuleManager.TryProcessNext()) return;

            ModuleManager.ClearProcessQueue();
        }
    }
}