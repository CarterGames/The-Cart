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
using UnityEngine.SceneManagement;

namespace CarterGames.Cart
{
    /// <summary>
    /// A helper class for scene management API.
    /// </summary>
    public static class SceneHelper
    {
        /// <summary>
        /// Gets all the currently active scenes.
        /// </summary>
        /// <returns>An IEnumerable of Scenes</returns>
        public static IEnumerable<Scene> GetAllActiveScenes()
        {
            var list = new List<Scene>();

            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                list.Add(SceneManager.GetSceneAt(i));
            }

            return list;
        }
        
        
        /// <summary>
        /// Gets if the scene is loaded or not.
        /// </summary>
        /// <param name="sceneName">The scene name to look for.</param>
        /// <returns>If the scene is active (bool).</returns>
        public static bool IsSceneLoaded(string sceneName)
        {
            return GetAllActiveScenes().Any(scene => scene.name == sceneName);
        }
    }
}