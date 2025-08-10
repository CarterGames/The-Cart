#if CARTERGAMES_CART_MODULE_LOCALIZATION && UNITY_EDITOR

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

using System.Linq;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;

namespace CarterGames.Cart.Modules.Localization.Editor
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
            return ScriptableRef.GetAssetDef<DataAssetDefinedLanguages>().AssetRef.Languages.FirstOrDefault(t =>
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