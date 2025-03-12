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
using CarterGames.Cart.Core.Management;

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// A base class for formatters.
    /// </summary>
    /// <typeparam name="TFormatter">The type of formatter interface to use.</typeparam>
    public sealed class Formatter<TFormatter>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private Dictionary<Type, TFormatter> formatterLookupCache;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private Dictionary<Type, TFormatter> FormatterLookup => CacheRef.GetOrAssign(ref formatterLookupCache, GetFormatters);
        
        
        private static Dictionary<Type, TFormatter> GetFormatters()
        {
            var lookup = new Dictionary<Type, TFormatter>();
            var formatters = AssemblyHelper.GetClassesOfType<TFormatter>();

            foreach (var formatter in formatters)
            {
                lookup.Add(formatter.GetType(), formatter);
            }

            return lookup;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Creates a new formatter instance if one doesn't exist at the time of requesting.
        /// </summary>
        /// <typeparam name="T">The type to create.</typeparam>
        /// <returns>The formatter to use.</returns>
        private TFormatter Create<T>() where T : TFormatter
        {
            var formatter = Activator.CreateInstance<T>();
            FormatterLookup.Add(typeof(T), formatter);
            return formatter;
        }


        /// <summary>
        /// Gets or make the formatter requested for use.
        /// </summary>
        /// <typeparam name="T">The type to create.</typeparam>
        /// <returns>The formatter to use.</returns>
        public TFormatter Get<T>() where T : TFormatter
        {
            return FormatterLookup.ContainsKey(typeof(T)) 
                ? FormatterLookup[typeof(T)] 
                : Create<T>();
        }
    }
}