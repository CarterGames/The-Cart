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

using CarterGames.Cart.Events;

namespace CarterGames.Cart.Crates.MultiScene.Editor
{
    /// <summary>
    /// Container class for events used to broadcast info in the editor space only...
    /// </summary>
    public struct MultiSceneEditorEvents
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Scene Group Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */     
        
        /// <summary>
        /// Contains any events that relate to scene group categories...
        /// </summary>
        public struct SceneGroups
        {
            /// <summary>
            /// Raises when a category is changed...
            /// </summary>
            public static readonly Evt OnSceneGroupCreated = new Evt();
            

            /// <summary>
            /// Raises when a category is changed...
            /// </summary>
            public static readonly Evt OnSceneGroupCategoryChanged = new Evt();
            
            
            /// <summary>
            /// Raises when a scene group is loaded in the editor only...
            /// </summary>
            public static readonly Evt OnSceneGroupLoadedInEditor = new Evt();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Settings Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// Contains any events that relate to the settings asset being modified...
        /// </summary>
        public struct Settings
        {
            /// <summary>
            /// Raises when the settings asset is modified...
            /// </summary>
            public static readonly Evt OnSettingChanged = new Evt();
            
            
            /// <summary>
            /// Raises when a new settings asset is generated after an existing one is deleted...
            /// </summary>
            public static readonly Evt OnSettingsAssetRegenerated = new Evt();
            
            
            /// <summary>
            /// Raises when the group categories are edited in the settings window...
            /// </summary>
            public static readonly Evt OnGroupCategoriesChanged = new Evt();
        }
    }
}

#endif