#if CARTERGAMES_CART_CRATE_PARAMETERS && UNITY_EDITOR

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

namespace CarterGames.Cart.Crates.Parameters.Editor
{
	[CustomPropertyDrawer(typeof(ParameterKeyAttribute))]
	public class PropertyDrawerParameterKeyAttribute : PropertyDrawerSearchProviderSelectable<SearchProviderParameterKeys, string>
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
		protected override SearchProviderParameterKeys Provider => SearchProviderParameterKeys.GetProvider();
		protected override string InitialSelectButtonLabel => "Select Parameter Key";
        
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		protected override bool IsValid(SerializedProperty property)
		{
			return !string.IsNullOrEmpty(property.stringValue);
		}
        
        
		protected override string GetCurrentValue(SerializedProperty property)
		{
			return property.stringValue;
		}
        

		protected override string GetCurrentValueString(SerializedProperty property)
		{
			return property.stringValue;
		}


		protected override bool GetHasValue(SerializedProperty property)
		{
			return !string.IsNullOrEmpty(GetCurrentValue(property));
		}
        

		protected override void OnSelectionMade(SerializedProperty property, string selectedEntry)
		{
			property.stringValue = selectedEntry;
            
			property.serializedObject.ApplyModifiedProperties();
			property.serializedObject.Update();
		}

        
		protected override void ClearValue(SerializedProperty property)
		{
			property.stringValue = string.Empty;
            
			property.serializedObject.ApplyModifiedProperties();
			property.serializedObject.Update();
		}
	}
}

#endif