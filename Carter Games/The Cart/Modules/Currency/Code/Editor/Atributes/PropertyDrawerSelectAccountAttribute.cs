#if CARTERGAMES_CART_MODULE_CURRENCY

using CarterGames.Cart.Core.Editor;
using UnityEditor;

namespace CarterGames.Cart.Modules.Currency.Editor
{
    [CustomPropertyDrawer(typeof(SelectAccountAttribute))]
    public class PropertyDrawerSelectAccountAttribute : PropertyDrawerSearchProviderSelectable<SearchProviderAccounts, string>
    {
        protected override string CurrentValue => TargetProperty.stringValue;
        protected override SearchProviderAccounts Provider => SearchProviderAccounts.GetProvider();
        protected override SerializedProperty EditDisplayProperty => TargetProperty;
        protected override string InitialSelectButtonLabel => "Select Account";
        
        
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
    }
}

#endif