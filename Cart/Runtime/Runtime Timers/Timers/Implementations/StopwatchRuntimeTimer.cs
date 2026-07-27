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

namespace CarterGames.Cart
{
    /// <summary>
    /// Handles a runtime timer that counts up until stopped manually by you.
    /// </summary>
    public class StopwatchRuntimeTimer : RuntimeTimer
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets if the timer is completed (always false on a stopwatch!).
        /// </summary>
        public override bool IsComplete => false;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructor
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Makes a new stopwatch timer.
        /// </summary>
        /// <param name="initialTime">The time to start from.</param>
        /// <param name="updateSource">The source to update from. (Default = Global).</param>
        /// <param name="useUnscaledTime">Use unscaled time for the timer? (Default = false).</param>
        public StopwatchRuntimeTimer(float initialTime, TimerUpdateSource updateSource, bool? useUnscaledTime) : base(initialTime, updateSource, useUnscaledTime)
        {
            TimerType = TimerType.Stopwatch;
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
            CurrentTime += change;
        }
    }
}