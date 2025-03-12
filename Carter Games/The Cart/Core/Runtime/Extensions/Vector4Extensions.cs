/*
 * Copyright (c) 2025 Carter Games
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
    /// A load of extensions for Unity Vector4's
    /// </summary>
    public static class Vector4Extensions
    {
        /// <summary>
        /// Adds the two vectors together returning the result.
        /// </summary>
        /// <param name="vec4">The starting vector.</param>
        /// <param name="value">The value to add.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Add(this Vector4 vec4, Vector4 value)
        {
            return vec4 + value;
        }
     
        
        /// <summary>
        /// Adds the two vectors together returning the result.
        /// </summary>
        /// <param name="vec4">The starting vector.</param>
        /// <param name="x">The x value to add.</param>
        /// <param name="y">The y value to add.</param>
        /// <param name="z">The z value to add.</param>
        /// <param name="w">The w value to add.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Add4F(this Vector4 vec4, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            return new Vector4(vec4.x + x ?? 0, vec4.y + y ?? 0, vec4.z + z ?? 0, vec4.w + w ?? 0);
        }
        
        
        /// <summary>
        /// Adjusts the incoming vector4 by the entered value.
        /// </summary>
        /// <param name="vec4">The starting vector.</param>
        /// <param name="x">The x adjustment,</param>
        /// <param name="y">The y adjustment.</param>
        /// <param name="z">The z adjustment.</param>
        /// <param name="w">The w adjustment.</param>
        /// <returns>The adjusted vector.</returns>
        public static Vector4 Adjust(this Vector4 vec4, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            return new Vector4(vec4.x + x ?? vec4.x, vec4.y + y ?? vec4.y, vec4.z + z ?? vec4.z, vec4.w + w ?? vec4.w);
        }


        /// <summary>
        /// Clamps the vector to the entered vectors.
        /// </summary>
        /// <param name="vec4">The starting vector.</param>
        /// <param name="min">The min vector.</param>
        /// <param name="max">The max vector.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Clamp(this Vector4 vec4, Vector4 min, Vector4 max)
        {
            return new Vector4(
                Mathf.Clamp(vec4.x, min.x, max.x), 
                Mathf.Clamp(vec4.y, min.y, max.y),
                Mathf.Clamp(vec4.z, min.z, max.z),
                Mathf.Clamp(vec4.w, min.w, max.w)
                );
        }
        
        
        /// <summary>
        /// Clamps the vector to the entered vectors.
        /// </summary>
        /// <param name="vec4">The starting vector.</param>
        /// <param name="min">The min value per co-ord.</param>
        /// <param name="max">The max value per co-ord.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Clamp4F(this Vector4 vec4, float min, float max)
        {
            return new Vector4(
                Mathf.Clamp(vec4.x, min, max), 
                Mathf.Clamp(vec4.y, min, max),
                Mathf.Clamp(vec4.z, min, max),
                Mathf.Clamp(vec4.w, min, max)
                );
        }


        /// <summary>
        /// Copies the vector into a new instance.
        /// </summary>
        /// <param name="vec4">The starting to copy.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Copy(this Vector4 vec4)
        {
            return new Vector4(vec4.x, vec4.y, vec4.z, vec4.w);
        }
        
        
        /// <summary>
        /// Gets a line from the entered vectors
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="target">The end vector.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 DirectionTo(this Vector4 vec4, Vector4 target)
        {
            return (target - vec4).normalized;
        }


        /// <summary>
        /// Divides the vector by another vector.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="value">The value to divide by.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Divide(this Vector4 vec4, Vector4 value)
        {
            return new Vector4(vec4.x / value.x, vec4.y / value.y, vec4.z / value.z, vec4.w / value.w);
        }
        
        
        /// <summary>
        /// Divides the vector by the entered x/y/z/w values.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="x">The x value to divide by.</param>
        /// <param name="y">The y value to divide by.</param>
        /// <param name="z">The z value to divide by.</param>
        /// <param name="w">The w value to divide by.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Divide4F(this Vector4 vec4, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            return new Vector4(vec4.x / x ?? 1, vec4.y / y ?? 1, vec4.z / z ?? 1, vec4.w / w ?? 1);
        }
        
        
        /// <summary>
        /// Gets if the vector has equalling x/y values.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="x">The x value to check.</param>
        /// <param name="y">The y value to check.</param>
        /// <param name="z">The z value to check.</param>
        /// <param name="w">The w value to check.</param>
        /// <returns>The processed Vector4.</returns>
        public static bool Equals4F(this Vector4 vec4, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            return Math.Abs(vec4.x - (x ?? 1)) < float.Epsilon && 
                   Math.Abs(vec4.y - (y ?? 1)) < float.Epsilon && 
                   Math.Abs(vec4.z - (z ?? 1)) < float.Epsilon &&
                   Math.Abs(vec4.w - (w ?? 1)) < float.Epsilon;
        }
        
        
        /// <summary>
        /// Gets a line from a to b.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="target">The target vector.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Line(this Vector4 vec4, Vector4 target)
        {
            return (target - vec4).normalized + vec4;
        }
        
        
        /// <summary>
        /// Gets a line from a to b.
        /// </summary>
        /// <param name="vec4">The start vector.</param>>
        /// <param name="target">The target vector.</param>
        /// <param name="distance">The length the line should extend to.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Line(this Vector4 vec4, Vector4 target, float distance)
        {
            return ((target - vec4).normalized + vec4) * distance;
        }
        
        
        /// <summary>
        /// Gets a middle point of a line between a & b.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="target">The target vector.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 MidPoint(this Vector4 vec4, Vector4 target)
        {
            return (vec4 + target) * .5f;
        }
        
        
        /// <summary>
        /// Multiplies the vector by the entered vector.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="value">The vector to multiply by.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Multiply(this Vector4 vec4, Vector4 value)
        {
            return new Vector4(vec4.x * value.x, vec4.y * value.y, vec4.z * value.z, vec4.w * value.w);
        }
        
        
        /// <summary>
        /// Multiplies the vector by the entered float scale.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="value">The scale to apply to the vector.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 MultiplyScalar(this Vector4 vec4, float value)
        {
            return new Vector4(vec4.x * value, vec4.y * value, vec4.z * value, vec4.w * value);
        }
        
        
        /// <summary>
        /// Multiplies the vector by the entered x/y/z scale.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="x">The x value to multiply by.</param>
        /// <param name="y">The y value to multiply by.</param>
        /// <param name="z">The z value to multiply by.</param>
        /// <param name="w">The w value to multiply by.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Multiply4F(this Vector4 vec4, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            return new Vector4(vec4.x * x ?? 0, vec4.y * y ?? 0, vec4.z * z ?? 0, vec4.w * w ?? 0);
        }
        
        
        /// <summary>
        /// Subtracts the vector by the entered vector.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="value">The vector to subtract by.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Subtract(this Vector4 vec4, Vector4 value)
        {
            return vec4 - value;
        }
     
        
        /// <summary>
        /// Subtracts the vector by the entered x/y/z.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="x">The x value to multiply by.</param>
        /// <param name="y">The y value to multiply by.</param>
        /// <param name="z">The z value to multiply by.</param>
        /// <param name="w">The z value to multiply by.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Subtract4F(this Vector4 vec4, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            return new Vector4(vec4.x - x ?? 0, vec4.y - y ?? 0, vec4.z - z ?? 0, vec4.w - w ?? 0);
        }
        
        
        /// <summary>
        /// Returns a vector with the modified x, y or z values.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <param name="z">The new z value</param>
        /// <param name="w">The new z value</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Set(this Vector4 vec4, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            return new Vector4(x ?? vec4.x, y ?? vec4.y, z ?? vec4.z, w ?? vec4.w);
        }
    }
}