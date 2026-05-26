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
    /// An extension class for materials.
    /// </summary>
    public static class MaterialExtensions
    {
        /// <summary>
        /// Get a named bool value.
        /// </summary>
        /// <param name="material">The material to get from.</param>
        /// <param name="nameId">The name ID of the property retrieved by Shader.PropertyToID.</param>
        public static bool GetBool(this Material material, int nameId)
        {
            return material.GetInt(nameId).Equals(1);
        }
        
        
        /// <summary>
        /// Get a named bool value.
        /// </summary>
        /// <param name="material">The material to get from.</param>
        /// <param name="name">The name of the property.</param>
        public static bool GetBool(this Material material, string name)
        {
            return material.GetInt(Shader.PropertyToID(name)).Equals(1);
        }
        
        
        
        /// <summary>
        /// Set a named bool value.
        /// </summary>
        /// <param name="material">The material to get from.</param>
        /// <param name="nameId">The name ID of the property retrieved by Shader.PropertyToID.</param>
        /// <param name="value">The value to set to.</param>
        public static void SetBool(this Material material, int nameId, bool value)
        {
            material.SetInt(nameId, value ? 1 : 0);
        }
        
        
        /// <summary>
        /// Set a named bool value.
        /// </summary>
        /// <param name="material">The material to get from.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value to set to.</param>
        public static void SetBool(this Material material, string name, bool value)
        {
            material.SetInt(Shader.PropertyToID(name), value ? 1 : 0);
        }
    }
}