using CarterGames.Cart.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    [CustomEditor(typeof(HierarchyNote))]
    public class HierarchyNoteEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Change Copy", GUILayout.Width(100)))
            {
                UtilityEditorWindow.Open<HierarchyNoteEditWindow>("Hierarchy Note Editor");
            }
            
            if (GUILayout.Button("Change Icon", GUILayout.Width(100)))
            {
                
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }
}