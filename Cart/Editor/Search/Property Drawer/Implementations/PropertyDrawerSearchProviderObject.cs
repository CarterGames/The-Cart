using CarterGames.Cart.Editor.Implementations;
using CarterGames.Cart.Runtime;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    [CustomPropertyDrawer(typeof(SearchObjectAttribute), true)]
    public class PropertyDrawerSearchProviderObject : PropertyDrawerSearchProviderSelectable<SearchProviderObject, Object>
    {
        protected override SearchProviderObject Provider { get; }
        protected override string InitialSelectButtonLabel => "Select initial value";
        
        protected override bool IsValid(SerializedProperty property)
        {
            return property.objectReferenceValue != null;
        }

        protected override bool GetHasValue(SerializedProperty property)
        {
            return property.objectReferenceValue != null;
        }

        protected override Object GetCurrentValue(SerializedProperty property)
        {
            return property.objectReferenceValue;
        }

        protected override string GetCurrentValueString(SerializedProperty property)
        {
            return property.objectReferenceValue.ToString();
        }

        
        protected override void OnSelectionMade(SerializedProperty property, Object selectedEntry)
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