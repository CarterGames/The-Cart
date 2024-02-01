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

using System;
using UnityEngine;

namespace CarterGames.Cart.General
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