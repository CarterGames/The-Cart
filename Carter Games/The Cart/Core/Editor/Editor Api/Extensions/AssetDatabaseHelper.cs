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
using System.Linq;
using CarterGames.Cart.Core.Logs;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarterGames.Cart.Core.Editor
{
    /// <summary>
    /// A helper class for asset database queries.
    /// </summary>
    public static class AssetDatabaseHelper
    {
        /// <summary>
        /// Gets if asset database can find an asset at the defined path.
        /// </summary>
        /// <param name="path">The path top find.</param>
        /// <typeparam name="T">The type to try and get.</typeparam>
        /// <returns>If the asset exists in the asset database.</returns>
        public static bool FileIsInProject<T>(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                CartLogger.Log<LogCategoryCore>($"Unable to find asset at the path: {path}", typeof(AssetDatabaseHelper));
                return false;
            }
            
            return AssetDatabase.LoadAssetAtPath(path, typeof(T)) != null;
        }


        /// <summary>
        /// Tries to get the path to the entered script.
        /// </summary>
        /// <param name="path">The path found.</param>
        /// <typeparam name="T">The type to find.</typeparam>
        /// <returns>If it was successful.</returns>
        public static bool TryGetScriptPath<T>(out string path)
        {
            return TryGetScriptPath(typeof(T), out path);
        }
        
        
        /// <summary>
        /// Tries to get the path to the entered script.
        /// </summary>
        /// <param name="type">The type to find.</param>
        /// <param name="path">The path found.</param>
        /// <returns>If it was successful.</returns>
        public static bool TryGetScriptPath(Type type, out string path)
        {
            path = string.Empty;

            var foundClass = AssetDatabase.FindAssets($"t:Script {type.Name}");
            if (foundClass == null) return false;
            if (foundClass.Length <= 0) return false;
            
            path = AssetDatabase.GUIDToAssetPath(foundClass[0]);
            
            return !string.IsNullOrEmpty(path);
        }


        public static string FindScriptInProject<T>()
        {
            return AssetDatabase.FindAssets($"t:Script {nameof(T)}").FirstOrDefault();
        }
        
        
        public static string PathToClass(string target)
        {
            return AssetDatabase.GUIDToAssetPath(target);
        }


        public static T[] GetAllInstancesInProject<T>() where T : Object
        {
            var assets = AssetDatabase.FindAssets($"t:{typeof(T)}");
            
            if (assets == null) return Array.Empty<T>();

            var array = new T[assets.Length];

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = AssetDatabase.LoadAssetAtPath<T>(PathToClass(assets[i]));
            }

            return array;
        }
    }
}