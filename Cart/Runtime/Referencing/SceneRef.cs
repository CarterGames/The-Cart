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
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarterGames.Cart
{
    /// <summary>
    /// A helper class for getting references to other scripts.
    /// </summary>
    public static class SceneRef
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the first of any and all of the type requested from the active scene.
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>First instance of the type found in the active scene</returns>
        public static T GetComponentFromScene<T>()
        {
            var allOfType = GetComponentsFromScene<T>();

            return allOfType.Count > 0 
                ? allOfType[0] 
                : default;
        }
        
        
        /// <summary>
        /// Gets the first of any and all of the type requested from all scenes...
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>First instance of the type found in all scenes</returns>
        public static T GetComponentFromAllScenes<T>()
        {
            var allOfType = GetComponentsFromAllScenes<T>();

            return allOfType.Count > 0 
                ? allOfType[0] 
                : default;
        }
        
        
        /// <summary>
        /// Gets any and all of the type requested from the active scene...
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>List of any instances of the type found in the scene</returns>
        public static List<T> GetComponentsFromScene<T>()
        {
            var objs = new List<GameObject>();
            var scene = SceneManager.GetActiveScene();
            var validObjectsFromScene = new List<T>();
            
            scene.GetRootGameObjects(objs);
            
            foreach (var go in objs)
            {
                validObjectsFromScene.AddRange(go.GetComponentsInChildren<T>(true));
            }

            return validObjectsFromScene;
        }
        
        
        /// <summary>
        /// Gets any and all of the type requested from all scenes...
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>List of any instances of the type found in all scenes</returns>
        public static List<T> GetComponentsFromAllScenes<T>()
        {
            var objs = new List<GameObject>();
            var scenes = new List<Scene>();
            var validObjectsFromScene = new List<T>();

            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                scenes.Add(SceneManager.GetSceneAt(i));
            }

            foreach (var scene in scenes)
            {
                objs.AddRange(scene.GetRootGameObjects());
            }

            foreach (var go in objs)
            {
                validObjectsFromScene.AddRange(go.GetComponentsInChildren<T>(true));
            }

            return validObjectsFromScene;
        }
    }
}