using CarterGames.Common.Management;

namespace CarterGames.Common.Utility
{
    /// <summary>
    /// A utility class to get common things at runtime.
    /// </summary>
    public static class UtilRuntime
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static CommonLibraryRuntimeSettings settingsCache;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The runtime settings asset.
        /// </summary>
        public static CommonLibraryRuntimeSettings Settings
        {
            get
            {
                if (settingsCache != null) return settingsCache;
                settingsCache = CommonAssetAccessor.GetAsset<CommonLibraryRuntimeSettings>();
                return settingsCache;
            }
        }
    }
}