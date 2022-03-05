// ----------------------------------------------------------------------------
// ColorExtensions.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 22/12/2021
// ----------------------------------------------------------------------------

using UnityEngine;

namespace JTools
{
    public static class ColorExtension
    {
        /// <summary>
        /// Compares the two colours and see's if it matches.
        /// </summary>
        /// <param name="color">The color to edit</param>
        /// <param name="b">The second color to edit</param>
        /// <param name="compareAlpha">Should the check match the alpha</param>
        /// <returns>Bool</returns>
        public static bool Match(this Color color, Color b, bool compareAlpha = false)
        {
            return compareAlpha 
                ? Mathf.Approximately(color.r, b.r) && Mathf.Approximately(color.g, b.g) && Mathf.Approximately(color.b, b.b) && Mathf.Approximately(color.a, b.a)
                : Mathf.Approximately(color.r, b.r) && Mathf.Approximately(color.g, b.g) && Mathf.Approximately(color.b, b.b);
        }
        
        
        /// <summary>
        /// Changes the alpha of the color without effect the other values.
        /// </summary>
        /// <param name="color">The colour to edit</param>
        /// <param name="alpha">The alpha value to set</param>
        /// <returns>Color</returns>
        public static Color SetAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        
        /// <summary>
        /// Swaps the color to the second colour entered without changing the alpha value.
        /// </summary>
        /// <param name="color">The color to edit</param>
        /// <param name="b">the color to swap to</param>
        /// <returns>The Changed Color</returns>
        public static Color Swap(this Color color, Color b)
        {
            return new Color(b.r, b.g, b.b, color.a);
        }


        public static Color With(this Color color, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            return new Color(r ?? color.r, g ?? color.g, b ?? color.b, a ?? color.a);
        }
    }
}