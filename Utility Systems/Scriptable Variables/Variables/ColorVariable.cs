// ----------------------------------------------------------------------------
// ColorVariable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "Color Variable", menuName = "Dependency Library/Variables/Color Variable", order = 0)]
    public class ColorVariable : ScriptableObject, IDependencyLibVariable<Color>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public Color Value { get; set; }
        [field: SerializeField] public Color DefaultValue { get; set; }

        public void SetValue(Color value) => Value = value;
        public void ResetValue() => Value = DefaultValue;
    }
}