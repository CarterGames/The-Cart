#if CARTERGAMES_CART_CRATE_MULTISCENE && UNITY_EDITOR

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

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Events;
using UnityEditor;

namespace CarterGames.Cart.Crates.MultiScene.Editor
{
    /// <summary>
    /// Helps with getting the scenes in the project in different formats for other elements of the asset.
    /// </summary>
    public static class EditorSceneHelper
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static List<string> cacheAllSceneNamesInProject = new List<string>();
        private static Dictionary<string, string> cachedScenesInBuildSettings = new Dictionary<string, string>();
        private static List<string> cachedScenesInBuildSettingsKeys = new List<string>();

        private static bool hasCache;
        private static bool isListening;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets all the scene names in the project.
        /// </summary>
        public static List<string> AllSceneNamesInProject
        {
            get
            {
                if (hasCache) return cacheAllSceneNamesInProject;
                UpdateCaches();
                return cacheAllSceneNamesInProject;
            }
        }
        
        
        /// <summary>
        /// Gets all the scenes in the build settings.
        /// </summary>
        public static Dictionary<string, string> ScenesInBuildSettings
        {
            get
            {
                if (hasCache) return cachedScenesInBuildSettings;
                UpdateCaches();
                return cachedScenesInBuildSettings;
            }
        }
        
        
        /// <summary>
        /// Gets all the scenes in the build settings but only returns the keys of the dictionary stored in this class. 
        /// </summary>
        private static List<string> ScenesInBuildSettingsKeys
        {
            get
            {
                if (hasCache) return cachedScenesInBuildSettingsKeys;
                UpdateCaches();
                return cachedScenesInBuildSettingsKeys;
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Raises when the build settings are modified to update the cache.
        /// </summary>
        public static readonly Evt OnCacheUpdate = new Evt();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   General Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        
        /// <summary>
        /// Updates the cache for scenes in the project when called.
        /// </summary>
        public static void UpdateCaches()
        {
            cacheAllSceneNamesInProject = GetNamesOfScenesInProject();
            cachedScenesInBuildSettings = GetAllScenesInProject();
            hasCache = true;
            
            if (isListening) return;
       
            EditorBuildSettings.sceneListChanged -= OnSceneListChanged;
            EditorBuildSettings.sceneListChanged += OnSceneListChanged;
            
            isListening = true;
            // _isListening doesn't reset as we only want to sub to it once. 
        }


        /// <summary>
        /// Resets the cache bool so the system refreshes all the cached data for the scenes in the project. 
        /// </summary>
        private static void OnSceneListChanged()
        {
            hasCache = false;
            OnCacheUpdate.Raise();
        }


        /// <summary>
        /// Gets the names of all the scenes in the project & processes them into a readable format for use.
        /// </summary>
        /// <returns>A list of scene names in string format.</returns>
        private static List<string> GetNamesOfScenesInProject()
        {
            if (hasCache) return cacheAllSceneNamesInProject;
            
            var assets = AssetDatabase.FindAssets("t:scene", null);

            var list = new List<string>();

            for (var i = 0; i < assets.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(assets[i]);
                var split = path.Split('/');
                list.Add(split[split.Length - 1].Replace(".unity", ""));
            }

            return list;
        }


        /// <summary>
        /// Sorts all the scenes in the build settings into a dictionary for use. 
        /// </summary>
        /// <returns>An organised dictionary.</returns>
        public static Dictionary<string, string> GetAllScenesInProject()
        {
            if (hasCache) return cachedScenesInBuildSettings;
            
            var scenes = EditorBuildSettings.scenes;
            var buildSettingsScenes = new Dictionary<string, string> { { "", "" } };

            foreach (var scene in scenes)
            {
                var filteredPath = scene.path.Replace("Assets/", "").Replace(".unity", "");
                var split = filteredPath.Split('/');

                if (buildSettingsScenes.ContainsKey(split[split.Length - 1]))
                {
                    buildSettingsScenes.Add(split[split.Length - 2] + "/" + split[split.Length - 1], scene.path);
                }
                else
                {
                    buildSettingsScenes.Add(split[split.Length - 1], scene.path);
                }
            }

            cachedScenesInBuildSettingsKeys = buildSettingsScenes.Keys.ToList();

            return buildSettingsScenes;
        }

        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Utility Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        
        /// <summary>
        /// Converts an int for an index of a scene to a scene name.
        /// </summary>
        /// <param name="value">The int to convert.</param>
        /// <returns>The scene name.</returns>
        public static string ConvertIntToScene(int value, string[] optionsShown)
        {
            if (value.Equals(-1)) return "";
            return optionsShown[value];
        }
        
        
        /// <summary>
        /// Converts a string into an int index of the string in all the scene names.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>The index of the scene name.</returns>
        public static int ConvertStringToIndex(string value, string[] optionsShown)
        {
            return optionsShown.ToList().IndexOf(value);
        }
    }
}

#endif