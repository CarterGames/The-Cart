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

using UnityEngine;

namespace CarterGames.Cart
{
    /// <summary>
    /// An extension class for color's
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Compares the two colours and see's if it matches.
        /// </summary>
        /// <param name="col">The color to edit</param>
        /// <param name="b">The second color to edit</param>
        /// <param name="compareAlpha">Should the check match the alpha</param>
        /// <returns>Bool</returns>
        public static bool Equals(this Color col, Color b, bool compareAlpha = false)
        {
            return compareAlpha 
                ? Mathf.Approximately(col.r, b.r) && Mathf.Approximately(col.g, b.g) && Mathf.Approximately(col.b, b.b) && Mathf.Approximately(col.a, b.a)
                : Mathf.Approximately(col.r, b.r) && Mathf.Approximately(col.g, b.g) && Mathf.Approximately(col.b, b.b);
        }
        
        
        /// <summary>
        /// Changes the alpha of the color without effect the other values.
        /// </summary>
        /// <param name="col">The colour to edit</param>
        /// <param name="alpha">The alpha value to set</param>
        /// <returns>The edited Color</returns>
        public static Color SetAlpha(this Color col, float alpha)
        {
            return new Color(col.r, col.g, col.b, alpha);
        }


        /// <summary>
        /// Changes any value of the color to the new value entered.
        /// </summary>
        /// <param name="col">The color to edit.</param>
        /// <param name="r">The new R value.</param>
        /// <param name="g">The new G value.</param>
        /// <param name="b">The new B value.</param>
        /// <param name="a">The new A value.</param>
        /// <returns>The edited Color</returns>
        public static Color With(this Color col, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            return new Color(r ?? col.r, g ?? col.g, b ?? col.b, a ?? col.a);
        }
        
        
        /// <summary>
        /// Changes any value of the color to the new value entered.
        /// </summary>
        /// <param name="col">The color to edit.</param>
        /// <param name="r">The new R value.</param>
        /// <param name="g">The new G value.</param>
        /// <param name="b">The new B value.</param>
        /// <param name="a">The new A value.</param>
        /// <returns>The edited Color</returns>
        public static Color Adjust(this Color col, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            return new Color(col.r + r ?? col.r, col.g + g ?? col.g, col.b + b ?? col.b, col.a + a ?? col.a);
        }
        
        
        /// <summary>
        /// Inverts the colour entered.
        /// </summary>
        /// <param name="color">The colour to edit.</param>
        /// <returns>The inverted colour.</returns>
        public static Color Invert(this Color color)
        {
            return new Color(1.0f - color.r, 1.0f - color.g, 1.0f - color.b);
        }
    }
}