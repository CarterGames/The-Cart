using CarterGames.Common.Events;
using CarterGames.Common.Utility;
using UnityEngine;

namespace CarterGames.Common.Optimisation
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
            if (!UtilRuntime.Settings.GameTickUseGlobalTicker) return;
            if (globalTicker != null) return;
            
            var obj = new GameObject("Global Game Ticker");
            obj.AddComponent<GameTicker>();
            globalTicker = obj.GetComponent<GameTicker>();
            globalTicker.SetTicksPerSecond(UtilRuntime.Settings.GameTickTicksPerSecond);
            Object.DontDestroyOnLoad(obj);
        }
    }
}