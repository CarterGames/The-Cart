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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Linq;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Editor;
using UnityEditor;

namespace CarterGames.Cart.Modules.Localization.Editor
{
    [CustomPropertyDrawer(typeof(LanguageSelectableAttribute))]
    public sealed class PropertyDrawerLanguageSelectableAttribute : PropertyDrawerSearchProviderSelectable<SearchProviderLanguages, Language>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected override Language CurrentValue => DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages.FirstOrDefault(t =>
            t.DisplayName.Equals(TargetProperty.Fpr("name").stringValue));

        protected override SearchProviderLanguages Provider => SearchProviderLanguages.GetProvider();
        protected override SerializedProperty EditDisplayProperty => TargetProperty.Fpr("name");
        protected override string InitialSelectButtonLabel => "Select Language";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected override bool IsValid(SerializedProperty property)
        {
            return !string.IsNullOrEmpty(property.Fpr("name").stringValue) &&
                   !string.IsNullOrEmpty(property.Fpr("code").stringValue);
        }


        protected override void OnSelectionMade(Language selectedEntry)
        {
            TargetProperty.Fpr("name").stringValue = selectedEntry.DisplayName;
            TargetProperty.Fpr("code").stringValue = selectedEntry.Code;

            TargetProperty.serializedObject.ApplyModifiedProperties();
            TargetProperty.serializedObject.Update();
        }
    }
}

#endif