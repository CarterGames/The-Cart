﻿/*
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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
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
            property.DeleteArrayElementAtIndex(index);

            if (property.arraySize <= 0) return;
            property.DeleteArrayElementAtIndex(index);
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
                Debug.LogError($"Not an array... {property.type} {property.arraySize}");
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
    }
}