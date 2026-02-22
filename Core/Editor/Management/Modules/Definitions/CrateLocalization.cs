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

using System;
using System.Collections.Generic;

namespace CarterGames.Cart.Crate
{
    /// <summary>
    /// The definition for the localization crate
    /// </summary>
    public sealed class CrateLocalization : ICrate, IGitPackageDependency
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The name of the crate.
        /// </summary>
        public string Name => "Localization";
        
        
        /// <summary>
        /// A description of what the crate does.
        /// </summary>
        public string Description => "A system to handle elements that need to change for different languages.";
        
        
        /// <summary>
        /// The author of the crate.
        /// </summary>
        public string Author => "Carter Games";
        
        
        /// <summary>
        /// Any modules that are required for the crate to work.
        /// </summary>
        public ICrate[] PreRequisites => Array.Empty<ICrate>();

        
        /// <summary>
        /// Any optional modules that the crate can use.
        /// </summary>
        public ICrate[] OptionalPreRequisites => new ICrate[] { new CrateHierarchy() };
        
        
        /// <summary>
        /// The scripting define the crate uses.
        /// </summary>
        public string Define => "CARTERGAMES_CART_CRATE_LOCALIZATION";


        public List<GitPackageInfo> Packages => new List<GitPackageInfo>()
        {
            new GitPackageInfo("Notion Data", "games.carter.standalone.notiondata", "https://github.com/CarterGames/NotionToUnity.git", new Version(0,7,0))
        };
    }
}