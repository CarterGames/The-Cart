/*
 * Copyright (c) 2018-Present Carter Games
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
using Scarlet.Logs;

namespace Scarlet.General
{
    /// <summary>
    /// A modular class to handle caching references of any class or object. 
    /// </summary>
    /// <typeparam name="T">The type to cache.</typeparam>
    public class CachedRef<T>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private T cache;
        private Func<T> getCacheAction;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the value in the cache.
        /// </summary>
        public T Value
        {
            get
            {
                if (cache != null) return cache;
                
                if (getCacheAction == null)
                {
                    ScarletLogs.Error(typeof(CachedRef<>),"Get action not defined, have you initialised the class?");
                    return default;
                }
                
                cache = getCacheAction.Invoke();

                if (cache != null) return cache;
                
                ScarletLogs.Error(typeof(CachedRef<>),"Cache is still null after get action, is the get action correctly defined?");
                return default;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Initialises the cache with the function to get the value on first run.
        /// </summary>
        /// <param name="getAction">The function to get the value.</param>
        public CachedRef(Func<T> getAction)
        {
            getCacheAction = getAction;
            cache = Value;
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Operator
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public static implicit operator T(CachedRef<T> cache)
        {
            return cache.Value;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Sets the get action to the entered function.
        /// </summary>
        /// <param name="getAction">The function to get the value.</param>
        public void SetGetAction(Func<T> getAction)
        {
            getCacheAction = getAction;
        }

        
        /// <summary>
        /// Clears the cache value to its default value.
        /// </summary>
        public void ClearCache()
        {
            cache = default;
        }
    }
}