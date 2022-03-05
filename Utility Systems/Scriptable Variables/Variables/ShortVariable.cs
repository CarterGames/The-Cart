// ----------------------------------------------------------------------------
// ShortVariable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "Short Variable", menuName = "Dependency Library/Variables/Short Variable", order = 0)]
    public class ShortVariable : ScriptableObject, IDependencyLibVariable<short>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public short Value { get; set; }
        [field: SerializeField] public short DefaultValue { get; set; }

        public void SetValue(short value) => Value = value;
        public void IncrementValue() => Value += Value;
        public void ResetValue() => Value = DefaultValue;
    }
}