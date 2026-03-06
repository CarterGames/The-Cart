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

using CarterGames.Cart.Management.Editor;
using UnityEditor;

namespace CarterGames.Cart.Crates.MultiScene.Editor
{
    /// <summary>
    /// Detects events in the engine and processes them for use in the asset. 
    /// </summary>
    public sealed class EditorEvtDetector : AssetPostprocessor, IAssetEditorReload
    {
        private static string[] importedAssetsCache;
        
        
        /// <summary>
        /// Runs when any file has finished being added to the project.
        /// </summary>
        /// <param name="importedAssets">array of all imported assets.</param>
        /// <param name="deletedAssets">array of all deleted assets.</param>
        /// <param name="movedAssets">array of all moved assets.</param>
        /// <param name="movedFromAssetPaths">array of all moved assets to a new path.</param>
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            importedAssetsCache = importedAssets;
        }
        

        public void OnEditorReloaded()
        {
            if (importedAssetsCache == null) return;
            
            foreach (var asset in importedAssetsCache)
            {
                if (!asset.Contains(".asset")) continue;
                MultiSceneEditorEvents.SceneGroups.OnSceneGroupCreated.Raise();
                return;
            }
        }
    }
}

#endif