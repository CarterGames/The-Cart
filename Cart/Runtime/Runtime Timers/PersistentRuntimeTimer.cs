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

using System;
using UnityEngine;

namespace CarterGames.Cart
{
    public class PersistentRuntimeTimer : RuntimeTimer
    {
        /// <summary>
        /// Disposes of the timer when called.
        /// </summary>
        public override void Dispose()
        {
            gameObject.SetActive(false);
        }


        /// <summary>
        /// Sets a new runtime timer with the entered values.
        /// </summary>
        /// <param name="duration">The duration for the timer.</param>
        /// <param name="onComplete">The action to fire when the timer completed.</param>
        /// <param name="useUnscaledTime">Should the timer be in unscaled time or not?</param>
        /// <param name="autoStart">Should the timer auto start on this method call or wait for the user to manually start it?</param>
        /// <returns>The timer created.</returns>
        public new static PersistentRuntimeTimer Set(float duration, Action onComplete, bool? useUnscaledTime = false, bool? autoStart = true)
        {
            var timer = Create(duration, onComplete);
            
            if (useUnscaledTime.HasValue)
            {
                timer.UseUnscaledTime = useUnscaledTime.Value;
            }

            if (!autoStart.HasValue) return timer;
            
            if (autoStart.Value)
            {
                timer.StartTimer();
            }

            timer.TimerCompleted.Add(() => timer.gameObject.SetActive(false));
            return timer;
        }


        /// <summary>
        /// Creates a new timer or uses an existing one that isn't in use...
        /// </summary>
        /// <param name="duration">The duration for the timer.</param>
        /// <param name="onComplete">The action to fire when the timer completed.</param>
        /// <returns>The timer created.</returns>
        private static PersistentRuntimeTimer Create(float duration, Action onComplete)
        {
            var obj = new GameObject("PersistentRuntimeTimer-" + Guid.NewGuid()).AddComponent<PersistentRuntimeTimer>();
            DontDestroyOnLoad(obj.gameObject);
            obj.Initialize(duration, onComplete);
            RuntimeTimerManager.Register(obj);
            return obj;
        }
    }
}