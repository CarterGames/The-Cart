// ----------------------------------------------------------------------------
// Vector3Variable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "Vector3 Variable", menuName = "Dependency Library/Variables/Vector3 Variable", order = 0)]
    public class Vector3Variable : ScriptableObject, IDependencyLibVariable<Vector3>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public Vector3 Value { get; set; }
        [field: SerializeField] public Vector3 DefaultValue { get; set; }

        public void SetValue(Vector3 value) => Value = value;
        public void ResetValue() => Value = DefaultValue;
    }
}