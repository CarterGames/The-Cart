// ----------------------------------------------------------------------------
// ListExtension.cs
// 
// Description: An extension class for lists.
// ----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Scarlet.General
{
    public static class ListExtension
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
    }
}