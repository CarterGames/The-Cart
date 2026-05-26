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

using CarterGames.Cart.Data;
using UnityEngine;

namespace CarterGames.Cart.Crates.MultiScene
{
    public class MultiSceneSettings : DataAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private SceneGroupEditorLoadMode sceneGroupLoadMode;
        [SerializeField] private SceneGroup startGroup;
        [SerializeField] private SceneGroup lastGroupLoaded;

#pragma warning disable 0414
        [SerializeField] private bool showToolbar = true;
#pragma warning restore
        
        [SerializeField] private int listenerFrequency = 5;
        [SerializeField] private bool useUnloadResources;

        [SerializeField, HideInInspector] private bool showSceneGroupOptions;
        [SerializeField, HideInInspector] private bool showGeneralOptions;
        [SerializeField, HideInInspector] private bool showGroupCategoryOptions;
        [SerializeField, HideInInspector] private bool showDefaultGroupsInSetAsset;
        [SerializeField, HideInInspector] private bool showUserGroupsInSetAsset;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The scene group to load first.
        /// </summary>
        public SceneGroup StartGroup => startGroup;
        
        /// <summary>
        /// How many of the listeners of IMultiSceneAwake/Enable/Start invoke per frame.
        /// </summary>
        public int ListenerFrequency => listenerFrequency;
        
        /// <summary>
        /// Defines if the Resources.UnloadUnusedAssets is used when changing scene groups.
        /// </summary>
        public bool UseUnloadResources => useUnloadResources;
        
        
        /// <summary>
        /// The load mode to use when loading scene groups.
        /// </summary>
        public SceneGroupEditorLoadMode LoadMode => sceneGroupLoadMode;

        
        /// <summary>
        /// The last scene group loaded if saved.
        /// </summary>
        public SceneGroup LastGroup
        {
            get => lastGroupLoaded;
            set => lastGroupLoaded = value;
        }
    }
}

#endif