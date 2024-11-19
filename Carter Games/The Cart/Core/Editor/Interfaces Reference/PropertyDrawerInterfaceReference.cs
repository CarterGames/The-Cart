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

using System.Linq;
using CarterGames.Cart.Core.Logs;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
{
    /// <summary>
    /// A property drawer for the interface reference field.
    /// </summary>
    [CustomPropertyDrawer(typeof(InterfaceRef<>), true)]
    public sealed class PropertyDrawerInterfaceReference : PropertyDrawer   
    {
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
                
                            CartLogger.LogError<LogCategoryCore>($"Unable to assign: {property.FindPropertyRelative("target").objectReferenceValue.name} as interface {fieldInfo.FieldType.GenericTypeArguments[0]}.", typeof(PropertyDrawerInterfaceReference));
                            property.FindPropertyRelative("target").objectReferenceValue = null;
                            Skip: ;
                        }
                    }
                }
            }
            
            EditorGUI.EndProperty();
        }
    }
}