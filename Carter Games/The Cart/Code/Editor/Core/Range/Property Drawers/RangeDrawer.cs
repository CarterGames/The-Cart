/*
 * Copyright (c) 2024 Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
{
    public class RangeDrawer
    {
        public static void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
            
            var _elementOne = property.Fpr("min");
            var _elementTwo = property.Fpr("max");
            
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


    [CustomPropertyDrawer(typeof(IntRange))]
    public sealed class IntRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            RangeDrawer.OnGUI(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2 + 1.5f;
        }
    }
    
    [CustomPropertyDrawer(typeof(FloatRange))]
    public sealed class FloatRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            RangeDrawer.OnGUI(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2 + 1.5f;
        }
    }
    
    
    [CustomPropertyDrawer(typeof(DoubleRange))]
    public sealed class DoubleRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            RangeDrawer.OnGUI(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2 + 1.5f;
        }
    }
}