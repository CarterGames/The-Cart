using CarterGames.Cart.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Data.Editor
{
    [CustomEditor(typeof(EditorOnlyDataAsset), true)]
    public class InspectorEditorOnlyDataAsset : CustomInspector
    {
        protected override string[] HideProperties => new string[]
        {
            "m_Script", "variantId", "excludeFromAssetIndex"
        };

        
        protected override void DrawInspectorGUI()
        {
            GUILayout.Space(1.5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            EditorGUILayout.PropertyField(serializedObject.Fp("variantId"));

            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            
            GUILayout.Space(5f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            GUILayout.Space(5f);

            DrawBaseInspectorGUI();
        }
    }
}