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

using CarterGames.Cart.Core.Logs;
using CarterGames.Cart.Core.Management.Editor;

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
            ModuleManager.UpdatingModule = module.ModuleName;
            
            // Uninstall and re-install the module by hard delete of old first...
            ModuleManager.IsUpdating = true;
            
            ModuleManager.AddModuleToQueue(new ModuleChangeStateElement(module, ModuleOperations.Uninstall));
            ModuleManager.AddModuleToQueue(new ModuleChangeStateElement(module, ModuleOperations.Install));

            ModuleManager.CurrentProcess.Process();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IAssetEditorReload Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Is raised when the editor reloads.
        /// </summary>
        public void OnEditorReloaded()
        {
            if (!ModuleManager.IsUpdating) return;
            if (ModuleManager.CurrentProcess == null) return;
            
            
            ModuleManager.RefreshNamespaceCache();
            
            if (ModuleManager.TryProcessNext())
            {
                CartLogger.Log<LogCategoryModules>("Continue to next step.", typeof(ModuleUpdater), true);
                return;
            }
            
            
            ModuleManager.UpdateModuleInstallInfo(ModuleManager.ProcessQueue.ModulesEditing[0]);
            
            ModuleManager.ClearProcessQueue();
            ModuleManager.IsUpdating = false;
            
            CartLogger.Log<LogCategoryModules>($"Module {ModuleManager.CurrentProcess.Package} updated.", typeof(ModuleUpdater), true);
        }
    }
}