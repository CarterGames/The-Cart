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

namespace CarterGames.Cart
{
    /// <summary>
    /// Handles a runtime timer that counts down from an initial value to zero.
    /// </summary>
    public class CountdownRuntimeTimer : RuntimeTimer
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets if the timer is complete.
        /// </summary>
        public override bool IsComplete => CurrentTime <= 0;
        
        
        /// <summary>
        /// Gets if the timer is a looping timer or not.
        /// </summary>
        public bool IsLooping { get; private set; }
        
        
        /// <summary>
        /// Gets the total loops the timer needs to complete.
        /// Will be -1 is infinite.
        /// </summary>
        public int LoopFor { get; private set; } = -1;
        
        
        /// <summary>
        /// Gets the total number of loops the timer has completed.
        /// </summary>
        private int LoopsCompleted { get; set; } = 0;
        
        
        /// <summary>
        /// Gets if all the required loops have completed.
        /// </summary>
        private bool CompletedAllLoops
        {
            get
            {
                if (LoopFor == -1) return false;
                return LoopFor == LoopsCompleted;
            }
        }
        
        
        /// <summary>
        /// Defines the action to run on a loop completing, if applicable.
        /// </summary>
        private Action OnLoopAction { get; set; }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Makes a new countdown timer.
        /// </summary>
        /// <param name="duration">The duration of the timer.</param>
        /// <param name="updateSource">The source to update from. (Default = Global).</param>
        /// <param name="useUnscaledTime">Use unscaled time for the timer? (Default = false).</param>
        public CountdownRuntimeTimer(float duration, TimerUpdateSource updateSource = TimerUpdateSource.Global,
            bool? useUnscaledTime = false) : base(
            duration, updateSource, useUnscaledTime)
        {
            TimerType = TimerType.Countdown;
        }

        /// <summary>
        /// Makes a new looping countdown timer.
        /// </summary>
        /// <param name="duration">The duration of the timer.</param>
        /// <param name="loopFor">The total number of loops to run through, set to -1 for infinite looping.</param>
        /// <param name="onLooped">Action to run when a loop is completed.</param>
        /// <param name="updateSource">The source to update from. (Default = Global).</param>
        /// <param name="useUnscaledTime">Use unscaled time for the timer? (Default = false).</param>
        public CountdownRuntimeTimer(float duration, int loopFor,
            Action onLooped = null, TimerUpdateSource updateSource = TimerUpdateSource.Global, bool? useUnscaledTime = false) 
            : base(duration, updateSource, useUnscaledTime)
        {
            TimerType = TimerType.Countdown;
            LoopFor = loopFor;
            IsLooping = true;
            LoopsCompleted = 0;
            OnLoopAction = onLooped;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Apples the tick change to the timer.
        /// </summary>
        /// <param name="change">The change to apply.</param>
        protected override void OnTicked(float change)
        {
            CurrentTime -= change;
        }

        
        /// <summary>
        /// Checks for complete when the timer is done on its current pass.
        /// </summary>
        protected override void OnTimerCompleteDetected()
        {
            if (!IsLooping)
            {
                base.OnTimerCompleteDetected();
                return;
            }

            LoopsCompleted++;
            
            if (!CompletedAllLoops)
            {
                OnLoopAction?.Invoke();
                return;
            }
            
            base.OnTimerCompleteDetected();
        }
    }
}