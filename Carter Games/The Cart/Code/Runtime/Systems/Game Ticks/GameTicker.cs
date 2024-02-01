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

using CarterGames.Cart.Events;
using CarterGames.Cart.Utility;
using UnityEngine;

namespace CarterGames.Cart.Optimisation
{
    /// <summary>
    /// Handles a tick system with the ticks defined locally.
    /// </summary>
    public class GameTicker : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Field
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private int ticksPerSecond;

        private bool hasTimescaleOverride;
        private bool timescaleOverride;
        
        private float tickTimer;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The amount of time between each tick.
        /// </summary>
        private float MaxTimeBetweenTicks => 1f / ticksPerSecond;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Raises when the a tick is reached.
        /// </summary>
        public readonly Evt Ticked = new Evt();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void Update()
        {
            var change = 0f;

            if (hasTimescaleOverride)
            {
                change = timescaleOverride ? Time.unscaledDeltaTime : Time.deltaTime;
            }
            else
            {
                change = UtilRuntime.Settings.GameTickUseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            }

            tickTimer += change;

            if (tickTimer < MaxTimeBetweenTicks) return;
            
            tickTimer -= MaxTimeBetweenTicks;
            Ticked.Raise();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Sets the ticks per second to the value provided.
        /// </summary>
        /// <param name="value">The value to set to.</param>
        public void SetTicksPerSecond(int value)
        {
            ticksPerSecond = value;
        }


        /// <summary>
        /// Sets the timescale type from unscaled or scaled regardless of the global setting.
        /// </summary>
        /// <param name="useUnscaled">The scale to use.</param>
        public void OverrideTimeScaleType(bool useUnscaled)
        {
            hasTimescaleOverride = true;
            timescaleOverride = useUnscaled;
        }
    }
}