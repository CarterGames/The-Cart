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

namespace CarterGames.Cart
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