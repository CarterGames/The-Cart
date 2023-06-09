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

using UnityEngine;

namespace Scarlet.General
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
    }
}