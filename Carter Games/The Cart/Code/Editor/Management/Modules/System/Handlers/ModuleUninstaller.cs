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
using System.Linq;
using System.Text;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Logs;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules
{
    /// <summary>
    /// Handles uninstalling modules.
    /// </summary>
    public sealed class ModuleUninstaller : IAssetEditorReload
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static readonly StringBuilder Builder = new StringBuilder();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Uninstalls the module entered when called.
        /// </summary>
        /// <remarks>Will also uninstall dependants of the module if installed. Prompted if that is the case</remarks>
        /// <param name="module">The module to uninstall.</param>
        public static void Uninstall(IModule module)
        {
            var toUninstall = new List<string>();
            ModuleManager.HasPrompted = false;

            if (ModuleManager.AllModules.Any(t =>
                    t.PreRequisites.Any(x => x.ModuleInstallPath.Equals(module.ModuleInstallPath))))
            {
                toUninstall.AddRange(ModuleManager.AllModules
                    .Where(t => t.PreRequisites.Any(x =>
                        x.ModuleInstallPath.Equals(module.ModuleInstallPath) && ModuleManager.IsInstalled(t)))
                    .Select(t => t.ModuleInstallPath)
                    .ToList());
            }

            toUninstall.Add(module.ModuleInstallPath);

            
            foreach (var toProcess in toUninstall)
            {
                var moduleData = ModuleManager.AllModules.First(t => t.ModuleInstallPath.Equals(toProcess));
                
                if (!ModuleManager.IsInstalled(moduleData)) continue;
                ModuleManager.AddModuleToQueue(new ModuleChangeStateElement(moduleData, ModuleOperations.Uninstall));
            }
            

            if (toUninstall.Count > 1 && ModuleManager.CurrentProcess.FlowInUse.Equals(ModuleOperations.Uninstall) && !ModuleManager.HasPrompted)
            {
                Builder.Clear();
                Builder.Append("This module is a dependency for some other modules.");
                Builder.AppendLine();
                Builder.AppendLine();
                Builder.Append("Proceeding with the uninstall will also uninstall these modules:");
                Builder.AppendLine();
                Builder.AppendLine();

                for (var i = 0; i < toUninstall.Count - 1; i++)
                {
                    Builder.Append("- ");
                    Builder.Append(ModuleManager.AllModules.First(t => t.ModuleInstallPath.Equals(toUninstall[i]))
                        .ModuleName);
                    Builder.AppendLine();
                }

                Builder.AppendLine();
                Builder.Append("Are you sure you want to continue?");

                if (!Dialogue.Display("Uninstall " + module.ModuleName, Builder.ToString(), "Uninstall", "Cancel"))
                {
                    ModuleManager.ClearProcessQueue();
                    return;
                }
                else
                {
                    ModuleManager.HasPrompted = true;
                }
            }

            
            try
            {
                AssetDatabase.StartAssetEditing();
                DeleteDirectoryAndContents(ModuleManager.AllModules.First(t => t.ModuleInstallPath.Equals(ModuleManager.CurrentProcess.PackageInstallLocation)));
                AssetDatabase.StopAssetEditing();
            }
#pragma warning disable
            catch (Exception e)
#pragma warning restore
            {
                CartLogger.LogError<LogCategoryModules>("Failed to uninstall a module. Stopping the operation.", typeof(ModuleUninstaller),true);
                ModuleManager.RefreshNamespaceCache();
                ModuleManager.ClearProcessQueue();

                if (ModuleManager.IsUpdating)
                {
                    ModuleManager.IsUpdating = false;
                }
            }
        }


        /// <summary>
        /// Actually deleted the directory entered along with any contents. 
        /// </summary>
        /// <param name="module">The module to uninstall.</param>
        private static void DeleteDirectoryAndContents(IModule module)
        {
            AssetDatabase.DeleteAsset(module.ModuleInstallPath);
            DataAccess.GetAsset<ModuleCache>().RemoveInstalledInfo(module);
        }


        public static void UninstallFromQueue()
        {
            AssetDatabase.StartAssetEditing();
            DeleteDirectoryAndContents(ModuleManager.AllModules.First(t => t.ModuleInstallPath.Equals(ModuleManager.CurrentProcess.PackageInstallLocation)));
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
            if (ModuleManager.IsUpdating) return;
            if (ModuleManager.CurrentProcess == null) return;
            if (!ModuleManager.CurrentProcess.FlowInUse.Equals(ModuleOperations.Uninstall)) return;
            
            ModuleManager.RefreshNamespaceCache();

            CartLogger.Log<LogCategoryModules>($"Module {ModuleManager.CurrentProcess.Package} uninstalled.", typeof(ModuleUninstaller),true);
            
            if (ModuleManager.TryProcessNext()) return;
            
            ModuleManager.ClearProcessQueue();
        }
    }
}