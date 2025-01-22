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

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core.Random;

namespace CarterGames.Cart.Core
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