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

using System;
using System.IO;
using Scarlet.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace Scarlet.Editor
{
    public static class FileEditorUtil
    {
        /// <summary>
        /// Gets a file via filter.
        /// </summary>
        /// <param name="filter">The filter to search for.</param>
        /// <param name="containsChecks">Parts of a string the path should contain.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The found file as an object if found successfully.</returns>
        private static object GetFileViaFilter<T>(string filter, string assetPath, params string[] containsChecks)
        {
            string path = string.Empty;
                
            foreach (var scriptFound in AssetDatabase.FindAssets($"t:Script {nameof(T)}"))
            {
                path = AssetDatabase.GUIDToAssetPath(scriptFound);

                foreach (var containCheck in containsChecks)
                {
                    if (!path.Contains(containCheck)) goto Loop;
                }
                
                path = AssetDatabase.GUIDToAssetPath(scriptFound);
                path = path.Replace(assetPath, "");
                return AssetDatabase.LoadAssetAtPath(path, typeof(T));
                Loop: ;
            }

            return null;
        }
        
        
        /// <summary>
        /// Gets a file via filter.
        /// </summary>
        /// <param name="filter">The filter to search for.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The found file as an object if found successfully.</returns>
        private static object GetFileViaFilter<T>(string filter)
        {
            var asset = AssetDatabase.FindAssets(filter, null);
            if (asset == null || asset.Length <= 0) return null;
            var path = AssetDatabase.GUIDToAssetPath(asset[0]);
            return AssetDatabase.LoadAssetAtPath(path, typeof(T));
        }
        
        
        /// <summary>
        /// Gets a file via filter.
        /// </summary>
        /// <param name="filter">The filter to search for.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The found file as an object if found successfully.</returns>
        public static object GetFileViaFilter(Type type, string filter)
        {
            var asset = AssetDatabase.FindAssets(filter, null);
            if (asset == null || asset.Length <= 0) return null;
            var path = AssetDatabase.GUIDToAssetPath(asset[0]);
            return AssetDatabase.LoadAssetAtPath(path, type);
        }


        /// <summary>
        /// Gets or assigned the cached value of any type, just saving writing the same lines over and over xD
        /// </summary>
        /// <param name="cache">The cached value to assign or get.</param>
        /// <param name="filter">The filter to use.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The assigned cache.</returns>
        public static T GetOrAssignCache<T>(ref T cache, string filter)
        {
            if (cache != null) return cache;
            cache = (T) GetFileViaFilter<T>(filter);
            return cache;
        }
        
        
        /// <summary>
        /// Gets or assigned the cached value of any type, just saving writing the same lines over and over xD
        /// </summary>
        /// <param name="cache">The cached value to assign or get.</param>
        /// <param name="filter">The filter to use.</param>
        /// <param name="containsChecks">Parts of a string the path should contain.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The assigned cache.</returns>
        public static T GetOrAssignCache<T>(ref T cache, string filter, params string[] containsChecks)
        {
            if (cache != null) return cache;
            cache = (T) GetFileViaFilter<T>(filter, "", containsChecks);
            return cache;
        }
        
        
        /// <summary>
        /// Gets or assigned the cached value of any type, just saving writing the same lines over and over xD
        /// </summary>
        /// <param name="cache">The cached value to assign or get.</param>
        /// <param name="filter">The filter to use.</param>
        /// <param name="containsChecks">Parts of a string the path should contain.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The assigned cache.</returns>
        public static T GetOrAssignCache<T>(ref T cache, string filter, string assetPath, params string[] containsChecks)
        {
            if (cache != null) return cache;
            cache = (T) GetFileViaFilter<T>(filter, assetPath, containsChecks);
            return cache;
        }


        /// <summary>
        /// Creates a scriptable object if it doesn't exist and then assigns it to its cache. 
        /// </summary>
        /// <param name="cache">The cached value to assign or get.</param>
        /// <param name="path">The path to create to if needed.</param>
        /// <param name="filter">The filter to use.</param>
        /// <param name="containChecks">Parts of a string the path should contain.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The assigned cache.</returns>
        public static T CreateSoGetOrAssignCache<T>(ref T cache, string path, string filter, string assetPath = "", params string[] containChecks) where T : ScriptableObject
        {
            if (cache != null) return cache;
            
            if (containChecks.Length > 0)
            {
                cache = (T)GetFileViaFilter<T>(filter, assetPath, containChecks);
            }
            else
            {
                cache = (T)GetFileViaFilter<T>(filter);
            }

            if (cache == null)
            {
                cache = CreateScriptableObject<T>(path);
            }
            
            AssetIndexHandler.UpdateIndex();

            return cache;
        }


        /// <summary>
        /// Creates a scriptable object of the type entered when called.
        /// </summary>
        /// <param name="path">The path to create the new asset at.</param>
        /// <typeparam name="T">The type to make.</typeparam>
        /// <returns>The newly created asset.</returns>
        public static T CreateScriptableObject<T>(string path) where T : ScriptableObject
        {
            var instance = ScriptableObject.CreateInstance(typeof(T));

            CreateToDirectory(path);

            AssetDatabase.CreateAsset(instance, path);
            AssetDatabase.Refresh();

            return (T)instance;
        }
        
        
        /// <summary>
        /// Creates all the folders to a path if they don't exist already.
        /// </summary>
        /// <param name="path">The path to create to.</param>
        public static void CreateToDirectory(string path)
        {
            var currentPath = string.Empty;
            var split = path.Split('/');

            for (var i = 0; i < path.Split('/').Length; i++)
            {
                var element = path.Split('/')[i];
                currentPath += element + "/";

                if (i.Equals(split.Length - 1))
                {
                    continue;
                }

                if (Directory.Exists(currentPath))
                {
                    continue;
                }

                Directory.CreateDirectory(currentPath);
            }
        }
        
        
        /// <summary>
        /// Draws the script fields in the custom inspector...
        /// </summary>
        public static void DrawMonoScriptSection<T>(T target) where T : MonoBehaviour
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour(target), typeof(T), false);
            EditorGUI.EndDisabledGroup();

            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the script fields in the custom inspector...
        /// </summary>
        public static void DrawSoScriptSection(object target)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script:", MonoScript.FromScriptableObject((ScriptableObject)target),
                typeof(object), false);
            EditorGUI.EndDisabledGroup();
        }
    }
}