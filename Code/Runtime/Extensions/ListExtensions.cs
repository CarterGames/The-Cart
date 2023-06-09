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

using System.Collections.Generic;
using Scarlet.Random;

namespace Scarlet.General
{
    /// <summary>
    /// An extension class for lists.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Checks to see if the list is empty or null.
        /// </summary>
        /// <param name="list">The list to compare.</param>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>Bool</returns>
        public static bool IsEmptyOrNull<T>(this IList<T> list)
        {
            if (list == null) return true;
            return list.Count <= 0;
        }
        
        
        /// <summary>
        /// Checks to see if the list is empty or null.
        /// </summary>
        /// <param name="list">The list to compare.</param>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>Bool</returns>
        public static bool IsEmpty<T>(this IList<T> list)
        {
            return list.Count <= 0;
        }


        /// <summary>
        /// Returns a random element from the list called from.
        /// </summary>
        /// <param name="list">The list to choose from</param>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>The element selected</returns>
        public static T RandomElement<T>(this IList<T> list)
        {
            return list[Rng.Int(list.Count - 1)];
        }


        /// <summary>
        /// Shuffles the list into a random order.
        /// </summary>
        /// <param name="list">The list to effect.</param>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>Bool</returns>
        public static bool Shuffle<T>(this IList<T> list)
        {
            var n = list.Count; 
            
            while (n > 1)
            {  
                n--;
                var k = Rng.Int(n + 1);
                // var value = list[k]; list[k] = list[n]; list[n] = value;
                (list[k], list[n]) = (list[n], list[k]);
            } 
            
            return list.Count <= 0;
        }
        
        
        /// <summary>
        /// Checks to see if a list has any null entries.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <returns>If there was a null entry or not.</returns>
        public static bool HasNullEntries<T>(this IList<T> list)
        {
            if (list == null || list.Count <= 0) return false;

            for (var i = list.Count - 1; i > -1; i--)
            {
                if (list[i] != null) continue;
                return true;
            }

            return false;
        }


        /// <summary>
        /// Removes missing entries in a list when called.
        /// </summary>
        /// <param name="list">The list to edit.</param>
        /// <returns>The list with no null entries.</returns>
        public static IList<T> RemoveMissing<T>(this IList<T> list)
        {
            for (var i = list.Count - 1; i > -1; i--)
            {
                if (list[i] == null)
                {
                    list.RemoveAt(i);
                }
            }

            return list;
        }


        /// <summary>
        /// Removed any duplicate entries from the list entered.
        /// </summary>
        /// <param name="list">The list to edit.</param>
        /// <typeparam name="T">The type for the list.</typeparam>
        /// <returns>The list with no null entries.</returns>
        public static IList<T> RemoveDuplicates<T>(this IList<T> list)
        {
            var validateList = new List<T>();

            for (var i = list.Count - 1; i > -1; i--)
            {
                if (validateList.Contains((T)list[i])) continue;
                validateList.Add((T)list[i]);
            }

            return validateList;
        }
        
        
        /// <summary>
        /// Gets if the index is within the lists bounds.
        /// </summary>
        /// <param name="list">The list to edit.</param>
        /// <param name="index">The index to check for.</param>
        /// <typeparam name="T">The type for the list.</typeparam>
        /// <returns>If the index was inside the list.</returns>
        public static bool IsWithinIndex<T>(this IList<T> list, int index)
        {
            return index >= 0 && index < list.Count;
        }
    }
}