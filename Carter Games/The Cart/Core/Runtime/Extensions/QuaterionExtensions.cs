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

using UnityEngine;

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// A collection of extension methods for Quaternions
    /// </summary>
    public static class QuaternionExtensions
    {
        /// <summary>
        /// Returns a vector with the modified w, x, y, z value...
        /// </summary>
        /// <param name="original">The initial vector to edit...</param>
        /// <param name="w">The new w value</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <param name="z">The new z value</param>
        /// <returns>Vector4</returns>
        public static Quaternion With(this Quaternion original, float? w = null, float? x = null, float? y = null, float? z = null)
        {
            return new Quaternion(w ?? original.w, x ?? original.x, y ?? original.y, z ?? original.z);
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
        public static Quaternion Adjust(this Quaternion original, float? w = null, float? x = null, float? y = null, float? z = null)
        {
            return new Quaternion(original.w + w ?? original.w, original.x + x ?? original.x, original.y + y ?? original.y, original.z + z ?? original.z);
        }
    }
}