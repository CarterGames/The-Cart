#if CARTERGAMES_CART_CRATE_EASING && UNITY_EDITOR

/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using CarterGames.Cart.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Easing.Editor
{
    public static class BaseEaseDataPropertyDrawer
    {
        public static void OnGUILogic(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
            
            var _elementOne = property.Fpr("easeType");
            var _elementTwo = property.Fpr("easeDuration");
            
            EditorGUI.BeginChangeCheck();

            var indent = EditorGUI.indentLevel;
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

#endif