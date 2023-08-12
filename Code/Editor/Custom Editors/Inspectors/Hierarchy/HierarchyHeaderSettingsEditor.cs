using CarterGames.Common.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Common.Hierarchy.Editor
{
    /// <summary>
    /// Handles the custom inspector for the hierarchy header settings script.
    /// </summary>
    [CustomEditor(typeof(HierarchyHeaderSettings))]
    public sealed class HierarchyHeaderSettingsEditor : UnityEditor.Editor
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static readonly GUIContent BackgroundCol = new GUIContent("Header Color:", "The color the header block should be rendered as.");
        private static readonly GUIContent FullWidth = new GUIContent("Full Width?", "Defines if the header takes up the full width regardless of child/parenting.");
        
        private static readonly GUIContent Label = new GUIContent("Label:", "The text shown on the header.");
        private static readonly GUIContent LabelCol = new GUIContent("Color:", "The color the header text is displayed in.");
        private static readonly GUIContent BoldStyle = new GUIContent("Bold Label?", "Should the label appear in a bold style?");
        private static readonly GUIContent Alignment = new GUIContent("Alignment:", "Choose how the header label should be aligned.");
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Overrides
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public override void OnInspectorGUI()
        {
            GUILayout.Space(4f);
            GeneralUtilEditor.DrawMonoScriptSection((HierarchyHeaderSettings)target);
            GUILayout.Space(2f);
            
            DrawHeaderBox();
            GUILayout.Space(2f);
            DrawLabelBox();

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Draws the header settings box & its options.
        /// </summary>
        private void DrawHeaderBox()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Header", EditorStyles.boldLabel);
            GUILayout.Space(1f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            GUILayout.Space(1f);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("backgroundColor"), BackgroundCol); 

            if (GUILayout.Button("R", GUILayout.Width("  R  ".GUIWidth())))
            {
                serializedObject.FindProperty("backgroundColor").colorValue = Color.gray;
            }
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("fullWidth"), FullWidth); 
           
            GUILayout.Space(2f);
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the label settings box & its options.
        /// </summary>
        private void DrawLabelBox()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Label", EditorStyles.boldLabel);
            GUILayout.Space(1f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            GUILayout.Space(1f);
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("label"), Label);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("labelColor"), LabelCol);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("boldLabel"), BoldStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("textAlign"), Alignment);
          
            GUILayout.Space(2f);
            EditorGUILayout.EndVertical();
        }
    }
}