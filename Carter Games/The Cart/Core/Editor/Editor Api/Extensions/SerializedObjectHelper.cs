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

using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;

namespace CarterGames.Cart.Core.Editor
{
    /// <summary>
    /// A helper class to aid with editor scripting where the API is really wordy etc.
    /// </summary>
    public static class SerializedObjectHelper
    {
        /// <summary>
        /// Gets the total number of properties on the target Object, this includes the script itself as 1.
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        /// <param name="enterChildren">Should children be included.</param>
        /// <returns>The total amount on the object.</returns>
        public static int TotalProperties(this SerializedObject targetObject, bool enterChildren = true)
        {
            var total = 0;
            var iterator = targetObject.GetIterator();

            while (iterator.NextVisible(enterChildren))
            {
                total++;
            }

            return total;
        }

        
        public static void AddToObject(this SerializedObject target, Object toAdd, SerializedProperty listProperty)
        {
            AssetDatabase.AddObjectToAsset(toAdd, target.targetObject);
			
            listProperty.InsertIndex(listProperty.arraySize);
            listProperty.GetIndex(listProperty.arraySize - 1).objectReferenceValue = toAdd;
				
            listProperty.serializedObject.ApplyModifiedProperties();
            listProperty.serializedObject.Update();
			
            AssetDatabase.SaveAssets();
        }


        public static void RemoveFromObject(Object toRemove, SerializedProperty listProperty)
        {
            var listClone = new List<Object>();
            
            for (var i = 0; i < listProperty.arraySize; i++)
            {
                if (listProperty.GetIndex(i).objectReferenceValue == toRemove) continue;
                listClone.Add(listProperty.GetIndex(i).objectReferenceValue);
            }
            
            listProperty.ClearArray();

            for (var i = 0; i < listClone.Count; i++)
            {
                listProperty.InsertIndex(listProperty.arraySize);
                listProperty.GetIndex(listProperty.arraySize - 1).objectReferenceValue = listClone[i];
            }
			
            listProperty.serializedObject.ApplyModifiedProperties();
            listProperty.serializedObject.Update();
			
            AssetDatabase.RemoveObjectFromAsset(toRemove);
            AssetDatabase.SaveAssets();
        }
    }
}