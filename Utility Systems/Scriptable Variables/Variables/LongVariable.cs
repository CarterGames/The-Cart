// ----------------------------------------------------------------------------
// LongVariable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "Long Variable", menuName = "Dependency Library/Variables/Long Variable", order = 0)]
    public class LongVariable : ScriptableObject, IDependencyLibVariable<long>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public long Value { get; set; }
        [field: SerializeField] public long DefaultValue { get; set; }

        public void SetValue(long value) => Value = value;
        public void IncrementValue() => Value += Value;
        public void ResetValue() => Value = DefaultValue;
    }
}