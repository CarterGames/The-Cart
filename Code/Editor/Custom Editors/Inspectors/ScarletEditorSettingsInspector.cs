using Scarlet.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace Scarlet.Editor
{
    /// <summary>
    /// Handles the custom editor for the editor settings asset.
    /// </summary>
    [CustomEditor(typeof(ScarletLibraryEditorSettings))]
    public sealed class ScarletEditorSettingsInspector : UnityEditor.Editor
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
            
            if (GUILayout.Button(UtilEditor.ScarletRose, GUIStyle.none, GUILayout.MaxHeight(85)))
            {
                GUI.FocusControl(null);
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();   
            
            GUILayout.Space(7.5f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Editor Settings", EditorStyles.boldLabel, GUILayout.Width("Editor Settings ".Width()));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }


        /// <summary>
        /// Draws the script reference section.
        /// </summary>
        private void DrawScript()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            FileEditorUtil.DrawSoScriptSection(target);
            EditorGUILayout.EndVertical();
        }
    }
}