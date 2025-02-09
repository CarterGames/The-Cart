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

using System.Collections.Generic;
using UnityEditor;

namespace CarterGames.Cart.Modules
{
    /// <summary>
    /// Handles installing modules.
    /// </summary>
    public sealed class ModuleInstaller
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Installs the module entered when called.
        /// </summary>
        /// <param name="module">The module to install.</param>
        public static void Install(IModule module)
        {
            var toInstall = new List<IModule>();
            
            if (module.PreRequisites.Length > 0)
            {
                foreach (var preReq in module.PreRequisites)
                {
                    if (ModuleManager.IsEnabled(preReq)) continue;
                    toInstall.Add(preReq);
                }
            }
            
            toInstall.Add(module);


            if (toInstall.Count > 1)
            {
                CscFileHandler.AddDefine(toInstall);
            }
            else
            {
                CscFileHandler.AddDefine(toInstall[0]);
            }
            
            AssetDatabase.Refresh();
        }
    }
}