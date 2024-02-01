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

using UnityEngine;

namespace CarterGames.Cart.General
{
    /// <summary>
    /// An extension class for some common vector maths.
    /// </summary>
    public static class VectorExtensions
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Direction To
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector2</returns>
        public static Vector2 DirectionTo(this Vector2 original, Vector2 target)
        {
            return (target - original).normalized;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector3</returns>
        public static Vector3 DirectionTo(this Vector3 original, Vector3 target)
        {
            return (target - original).normalized;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector4</returns>
        public static Vector4 DirectionTo(this Vector4 original, Vector4 target)
        {
            return (target - original).normalized;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Line
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector2</returns>
        public static Vector2 Line(this Vector2 original, Vector2 target)
        {
            return (target - original).normalized + original;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector3</returns>
        public static Vector3 Line(this Vector3 original, Vector3 target)
        {
            return (target - original).normalized + original;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector4</returns>
        public static Vector4 Line(this Vector4 original, Vector4 target)
        {
            return (target - original).normalized + original;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <param name="distance">The length the line should extend to...</param>
        /// <returns>Vector2</returns>
        public static Vector2 Line(this Vector2 original, Vector2 target, float distance)
        {
            return ((target - original).normalized + original) * distance;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <param name="distance">The length the line should extend to...</param>
        /// <returns>Vector3</returns>
        public static Vector3 Line(this Vector3 original, Vector3 target, float distance)
        {
            return ((target - original).normalized + original) * distance;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <param name="distance">The length the line should extend to...</param>
        /// <returns>Vector4</returns>
        public static Vector4 Line(this Vector4 original, Vector4 target, float distance)
        {
            return ((target - original).normalized + original) * distance;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Mid Point
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets a middle point of a line between a & b
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector2</returns>
        public static Vector2 MidPoint(this Vector2 original, Vector2 target)
        {
            return (original + target) * .5f;
        }
        
        /// <summary>
        /// Gets a middle point of a line between a & b
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector3</returns>
        public static Vector3 MidPoint(this Vector3 original, Vector3 target)
        {
            return (original + target) * .5f;
        }
        
        /// <summary>
        /// Gets a middle point of a line between a & b
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector3</returns>
        public static Vector4 MidPoint(this Vector4 original, Vector4 target)
        {
            return (original + target) * .5f;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   With
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Returns a vector with the modified x or y values...
        /// </summary>
        /// <param name="original">The initial vector to edit...</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <returns>Vector2</returns>
        public static Vector2 With(this Vector2 original, float? x = null, float? y = null)
        {
            return new Vector2(x ?? original.x, y ?? original.y);
        }

        /// <summary>
        /// Returns a vector with the modified x, y, z value...
        /// </summary>
        /// <param name="original">The initial vector to edit...</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <param name="z">The new z value</param>
        /// <returns>Vector3</returns>
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }

        /// <summary>
        /// Returns a vector with the modified w, x, y, z value...
        /// </summary>
        /// <param name="original">The initial vector to edit...</param>
        /// <param name="w">The new w value</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <param name="z">The new z value</param>
        /// <returns>Vector4</returns>
        public static Vector4 With(this Vector4 original, float? w = null, float? x = null, float? y = null, float? z = null)
        {
            return new Vector4(w ?? original.w, x ?? original.x, y ?? original.y, z ?? original.z);
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Adjust
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Returns a vector with the adjusted x or y values...
        /// </summary>
        /// <param name="original">The initial vector to edit...</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <returns>Vector2</returns>
        public static Vector2 Adjust(this Vector2 original, float? x = null, float? y = null)
        {
            return new Vector2(original.x + x ?? original.x, original.y + y ?? original.y);
        }

        /// <summary>
        /// Returns a vector with the adjusted x, y, z value...
        /// </summary>
        /// <param name="original">The initial vector to edit...</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <param name="z">The new z value</param>
        /// <returns>Vector3</returns>
        public static Vector3 Adjust(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(original.x + x ?? original.x, original.y + y ?? original.y, original.z + z ?? original.z);
        }

        /// <summary>
        /// Returns a vector with the adjusted w, x, y, z value...
        /// </summary>
        /// <param name="original">The initial vector to edit...</param>
        /// <param name="w">The new w value</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <param name="z">The new z value</param>
        /// <returns>Vector4</returns>
        public static Vector4 Adjust(this Vector4 original, float? w = null, float? x = null, float? y = null, float? z = null)
        {
            return new Vector4(original.w + w ?? original.w, original.x + x ?? original.x, original.y + y ?? original.y, original.z + z ?? original.z);
        }
    }
}