// ----------------------------------------------------------------------------
// QuaternionVariable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "Quaternion Variable", menuName = "Dependency Library/Variables/Quaternion Variable", order = 0)]
    public class QuaternionVariable : ScriptableObject, IDependencyLibVariable<Quaternion>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public Quaternion Value { get; set; }
        [field: SerializeField] public Quaternion DefaultValue { get; set; }

        public void SetValue(Quaternion value) => Value = value;
        public void ResetValue() => Value = DefaultValue;
    }
}