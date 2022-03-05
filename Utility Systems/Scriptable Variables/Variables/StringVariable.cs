// ----------------------------------------------------------------------------
// StringVariable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "String Variable", menuName = "Dependency Library/Variables/String Variable", order = 0)]
    public class StringVariable : ScriptableObject, IDependencyLibVariable<string>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public string Value { get; set; }
        [field: SerializeField] public string DefaultValue { get; set; }

        public void SetValue(string value) => Value = value;
        public void ResetValue() => Value = DefaultValue;
    }
}