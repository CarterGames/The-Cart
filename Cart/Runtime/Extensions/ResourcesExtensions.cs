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
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarterGames.Cart
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