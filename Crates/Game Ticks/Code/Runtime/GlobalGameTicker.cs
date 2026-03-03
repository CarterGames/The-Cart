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
using CarterGames.Cart.Reflection;
using UnityEngine;

namespace CarterGames.Cart.Crates.GameTicks
{
    /// <summary>
    /// A game ticker that is intended for global use instead of local use.
    /// </summary>
    public static class GlobalGameTicker
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Field
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The game ticker for the global system.
        /// </summary>
        private static GameTicker globalTicker;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Runs when a tick is registered from the global ticker.
        /// </summary>
        public static Evt GlobalTick => globalTicker.Ticked;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Initializes the global tick when called.
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            if (!DataAccess.GetAsset<DataAssetSettingsGameTicker>().GameTickUseGlobalTicker) return;
            if (globalTicker != null) return;
            
            var obj = new GameObject("Global Game Ticker");
            obj.AddComponent<GameTicker>();
            globalTicker = obj.GetComponent<GameTicker>();
            ReflectionHelper.GetField(typeof(GameTicker), "syncState", false).SetValue(globalTicker, DataAccess.GetAsset<DataAssetSettingsGameTicker>().GameTickGlobalSyncState);
            globalTicker.SetTicksPerSecond(DataAccess.GetAsset<DataAssetSettingsGameTicker>().GameTickTicksPerSecond);
            Object.DontDestroyOnLoad(obj);
        }
    }
}

#endif