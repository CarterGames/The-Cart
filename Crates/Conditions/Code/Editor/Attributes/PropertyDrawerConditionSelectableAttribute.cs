#if CARTERGAMES_CART_CRATE_CONDITIONS && UNITY_EDITOR

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
using CarterGames.Cart.Crates.Conditions.Editor.Search;
using UnityEditor;

namespace CarterGames.Cart.Crates.Conditions.Editor
{
	[CustomPropertyDrawer(typeof(SelectConditionAttribute))]
	public sealed class PropertyDrawerConditionSelectableAttribute : PropertyDrawerSearchProviderSelectable<SearchProviderConditionAsset, Condition>
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		protected override SearchProviderConditionAsset Provider => SearchProviderConditionAsset.GetProvider();
		protected override string InitialSelectButtonLabel => "Select Condition";
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		protected override bool IsValid(SerializedProperty property)
		{
			return property.objectReferenceValue != null;
		}

		protected override bool GetHasValue(SerializedProperty property)
		{
			return IsValid(property);
		}

		protected override Condition GetCurrentValue(SerializedProperty property)
		{
			return (Condition) property.objectReferenceValue;
		}

		protected override string GetCurrentValueString(SerializedProperty property)
		{
			return GetCurrentValue(property).Id;
		}

		protected override void OnSelectionMade(SerializedProperty property, Condition selectedEntry)
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

#endif