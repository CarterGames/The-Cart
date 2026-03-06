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
using System.Collections.Generic;
using System.Reflection;
using CarterGames.Cart.Logs;
using CarterGames.Cart.Crates;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarterGames.Cart.Editor
{
    /// <summary>
    /// A helper class to aid with editor scripting where the API is really wordy...
    /// </summary>
    public static class SerializedPropertyHelper
    {
        /// <summary>
        /// Gets if the index is in the array.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="index">The index.</param>
        public static bool HasIndex(this SerializedProperty property, int index)
        {
            return index >= property.arraySize - 1 && property.arraySize > 0;
        }
        
        
        /// <summary>
        /// Calls InsertArrayElementAtIndex()
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="index">The index.</param>
        public static void InsertIndex(this SerializedProperty property, int index)
        {
            property.InsertArrayElementAtIndex(index);
        }
        
        
        /// <summary>
        /// Calls InsertArrayElementAtIndex() followed by GetArrayElementAtIndex() for the same index.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="index">The index.</param>
        public static SerializedProperty InsertAndGetIndex(this SerializedProperty property, int index)
        {
            property.InsertArrayElementAtIndex(index);
            return property.GetArrayElementAtIndex(index);
        }
        
        
        /// <summary>
        /// Calls InsertArrayElementAtIndex(ArraySize)
        /// </summary>
        /// <param name="property">The property.</param>
        public static void InsertAtEnd(this SerializedProperty property)
        {
            property.InsertArrayElementAtIndex(property.arraySize);
        }
        
        
        /// <summary>
        /// Calls InsertArrayElementAtIndex(ArraySize) followed by GetArrayElementAtIndex(ArraySize).
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The inserted value.</param>
        public static void InsertAtEnd(this SerializedProperty property, out SerializedProperty value)
        {
            property.InsertArrayElementAtIndex(property.arraySize);
            value = property.GetArrayElementAtIndex(property.arraySize - 1);
        }


        public static void ClearAllValues(SerializedProperty property)
        {
            while (property.Next(true))
            {
                property.TryResetToDefaultValue();
            }
        }
        
        
        /// <summary>
        /// Calls DeleteArrayElementAtIndex()
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="index">The index.</param>
        public static void DeleteIndex(this SerializedProperty property, int index)
        {
            property.DeleteArrayElementAtIndex(index);
        }
        
        
        /// <summary>
        /// Calls DeleteArrayElementAtIndex()
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="index">The index.</param>
        public static void DeleteAndRemoveIndex(this SerializedProperty property, int index)
        {
            if (property == null) return;
            
            var arraySize = property.arraySize.ToString();  // Done so the value is a copy, not a reference.
            property.DeleteArrayElementAtIndex(index);

            // Hacky solution as 1 remove will remove a reference, but not an index if in an array etc.
            try
            {
                if (int.Parse(arraySize) == property.arraySize)
                {
                    property.DeleteArrayElementAtIndex(index);
                }
            }
#pragma warning disable 0168
            catch (Exception e)
#pragma warning restore
            {
                // Do nothing... Should all be good xD
            }
        }
        
        
        /// <summary>
        /// Calls GetArrayElementAtIndex()
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="index">The index.</param>
        /// <returns>The property at the index entered.</returns>
        public static SerializedProperty GetIndex(this SerializedProperty property, int index)
        {
            return property.GetArrayElementAtIndex(index);
        }
        
        
        /// <summary>
        /// Calls GetArrayElementAtIndex(ArraySize - 1)
        /// </summary>
        /// <param name="property">The property.</param>
        public static SerializedProperty GetLastIndex(this SerializedProperty property)
        {
            return property.GetArrayElementAtIndex(property.arraySize - 1);
        }
        
        
        /// <summary>
        /// Finds the index of an element in an array.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="toFind">The item to find.</param>
        /// <returns>The index of the entered property if found.</returns>
        public static int GetIndexOf(this SerializedProperty property, SerializedProperty toFind)
        {
            if (!property.isArray) return -1;
            
            for (var i = 0; i < property.arraySize; i++)
            {
                if (SerializedProperty.EqualContents(property.GetIndex(i), toFind)) return i;
            }
            
            return -1;
        }
        
        
        /// <summary>
        /// Finds the index of an element in an array.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="toFind">The item to find.</param>
        /// <returns>The index of the entered property if found.</returns>
        public static int GetIndexOf(this SerializedProperty property, Object toFind)
        {
            if (!property.isArray) return -1;
            
            for (var i = 0; i < property.arraySize; i++)
            {
                if (property.GetIndex(i).objectReferenceValue == toFind) return i;
            }
            
            return -1;
        }
        
        
        /// <summary>
        /// Gets if the object exists in the array.
        /// </summary>
        /// <param name="property">The property to look at.</param>
        /// <param name="toFind">The object to find.</param>
        /// <returns>If the element is in the array.</returns>
        public static bool Contains(this SerializedProperty property, Object toFind)
        {
            if (!property.isArray)
            {
                CartLogger.LogError<LogCategoryCart>($"Not an array... {property.type} {property.arraySize}", typeof(SerializedPropertyHelper));
                return false;
            }
            
            for (var i = 0; i < property.arraySize; i++)
            {
                if (property.GetIndex(i).objectReferenceValue == toFind) return true;
            }
            
            return false;
        }
        
        
        /// <summary>
        /// Calls FindProperty()
        /// </summary>
        /// <param name="serializedObject">The target object.</param>
        /// <param name="propName">The name of the property.</param>
        /// <returns>The found property.</returns>
        public static SerializedProperty Fp(this SerializedObject serializedObject, string propName)
        {
            return serializedObject.FindProperty(propName);
        }
        
        
        /// <summary>
        /// Calls FindPropertyRelative()
        /// </summary>
        /// <param name="property">The target property.</param>
        /// <param name="propName">The name of the property.</param>
        /// <returns>The found property.</returns>
        public static SerializedProperty Fpr(this SerializedProperty property, string propName)
        {
            return property.FindPropertyRelative(propName);
        }


        public static void TryResetToDefaultValue(this SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Generic:
                    property.objectReferenceValue = default;
                    break;
                case SerializedPropertyType.Integer:
                    property.intValue = default;
                    break;
                case SerializedPropertyType.Boolean:
                    property.boolValue = default;
                    break;
                case SerializedPropertyType.Float:
                    property.floatValue = default;
                    break;
                case SerializedPropertyType.String:
                    property.stringValue = default;
                    break;
                case SerializedPropertyType.Color:
                    property.colorValue = default;
                    break;
                case SerializedPropertyType.ObjectReference:
                    property.objectReferenceValue = default;
                    break;
                case SerializedPropertyType.LayerMask:
                    property.intValue = default;
                    break;
                case SerializedPropertyType.Enum:
                    property.intValue = default;
                    break;
                case SerializedPropertyType.Vector2:
                    property.vector2Value = default;
                    break;
                case SerializedPropertyType.Vector3:
                    property.vector3Value = default;
                    break;
                case SerializedPropertyType.Vector4:
                    property.vector4Value = default;
                    break;
                case SerializedPropertyType.Rect:
                    property.rectValue = default;
                    break;
                case SerializedPropertyType.ArraySize:
                    property.ClearArray();
                    break;
                case SerializedPropertyType.Character:
                    property.stringValue = default;
                    break;
                case SerializedPropertyType.AnimationCurve:
                    property.animationCurveValue = default;
                    break;
                case SerializedPropertyType.Bounds:
                    property.boundsValue = default;
                    break;
                case SerializedPropertyType.Quaternion:
                    property.quaternionValue = default;
                    break;
                case SerializedPropertyType.ExposedReference:
                    property.exposedReferenceValue = default;
                    break;
                case SerializedPropertyType.Vector2Int:
                    property.vector2IntValue = default;
                    break;
                case SerializedPropertyType.Vector3Int:
                    property.vector3IntValue = default;
                    break;
                case SerializedPropertyType.RectInt:
                    property.rectIntValue = default;
                    break;
                case SerializedPropertyType.BoundsInt:
                    property.boundsIntValue = default;
                    break;
                case SerializedPropertyType.ManagedReference:
                    property.managedReferenceValue = default;
                    break;
                case SerializedPropertyType.Gradient:
                case SerializedPropertyType.FixedBufferSize:
                default:
                    break;
            }
        }
    }
}