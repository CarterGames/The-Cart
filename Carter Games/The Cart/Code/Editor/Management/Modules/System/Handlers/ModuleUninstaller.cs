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

namespace CarterGames.Cart.Modules
{
    /// <summary>
    /// Handles uninstalling modules.
    /// </summary>
    public sealed class ModuleUninstaller
    {
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
            var toUninstall = new List<IModule>();
            ModuleManager.HasPrompted = false;

            if (ModuleManager.AllModules.Any(t =>
                    t.PreRequisites.Any(x => x.Equals(module))))
            {
                toUninstall.AddRange(ModuleManager.AllModules
                    .Where(t => t.PreRequisites.Any(x => x.Equals(module) && ModuleManager.IsEnabled(t)))
                    .ToList());
            }

            toUninstall.Add(module);

            if (toUninstall.Count > 1)
            {
                CscFileHandler.RemoveDefine(toUninstall);
            }
            else
            {
                CscFileHandler.RemoveDefine(toUninstall[0]);
            }
        }
    }
}