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

namespace CarterGames.Cart
{
    /// <summary>
    /// An instance that can be statically referenced for things such as coroutines in static classes etc.
    /// </summary>
    public class MonoInstance : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private const string MonoName = "[DND] Mono Instance (The Cart)";
        private static Instance<MonoInstance> instance;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The instance of a monoBehaviour to access.
        /// </summary>
        public static Instance<MonoInstance> Instance
        {
            get
            {
                if (instance != null) return instance;
                
                var obj = new GameObject(MonoName);
                obj.AddComponent<MonoInstance>();
                instance = new Instance<MonoInstance>(obj.GetComponent<MonoInstance>(), true);
                
                return instance;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Mono Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var obj = new GameObject(MonoName);
            obj.AddComponent<MonoInstance>();
            instance = new Instance<MonoInstance>(obj.GetComponent<MonoInstance>(), true);
        }
        
        private void Awake() => MonoEvents.Awake.Raise();
        private void Start() => MonoEvents.Start.Raise();
        private void OnDestroy() => MonoEvents.OnDestroy.Raise();
        private void OnApplicationFocus(bool hasFocus) => MonoEvents.ApplicationFocus.Raise(hasFocus);
        private void OnApplicationQuit() => MonoEvents.ApplicationQuit.Raise();
    }
}