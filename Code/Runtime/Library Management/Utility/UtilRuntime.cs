using Scarlet.Management;

namespace Scarlet.Utility
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
        private static ScarletLibraryRuntimeSettings settingsCache;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The runtime settings asset.
        /// </summary>
        public static ScarletLibraryRuntimeSettings Settings
        {
            get
            {
                if (settingsCache != null) return settingsCache;
                settingsCache = ScarletLibraryAssetAccessor.GetAsset<ScarletLibraryRuntimeSettings>();
                return settingsCache;
            }
        }
    }
}