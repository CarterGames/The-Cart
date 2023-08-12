using CarterGames.Common.Management;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Common.Editor
{
    /// <summary>
    /// Handles the custom editor for the runtime settings asset.
    /// </summary>
    [CustomEditor(typeof(CommonLibraryRuntimeSettings))]
    public sealed class CommonRuntimeSettingsInspector : UnityEditor.Editor
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Editor Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Overrides the InspectorGUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawHeaderTitle();
            GUILayout.Space(7.5f);
            DrawScript();
            RngSettingsDrawer.DrawInspector(serializedObject);
            LoggingSettingsDrawer.DrawInspector(serializedObject);
            GameTickerSettingsDrawer.DrawInspector(serializedObject);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Drawer Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Draws the header title section.
        /// </summary>
        private static void DrawHeaderTitle()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Runtime Settings", EditorStyles.boldLabel, GUILayout.Width("Runtime Settings ".GUIWidth()));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        
        
        /// <summary>
        /// Draws the script reference section.
        /// </summary>
        private void DrawScript()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GeneralUtilEditor.DrawSoScriptSection(target);
            EditorGUILayout.EndVertical(); 
        }
    }
}