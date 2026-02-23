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

using System.Threading.Tasks;
using CarterGames.Cart.Events;
using UnityEditor;
using UnityEditor.Callbacks;

namespace CarterGames.Cart.Management.Editor
{
    /// <summary>
    /// Handles any reload listeners in the project for the asset.
    /// </summary>
    public static class AssetReloadHandler
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Raises when the reload has occured.
        /// </summary>
        public static readonly Evt Reloaded = new Evt();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Add subscription to the delay call when scripts reload.
        /// </summary>
        [DidReloadScripts]
        private static void FireReloadCalls()
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating)
            {
                EditorApplication.delayCall -= CallListeners;
                EditorApplication.delayCall += CallListeners;
                return;
            }
            
            EditorApplication.delayCall -= CallListeners;
            EditorApplication.delayCall += CallListeners;
        }
        

        /// <summary>
        /// Updates all the listeners when called.
        /// </summary>
        private static async void CallListeners()
        {
            var reloadClasses = InterfaceHelper.GetAllInterfacesInstancesOfType<IAssetEditorReload>();
            
            if (reloadClasses.Length > 0)
            {
                foreach (var init in reloadClasses)
                {
                    init.OnEditorReloaded();
                    await Task.Yield();
                }
            }
            
            Reloaded.Raise();
        }
    }
}