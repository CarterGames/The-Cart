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
    /// <summary>
    /// A load of extensions for Unity Vector2's
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Adds the two vectors together returning the result.
        /// </summary>
        /// <param name="vec2">The starting vector.</param>
        /// <param name="value">The value to add.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Add(this Vector2 vec2, Vector2 value)
        {
            return vec2 + value;
        }
     
        
        /// <summary>
        /// Adds the two vectors together returning the result.
        /// </summary>
        /// <param name="vec2">The starting vector.</param>
        /// <param name="x">The x value to add.</param>
        /// <param name="y">The y value to add.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Add2F(this Vector2 vec2, float? x = null, float? y = null)
        {
            return new Vector2(vec2.x + x ?? 0, vec2.y + y ?? 0);
        }
        
        
        /// <summary>
        /// Adjusts the incoming vector2 by the entered value.
        /// </summary>
        /// <param name="vec2">The starting vector.</param>
        /// <param name="x">The x adjustment,</param>
        /// <param name="y">The y adjustment.</param>
        /// <returns>The adjusted vector.</returns>
        public static Vector2 Adjust(this Vector2 vec2, float? x = null, float? y = null)
        {
            return new Vector2(vec2.x + x ?? vec2.x, vec2.y + y ?? vec2.y);
        }

        
        /// <summary>
        /// Clamps the vector to the entered vectors.
        /// </summary>
        /// <param name="vec2">The starting vector.</param>
        /// <param name="min">The min vector.</param>
        /// <param name="max">The max vector.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Clamp(this Vector2 vec2, Vector2 min, Vector2 max)
        {
            return new Vector2(Mathf.Clamp(vec2.x, min.x, max.x), Mathf.Clamp(vec2.y, min.y, max.y));
        }
        
        
        /// <summary>
        /// Clamps the vector to the entered vectors.
        /// </summary>
        /// <param name="vec2">The starting vector.</param>
        /// <param name="min">The min value per co-ord.</param>
        /// <param name="max">The max value per co-ord.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Clamp2F(this Vector2 vec2, float min, float max)
        {
            return new Vector2(Mathf.Clamp(vec2.x, min, max), Mathf.Clamp(vec2.y, min, max));
        }


        /// <summary>
        /// Copies the vector into a new instance.
        /// </summary>
        /// <param name="vec2">The starting to copy.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Copy(this Vector2 vec2)
        {
            return new Vector2(vec2.x, vec2.y);
        }
        
        
        /// <summary>
        /// Gets a line from the entered vectors
        /// </summary>
        /// <param name="vec2">The start vector.</param>
        /// <param name="target">The end vector.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 DirectionTo(this Vector2 vec2, Vector2 target)
        {
            return (target - vec2).normalized;
        }


        /// <summary>
        /// Divides the vector by another vector.
        /// </summary>
        /// <param name="vec2">The start vector.</param>
        /// <param name="value">The value to divide by.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Divide(this Vector2 vec2, Vector2 value)
        {
            return new Vector2(vec2.x / value.x, vec2.y / value.y);
        }
        
        
        /// <summary>
        /// Divides the vector by the entered x/y values.
        /// </summary>
        /// <param name="vec2">The start vector.</param>
        /// <param name="x">The x value to divide by.</param>
        /// <param name="y">The y value to divide by.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Divide2F(this Vector2 vec2, float? x = null, float? y = null)
        {
            return new Vector2(vec2.x / x ?? 1, vec2.y / y ?? 1);
        }
        
        
        /// <summary>
        /// Gets if the vector has equalling x/y values.
        /// </summary>
        /// <param name="vec2">The start vector.</param>
        /// <param name="x">The x value to check.</param>
        /// <param name="y">The y value to check.</param>
        /// <returns>The processed vector2.</returns>
        public static bool Equals2F(this Vector2 vec2, float? x = null, float? y = null)
        {
            return Math.Abs(vec2.x - (x ?? 1)) < float.Epsilon && Math.Abs(vec2.y - (y ?? 1)) < float.Epsilon;
        }
        
        
        /// <summary>
        /// Gets a line from a to b.
        /// </summary>
        /// <param name="vec2">The start vector.</param>
        /// <param name="target">The target vector.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Line(this Vector2 vec2, Vector2 target)
        {
            return (target - vec2).normalized + vec2;
        }
        
        
        /// <summary>
        /// Gets a line from a to b.
        /// </summary>
        /// <param name="vec2">The start vector.</param>>
        /// <param name="target">The target vector.</param>
        /// <param name="distance">The length the line should extend to.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Line(this Vector2 vec2, Vector2 target, float distance)
        {
            return ((target - vec2).normalized + vec2) * distance;
        }
        
        
        /// <summary>
        /// Gets a middle point of a line between a & b.
        /// </summary>
        /// <param name="vec2">The start vector.</param>
        /// <param name="target">The target vector.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 MidPoint(this Vector2 vec2, Vector2 target)
        {
            return (vec2 + target) * .5f;
        }
        
        
        /// <summary>
        /// Multiplies the vector by the entered vector.
        /// </summary>
        /// <param name="vec2">The start vector.</param>
        /// <param name="value">The vector to multiply by.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Multiply(this Vector2 vec2, Vector2 value)
        {
            return new Vector2(vec2.x * value.x, vec2.y * value.y);
        }
        
        
        /// <summary>
        /// Multiplies the vector by the entered float scale.
        /// </summary>
        /// <param name="vec2">The start vector.</param>
        /// <param name="value">The scale to apply to the vector.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 MultiplyScalar(this Vector2 vec2, float value)
        {
            return new Vector2(vec2.x * value, vec2.y * value);
        }
        
        
        /// <summary>
        /// Multiplies the vector by the entered x/y scale.
        /// </summary>
        /// <param name="vec2">The start vector.</param>
        /// <param name="x">The x value to multiply by.</param>
        /// <param name="y">The y value to multiply by.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Multiply2F(this Vector2 vec2, float? x = null, float? y = null)
        {
            return new Vector2(vec2.x * x ?? 0, vec2.y * y ?? 0);
        }
        
        
        /// <summary>
        /// Subtracts the vector by the entered vector.
        /// </summary>
        /// <param name="vec2">The start vector.</param>
        /// <param name="value">The vector to subtract by.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Subtract(this Vector2 vec2, Vector2 value)
        {
            return vec2 - value;
        }
     
        
        /// <summary>
        /// Subtracts the vector by the entered x/y.
        /// </summary>
        /// <param name="vec2">The start vector.</param>
        /// <param name="x">The x value to multiply by.</param>
        /// <param name="y">The y value to multiply by.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Subtract2F(this Vector2 vec2, float? x = null, float? y = null)
        {
            return new Vector2(vec2.x - x ?? 0, vec2.y - y ?? 0);
        }
        
        
        /// <summary>
        /// Returns a vector with the modified x or y values.
        /// </summary>
        /// <param name="vec2">The start vector.</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Set(this Vector2 vec2, float? x = null, float? y = null)
        {
            return new Vector2(x ?? vec2.x, y ?? vec2.y);
        }
    }
}