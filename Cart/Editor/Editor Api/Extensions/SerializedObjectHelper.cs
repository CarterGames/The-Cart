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

using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;

namespace CarterGames.Cart.Editor
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