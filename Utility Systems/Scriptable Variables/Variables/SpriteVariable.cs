// ----------------------------------------------------------------------------
// SpriteVariable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "Sprite Variable", menuName = "Dependency Library/Variables/Sprite Variable", order = 0)]
    public class SpriteVariable : ScriptableObject, IDependencyLibVariable<Sprite>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public Sprite Value { get; set; }
        [field: SerializeField] public Sprite DefaultValue { get; set; }

        public void SetValue(Sprite value) => Value = value;
        public void ResetValue() => Value = DefaultValue;
    }
}