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

namespace CarterGames.Cart.Modules
{
    /// <summary>
    /// An interface to define a module
    /// </summary>
    public interface IModule
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The name of the module.
        /// </summary>
        string ModuleName { get; } 
        
        
        /// <summary>
        /// A description of what the module does.
        /// </summary>
        string ModuleDescription { get; }
        
        
        /// <summary>
        /// The author of the module.
        /// </summary>
        string ModuleAuthor { get; }
        
        
        /// <summary>
        /// Any modules that are required for the module to work.
        /// </summary>
        IModule[] PreRequisites { get; }
        
        
        /// <summary>
        /// Any modules that are required for the module to work.
        /// </summary>
        IModule[] OptionalPreRequisites { get; }
        
        
        /// <summary>
        /// The scripting define for the module.
        /// </summary>
        string ModuleDefine { get; }
    }
}