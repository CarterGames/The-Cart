#if CARTERGAMES_CART_CRATE_CURRENCY && UNITY_EDITOR

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

namespace CarterGames.Cart.Crates.Currency.Editor
{
    [CustomPropertyDrawer(typeof(SelectFormatterAttribute))]
    public class PropertyDrawerSelectFormatterAttribute : PropertyDrawerSearchProviderSelectable<SearchProviderFormatters, AssemblyClassDef>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected override SearchProviderFormatters Provider => SearchProviderFormatters.GetProvider();
        protected override string InitialSelectButtonLabel => "Select Formatter";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected override bool IsValid(SerializedProperty property)
        {
            return !string.IsNullOrEmpty(property.Fpr("assembly").stringValue) && !string.IsNullOrEmpty(property.Fpr("type").stringValue);
        }

        
        protected override bool GetHasValue(SerializedProperty property)
        {
            return GetCurrentValue(property) != null;
        }

        
        protected override AssemblyClassDef GetCurrentValue(SerializedProperty property)
        {
            return new AssemblyClassDef(property.Fpr("assembly").stringValue, property.Fpr("type").stringValue);
        }
        

        protected override string GetCurrentValueString(SerializedProperty property)
        {
            return property.Fpr("type").stringValue.SplitAndGetLastElement('.');
        }

        protected override void OnSelectionMade(SerializedProperty property, AssemblyClassDef selectedEntry)
        {
            property.Fpr("assembly").stringValue = selectedEntry.Assembly;
            property.Fpr("type").stringValue = selectedEntry.Type;

            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }


        protected override void ClearValue(SerializedProperty property)
        {
            property.Fpr("assembly").stringValue = string.Empty;
            property.Fpr("type").stringValue = string.Empty;
            
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }
    }
}

#endif