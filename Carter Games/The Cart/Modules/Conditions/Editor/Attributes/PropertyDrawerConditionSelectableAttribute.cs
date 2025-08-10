#if CARTERGAMES_CART_MODULE_CONDITIONS && UNITY_EDITOR

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

using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Modules.Conditions.Editor.Search;
using UnityEditor;

namespace CarterGames.Cart.Modules.Conditions.Editor
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