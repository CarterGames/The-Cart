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