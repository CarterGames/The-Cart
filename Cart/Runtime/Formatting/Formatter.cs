/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using System;
using System.Collections.Generic;
using CarterGames.Cart.Management;

namespace CarterGames.Cart
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