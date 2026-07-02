using CarterGames.Cart.Editor.Implementations;
using CarterGames.Cart.Runtime;
using UnityEditor;

namespace CarterGames.Cart.Editor
{
    [CustomPropertyDrawer(typeof(SearchStringAttribute), true)]
    public class PropertyDrawerSearchProviderString : PropertyDrawerSearchProviderSelectable<SearchProviderString, string>
    {
        protected override SearchProviderString Provider { get; }
        protected override string InitialSelectButtonLabel => "Select initial value";
        
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