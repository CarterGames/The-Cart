/*
 * Copyright (c) 2024 Carter Games
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

using System;
using UnityEngine;

namespace CarterGames.Cart.Core
{
    public static class MathExtensions
    {
        /// <summary>
        /// Gets just the whole numbers & avoids the remainder.
        /// </summary>
        /// <remarks>E.g. 12.345 would turn into > 12 via this extension.</remarks>
        /// <param name="value">The value to edit</param>
        /// <returns>The converted value.</returns>
        public static float ToNumberOnly(this float value)
        {
            return (float)Math.Truncate(value);
        }
        
        
        /// <summary>
        /// Gets just the whole numbers & avoids the remainder.
        /// </summary>
        /// <remarks>E.g. 12.345 would turn into > 12 via this extension.</remarks>
        /// <param name="value">The value to edit</param>
        /// <returns>The converted value.</returns>
        public static double ToNumberOnly(this double value)
        {
            return Math.Truncate(value);
        }
        
        
        /// <summary>
        /// Gets just the decimal points and removed the whole numbers.
        /// </summary>
        /// <remarks>E.g. 12.345 would turn into > 0.345 via this extension.</remarks>
        /// <param name="value">The value to edit</param>
        /// <returns>The converted value.</returns>
        public static float ToDecimalOnly(this float value)
        {
            return value - (float)Math.Truncate(value);
        }
        
        
        /// <summary>
        /// Gets just the decimal points and removed the whole numbers.
        /// </summary>
        /// <remarks>E.g. 12.345 would turn into > 0.345 via this extension.</remarks>
        /// <param name="value">The value to edit</param>
        /// <returns>The converted value.</returns>
        public static double ToDecimalOnly(this double value)
        {
            return value - Math.Truncate(value);
        }


        /// <summary>
        /// Converts the number from a negative value to a positive one if it is negative to start with. 
        /// </summary>
        /// <param name="value">The value to edit</param>
        /// <returns>The converted value.</returns>
        public static float ToPositive(this float value)
        {
            if (value >= 0) return value;
            return Mathf.Abs(value);
        }
        
        
        /// <summary>
        /// Converts the number from a negative value to a positive one if it is negative to start with. 
        /// </summary>
        /// <param name="value">The value to edit</param>
        /// <returns>The converted value.</returns>
        public static double ToPositive(this double value)
        {
            if (value >= 0) return value;
            return Math.Abs(value);
        }
        
        
        /// <summary>
        /// Gets if the number a is within the threshold of the target b. 
        /// </summary>
        /// <param name="a">The number to check.</param>
        /// <param name="b">The target value.</param>
        /// <param name="threshold">The amount of lee-way to allow.</param>
        /// <returns>If the number a is close enough to the number b.</returns>
        public static bool Approximately(this float a, float b, float threshold)
        {
            return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= threshold;
        }
        
        
        /// <summary>
        /// Gets if the number a is within the threshold of the target b. 
        /// </summary>
        /// <param name="a">The number to check.</param>
        /// <param name="b">The target value.</param>
        /// <param name="threshold">The amount of lee-way to allow.</param>
        /// <returns>If the number a is close enough to the number b.</returns>
        public static bool Approximately(this double a, double b, double threshold)
        {
            return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= threshold;
        }


        /// <summary>
        /// Handy to check if a float equals a value.
        /// </summary>
        /// <param name="a">The starting float.</param>
        /// <param name="b">The float to check.</param>
        /// <returns>The result.</returns>
        public static bool FloatEquals(this float a, float b)
        {
            return Math.Abs(a - b) < float.Epsilon;
        }
        
        
        /// <summary>
        /// Handy to check if a double equals a value.
        /// </summary>
        /// <param name="a">The starting double.</param>
        /// <param name="b">The double to check.</param>
        /// <returns>The result.</returns>
        public static bool DoubleEquals(this double a, double b)
        {
            return Math.Abs(a - b) < double.Epsilon;
        }
    }
}