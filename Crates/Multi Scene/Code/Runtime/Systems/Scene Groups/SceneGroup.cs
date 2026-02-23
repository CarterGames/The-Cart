#if CARTERGAMES_CART_CRATE_MULTISCENE

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
using CarterGames.Cart;
using CarterGames.Cart.Data;
using UnityEngine;

namespace CarterGames.Cart.Crates.MultiScene
{
    /// <summary>
    /// Contains the data required to load a group of scenes...
    /// </summary>
    [CreateAssetMenu(fileName = "New Scene Group", menuName = "Carter Games/The Cart/Crates/Multi Scene/New Scene Group", order = 0)]
    public class SceneGroup : DataAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties / Getters
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The scenes in this group...
        /// </summary>
        [HideInInspector] public List<SceneReference> scenes = new List<SceneReference>();


        /// <summary>
        /// Confirms if the scene group is setup with at-least 1 scene... If not it'll return false...
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (scenes == null) return false;
                if (scenes.Count <= 0) return false;
                if (scenes[0].SceneName.Length <= 0) return false;
                
                foreach (var data in scenes)
                {
                    if (data.SceneName.Equals(string.Empty)) return false;
                }

                if (!scenes.Distinct().Count().Equals(scenes.Count)) return false;

                return true;
            }
        }
        

        /// <summary>
        /// Gets the base scene of this group...
        /// </summary>
        public string GetBaseScene
        {
            get
            {
                if (scenes == null) return string.Empty;
                if (scenes.Count <= 0) return string.Empty;
                return scenes[0].SceneName;
            }
        }

        
        /// <summary>
        /// Gets all the additive scenes in this group...
        /// </summary>
        public List<string> GetAdditiveScenes
        {
            get
            {
                if (scenes == null) return null;
                if (scenes.Count <= 0) return null;
                
                var list = new List<string>();
                
                foreach (var data in scenes)
                {
                    if (data.SceneName.Equals(scenes[0].SceneName)) continue;
                    list.Add(data.SceneName);
                }

                return list;
            }
        }


        /// <summary>
        /// Checks whether or not the group contains the scene name entered...
        /// </summary>
        /// <param name="toFind">The scene to find</param>
        /// <returns>True or False</returns>
        public bool ContainsScene(string toFind)
        {
            for (var i = 0; i < scenes.Count; i++)
            {
                if (scenes[i].SceneName != toFind) continue;
                return true;
            }

            return false;
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */        
        
        /// <summary>
        /// Clears the asset of all the scenes setup...
        /// </summary>
        private void ClearAsset()
        {
            scenes?.Clear();
        }
    }
}

#endif