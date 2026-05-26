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
    public static class VectorExtensions
    {
        //
        // Add
        //
        
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
        /// <param name="vec4">The starting vector.</param>
        /// <param name="value">The value to add.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Add(this Vector4 vec4, Vector4 value)
        {
            return vec4 + value;
        }
        
        //
        // Add Float
        //
        
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
        
        //
        // Clamp
        //
        
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
        
        //
        // Clamp Float
        //

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
        
        //
        // Copy
        //
        
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
        /// Copies the vector into a new instance.
        /// </summary>
        /// <param name="vec3">The starting to copy.</param>
        /// <returns>The processed Vector3.</returns>
        public static Vector3 Copy(this Vector3 vec3)
        {
            return new Vector3(vec3.x, vec3.y, vec3.z);
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
        
        //
        // Direction To
        //
        
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
        /// Gets a line from the entered vectors
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="target">The end vector.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 DirectionTo(this Vector4 vec4, Vector4 target)
        {
            return (target - vec4).normalized;
        }
        
        //
        // Divide
        //
        
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
        /// Divides the vector by another vector.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="value">The value to divide by.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Divide(this Vector4 vec4, Vector4 value)
        {
            return new Vector4(vec4.x / value.x, vec4.y / value.y, vec4.z / value.z, vec4.w / value.w);
        }
        
        //
        // Divide Float
        //
        
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
        
        //
        // Equals Float
        //
        
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
        
        //
        // Line
        //
        
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
        /// <param name="vec2">The start vector.</param>
        /// <param name="target">The target vector.</param>
        /// <param name="distance">The length the line should extend to.</param>
        /// <returns>The processed vector2.</returns>
        public static Vector2 Line(this Vector2 vec2, Vector2 target, float distance)
        {
            return ((target - vec2).normalized + vec2) * distance;
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
        
        //
        // Mid point
        //
        
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
        /// Gets a middle point of a line between a & b.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="target">The target vector.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 MidPoint(this Vector4 vec4, Vector4 target)
        {
            return (vec4 + target) * .5f;
        }
        
        //
        // Multiply
        //
        
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
        /// Multiplies the vector by the entered vector.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="value">The vector to multiply by.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 Multiply(this Vector4 vec4, Vector4 value)
        {
            return new Vector4(vec4.x * value.x, vec4.y * value.y, vec4.z * value.z, vec4.w * value.w);
        }
        
        //
        // Multiply Scalar
        //
        
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
        /// Multiplies the vector by the entered float scale.
        /// </summary>
        /// <param name="vec4">The start vector.</param>
        /// <param name="value">The scale to apply to the vector.</param>
        /// <returns>The processed Vector4.</returns>
        public static Vector4 MultiplyScalar(this Vector4 vec4, float value)
        {
            return new Vector4(vec4.x * value, vec4.y * value, vec4.z * value, vec4.w * value);
        }
        
        //
        // Multiply Float
        //
        
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
        
        //
        // Set
        //
        
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
        
        //
        // LEGACY
        //
        
        [Obsolete("Use Add2F() extension instead.")]
        public static Vector2 Adjust(this Vector2 vec2, float? x = null, float? y = null) => vec2.Add2F(x, y);
        
        [Obsolete("Use Add3F() extension instead.")]
        public static Vector3 Adjust(this Vector3 vec3, float? x = null, float? y = null, float? z = null) 
            => vec3.Add3F(x, y, z);
        
        [Obsolete("Use Add4F() extension instead.")]
        public static Vector4 Adjust(this Vector4 vec4, float? x = null, float? y = null, float? z = null, float? w = null)
            => vec4.Add4F(x, y, z, w);

        [Obsolete("Use Add() extension instead, pass through values as if subtraction to apply same effect.")]
        public static Vector2 Subtract(this Vector2 vec2, Vector2 value) => vec2.Add(-value);

        [Obsolete("Use Add() extension instead, pass through values as if subtraction to apply same effect.")]
        public static Vector3 Subtract(this Vector3 vec3, Vector3 value) => vec3.Add(-value);
        
        [Obsolete("Use Add() extension instead, pass through values as if subtraction to apply same effect.")]
        public static Vector4 Subtract(this Vector4 vec4, Vector4 value) => vec4.Add(-value);

        [Obsolete("Use Add2F() extension instead, pass through values as if subtraction to apply same effect.")]
        public static Vector2 Subtract2F(this Vector2 vec2, float? x = null, float? y = null) => vec2.Add2F(-x, -y);
        
        [Obsolete("Use Add3F() extension instead, pass through values as if subtraction to apply same effect.")]
        public static Vector3 Subtract3F(this Vector3 vec3, float? x = null, float? y = null, float? z = null)
        => vec3.Add3F(-x, -y, -z);

        [Obsolete("Use Add4F() extension instead, pass through values as if subtraction to apply same effect.")]
        public static Vector4 Subtract4F(this Vector4 vec4, float? x = null, float? y = null, float? z = null,
            float? w = null) => vec4.Add4F(-x, -y, -z, -w);
    }
}