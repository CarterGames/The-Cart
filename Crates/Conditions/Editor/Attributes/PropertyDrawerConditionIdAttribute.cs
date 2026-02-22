#if CARTERGAMES_CART_CRATE_CONDITIONS && UNITY_EDITOR

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using CarterGames.Cart.Editor;
using CarterGames.Cart.Crates.Conditions.Editor.Search;
using UnityEditor;

namespace CarterGames.Cart.Crates.Conditions.Editor
{
	[CustomPropertyDrawer(typeof(SelectConditionIdAttribute))]
	public sealed class PropertyDrawerConditionIdAttribute : PropertyDrawerSearchProviderSelectable<SearchProviderConditionId, string>
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		protected override SearchProviderConditionId Provider => SearchProviderConditionId.GetProvider();
		protected override string InitialSelectButtonLabel => "Select Condition Id";
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		protected override bool IsValid(SerializedProperty property)
		{
			return !string.IsNullOrEmpty(property.stringValue);
		}

		protected override bool GetHasValue(SerializedProperty property)
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