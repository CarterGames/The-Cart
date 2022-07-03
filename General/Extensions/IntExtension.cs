// ----------------------------------------------------------------------------
// IntExtension.cs
// 
// Description: An extension class for normal ints.
// ----------------------------------------------------------------------------

using UnityEngine;

namespace Scarlet.General
{
    /// <summary>
    /// An extension class for normal ints.
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// Inverts the value...
        /// </summary>
        /// <param name="original">The value to edit...</param>
        /// <returns>The inverted value</returns>
        public static int Invert(this int original)
        {
            return original * -1;
        }
        
        
        /// <summary>
        /// Inverts the value between 0-1...
        /// </summary>
        /// <param name="original">The value to edit...</param>
        /// <returns>The inverted value</returns>
        public static int Invert01(this int original)
        {
            return (int) Mathf.Clamp01(1f - original);
        }


        /// <summary>
        /// Converts an int to a bool anchored around 0.
        /// </summary>
        /// <param name="original">The int to use.</param>
        /// <returns>1+ = True, 0 or lower False.</returns>
        public static bool ParseAsBool(this int original)
        {
            return original > 0;
        }
        
        
        /// <summary>
        /// Normalises the int between a min & max.
        /// </summary>
        /// <param name="value">The value to edit.</param>
        /// <param name="max">The max value</param>
        /// <param name="min">The min value, is 0 by default.</param>
        /// <returns>The normalised int.</returns>
        public static int Normalise(this int value, int max, int min = 0)
        {
            return (value - min) / (max - min);
        }
        
        
        /// <summary>
        /// Normalises the int between a min & max.
        /// </summary>
        /// <param name="value">The value to edit.</param>
        /// <param name="minMaxInt">The minmax range to use</param>
        /// <returns>The normalised int.</returns>
        public static int Normalise(this int value, MinMaxInt minMaxInt)
        {
            return (value - minMaxInt.min) / (minMaxInt.max - minMaxInt.min);
        }
    }
}