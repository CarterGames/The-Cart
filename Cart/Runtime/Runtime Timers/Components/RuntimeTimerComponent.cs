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
using UnityEngine.Events;

namespace CarterGames.Cart.Components
{
    /// <summary>
    /// A component to add to apply a runtime timer from a GameObject without code.
    /// </summary>
    [AddComponentMenu("Carter Games/The Cart/Cart/RuntimeTimerComponent")]
    public class RuntimeTimerComponent : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private TimerType timerType;
        [SerializeField] private float timerDuration;
        [SerializeField] private float stopAt;
        [SerializeField] private bool unscaledTime = true;
        [SerializeField] private bool startOnEnable;
        
        [SerializeField] private bool loop;
        [SerializeField] private bool infiniteLoop;
        [SerializeField] private int loops = 1;
        
        [SerializeField] private bool showUnityEvents;
        [SerializeField] private UnityEvent timerStartedUnityEvt;
        [SerializeField] private UnityEvent timerTickedUnityEvt;
        [SerializeField] private UnityEvent timerSecondPassedUnityEvt;
        [SerializeField] private UnityEvent timerLoopedUnityEvt;
        [SerializeField] private UnityEvent timerCompleteUnityEvt;
        
        private RuntimeTimer timerInstance;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the timer used in this component.
        /// </summary>
        public RuntimeTimer Timer => timerInstance;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void Awake()
        {
            switch (timerType)
            {
                case TimerType.Countdown:

                    timerInstance = loop 
                        ? new CountdownRuntimeTimer(timerDuration, loops, timerLoopedUnityEvt.Invoke, TimerUpdateSource.Local, unscaledTime) 
                        : new CountdownRuntimeTimer(timerDuration, TimerUpdateSource.Local, unscaledTime);
          
                    break;
                case TimerType.Stopwatch:
                    timerInstance = new StopwatchRuntimeTimer(timerDuration, TimerUpdateSource.Local, unscaledTime);
                    break;
            }
            
            timerInstance.StartedEvt.Add(timerStartedUnityEvt.Invoke);
            timerInstance.TickedEvt.Add(timerTickedUnityEvt.Invoke);
            timerInstance.SecondPassedEvt.Add(timerSecondPassedUnityEvt.Invoke);
            timerInstance.CompletedEvt.Add(timerCompleteUnityEvt.Invoke);
        }


        private void OnEnable()
        {
            if (startOnEnable && !timerInstance.IsActive)
            {
                StartTimer();
            }
        }


        private void OnDestroy()
        {
            StopTimer();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Starts the timer when called.
        /// </summary>
        public void StartTimer() => timerInstance.Start();


        /// <summary>
        /// Pauses the timerOld when called.
        /// </summary>
        public void PauseTimer() => timerInstance.Pause();


        /// <summary>
        /// Resumes the timerOld when called.
        /// </summary>
        public void ResumeTimer() => timerInstance.Resume();
        
        
        /// <summary>
        /// Stops the timerOld when called.
        /// </summary>
        public void StopTimer() => timerInstance.Stop();
        

        /// <summary>
        /// Resets the timer when called.
        /// </summary>
        public void ResetTimer() => timerInstance.Reset();
        
        
        /// <summary>
        /// Changes the duration of the timer when called and resets it.
        /// </summary>
        /// <param name="initialTime">The initial time to set.</param>
        public void ChangeDuration(float initialTime) => timerInstance.Reset(initialTime);
    }
}