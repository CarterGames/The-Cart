﻿/*
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
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Management;
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
            
            AssetDatabase.importPackageStarted -= OnImportPackageStarted;
            AssetDatabase.importPackageStarted += OnImportPackageStarted;
            
            AssetDatabase.importPackageCancelled -= OnImportPackageCancelled;
            AssetDatabase.importPackageCancelled += OnImportPackageCancelled;
            
            AssetDatabase.importPackageFailed -= OnImportPackageFailed;
            AssetDatabase.importPackageFailed += OnImportPackageFailed;
            
            AssetDatabase.importPackageCompleted -= OnImportPackageCompleted;
            AssetDatabase.importPackageCompleted += OnImportPackageCompleted;

            var toInstall = Array.Empty<IModule>();
            
            if (module.PreRequisites.Length > 0)
            {
                foreach (var preReq in module.PreRequisites)
                {
                    toInstall = toInstall.Add(preReq);
                }
            }
            
            toInstall = toInstall.Add(module);
            ModuleManager.ProcessQueue = toInstall.Select(t => t.ModulePackagePath).ToList();

            if (!ModuleManager.CurrentProcess.Equals(ModuleOperations.Updating))
            {
                ModuleManager.CurrentProcess = ModuleOperations.Installing;
                ModuleManager.IsProcessing = true;
            }
            
            AssetDatabase.StartAssetEditing();
            
            foreach (var mod in toInstall)
            {
                AssetDatabase.ImportPackage(mod.ModulePackagePath, false);
            }
            
            AssetDatabase.StopAssetEditing();
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Package Import Listeners
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static void OnImportPackageStarted(string packagename)
        {
            ModuleManager.IsProcessing = false;
        }
        

        private static void OnImportPackageCancelled(string packageName)
        {
            ModuleManager.IsProcessing = false;
        }

        
        private static void OnImportPackageCompleted(string packagename)
        {
            if (!ScriptableRef.AssetBasePath.Equals(("Assets")))
            {
                AssetDatabase.StartAssetEditing();
                
                foreach (var mod in ModuleManager.ProcessQueue)
                {
                    // Skip if in the right path already
                    var path = "Assets/";
                    path += ModuleManager.AllModules.First(t => t.ModulePackagePath.Equals(mod)).ModuleInstallPath
                        .Replace(ScriptableRef.AssetBasePath, string.Empty);
                    AssetDatabase.MoveAsset(path,
                        ModuleManager.AllModules.First(t => t.ModulePackagePath.Equals(mod)).ModuleInstallPath);
                }

                // Remove left over directories...
                AssetDatabase.DeleteAsset("Assets/Carter Games/The Cart/Modules");
                AssetDatabase.DeleteAsset("Assets/Carter Games/The Cart/");
                AssetDatabase.DeleteAsset("Assets/Carter Games/");
                
                AssetDatabase.StopAssetEditing();
            }
            
            ModuleManager.RefreshNamespaceCache();
            ModulesWindow.RepaintWindow();

            if (!ModuleManager.CurrentProcess.Equals(ModuleOperations.Installing)) return;
            ModuleManager.IsProcessing = false;
            ModuleManager.CurrentProcess = string.Empty;
        }

        
        private static void OnImportPackageFailed(string packagename, string errormessage)
        {
            ModuleManager.IsProcessing = false;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IAssetEditorReload Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Is raised when the editor reloads.
        /// </summary>
        public void OnEditorReloaded()
        {
            if (!ModuleManager.CurrentProcess.Equals(ModuleOperations.Installing)) return;
            
            ModuleManager.RefreshNamespaceCache();
            
            ModuleManager.CurrentProcess = string.Empty;
            ModuleManager.IsProcessing = false;
        }
    }
}