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
    /// An extension class for any array.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Checks to see if the array is empty or null.
        /// </summary>
        /// <param name="array">The array to compare.</param>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>Bool</returns>
        public static bool IsEmptyOrNull<T>(this T[] array)
        {
            if (array == null) return true;
            return array.Length <= 0;
        }
        
        
        /// <summary>
        /// Checks to see if the array is empty or null.
        /// </summary>
        /// <param name="array">The array to compare.</param>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>Bool</returns>
        public static bool IsEmpty<T>(this T[] array)
        {
            return array.Length <= 0;
        }
        
        
        /// <summary>
        /// Returns a random element from the list called from.
        /// </summary>
        /// <param name="array">The list to choose from</param>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>The element selected</returns>
        public static T RandomElement<T>(this T[] array)
        {
            return array[Rng.Int(array.Length - 1)];
        }
        
        
        /// <summary>
        /// Shuffles the list into a random order.
        /// </summary>
        /// <param name="array">The array to effect.</param>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>Bool</returns>
        public static bool Shuffle<T>(this T[] array)
        {
            var n = array.Length; 
            
            while (n > 1)
            {  
                n--;
                var k = Rng.Int(n + 1);
                // var value = list[k]; list[k] = list[n]; list[n] = value;
                (array[k], array[n]) = (array[n], array[k]);
            } 
            
            return array.Length <= 0;
        }


        /// <summary>
        /// Gets if there are any null entries in the array (missing or blank).
        /// </summary>
        /// <param name="array">The array to effect.</param>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>Bool</returns>
        public static bool HasNullEntries<T>(this T[] array)
        {
            if (array == null || array.Length <= 0) return false;

            for (var i = array.Length - 1; i > -1; i--)
            {
                if (array[i] != null) continue;
                return true;
            }

            return false;
        }
        
        
        /// <summary>
        /// Removes missing entries in a array when called.
        /// </summary>
        /// <param name="array">The array to edit.</param>
        /// <returns>The array with no null entries.</returns>
        public static T[] RemoveMissing<T>(this T[] array)
        {
            var newArray = new List<T>();
            
            for (var i = array.Length - 1; i > -1; i--)
            {
                if (array[i] == null) continue;
                newArray.Add(array[i]);
            }

            return newArray.ToArray();
        }
        
        
        /// <summary>
        /// Removed any duplicate entries from the array entered.
        /// </summary>
        /// <param name="array">The array to edit.</param>
        /// <typeparam name="T">The type for the array.</typeparam>
        /// <returns>The array with no null entries.</returns>
        public static T[] RemoveDuplicates<T>(this T[] array)
        {
            var validateList = new List<T>();

            for (var i = array.Length - 1; i > -1; i--)
            {
                if (validateList.Contains((T)array[i])) continue;
                validateList.Add((T)array[i]);
            }

            return validateList.ToArray();
        }
        
        
        /// <summary>
        /// Gets if the index is within the lists bounds.
        /// </summary>
        /// <param name="array">The list to edit.</param>
        /// <param name="index">The index to check for.</param>
        /// <typeparam name="T">The type for the list.</typeparam>
        /// <returns>If the index was inside the list.</returns>
        public static bool IsWithinIndex<T>(this T[] array, int index)
        {
            return index >= 0 && index < array.Length;
        }
    }
}