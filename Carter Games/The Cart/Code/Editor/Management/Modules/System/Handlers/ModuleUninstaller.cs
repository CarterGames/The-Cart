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
using System.Text;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;

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
            ModuleManager.CurrentProcess = ModuleOperations.Uninstalling;

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
            ModuleManager.ProcessQueue = toUninstall;

            if (toUninstall.Count > 1 && ModuleManager.CurrentProcess.Equals(ModuleOperations.Uninstalling) && !ModuleManager.HasPrompted)
            {
                Builder.Clear();
                Builder.Append("Uninstalling this module will also uninstall:");
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
                    ModuleManager.ProcessQueue = new List<string>();
                    return;
                }
                else
                {
                    ModuleManager.HasPrompted = true;
                }
            }

            ModuleManager.IsProcessing = true;
            
            AssetDatabase.StartAssetEditing();

            foreach (var toRemove in ModuleManager.ProcessQueue)
            {
                DeleteDirectoryAndContents(ModuleManager.AllModules.First(t => t.ModuleInstallPath.Equals(toRemove)));
            }

            AssetDatabase.StopAssetEditing();
        }


        /// <summary>
        /// Actually deleted the directory entered along with any contents. 
        /// </summary>
        /// <param name="module">The module to uninstall.</param>
        private static void DeleteDirectoryAndContents(IModule module)
        {
            AssetDatabase.DeleteAsset(module.ModuleInstallPath);
            CartSoAssetAccessor.GetAsset<ModuleCache>().RemoveInstalledInfo(module);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IAssetEditorReload Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Is raised when the editor reloads.
        /// </summary>
        public void OnEditorReloaded()
        {
            if (!ModuleManager.CurrentProcess.Equals(ModuleOperations.Uninstalling)) return;
            
            ModuleManager.RefreshNamespaceCache();
            
            ModuleManager.CurrentProcess = string.Empty;
            ModuleManager.IsProcessing = false;
        }
    }
}