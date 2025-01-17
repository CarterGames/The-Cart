#if CARTERGAMES_CART_MODULE_LOCALIZATION

/*
 * Copyright (c) 2024 Carter Games
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

using CarterGames.Cart.Core.Editor;
using UnityEditor;

namespace CarterGames.Cart.Modules.Localization.Editor
{
    [CustomPropertyDrawer(typeof(LocalizationIdAttribute))]
    public class PropertyDrawerLocalizationIdAttribute : PropertyDrawerSearchProviderSelectable<SearchProviderLocalizationIds, string>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected override bool HasValue => !string.IsNullOrEmpty(CurrentValue);
        protected override string CurrentValue => TargetProperty.stringValue;
        protected override SearchProviderLocalizationIds Provider => SearchProviderLocalizationIds.GetProvider();
        protected override SerializedProperty EditDisplayProperty => TargetProperty;
        protected override string InitialSelectButtonLabel => "Select Localization Id";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected override bool IsValid(SerializedProperty property)
        {
            return !string.IsNullOrEmpty(property.stringValue);
        }
        

        protected override void OnSelectionMade(string selectedEntry)
        {
            TargetProperty.stringValue = selectedEntry;
            
            TargetProperty.serializedObject.ApplyModifiedProperties();
            TargetProperty.serializedObject.Update();
        }

        
        protected override void ClearValue()
        {
            TargetProperty.stringValue = string.Empty;
            
            TargetProperty.serializedObject.ApplyModifiedProperties();
            TargetProperty.serializedObject.Update();
        }
    }
}

#endif