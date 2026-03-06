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
using CarterGames.Cart;
using CarterGames.Cart.Data;
using CarterGames.Cart.Events;
using CarterGames.Cart.Crates.MultiScene.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarterGames.Cart.Crates.MultiScene
{
    /// <summary>
    /// The main manager class for the asset. Used to load scenes instead of the SceneManager class. 
    /// </summary>
    public static class MultiSceneManager
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private const string PlaceholderSceneName = "Multi Scene Placeholder Scene";
        private static List<string> activeSceneNames;
        private static bool hasCachedScenesList;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The default scene group to load
        /// </summary>
        public static SceneGroup ActiveSceneGroup { get; private set; }


        /// <summary>
        /// Gets the settings asset for this asset for use...
        /// </summary>
        private static MultiSceneSettings Settings => DataAccess.GetAsset<MultiSceneSettings>();


        /// <summary>
        /// Gets the mono script for use...
        /// </summary>
        public static MonoInstance MonoInstance => MonoInstance.Instance;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Runs before a scene group loads
        /// </summary>
        public static readonly Evt BeforeScenesLoaded = new Evt();
        
        
        /// <summary>
        /// Runs after a scene group has loaded & all listeners have been called
        /// </summary>
        public static readonly Evt PostScenesLoaded = new Evt();

        
        /// <summary>
        /// Runs when each scene is loaded
        /// </summary>
        public static readonly Evt<string> OnSceneLoaded = new Evt<string>();

        
        /// <summary>
        /// Runs when each scene is loaded
        /// </summary>
        public static readonly Evt<string> OnSceneUnloaded = new Evt<string>();
        
        
        /// <summary>
        /// Runs when a scene group has loaded
        /// </summary>
        public static readonly Evt OnSceneGroupLoaded = new Evt();

        
        /// <summary>
        /// Runs when a scene group has loaded & returns the group as a parameter
        /// </summary>
        public static readonly Evt<SceneGroup> OnSceneGroupLoadedWithCtx = new Evt<SceneGroup>();
        
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Utility Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets whether or not a scene of the name entered is in the current group.
        /// </summary>
        /// <param name="sceneName">The scene name to lookup.</param>
        /// <returns>True/False</returns>
        public static bool IsSceneInGroup(string sceneName) => ActiveSceneGroup.ContainsScene(sceneName);
        
        
        /// <summary>
        /// Gets whether or not a scene is in the group requested.
        /// </summary>
        /// <param name="group">The group to look through.</param>
        /// <param name="sceneName">The scene name to lookup.</param>
        /// <returns>True/False</returns>
        public static bool IsSceneInGroup(SceneGroup group, string sceneName) => group.ContainsScene(sceneName);
        
        
        /// <summary>
        /// Returns whether or not a scene of the name entered is loaded in the current scene group.
        /// </summary>
        /// <param name="sceneName">The scene name to lookup.</param>
        /// <returns>True/False</returns>
        public static bool IsSceneLoaded(string sceneName)
        {
            if (!hasCachedScenesList)
            {
                UpdateActiveSceneNames();
            }

            return activeSceneNames.Contains(sceneName);
        }
        
        
        /// <summary>
        /// Sets the active group to the group entered...
        /// </summary>
        /// <param name="group">the group to set to</param>
        public static void SetGroup(SceneGroup group)
        {
            ActiveSceneGroup = group;
            UpdateActiveSceneNames();
        }
        
        
        /// <summary>
        /// Updates the list of active scenes for checking...
        /// </summary>
        private static void UpdateActiveSceneNames()
        {
            var activeSceneNamesList = new List<string>();
            
            for (var i = SceneManager.sceneCount - 1; i >= 0; i--)
            {
                activeSceneNamesList.Add(SceneManager.GetSceneAt(i).name);
            }

            activeSceneNames = activeSceneNamesList;
            hasCachedScenesList = true;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Scene Management Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Initialises with the scene group last loaded or the default one based on the asset settings...
        /// </summary>
        /// <remarks>This runs at runtime to load the scene group instead of whatever is currently loaded...</remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialise()
        {
            if (Settings.LoadMode == SceneGroupEditorLoadMode.None) return;
            
            LoadScenes(Settings.LoadMode == SceneGroupEditorLoadMode.Default 
                ? Settings.StartGroup 
                : Settings.LastGroup);
        }
        

        /// <summary>
        /// Loads all scenes in the start group, reloading and dup scenes as it goes by default.
        /// </summary>
        /// <param name="reloadDupScenes">Should scenes in the start group that are loaded already be reloaded?</param>
        public static void LoadScenes(bool reloadDupScenes = true)
        {
            LoadScenes(Settings.StartGroup, reloadDupScenes);
        }
        
        
        /// <summary>
        /// Loads all the scenes in the requested group, reloading and dup scenes as it goes by default.
        /// </summary>
        /// <param name="group">The group to load.</param>
        /// <param name="reloadDupScenes">Should scenes in the start group that are loaded already be reloaded?</param>
        public static void LoadScenes(SceneGroup group, bool reloadDupScenes = true)
        {
            RunSceneLoading(group, reloadDupScenes);
        }


        /// <summary>
        /// Loads the scenes in the selected scene group...overriding any existing active scenes. 
        /// </summary>
        private static void RunSceneLoading(SceneGroup group, bool? reloadDupScenes = true)
        {
            if (!group.IsValid)
            {
#if UNITY_EDITOR
                MultiScenePopups.ShowInvalidSceneGroup();
#endif
                return;
            }
                
            ActiveSceneGroup = group;

            var loadedScenes = new List<string>();

            // Unloads current scenes...
            UnloadAllActiveScenes();

            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                loadedScenes.Add(SceneManager.GetSceneAt(i).name);
            }


            // Loads the new scenes...
            for (var i = 0; i < ActiveSceneGroup.scenes.Count; i++)
            {
                var sceneData = ActiveSceneGroup.scenes[i];

                // Calls for the listeners to fire when the last scene in the group is loaded...
                if (i.Equals(ActiveSceneGroup.scenes.Count - 1))
                {
                    SceneManager.sceneLoaded += ListenerHandler.CallListeners;
                }
                
                // Skips if scenes are not the be reloaded...
                if (reloadDupScenes == false)
                {
                    if (loadedScenes.Contains(sceneData.SceneName)) continue;
                }

                // Loads the scene via the path...
                SceneManager.LoadSceneAsync(sceneData.ScenePath, new LoadSceneParameters(i.Equals(0) 
                        ? LoadSceneMode.Single 
                        : LoadSceneMode.Additive));

                // Runs any events for scenes loading...
                OnSceneLoaded.Raise(sceneData.SceneName);
            }

            if (loadedScenes.Contains(PlaceholderSceneName))
            {
                SceneManager.UnloadSceneAsync(PlaceholderSceneName);
            }
            
            OnSceneGroupLoaded.Raise();
            OnSceneGroupLoadedWithCtx.Raise(group);
            UpdateActiveSceneNames();
        }
        
        
        
        /// <summary>
        /// Unloads all the active scenes
        /// </summary>
        public static void UnloadAllActiveScenes()
        {
            SceneManager.CreateScene(PlaceholderSceneName);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(PlaceholderSceneName));
            
            var sceneNamesList = new List<string>();

            for (var i = SceneManager.sceneCount - 1; i >= 0; i--)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.buildIndex < 0) continue;
                sceneNamesList.Add(scene.name);
            }

            foreach (var sceneName in sceneNamesList)
            {
                if (sceneName.Equals(PlaceholderSceneName)) continue;
                SceneManager.UnloadSceneAsync(sceneName);
                OnSceneUnloaded.Raise(sceneName);
            }
            
            if (!DataAccess.GetAsset<MultiSceneSettings>().UseUnloadResources) return;
            Resources.UnloadUnusedAssets();
        }
        
        
        /// <summary>
        /// Unloads all the additive scenes loaded but keeps the base scene as is unless overridden with a load method...
        /// </summary>
        public static void UnloadAllAdditiveScenes()
        {
            var sceneNames = new List<string>();
            var activeSceneName = SceneManager.GetActiveScene().name;

            for (var i = SceneManager.sceneCount - 1; i >= 0; i--)
            {
                if (SceneManager.GetSceneAt(i).name.Equals(activeSceneName)) continue;
                sceneNames.Add(SceneManager.GetSceneAt(i).name);
            }

            foreach (var scene in sceneNames)
            {
                SceneManager.UnloadSceneAsync(scene);
                OnSceneUnloaded.Raise(scene);
            }

            if (!DataAccess.GetAsset<MultiSceneSettings>().UseUnloadResources) return;
            Resources.UnloadUnusedAssets();
        }


        /// <summary>
        /// Reloads the current scene group as if you just loaded the scene again...
        /// </summary>
        public static void ReloadScenes()
        {
            UnloadAllActiveScenes();
            LoadScenes();
        }
    }
}

#endif