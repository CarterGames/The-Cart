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
using CarterGames.Cart.Core.Logs;
using UnityEngine;

namespace CarterGames.Cart.Modules
{
    [Serializable]
    public sealed class ModuleProcessQueue
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private List<ModuleChangeStateElement> data = new List<ModuleChangeStateElement>();
        [SerializeField] private int currentIndex = 0;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public ModuleChangeStateElement CurrentProcess => data[currentIndex];
        public int Count => data.Count;


        public bool HasNext => data.Count > currentIndex;

        public List<IModule> ModulesEditing
        {
            get
            {
                if (data.Count <= 0) return new List<IModule>();

                var modules = new List<IModule>();

                foreach (var mod in data)
                {
                    var elly = ModuleManager.AllModules.First(t => t.ModulePackagePath.Equals(mod.PackageFileLocation));
                    
                    if (modules.Contains(elly)) continue;
                    
                    modules.Add(elly);
                }

                return modules;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public void IncrementIndex()
        {
            currentIndex++;
        }


        public void AddToQueue(ModuleChangeStateElement change)
        {
            if (data.Any(t => t.Package == change.Package && t.FlowInUse == change.FlowInUse)) return;
            data.Add(change);
            
            CartLogger.Log<LogCategoryModules>($"Added module: {change.Package} to process queue.", typeof(ModuleProcessQueue));
        }
        
        
        public void ClearQueue()
        {
            data.Clear();
            currentIndex = 0;
        }
    }
}