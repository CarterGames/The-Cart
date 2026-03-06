#if CARTERGAMES_CART_CRATE_DATAVALUES && UNITY_EDITOR

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

namespace CarterGames.Cart.Crates.DataValues.Editor
{
    [CustomPropertyDrawer(typeof(DataDictionaryRef<,,>))]
    public sealed class ReferenceDrawerDataDictionary : PropertyDrawer
    {
        /// <summary>
        /// Options to display in the popup to select constant or variable.
        /// </summary>
        private readonly string[] popupOptions = { "Use Constant", "Use Variable" };

        /// <summary> Cached style to use to draw the popup button. </summary>
        private GUIStyle popupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            // Get properties
            SerializedProperty useConstant = property.Fpr("useConstant");
            SerializedProperty constantValue = property.Fpr("constantValue");
            SerializedProperty variable = property.Fpr("variable");

            // Calculate rect for configuration button
            var buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.height = EditorGUIUtility.singleLineHeight;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            //int indent = EditorGUI.indentLevel;
            //EditorGUI.indentLevel = 0;

            var result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);
            
            useConstant.boolValue = result == 0;
            
            position.xMax -= 2.5f;
            position.x += 2.5f;

            if (property.Fpr("useConstant").boolValue)
            {
                var rect = new Rect(position);
                
                rect.y += EditorGUIUtility.singleLineHeight;
                rect.width = position.xMax - 45f;
                rect.x = 45f;
                
                EditorGUI.PropertyField(rect, constantValue.Fpr("list"), new GUIContent("Dictionary"));
            }
            else
            {
                EditorGUI.PropertyField(position, variable, GUIContent.none);
            }

            
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }

            // EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.Fpr("useConstant").boolValue)
            {
                return EditorGUI.GetPropertyHeight(property.Fpr("constantValue").Fpr("list")) + base.GetPropertyHeight(property, label);
            }
            
            return base.GetPropertyHeight(property, label);
        }
    }
}

#endif