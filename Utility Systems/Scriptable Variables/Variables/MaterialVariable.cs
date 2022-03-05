// ----------------------------------------------------------------------------
// MaterialVariable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 20/12/2021
// ----------------------------------------------------------------------------

using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "GameObject Variable", menuName = "Dependency Library/Variables/Material Variable", order = 0)]
    public class MaterialVariable : ScriptableObject, IDependencyLibVariable<Material>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public Material Value { get; set; }
        [field: SerializeField] public Material DefaultValue { get; set; }

        public void SetValue(Material value) => Value = value;
        public void ResetValue() => Value = DefaultValue;
    }
}