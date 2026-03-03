#if CARTERGAMES_CART_CRATE_GAMETICKER

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

using CarterGames.Cart.Data;
using CarterGames.Cart.Events;
using UnityEngine;

namespace CarterGames.Cart.Crates.GameTicks
{
    /// <summary>
    /// Handles a tick system with the ticks defined locally.
    /// </summary>
    public class GameTicker : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Field
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] private GameTickSyncState syncState;
        [SerializeField] private float ticksPerSecond = 30;

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


        /// <summary>
        /// Resets the tick position back to 0.
        /// </summary>
        public void ResetTickPosition()
        {
            tickTimer = 0;
        }
    }
}

#endif