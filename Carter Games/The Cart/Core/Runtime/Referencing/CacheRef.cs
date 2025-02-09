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

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// Handles getting referencing for fields etc, mostly to save writing.
    /// </summary>
    public static class CacheRef
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the cache if its not null, or assigns it via the func if it needs assigning.
        /// </summary>
        /// <param name="cache">The cache to edit.</param>
        /// <param name="getAction">The action to get the reference.</param>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <returns>The assigned cache.</returns>
        public static T GetOrAssign<T>(ref T cache, Func<T> getAction)
        {
            if (cache != null) return cache;
            cache = getAction.Invoke();
            return cache;
        }
        
        
        /// <summary>
        /// Gets the cache if its not null, or assigns it via the reference passed in like a new instance of a class etc.
        /// </summary>
        /// <param name="cache">The cache to edit.</param>
        /// <param name="reference">The reference to assign if the cache needs assigning.</param>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <returns>The assigned cache.</returns>
        public static T GetOrAssign<T>(ref T cache, T reference)
        {
            if (cache != null) return cache;
            cache = reference;
            return cache;
        }
    }
}