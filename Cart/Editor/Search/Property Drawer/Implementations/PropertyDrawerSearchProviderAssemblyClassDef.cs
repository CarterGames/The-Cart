using CarterGames.Cart.Editor.Implementations;
using CarterGames.Cart.Runtime;
using UnityEditor;

namespace CarterGames.Cart.Editor
{
    [CustomPropertyDrawer(typeof(SearchAssemblyClassDefAttribute), true)]
    public class PropertyDrawerSearchProviderAssemblyClassDef : PropertyDrawerSearchProviderSelectable<SearchProviderClassDef, AssemblyClassDef>
    {
        protected override SearchProviderClassDef Provider => null;

        protected override string InitialSelectButtonLabel => "Select initial value";
        
        
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
            property.Fpr("assembly").stringValue = selectedEntry.StoredAssembly;
            property.Fpr("type").stringValue = selectedEntry.StoredType;

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