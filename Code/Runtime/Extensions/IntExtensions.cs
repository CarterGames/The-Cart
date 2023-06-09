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
    /// An extension class for normal ints.
    /// </summary>
    public static class IntExtensions
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
        /// <param name="intRange">The minmax range to use</param>
        /// <returns>The normalised int.</returns>
        public static int Normalise(this int value, IntRange intRange)
        {
            return (value - intRange.min) / (intRange.max - intRange.min);
        }
    }
}