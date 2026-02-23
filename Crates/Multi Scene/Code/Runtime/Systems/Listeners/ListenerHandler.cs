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

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CarterGames.Cart.Data;
using CarterGames.Cart.Logs;
using UnityEngine.SceneManagement;

namespace CarterGames.Cart.Crates.MultiScene
{
    /// <summary>
    /// Handles the logic for running the listeners when called.
    /// </summary>
    public static class ListenerHandler
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private const string AwakeMethodName = "OnMultiSceneAwake";
        private const string EnableMethodName = "OnMultiSceneEnable";
        private const string StartMethodName = "OnMultiSceneStart";

        private static List<OrderedListenerData<IMultiSceneAwake>> awakeOrderedListeners;
        private static List<OrderedListenerData<IMultiSceneEnable>> enableOrderedListeners;
        private static List<OrderedListenerData<IMultiSceneStart>> startOrderedListeners;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Assigns the sorted lists for use. 
        /// </summary>
        private static void GetSortedListeners()
        {
            awakeOrderedListeners = OrderedHandler.OrderListeners(MultiSceneRef.GetComponentsFromAllScenes<IMultiSceneAwake>(), AwakeMethodName); 
            enableOrderedListeners = OrderedHandler.OrderListeners(MultiSceneRef.GetComponentsFromAllScenes<IMultiSceneEnable>(), EnableMethodName); 
            startOrderedListeners = OrderedHandler.OrderListeners(MultiSceneRef.GetComponentsFromAllScenes<IMultiSceneStart>(), StartMethodName); 
        }
        
        
        /// <summary>
        /// Calls all the listeners, but only actually runs on the last scene to load...
        /// </summary>
        public static void CallListeners(Scene s, LoadSceneMode l)
        {
            MultiSceneManager.OnSceneLoaded.Raise(s.name);
            
            if (!s.name.Equals(MultiSceneManager.ActiveSceneGroup.scenes[MultiSceneManager.ActiveSceneGroup.scenes.Count - 1].SceneName))
            {
                return;
            }

            GetSortedListeners();

            typeof(MultiSceneManager).GetMethod("UpdateActiveSceneNames", BindingFlags.Static)?.Invoke(null, null);

            MultiSceneManager.MonoInstance.StartCoroutine(CallMultiSceneListeners());
            SceneManager.sceneLoaded -= CallListeners;
        }


        private static IEnumerator CallMultiSceneListeners()
        {
            yield return CallListeners(awakeOrderedListeners, "OnMultiSceneAwake");
            yield return null;
            yield return CallListeners(enableOrderedListeners, "OnMultiSceneEnable");
            yield return null;
            yield return CallListeners(startOrderedListeners, "OnMultiSceneStart");
        }


        private static IEnumerator CallListeners<T>(List<OrderedListenerData<T>> orderedListeners, string methodName)
        {
            var count = 0;

            if (methodName.Equals(string.Empty))
            {
                CartLogger.LogWarning<MultiSceneLogs>("Unable to find the interface type to send listeners for... skipping.");
                yield break;
            }
            
            if (orderedListeners.Count <= 0) yield break;

            foreach (var listener in orderedListeners)
            {
                listener.Listener.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance)?.Invoke(listener.Listener, null);
                count++;

                if (count < DataAccess.GetAsset<MultiSceneSettings>().ListenerFrequency) continue;
                
                count = 0;
                yield return null;
            }
        }
    }
}

#endif