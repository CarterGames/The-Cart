#if CARTERGAMES_CART_CRATE_LOCALIZATION && UNITY_EDITOR

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

using System.Linq;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using UnityEditor;

namespace CarterGames.Cart.Crates.Localization.Editor
{
    [CustomPropertyDrawer(typeof(LanguageSelectableAttribute), true)]
    public sealed class PropertyDrawerLanguageSelectableAttribute : PropertyDrawerSearchProviderSelectable<SearchProviderLanguages, Language>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected override SearchProviderLanguages Provider => SearchProviderLanguages.GetProvider();
        protected override string InitialSelectButtonLabel => "Select Language";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected override bool IsValid(SerializedProperty property)
        {
            return !string.IsNullOrEmpty(property.Fpr("code").stringValue);
        }
        

        protected override bool GetHasValue(SerializedProperty property)
        {
            return !string.IsNullOrEmpty(GetCurrentValue(property).Code);
        }

        
        protected override Language GetCurrentValue(SerializedProperty property)
        {
            return AutoMakeDataAssetManager.GetDefine<DataAssetDefinedLanguages>().AssetRef.Languages.FirstOrDefault(t =>
                t.Code.Equals(property.Fpr("code").stringValue));
        }
        

        protected override string GetCurrentValueString(SerializedProperty property)
        {
            return GetCurrentValue(property).Code;
        }


        protected override void OnSelectionMade(SerializedProperty property, Language selectedEntry)
        {
            property.Fpr("code").stringValue = selectedEntry.Code;

            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }
        

        protected override void ClearValue(SerializedProperty property)
        {
            property.Fpr("code").stringValue = string.Empty;
            
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }
    }
}

#endif