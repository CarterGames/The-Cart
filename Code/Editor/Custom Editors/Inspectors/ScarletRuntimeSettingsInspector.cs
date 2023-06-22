using Scarlet.Editor.Utility;
using Scarlet.Management;
using UnityEditor;
using UnityEngine;

namespace Scarlet.Editor
{
    [CustomEditor(typeof(ScarletLibraryRuntimeSettings))]
    public class ScarletRuntimeSettingsInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawHeaderTitle();
            GUILayout.Space(7.5f);
            DrawRandom();
        }


        private void DrawHeaderTitle()
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
            EditorGUILayout.LabelField("Runtime Settings", EditorStyles.boldLabel, GUILayout.Width("Runtime Settings ".Width()));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        

        private void DrawRandom()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Random", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isRngExpanded"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rngProvider"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("systemSeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("aleaSeed"));
            
            EditorGUILayout.EndVertical();
        }
    }
}