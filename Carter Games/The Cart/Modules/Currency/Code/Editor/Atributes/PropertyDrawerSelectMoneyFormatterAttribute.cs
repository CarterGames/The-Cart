using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management;
using UnityEditor;

#if CARTERGAMES_CART_MODULE_CURRENCY && UNITY_EDITOR

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


namespace CarterGames.Cart.Modules.Currency.Editor
{
    [CustomPropertyDrawer(typeof(SelectMoneyFormatterAttribute))]
    public class PropertyDrawerSelectMoneyFormatterAttribute : PropertyDrawerSearchProviderSelectable<SearchProviderMoneyFormatters, AssemblyClassDef>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected override SearchProviderMoneyFormatters Provider => SearchProviderMoneyFormatters.GetProvider();
        protected override string InitialSelectButtonLabel => "Select Money Formatter";
        
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