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
using CarterGames.Cart.Random;

namespace CarterGames.Cart
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
                
                var k = Rng.Int(n);
                
                // ReSharper disable once SwapViaDeconstruction
                // Easier to read and understand written this way.
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
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
        
        
        /// <summary>
        /// Gets the total indexes in an list (so the index count -1)
        /// </summary>
        /// <param name="list">The list to edit.</param>
        /// <typeparam name="T">The type for the list.</typeparam>
        /// <returns>The index count.</returns>
        public static int IndexCount<T>(this IList<T> list) => list.Count - 1;
    }
}