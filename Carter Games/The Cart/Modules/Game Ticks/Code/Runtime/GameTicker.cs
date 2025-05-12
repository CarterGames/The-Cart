#if CARTERGAMES_CART_MODULE_GAMETICKER

/*
 * Copyright (c) 2025 Carter Games
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

using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Events;
using UnityEngine;

namespace CarterGames.Cart.Modules.GameTicks
{
    /// <summary>
    /// Handles a tick system with the ticks defined locally.
    /// </summary>
    [AddComponentMenu("Carter Games/The Cart/Modules/Game Ticks/Game Ticker")]
    public class GameTicker : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Field
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] private GameTickSyncState syncState;
        [SerializeField] private float ticksPerSecond;

        private bool hasTimescaleOverride;
        private bool tickerEnabled = true;
        private float tickTimer;
        private bool timescaleOverride;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets if the system is toggled on or off.
        /// </summary>
        public bool Enabled
        {
            get => tickerEnabled;
            private set => tickerEnabled = value;
        }


        /// <summary>
        /// The amount of time between each tick.
        /// </summary>
        private float MaxTimeBetweenTicks => 1f / TickRate;


        /// <summary>
        /// Gets the tick rate of the ticker based on the sync state defined.
        /// </summary>
        private float TickRate => syncState == GameTickSyncState.ApplicationTargetFrameRate ? Application.targetFrameRate : ticksPerSecond;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Raises when a tick is reached.
        /// </summary>
        public readonly Evt Ticked = new Evt();
        
        
        /// <summary>
        /// Raises when the ticker is toggled on or off.
        /// </summary>
        public readonly Evt<bool> Toggled = new Evt<bool>();


        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private void Update()
        {
            if (!Enabled) return;
            
            var change = 0f;

            if (hasTimescaleOverride)
            {
                change = timescaleOverride ? Time.unscaledDeltaTime : Time.deltaTime;
            }
            else
            {
                change = DataAccess.GetAsset<DataAssetSettingsGameTicker>().GameTickUseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
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
        /// Toggles the ticker when called.
        /// </summary>
        /// <param name="state">The state to toggle to.</param>
        public void Toggle(bool state)
        {
            Enabled = state;
            Toggled.Raise(state);
        }


        /// <summary>
        /// Sets the ticks per second to the value provided.
        /// </summary>
        /// <param name="value">The value to set to.</param>
        public void SetTicksPerSecond(float value)
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

#endif