// ----------------------------------------------------------------------------
// ArrayExtension.cs
// 
// Description: An extension class for arrays.
// ----------------------------------------------------------------------------

namespace Scarlet.General
{
    /// <summary>
    /// An extension class for any array.
    /// </summary>
    public static class ArrayExtension
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
    }
}