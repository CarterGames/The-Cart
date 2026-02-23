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

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Random;

namespace CarterGames.Cart
{
    /// <summary>
    /// A collection of extension methods for dictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Checks to see if the dictionary is empty or null.
        /// </summary>
        /// <param name="dictionary">The dictionary to compare.</param>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <returns>Bool</returns>
        public static bool IsEmptyOrNull<TKey,TValue>(this Dictionary<TKey,TValue> dictionary)
        {
            if (dictionary == null) return true;
            return dictionary.Count <= 0;
        }
        
        
        /// <summary>
        /// Checks to see if the dictionary is empty.
        /// </summary>
        /// <param name="dictionary">The dictionary to compare.</param>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <returns>Bool</returns>
        public static bool IsEmpty<TKey,TValue>(this Dictionary<TKey,TValue> dictionary)
        {
            return dictionary.Count <= 0;
        }
        
        
        /// <summary>
        /// Returns a random element from the dictionary called from and returns the key of the random entry.
        /// </summary>
        /// <param name="dictionary">The dictionary to choose from.</param>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <returns>The element selected</returns>
        public static TKey RandomKey<TKey, TValue>(this Dictionary<TKey,TValue> dictionary)
        {
            return dictionary.Keys.ToArray()[Rng.Int(dictionary.Count - 1)];
        }
    }
}