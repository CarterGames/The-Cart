using CarterGames.Cart.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Data.Notion.Editor
{
    [CustomPropertyDrawer(typeof(NotionWrapper<>), true)]
    public class NotionWrapperEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            var spriteRef = property.Fpr("value");
            
            EditorGUI.BeginChangeCheck();

            EditorGUI.PropertyField(position, spriteRef, label);
            
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }        
            
            EditorGUI.EndProperty();
        }
        
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }
    }
}