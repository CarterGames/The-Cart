using CarterGames.Common.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Common.Editor
{
    /// <summary>
    /// Handles the custom editor for the editor settings asset.
    /// </summary>
    [CustomEditor(typeof(CommonLibraryEditorSettings))]
    public sealed class CommonEditorSettingsInspector : UnityEditor.Editor
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
            HierarchySeparatorSettingsDrawer.DrawInspector(serializedObject);
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
            EditorGUILayout.LabelField("Editor Settings", EditorStyles.boldLabel, GUILayout.Width("Editor Settings ".GUIWidth()));
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