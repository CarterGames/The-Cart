using Scarlet.Editor.Utility;
using Scarlet.Management;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Scarlet.Editor
{
    public static class AutoSetup
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Creates the scriptable objects for the asset if they don't exist yet.
        /// </summary>
        [DidReloadScripts(-10)]
        private static void OnScriptsReloaded() 
        {
            if (UtilEditor.AssetIndex.Lookup.ContainsKey(typeof(ScarletLibraryRuntimeSettings).ToString())) return;
            UtilEditor.Initialize();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}