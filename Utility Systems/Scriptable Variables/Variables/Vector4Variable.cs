// ----------------------------------------------------------------------------
// Vector4Variable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "Vector4 Variable", menuName = "Dependency Library/Variables/Vector4 Variable", order = 0)]
    public class Vector4Variable : ScriptableObject, IDependencyLibVariable<Vector4>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public Vector4 Value { get; set; }
        [field: SerializeField] public Vector4 DefaultValue { get; set; }

        public void SetValue(Vector4 value) => Value = value;
        public void ResetValue() => Value = DefaultValue;
    }
}