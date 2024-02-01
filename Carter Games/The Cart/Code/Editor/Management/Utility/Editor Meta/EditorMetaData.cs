using UnityEngine;

namespace CarterGames.Cart.Editor
{
    public static class EditorMetaData
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Asset Info
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        public struct AssetVersionInfo
        {
            public static readonly GUIContent Number =
                new GUIContent(
                    "Version",
                    "The version number of the asset you currently have imported.");
        
        
            public static readonly GUIContent Date =
                new GUIContent(
                    "Release date",
                    "The date the version of the asset you are using was released on.");
        }
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Per User Settings
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        public struct PerUser
        {
            public static readonly GUIContent AutoVersionCheck =
                new GUIContent(
                    "Update check on load",
                    "Checks for any updates to the asset from the GitHub page when you load the project.");
        
        
            public static readonly GUIContent RuntimeDebugLogs =
                new GUIContent(
                    "Show logs?",
                    "See debug.log messages from the asset in editor or runtime?");
        }
    }
}