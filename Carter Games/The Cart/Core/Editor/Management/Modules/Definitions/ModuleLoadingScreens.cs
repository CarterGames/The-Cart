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
using CarterGames.Cart.Core.Management.Editor;

namespace CarterGames.Cart.Modules
{
    /// <summary>
    /// A definition for the loading screen module
    /// </summary>
    public sealed class ModuleLoadingScreens : IModule
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The name of the module.
        /// </summary>
        public string ModuleName => "Loading Screens";
        
        
        /// <summary>
        /// A description of what the module does.
        /// </summary>
        public string ModuleDescription => "A setup to show basic loading screens when needed.";
        
        
        public string ModuleAuthor => "Carter Games";
        
        
        /// <summary>
        /// Any modules that are required for the module to work.
        /// </summary>
        public IModule[] PreRequisites => Array.Empty<IModule>();
        
        public IModule[] OptionalPreRequisites => Array.Empty<IModule>();
        
        public string ModuleDefine => "CARTERGAMES_CART_MODULE_LOADINGSCREENS";
    }
}