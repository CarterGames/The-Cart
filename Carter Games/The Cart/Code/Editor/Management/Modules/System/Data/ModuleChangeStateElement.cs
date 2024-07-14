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
using UnityEngine;

namespace CarterGames.Cart.Modules
{
    [Serializable]
    public class ModuleChangeStateElement
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private string packageNamespace;
        [SerializeField] private string packageFileLocation;
        [SerializeField] private string packageInstallLocation;
        [SerializeField] private string flow;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public string Package => packageNamespace;
        public string PackageFileLocation => packageFileLocation;
        public string PackageInstallLocation => packageInstallLocation;
        public string FlowInUse => flow;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public ModuleChangeStateElement() {}
        

        public ModuleChangeStateElement(IModule module, string flow)
        {
            packageNamespace = module.Namespace;
            packageInstallLocation = module.ModuleInstallPath;
            packageFileLocation = module.ModulePackagePath;
            
            this.flow = flow;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public void Process()
        {
            switch (FlowInUse)
            {
                case ModuleOperations.Install:
                    ModuleInstaller.InstallNextInQueue();
                    break;
                case ModuleOperations.Uninstall:
                    ModuleUninstaller.UninstallFromQueue();
                    break;
                default:
                    break;
            }
        }
        

        public void ResetData()
        {
            packageNamespace = string.Empty;
            flow = string.Empty;
        }
    }
}