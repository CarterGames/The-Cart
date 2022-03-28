// ----------------------------------------------------------------------------
// MinMaxDrawer.cs
// 
// Description: Handles & contains the property drawers for the min/max value system.
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Adriana.Editor
{
    public class MinMaxDrawer
    {
        public static void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
            
            var _elementOne = property.FindPropertyRelative("min");
            var _elementTwo = property.FindPropertyRelative("max");
            
            EditorGUI.BeginChangeCheck();

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var _left = new Rect(position.x, position.y, (position.width / 6) - 1.5f, EditorGUIUtility.singleLineHeight);
            var _leftLower = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 1.5f, (position.width / 6) - 1.5f, EditorGUIUtility.singleLineHeight);
            var _right = new Rect(position.x + (position.width / 6) + 1.5f, position.y, (position.width / 6) * 5 - 1.5f, EditorGUIUtility.singleLineHeight);
            var _rightLower = new Rect(position.x + (position.width / 6) + 1.5f, position.y + EditorGUIUtility.singleLineHeight + 1.5f, (position.width / 6) * 5 - 1.5f, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_left, "Min:");
            EditorGUI.LabelField(_leftLower, "Max:");
            EditorGUI.PropertyField(_right, _elementOne, GUIContent.none);
            EditorGUI.PropertyField(_rightLower, _elementTwo, GUIContent.none);
   

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();            
            
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }


    [CustomPropertyDrawer(typeof(MinMaxInt))]
    public class MinMaxIntDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MinMaxDrawer.OnGUI(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2 + 1.5f;
        }
    }
    
    [CustomPropertyDrawer(typeof(MinMaxFloat))]
    public class MinMaxFloatDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MinMaxDrawer.OnGUI(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2 + 1.5f;
        }
    }
    
    
    [CustomPropertyDrawer(typeof(MinMaxDouble))]
    public class MinMaxDoubleDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MinMaxDrawer.OnGUI(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2 + 1.5f;
        }
    }
}