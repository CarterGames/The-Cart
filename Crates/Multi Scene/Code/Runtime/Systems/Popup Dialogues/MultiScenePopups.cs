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
using UnityEditor;

namespace CarterGames.Cart.Crates.MultiScene.Editor
{
    /// <summary>
    /// Container class that holds all the popups editor dialogues that the system uses to prompt the user some info...
    /// </summary>
    public static class MultiScenePopups
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */        
        
        /// <summary>
        /// Broadcasts the result of a editor dialogue popups for use...
        /// </summary>
        public static readonly Evt<string, bool> OnPopupResolved = new Evt<string, bool>();
        
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Dialogue Popups
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */        
        
        /// <summary>
        /// Displays a dialogue popup for the user trying to use an invalid scene group setup...
        /// </summary>
        public static void ShowInvalidSceneGroup()
        {
            OnPopupResolved.Raise("InvalidSceneGroup", EditorUtility.DisplayDialog("Invalid Scene Group Detected!",
                "The scene group you are trying to load is invalid! Please ensure you have setup the group correctly in the editor.", "Close"));
        }
    }
}

#endif