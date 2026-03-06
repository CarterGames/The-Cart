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

using System;
using UnityEngine;

namespace CarterGames.Cart
{
    /// <summary>
    /// A load of extensions for Unity Vector3's
    /// </summary>
    public static class Vector3Extensions
    {
        /// <summary>
        /// Adds the two vectors together returning the result.
        /// </summary>
        /// <param name="vec3">The starting vector.</param>
        /// <param name="value">The value to add.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Add(this Vector3 vec3, Vector3 value)
        {
            return vec3 + value;
        }
     
        
        /// <summary>
        /// Adds the two vectors together returning the result.
        /// </summary>
        /// <param name="vec3">The starting vector.</param>
        /// <param name="x">The x value to add.</param>
        /// <param name="y">The y value to add.</param>
        /// <param name="z">The z value to add.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Add3F(this Vector3 vec3, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(vec3.x + x ?? 0, vec3.y + y ?? 0, vec3.z + z ?? 0);
        }
        
        
        /// <summary>
        /// Adjusts the incoming vector3 by the entered value.
        /// </summary>
        /// <param name="vec3">The starting vector.</param>
        /// <param name="x">The x adjustment,</param>
        /// <param name="y">The y adjustment.</param>
        /// <param name="z">The z adjustment.</param>
        /// <returns>The adjusted vector.</returns>
        public static Vector3 Adjust(this Vector3 vec3, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(vec3.x + x ?? vec3.x, vec3.y + y ?? vec3.y, vec3.z + z ?? vec3.z);
        }


        /// <summary>
        /// Clamps the vector to the entered vectors.
        /// </summary>
        /// <param name="vec3">The starting vector.</param>
        /// <param name="min">The min vector.</param>
        /// <param name="max">The max vector.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Clamp(this Vector3 vec3, Vector3 min, Vector3 max)
        {
            return new Vector3(
                Mathf.Clamp(vec3.x, min.x, max.x), 
                Mathf.Clamp(vec3.y, min.y, max.y),
                Mathf.Clamp(vec3.z, min.z, max.z)
                );
        }
        
        
        /// <summary>
        /// Clamps the vector to the entered vectors.
        /// </summary>
        /// <param name="vec3">The starting vector.</param>
        /// <param name="min">The min value per co-ord.</param>
        /// <param name="max">The max value per co-ord.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Clamp3F(this Vector3 vec3, float min, float max)
        {
            return new Vector3(
                Mathf.Clamp(vec3.x, min, max), 
                Mathf.Clamp(vec3.y, min, max),
                Mathf.Clamp(vec3.z, min, max)
                );
        }


        /// <summary>
        /// Copies the vector into a new instance.
        /// </summary>
        /// <param name="vec3">The starting to copy.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Copy(this Vector3 vec3)
        {
            return new Vector3(vec3.x, vec3.y, vec3.z);
        }
        
        
        /// <summary>
        /// Gets a line from the entered vectors
        /// </summary>
        /// <param name="vec3">The start vector.</param>
        /// <param name="target">The end vector.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 DirectionTo(this Vector3 vec3, Vector3 target)
        {
            return (target - vec3).normalized;
        }


        /// <summary>
        /// Divides the vector by another vector.
        /// </summary>
        /// <param name="vec3">The start vector.</param>
        /// <param name="value">The value to divide by.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Divide(this Vector3 vec3, Vector3 value)
        {
            return new Vector3(vec3.x / value.x, vec3.y / value.y, vec3.z / value.z);
        }
        
        
        /// <summary>
        /// Divides the vector by the entered x/y/z values.
        /// </summary>
        /// <param name="vec3">The start vector.</param>
        /// <param name="x">The x value to divide by.</param>
        /// <param name="y">The y value to divide by.</param>
        /// <param name="z">The z value to divide by.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Divide3F(this Vector3 vec3, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(vec3.x / x ?? 1, vec3.y / y ?? 1, vec3.z / z ?? 1);
        }
        
        
        /// <summary>
        /// Gets if the vector has equalling x/y/z values.
        /// </summary>
        /// <param name="vec3">The start vector.</param>
        /// <param name="x">The x value to check.</param>
        /// <param name="y">The y value to check.</param>
        /// <param name="z">The z value to check.</param>
        /// <returns>The processed Vector3.</returns>
        public static bool Equals3F(this Vector3 vec3, float? x = null, float? y = null, float? z = null)
        {
            return Math.Abs(vec3.x - (x ?? 1)) < float.Epsilon && 
                   Math.Abs(vec3.y - (y ?? 1)) < float.Epsilon && 
                   Math.Abs(vec3.z - (z ?? 1)) < float.Epsilon;
        }
        
        
        /// <summary>
        /// Gets a line from a to b.
        /// </summary>
        /// <param name="vec3">The start vector.</param>
        /// <param name="target">The target vector.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Line(this Vector3 vec3, Vector3 target)
        {
            return (target - vec3).normalized + vec3;
        }
        
        
        /// <summary>
        /// Gets a line from a to b.
        /// </summary>
        /// <param name="vec3">The start vector.</param>>
        /// <param name="target">The target vector.</param>
        /// <param name="distance">The length the line should extend to.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Line(this Vector3 vec3, Vector3 target, float distance)
        {
            return ((target - vec3).normalized + vec3) * distance;
        }
        
        
        /// <summary>
        /// Gets a middle point of a line between a & b.
        /// </summary>
        /// <param name="vec3">The start vector.</param>
        /// <param name="target">The target vector.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 MidPoint(this Vector3 vec3, Vector3 target)
        {
            return (vec3 + target) * .5f;
        }
        
        
        /// <summary>
        /// Multiplies the vector by the entered vector.
        /// </summary>
        /// <param name="vec3">The start vector.</param>
        /// <param name="value">The vector to multiply by.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Multiply(this Vector3 vec3, Vector3 value)
        {
            return new Vector3(vec3.x * value.x, vec3.y * value.y, vec3.z * value.z);
        }
        
        
        /// <summary>
        /// Multiplies the vector by the entered float scale.
        /// </summary>
        /// <param name="vec3">The start vector.</param>
        /// <param name="value">The scale to apply to the vector.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 MultiplyScalar(this Vector3 vec3, float value)
        {
            return new Vector3(vec3.x * value, vec3.y * value, vec3.z * value);
        }
        
        
        /// <summary>
        /// Multiplies the vector by the entered x/y/z scale.
        /// </summary>
        /// <param name="vec3">The start vector.</param>
        /// <param name="x">The x value to multiply by.</param>
        /// <param name="y">The y value to multiply by.</param>
        /// <param name="z">The z value to multiply by.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Multiply3F(this Vector3 vec3, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(vec3.x * x ?? 0, vec3.y * y ?? 0, vec3.z * z ?? 0);
        }
        
        
        /// <summary>
        /// Subtracts the vector by the entered vector.
        /// </summary>
        /// <param name="vec3">The start vector.</param>
        /// <param name="value">The vector to subtract by.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Subtract(this Vector3 vec3, Vector3 value)
        {
            return vec3 - value;
        }
     
        
        /// <summary>
        /// Subtracts the vector by the entered x/y/z.
        /// </summary>
        /// <param name="vec3">The start vector.</param>
        /// <param name="x">The x value to multiply by.</param>
        /// <param name="y">The y value to multiply by.</param>
        /// <param name="z">The z value to multiply by.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Subtract3F(this Vector3 vec3, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(vec3.x - x ?? 0, vec3.y - y ?? 0, vec3.z - z ?? 0);
        }
        
        
        /// <summary>
        /// Returns a vector with the modified x, y or z values.
        /// </summary>
        /// <param name="vec3">The start vector.</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <param name="z">The new z value</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Set(this Vector3 vec3, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vec3.x, y ?? vec3.y, z ?? vec3.z);
        }
    }
}