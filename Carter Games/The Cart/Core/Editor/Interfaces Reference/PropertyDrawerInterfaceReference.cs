/*
 * Copyright (c) 2025 Carter Games
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

using System;
using System.Linq;
using CarterGames.Cart.Core.Logs;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarterGames.Cart.Core.Editor
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
                
                            CartLogger.LogError<LogCategoryCore>($"Unable to assign: {property.FindPropertyRelative("target").objectReferenceValue.name} as interface {fieldInfo.FieldType.GenericTypeArguments[0].Name}.", typeof(PropertyDrawerInterfaceReference));
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