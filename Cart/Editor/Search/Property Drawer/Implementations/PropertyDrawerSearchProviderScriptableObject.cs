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

using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    [CustomPropertyDrawer(typeof(SearchScriptableObjectAttribute), true)]
    public class PropertyDrawerSearchProviderScriptableObject : PropertyDrawerSearchProviderSelectable<SearchProviderScriptableObject, ScriptableObject>
    {
        protected override bool IsValid(SerializedProperty property)
        {
            return property.objectReferenceValue != null;
        }

        protected override bool GetHasValue(SerializedProperty property)
        {
            return property.objectReferenceValue != null;
        }

        protected override ScriptableObject GetCurrentValue(SerializedProperty property)
        {
            return (ScriptableObject) property.objectReferenceValue;
        }

        protected override string GetCurrentValueString(SerializedProperty property)
        {
            return property.objectReferenceValue.ToString();
        }

        
        protected override void OnSelectionMade(SerializedProperty property, ScriptableObject selectedEntry)
        {
            property.objectReferenceValue = selectedEntry;
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }
        

        protected override void ClearValue(SerializedProperty property)
        {
            property.objectReferenceValue = null;
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }
    }
}