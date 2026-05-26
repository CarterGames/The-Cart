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

using System;
using System.Linq;
using CarterGames.Cart.Logs;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarterGames.Cart.Editor
{
    /// <summary>
    /// A property drawer for the interface reference field.
    /// </summary>
    [CustomPropertyDrawer(typeof(InterfaceRef<>), true)]
    public sealed class PropertyDrawerInterfaceReference : PropertyDrawer
    {
        private static GUIStyle labelStyle;
        
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.BeginChangeCheck();
            
            property.FindPropertyRelative("target").objectReferenceValue = EditorGUI.ObjectField(position, label,
                property.FindPropertyRelative("target").objectReferenceValue,
                typeof(Object), true);

            if (EditorGUI.EndChangeCheck())
            {
                if (property.FindPropertyRelative("target").objectReferenceValue != null)
                {
                    if (property.FindPropertyRelative("target").objectReferenceValue.GetType() !=
                        fieldInfo.FieldType.GenericTypeArguments[0])
                    {
                        if (property.FindPropertyRelative("target").objectReferenceValue is GameObject)
                        {
                            foreach (var comp in ((GameObject) property.FindPropertyRelative("target")
                                         .objectReferenceValue).GetComponents<Component>())
                            {
                                if (comp.GetType().GetInterfaces().All(t => t != fieldInfo.FieldType.GenericTypeArguments[0])) continue;
                                
                                property.FindPropertyRelative("target").objectReferenceValue = comp;
                                goto Skip;
                            }
                
                            CartLogger.LogError<CartLogs>($"Unable to assign: {property.FindPropertyRelative("target").objectReferenceValue.name} as interface {fieldInfo.FieldType.GenericTypeArguments[0].Name}.", typeof(PropertyDrawerInterfaceReference));
                            property.FindPropertyRelative("target").objectReferenceValue = null;
                            Skip: ;
                        }
                    }
                }
            }

            DrawTypeLabel(position, GetValueType());
            
            EditorGUI.EndProperty();
        }



        private Type GetValueType()
        {
            if (fieldInfo.FieldType.GenericTypeArguments[0].GenericTypeArguments.Length > 0)
            {
                return fieldInfo.FieldType.GenericTypeArguments[0].GenericTypeArguments[0];
            }
            else
            {
                return fieldInfo.FieldType.GenericTypeArguments[0];
            }
        }


        private static void DrawTypeLabel(Rect position, Type interfaceType)
        {
            InitializeStyleIfNeeded();

            var controlID = GUIUtility.GetControlID(FocusType.Passive) - 1;
            var displayString = $"({interfaceType.Name})";
            DrawInterfaceNameLabel(position, displayString, controlID);
        }


        private static void DrawInterfaceNameLabel(Rect position, string displayString, int controlID)
        {
            if (Event.current.type != EventType.Repaint) return;
            
            const int additionalLeftWidth = 3;
            const int verticalIndent = 1;

            var content = EditorGUIUtility.TrTextContent(displayString);
            var size = labelStyle.CalcSize(content);
            var labelPos = position;

            labelPos.width = size.x + additionalLeftWidth;
            labelPos.x += position.width - labelPos.width - 18;
            labelPos.height -= verticalIndent * 2;
            labelPos.y += verticalIndent;
            labelStyle.Draw(labelPos, EditorGUIUtility.TrTextContent(displayString), controlID,
                DragAndDrop.activeControlID == controlID, false);
        }


        private static void InitializeStyleIfNeeded()
        {
            if (labelStyle != null) return;

            var style = new GUIStyle(EditorStyles.label)
            {
                font = EditorStyles.objectField.font,
                fontSize = EditorStyles.objectField.fontSize,
                fontStyle = EditorStyles.objectField.fontStyle,
                alignment = TextAnchor.MiddleRight,
                padding = new RectOffset(0, 2, 0, 0)
            };
            
            labelStyle = style;
        }
    }
}