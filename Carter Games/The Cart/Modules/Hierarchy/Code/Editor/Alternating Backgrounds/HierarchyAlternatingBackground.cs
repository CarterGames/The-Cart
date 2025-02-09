using CarterGames.Cart.Core;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    public class HierarchyAlternatingBackground : IHierarchyEdit
    {
        private static Color32 AltColor => EditorGUIUtility.isProSkin
            ? new Color32(36, 36, 36, 75)
            : new Color32(174, 174, 174, 75);
        
        
        public int Order => -1;
        
        
        public void OnHierarchyDraw(int instanceId, Rect rect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            
            if (gameObject == null) return;

            var indexOfObj = gameObject.transform.GetSiblingIndex();
            
            if (Selection.gameObjects.Contains(gameObject)) return;
            if (indexOfObj % 2 == 0) return;
            
            EditorGUI.DrawRect(rect, AltColor);
            
            var textStyle = new GUIStyle();
            textStyle.normal.textColor = EditorStyles.label.normal.textColor;
            textStyle.alignment = TextAnchor.MiddleLeft; 
            
            var content = EditorGUIUtility.ObjectContent(gameObject, typeof(GameObject));
            
            var altRect = new Rect(rect.x - 1, rect.y, rect.width, rect.height);
            EditorGUI.LabelField(altRect, new GUIContent(content.image));
            
            altRect = new Rect(rect.x + 18, rect.y - 1, rect.width, rect.height);
            EditorGUI.LabelField(altRect, new GUIContent(gameObject.name), textStyle);
        }
    }
}