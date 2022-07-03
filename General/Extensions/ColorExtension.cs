// ----------------------------------------------------------------------------
// ColorExtension.cs
// 
// Description: An extension class for color's
// ----------------------------------------------------------------------------

using UnityEngine;

namespace Scarlet.General
{
    /// <summary>
    /// An extension class for color's
    /// </summary>
    public static class ColorExtension
    {
        /// <summary>
        /// Compares the two colours and see's if it matches.
        /// </summary>
        /// <param name="color">The color to edit</param>
        /// <param name="b">The second color to edit</param>
        /// <param name="compareAlpha">Should the check match the alpha</param>
        /// <returns>Bool</returns>
        public static bool Equals(this Color color, Color b, bool compareAlpha = false)
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
        /// <returns>The edited Color</returns>
        public static Color SetAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }


        /// <summary>
        /// Changes any value of the color to the new value entered.
        /// </summary>
        /// <param name="color">The color to edit.</param>
        /// <param name="r">The new R value.</param>
        /// <param name="g">The new G value.</param>
        /// <param name="b">The new B value.</param>
        /// <param name="a">The new A value.</param>
        /// <returns>The edited Color</returns>
        public static Color With(this Color color, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            return new Color(r ?? color.r, g ?? color.g, b ?? color.b, a ?? color.a);
        }
    }
}