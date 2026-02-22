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
using System.Linq;

namespace CarterGames.Cart.Crates
{
    /// <summary>
    /// Handles uninstalling crates.
    /// </summary>
    public sealed class CrateUninstaller
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Uninstalls the crate entered when called.
        /// </summary>
        /// <remarks>Will also uninstall dependants of the crate if installed. Prompted if that is the case</remarks>
        /// <param name="crate">The crate to uninstall.</param>
        public static void Uninstall(Crate crate)
        {
            var toUninstall = new List<Crate>();
            CrateManager.HasPrompted = false;

            if (CrateManager.AllCrates.Any(t =>
                    t.PreRequisites.Any(x => x.Equals(crate))))
            {
                toUninstall.AddRange(CrateManager.AllCrates
                    .Where(t => t.PreRequisites.Any(x => x.Equals(crate) && CrateManager.IsEnabled(t)))
                    .ToList());
            }

            toUninstall.Add(crate);

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