using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    public static class EditorUrlDrawer
    {
        public static void DrawMultiple(IEnumerable<EditorUrl> editorUrls)
        {
            EditorGUILayout.BeginVertical();
            
            foreach (var entry in editorUrls)
            {
                Draw(entry);
            }
            
            EditorGUILayout.EndVertical();
        }
        
        
        public static void Draw(EditorUrl editorUrl)
        {
            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.TextField(editorUrl.DisplayName, editorUrl.Target);

            if (GUILayout.Button("Open Link", GUILayout.Width(90)))
            {
                editorUrl.OpenLink();
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }
}