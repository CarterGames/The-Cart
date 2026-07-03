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

namespace CarterGames.Cart.Editor
{
    [CustomPropertyDrawer(typeof(SearchIntAttribute), true)]
    public class PropertyDrawerSearchProviderInt : PropertyDrawerSearchProviderSelectable<SearchProviderInt, int>
    {
        protected override bool IsValid(SerializedProperty property)
        {
            return int.TryParse(property.stringValue, out _);
        }

        protected override bool GetHasValue(SerializedProperty property)
        {
            return IsValid(property);
        }

        protected override int GetCurrentValue(SerializedProperty property)
        {
            return property.intValue;
        }

        protected override string GetCurrentValueString(SerializedProperty property)
        {
            return property.intValue.ToString();
        }

        protected override void OnSelectionMade(SerializedProperty property, int selectedEntry)
        {
            property.intValue = selectedEntry;
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }

        protected override void ClearValue(SerializedProperty property)
        {
            property.intValue = 0;
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }
    }
}