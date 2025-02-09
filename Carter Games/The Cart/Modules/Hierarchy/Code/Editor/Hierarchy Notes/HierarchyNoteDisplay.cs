using CarterGames.Cart.Core;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    public class HierarchyNoteDisplay : IHierarchyEdit
    {
        public int Order => -1;
        
        
        public void OnHierarchyDraw(int instanceId, Rect rect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            
            if (gameObject == null) return;
            if (!gameObject.TryGetComponentsInChildren<HierarchyNote>(out var notes)) return;

            notes.Reverse();
            var xOffset = 45f;
            
            foreach (var note in notes)
            {
                var altRect = new Rect(rect.width + xOffset, rect.y - 1, 22.5f, rect.height);
                xOffset -= 15f;

                var content = EditorGUIUtility.IconContent(note.Icon);
                content.tooltip = note.Text;

                EditorGUI.LabelField(altRect, content, GUIStyle.none);
            }
        }
    }
}