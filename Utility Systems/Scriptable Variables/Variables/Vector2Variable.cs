// ----------------------------------------------------------------------------
// Vector2Variable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "Vector2 Variable", menuName = "Dependency Library/Variables/Vector2 Variable", order = 0)]
    public class Vector2Variable : ScriptableObject, IDependencyLibVariable<Vector2>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public Vector2 Value { get; set; }
        [field: SerializeField] public Vector2 DefaultValue { get; set; }

        public void SetValue(Vector2 value) => Value = value;
        public void ResetValue() => Value = DefaultValue;
    }
}