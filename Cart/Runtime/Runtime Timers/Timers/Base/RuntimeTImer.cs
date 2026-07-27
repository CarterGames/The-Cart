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
using CarterGames.Cart.Events;
using UnityEngine;

namespace CarterGames.Cart
{
    /// <summary>
    /// An abstract class to handle code timers without the use of a coroutine.
    /// </summary>
    public abstract class RuntimeTimer : IDisposable
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Defines if the timer is currently is use.
        /// </summary>
        public bool IsActive { get; private set; }
        
        
        /// <summary>
        /// Defines if the timer is currently paused.
        /// </summary>
        public bool IsPaused { get; private set; }
        
        
        /// <summary>
        /// Defines the current time on the timer.
        /// </summary>
        public float CurrentTime { get; protected set; }


        /// <summary>
        /// Gets the current time in seconds.
        /// </summary>
        public int CurrentTimeInSeconds
        {
            get
            {
                return TimerType switch
                {
                    TimerType.Unassigned => Mathf.RoundToInt(CurrentTime / InitialTime),
                    TimerType.Countdown => Mathf.CeilToInt(CurrentTime / InitialTime),
                    TimerType.Stopwatch => Mathf.RoundToInt(CurrentTime / InitialTime),
                    _ => Mathf.RoundToInt(CurrentTime / InitialTime)
                };
            }
        }


        /// <summary>
        /// Defines the initial time set when the timer was created.
        /// </summary>
        protected float InitialTime { get; set; }
        
        
        /// <summary>
        /// Defines if the setup uses unscaled time or not. 
        /// </summary>
        protected bool UseUnscaledTime { get; set; }

        
        /// <summary>
        /// Returns the current timer progress clamped between 0 & 1.
        /// Returns -1 when not applicable to the timer type (Such as stopwatch)
        /// </summary>
        public float Progress
        {
            get
            {
                if (TimerType == TimerType.Stopwatch || TimerType == TimerType.Unassigned) return -1; 
                return Mathf.Clamp(CurrentTime / InitialTime, 0, 1);
            }
        }

        
        /// <summary>
        /// Gets if the timer has completed or not.
        /// </summary>
        public abstract bool IsComplete { get; }
        
        
        /// <summary>
        /// Defines the update source for the timer.
        /// </summary>
        private TimerUpdateSource UpdateSource { get; set; }
        
        
        /// <summary>
        /// Defines the timer type this timer is.
        /// </summary>
        protected TimerType TimerType { get; set; }
        
        
        /// <summary>
        /// Gets if the timer has been disposed of or not.
        /// </summary>
        private bool IsDisposedOf { get; set; }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Raised when the timer is started.
        /// </summary>
        public readonly Evt StartedEvt = new Evt();
        
        
        /// <summary>
        /// Raised when the timer is ticked.
        /// </summary>
        public readonly Evt TickedEvt = new Evt();
        
        
        /// <summary>
        /// Raised when the timer has passed a new second.
        /// </summary>
        public readonly Evt SecondPassedEvt = new Evt();
        
        
        /// <summary>
        /// Raised when the timer is stopped.
        /// </summary>
        public readonly Evt StoppedEvt = new Evt();
        
        
        /// <summary>
        /// Raised when the timer is completed.
        /// </summary>
        public readonly Evt CompletedEvt = new Evt();
        
        
        /// <summary>
        /// Raised when the timer has changed pause state.
        /// </summary>
        public readonly Evt<bool> PauseStateChangedEvt = new Evt<bool>();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructor
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected RuntimeTimer(float initialTime, TimerUpdateSource updateSource = TimerUpdateSource.Global, bool? useUnscaledTime = true)
        {
            InitialTime = initialTime;
            UpdateSource = updateSource;
            UseUnscaledTime = useUnscaledTime.GetValueOrDefault(true);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            if (IsActive) return;

            if (UpdateSource == TimerUpdateSource.Global)
            {
                RuntimeTimerGlobalUpdateHandler.UpdateTickedEvt.Add(OnUpdate);
            }
            
            CurrentTime = InitialTime;
            IsActive = true;
            StartedEvt.Raise();
        }


        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            if (!IsActive) return;
            
            if (UpdateSource == TimerUpdateSource.Global)
            {
                RuntimeTimerGlobalUpdateHandler.UpdateTickedEvt.Remove(OnUpdate);
            }
            
            IsActive = false;
            StoppedEvt.Raise();
        }


        /// <summary>
        /// Pauses the timer.
        /// </summary>
        public void Pause()
        {
            IsPaused = true;
            PauseStateChangedEvt.Raise(IsPaused);
        }
        
        
        /// <summary>
        /// Resumes the timer.
        /// </summary>
        public void Resume()
        {
            IsPaused = false;
            PauseStateChangedEvt.Raise(IsPaused);
        }


        /// <summary>
        /// Resets the timer.
        /// </summary>
        public virtual void Reset()
        {
            CurrentTime = InitialTime;
        }


        /// <summary>
        /// Resets the timer with a new initial timer.
        /// </summary>
        public virtual void Reset(float newTime)
        {
            InitialTime = newTime;
            Reset();
        }

        
        /// <summary>
        /// Gets the time change based on settings.
        /// </summary>
        /// <returns>Float</returns>
        private float GetTimeChange()
        {
            return UseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        }


        /// <summary>
        /// Sets the update source of the timer when called.
        /// </summary>
        /// <param name="updateSource">The source to set to.</param>
        public void SetUpdateSource(TimerUpdateSource updateSource)
        {
            if (UpdateSource == updateSource) return;

            if (UpdateSource == TimerUpdateSource.Global)
            {
                RuntimeTimerGlobalUpdateHandler.UpdateTickedEvt.Remove(OnUpdate);
            }

            UpdateSource = updateSource;
            
            if (UpdateSource == TimerUpdateSource.Global)
            {
                RuntimeTimerGlobalUpdateHandler.UpdateTickedEvt.Add(OnUpdate);
            }
        }


        /// <summary>
        /// Runs when the timer is ticked from the update source.
        /// </summary>
        /// <param name="change">The change to apply.</param>
        protected abstract void OnTicked(float change);

        
        /// <summary>
        /// Runs update logic for the timer.
        /// </summary>
        private void OnUpdate()
        {
            if (!IsActive || IsPaused) return;
            OnTicked(GetTimeChange());
            if (!IsComplete) return;
            OnTimerCompleteDetected();
        }


        /// <summary>
        /// Runs when the timer has reached a complete state.
        /// </summary>
        protected virtual void OnTimerCompleteDetected()
        {
            Stop();
            CompletedEvt.Raise();
        }
        
        
        /// <summary>
        /// De-constructs the timer when needed.
        /// </summary>
        ~RuntimeTimer()
        {
            DisposeTimer();
        }


        /// <summary>
        /// Disposes of the timer.
        /// </summary>
        private void DisposeTimer()
        {
            if (IsDisposedOf) return;

            StartedEvt.Clear();
            SecondPassedEvt.Clear();
            TickedEvt.Clear();
            StoppedEvt.Clear();
            PauseStateChangedEvt.Clear();

            if (UpdateSource == TimerUpdateSource.Global)
            {
                RuntimeTimerGlobalUpdateHandler.UpdateTickedEvt.Remove(OnUpdate);
            }
            
            IsDisposedOf = true;
        }
        

        /// <summary>
        /// Handles the disposing of the timer on IDisposable.
        /// </summary>
        public void Dispose()
        {
            DisposeTimer();
            GC.SuppressFinalize(this);
        }
    }
}