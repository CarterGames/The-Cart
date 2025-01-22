#if CARTERGAMES_CART_MODULE_RUNTIMETIMERS

/*
 * Copyright (c) 2024 Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Cart.Modules.RuntimeTimers
{
    /// <summary>
    /// Handles the management of runtime timers when the game is active. Only gets used if a runtime timer is called into existence.
    /// </summary>
    public static class RuntimeTimerManager
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Transform timersParentCache;
        private static Transform persistentTimersParentCache;
        private static Transform activeTimersParentCache;
        private static Transform freeTimersParentCache;
        
        private static readonly List<RuntimeTimer> PersistentTimers = new List<RuntimeTimer>();
        private static readonly List<RuntimeTimer> ActiveTimers = new List<RuntimeTimer>();
        private static readonly Stack<RuntimeTimer> FreeTimers = new Stack<RuntimeTimer>();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets if there are any free timers for use.
        /// </summary>
        public static bool HasFreeTimer => FreeTimers.Count > 0;

        
        /// <summary>
        /// Gets/Creates the timers parent object in the DoNotDestroy scene.
        /// </summary>
        private static Transform TimersParent
        {
            get
            {
                if (timersParentCache != null) return timersParentCache;
                timersParentCache = new GameObject("Runtime Timers").transform;
                Object.DontDestroyOnLoad(timersParentCache);
                return timersParentCache;
            }
        }

        
        /// <summary>
        /// Gets/Creates the persistent timers parent object in the DoNotDestroy scene.
        /// </summary>
        private static Transform PersistentTimersParent
        {
            get
            {
                if (persistentTimersParentCache != null) return persistentTimersParentCache;
                persistentTimersParentCache = new GameObject("Persistent Timers").transform;
                persistentTimersParentCache.SetParent(TimersParent);
                return persistentTimersParentCache;
            }
        }       
        
        
        /// <summary>
        /// Gets/Creates the active timers parent object in the DoNotDestroy scene.
        /// </summary>
        private static Transform ActiveTimersParent
        {
            get
            {
                if (activeTimersParentCache != null) return activeTimersParentCache;
                activeTimersParentCache = new GameObject("Active Timers").transform;
                activeTimersParentCache.SetParent(TimersParent);
                return activeTimersParentCache;
            }
        }
        
        
        /// <summary>
        /// Gets/Creates the free timers parent object in the DoNotDestroy scene.
        /// </summary>
        private static Transform FreeTimersParent
        {
            get
            {
                if (freeTimersParentCache != null) return freeTimersParentCache;
                freeTimersParentCache = new GameObject("Free Timers").transform;
                freeTimersParentCache.SetParent(TimersParent);
                return freeTimersParentCache;
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Registers a new runtime timer into the active timers collection.
        /// </summary>
        /// <param name="runtimeTimer">The timer to register.</param>
        public static void Register(RuntimeTimer runtimeTimer)
        {
            if (runtimeTimer.GetType() == typeof(PersistentRuntimeTimer))
            {
                PersistentTimers.Add(runtimeTimer);
                runtimeTimer.transform.SetParent(PersistentTimersParent);
                return;
            }
            
            runtimeTimer.transform.SetParent(ActiveTimersParent);
            ActiveTimers.Add(runtimeTimer);
        }


        /// <summary>
        /// Removes the runtime timer entered from the active timers collection and places it into the free timers collection for re-use.
        /// </summary>
        /// <param name="runtimeTimer">The timer to unregister.</param>
        public static void UnRegister(RuntimeTimer runtimeTimer)
        {
            if (runtimeTimer.GetType() == typeof(PersistentRuntimeTimer))
            {
                runtimeTimer.Dispose();
                return;
            }
            
            ActiveTimers.Remove(runtimeTimer);
            FreeTimers.Push(runtimeTimer);
            runtimeTimer.transform.SetParent(FreeTimersParent);
            runtimeTimer.Dispose();
        }


        /// <summary>
        /// Gets the next available free timer when called. 
        /// </summary>
        /// <returns>A runtime timer that is free for re-use.</returns>
        public static RuntimeTimer GetNextFreeTimer()
        {
            var timer = FreeTimers.Pop();
            timer.transform.SetParent(ActiveTimersParent);
            return timer;
        }

        
        /// <summary>
        /// Gets if a timer is already registered or not.
        /// </summary>
        /// <param name="runtimeTimer">The timer to check.</param>
        /// <returns>If that timer is registered or not.</returns>
        public static bool IsRegistered(RuntimeTimer runtimeTimer)
        {
            if (runtimeTimer.GetType() == typeof(PersistentRuntimeTimer))
            {
                return PersistentTimers.Contains(runtimeTimer);
            }
            
            return ActiveTimers.Contains(runtimeTimer);
        }
    }
}

#endif