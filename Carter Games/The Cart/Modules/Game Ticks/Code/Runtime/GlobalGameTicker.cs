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
using CarterGames.Cart.Core.Reflection;
using UnityEngine;

namespace CarterGames.Cart.Modules.GameTicks
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