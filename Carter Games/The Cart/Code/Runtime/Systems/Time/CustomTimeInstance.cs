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

namespace CarterGames.Cart.CustomTime
{
    /// <summary>
    /// A custom time scale class, handy for when you need to control the timescale of element separately from Time.timescale.
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
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Sets up a new timescale for use.
        /// </summary>
        /// <param name="scale"></param>
        public CustomTimeInstance(float scale)
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
            localTimeScale = Mathf.Epsilon;
        }

        
        /// <summary>
        /// Resumes the time when called.
        /// </summary>
        public void ResumeTime()
        {
            localTimeScale = 1f;
        }
    }
}