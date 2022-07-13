using UnityEditor;
using UnityEngine;

namespace Scarlet.Easing.Editor
{
    public static class BaseEaseDataPropertyDrawer
    {
        public static void OnGUILogic(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
            
            var _elementOne = property.FindPropertyRelative("easeType");
            var _elementTwo = property.FindPropertyRelative("easeDuration");
            
            EditorGUI.BeginChangeCheck();

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var _left = new Rect(position.x, position.y, (position.width / 3) - 1.5f, EditorGUIUtility.singleLineHeight);
            var _leftLower = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 1.5f, (position.width / 3) - 1.5f, EditorGUIUtility.singleLineHeight);
            var _right = new Rect(position.x + (position.width / 3) + 1.5f, position.y, (position.width / 3) * 2 - 1.5f, EditorGUIUtility.singleLineHeight);
            var _rightLower = new Rect(position.x + (position.width / 3) + 1.5f, position.y + EditorGUIUtility.singleLineHeight + 1.5f, (position.width / 3) * 2 - 1.5f, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_left, "Type:");
            EditorGUI.LabelField(_leftLower, "Duration:");
            EditorGUI.PropertyField(_right, _elementOne, GUIContent.none);
            EditorGUI.PropertyField(_rightLower, _elementTwo, GUIContent.none);
   

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();            
            
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}