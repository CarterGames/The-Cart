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

namespace CarterGames.Common.General
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