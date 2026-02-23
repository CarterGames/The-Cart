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
    /// A custom time-scale class, handy for when you need to control the timescale of element separately from Time.timescale.
    /// </summary>
    [Serializable]
    public class CustomTimeInstance
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Field
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private float localTimeScale;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The local time scale used.
        /// </summary>
        public float LocalTimeScale
        {
            get => localTimeScale;
            set => localTimeScale = value;
        }
        
        
        /// <summary>
        /// Gets the Time.deltaTime scales with this instance.
        /// </summary>
        public float DeltaTime => Time.deltaTime * LocalTimeScale;
        
        
        /// <summary>
        /// Gets the Time.fixedDeltaTime scales with this instance.
        /// </summary>
        public float FixedDeltaTime => Time.fixedDeltaTime * LocalTimeScale;
        
        
        /// <summary>
        /// Gets the Time.time scales with this instance.
        /// </summary>
        public float StandardTime => Time.time * LocalTimeScale;
        
        
        /// <summary>
        /// Gets is the time is paused.
        /// </summary>
        public bool IsPaused => Mathf.Approximately(LocalTimeScale, 0);

        
        /// <summary>
        /// The timescale for the instance.
        /// </summary>
        public float TimeScale => Time.timeScale * LocalTimeScale;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Raises when the time instance is paused.
        /// </summary>
        public readonly Evt PausedEvt = new Evt();
        
        
        /// <summary>
        /// Raises when the time instance is resumed.
        /// </summary>
        public readonly Evt ResumedEvt = new Evt();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Sets up a new timescale for use.
        /// </summary>
        /// <param name="scale">The scale to apply. DEF: 1</param>
        public CustomTimeInstance(float scale = 1f)
        {
            localTimeScale = scale;
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Pauses the time when called.
        /// </summary>
        public void PauseTime()
        {
            if (IsPaused) return;
            localTimeScale = Mathf.Epsilon;
            PausedEvt.Raise();
        }

        
        /// <summary>
        /// Resumes the time when called.
        /// </summary>
        public void ResumeTime()
        {
            if (!IsPaused) return;
            localTimeScale = 1f;
            ResumedEvt.Raise();
        }
    }
}