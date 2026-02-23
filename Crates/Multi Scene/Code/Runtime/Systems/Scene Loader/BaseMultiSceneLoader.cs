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

using UnityEngine;

namespace CarterGames.Cart.Crates.MultiScene
{
    /// <summary>
    /// Provides a base set of logic for loading a scene group to inherit from and override as needed.
    /// </summary>
    /// <remarks>
    /// Not abstract as it can be used as is for basic implementations.
    /// </remarks>
    public class BaseMultiSceneLoader : MonoBehaviour
    {
        /// <summary>
        /// The scene group to load...
        /// </summary>
        [SerializeField] protected SceneGroup loadGroup;

        
        /// <summary>
        /// Gets whether or not the scene group is loading or not...
        /// </summary>
        protected bool IsLoading { get; set; }
        
        
        /// <summary>
        /// Loads a scene group...
        /// </summary>
        public virtual void LoadSceneGroup()
        {
            if (IsLoading) return;
            MultiSceneManager.LoadScenes(loadGroup);
            IsLoading = true;
        }
    }
}

#endif