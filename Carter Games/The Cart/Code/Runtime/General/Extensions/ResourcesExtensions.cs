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
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarterGames.Cart.General
{
    public class ResourcesExtensions
    {
        /// <summary>
        /// Loads a asset from the resources folder.
        /// </summary>
        /// <param name="basePath">The base path to load from.</param>
        /// <param name="resourceName">The resource name to find.</param>
        /// <param name="systemTypeInstance">The type to look for.</param>
        /// <returns>The found asset.</returns>
        public static Object Load(string basePath, string resourceName, Type systemTypeInstance) 
        {
            var directories = Directory.GetDirectories(basePath,"*", SearchOption.AllDirectories);
            
            foreach (var item in directories)
            {
                var itemPath = item.Substring(basePath.Length+1);
                var result = Resources.Load(itemPath + "\\" + resourceName, systemTypeInstance);
                
                if (result == null) continue;
                return result;
            }
            
            return null;
        }
        
        
        /// <summary>
        /// Loads all assets from the resources folder of the type needed.
        /// </summary>
        /// <param name="basePath">The base path to load from.</param>
        /// <returns>The found assets.</returns>
        public static T[] LoadAll<T>(string basePath) where T : Object
        {
            var directories = Directory.GetDirectories(basePath,"*", SearchOption.AllDirectories);

            var allResults = new List<T>();

            foreach (var item in directories)
            {
                var path = item.Replace("Assets/Resources/", string.Empty);
                var result = Resources.LoadAll<T>(path);
                allResults.AddRange(result);
            }
            
            return allResults.ToArray();
        }
    }
}