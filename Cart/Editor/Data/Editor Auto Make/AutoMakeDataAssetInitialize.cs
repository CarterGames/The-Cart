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

using CarterGames.Cart.Management.Editor;

namespace CarterGames.Cart.Editor
{
    public class AutoMakeDataAssetInitialize : IAssetEditorInitialize, IAssetEditorReload
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IAssetEditorInitialize
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Defines the order that this initializer run at.
        /// </summary>
        public int InitializeOrder => 0;
        

        /// <summary>
        /// Runs when the asset initialize flow is used.
        /// </summary>
        public void OnEditorInitialized()
        {
            if (AutoMakeDataAssetManager.HasAllAssets()) return;
            AutoMakeDataAssetManager.TryCreateAssets();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IAssetEditorReload
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Runs when the asset reload flow is used.
        /// </summary>
        public void OnEditorReloaded()
        {
            if (AutoMakeDataAssetManager.HasAllAssets()) return;
            AutoMakeDataAssetManager.TryCreateAssets();
        }
    }
}