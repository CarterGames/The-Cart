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

using System;
using System.Collections;
using CarterGames.Cart.Core.Events;
using UnityEngine;

namespace CarterGames.Cart.Modules.RuntimeTimers
{
    /// <summary>
    /// A runtime timer that can invoke actions when completed and can be created from anywhere in runtime space.
    /// </summary>
    [AddComponentMenu("Carter Games/The Cart/Modules/Runtime Timers/Timer")]
    public class RuntimeTimer : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected Coroutine timerRoutine;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets if the timer is currently active.
        /// </summary>
        public bool TimerActive => timerRoutine != null;
        
        
        /// <summary>
        /// Gets if the timer is paused or not.
        /// </summary>
        private bool TimerPaused { get; set; }
        
        
        /// <summary>
        /// Gets the time remaining on the timer.
        /// </summary>
        public float TimeRemaining { get; protected set; }


        /// <summary>
        /// Gets the time remaining on the timer.
        /// </summary>
        public int TimeRemainingInSeconds => Mathf.CeilToInt(TimeRemaining);

        
        /// <summary>
        /// Gets the duration of the timer.
        /// </summary>
        public float TimerDuration { get; protected set; }
        
        
        /// <summary>
        /// Gets/Sets whether the timer uses unscaled time for its time deduction.
        /// </summary>
        protected bool UseUnscaledTime { get; set; }

        
        /// <summary>
        /// Gets the fraction of time remaining on the timer (between 0-1).
        /// </summary>
        public float TimeRemainingFraction => TimeRemaining / TimerDuration;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public readonly Evt TimerCompleted = new Evt();
        public readonly Evt<int> TimerSecondPassed = new Evt<int>();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Initializes the timer for use when called.
        /// </summary>
        /// <param name="duration">The duration for the timer.</param>
        /// <param name="onComplete">The action to fire when the timer completed.</param>
        protected virtual void Initialize(float duration, Action onComplete)
        {
            TimerDuration = duration;
            TimerCompleted.Add(onComplete);

            if (gameObject.activeSelf) return;
            gameObject.SetActive(true);
        }
        
        
        /// <summary>
        /// Disposes of the timer when called.
        /// </summary>
        public virtual void Dispose()
        {
            UseUnscaledTime = false;
            TimerCompleted.Clear();
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
        public static RuntimeTimer Set(float duration, Action onComplete, bool? useUnscaledTime = false, bool? autoStart = true)
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

            return timer;
        }


        /// <summary>
        /// Creates a new timer or uses an existing one that isn't in use...
        /// </summary>
        /// <param name="duration">The duration for the timer.</param>
        /// <param name="onComplete">The action to fire when the timer completed.</param>
        /// <returns>The timer created.</returns>
        private static RuntimeTimer Create(float duration, Action onComplete)
        {
            if (RuntimeTimerManager.HasFreeTimer)
            {
                var timer = RuntimeTimerManager.GetNextFreeTimer();
                timer.Initialize(duration, onComplete);
                return timer;
            }
            
            var obj = new GameObject("RuntimeTimer-" + Guid.NewGuid()).AddComponent<RuntimeTimer>();
            DontDestroyOnLoad(obj.gameObject);
            obj.Initialize(duration, onComplete);
            RuntimeTimerManager.Register(obj);
            return obj;
        }


        /// <summary>
        /// Starts the timer when called.
        /// </summary>
        public void StartTimer()
        {
            if (TimerActive)
            {
                if (TimerPaused)
                {
                    ResumeTimer();
                }
                
                return;
            }
            
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);    
            }
            
            TimerPaused = false;
            timerRoutine = StartCoroutine(Co_TimerRoutine());
        }


        /// <summary>
        /// Pauses the timer when called.
        /// </summary>
        public void PauseTimer()
        {
            StopCoroutine(timerRoutine);
            TimerPaused = true;
        }


        /// <summary>
        /// Resumes the timer when called.
        /// </summary>
        public void ResumeTimer()
        {
            if (!TimerPaused) return;
            timerRoutine = StartCoroutine(Co_TimerRoutine(TimeRemaining));
            TimerPaused = false;
        }


        /// <summary>
        /// Stops the timer when called.
        /// </summary>
        public void StopTimer()
        {
            StopCoroutine(timerRoutine);
            timerRoutine = null;
            TimeRemaining = 0;
            TimerPaused = false;
            RuntimeTimerManager.UnRegister(this);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Coroutines
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Runs the actual timer & complete evt.
        /// </summary>
        private IEnumerator Co_TimerRoutine(float duration = -1)
        {
            if (duration.Equals(-1))
            {
                TimeRemaining = TimerDuration;
            }
            
            var t = 0f;

            while (TimeRemaining > 0f)
            { 
                var change = UseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                TimeRemaining -= change;
                t += change;

                if (t > 1)
                {
                    t = 0;
                    TimerSecondPassed.Raise(TimeRemainingInSeconds);
                }
                
                yield return null;
            }
            
            TimerCompleted.Raise();
            timerRoutine = null;
            RuntimeTimerManager.UnRegister(this);
        }
    }
}

#endif