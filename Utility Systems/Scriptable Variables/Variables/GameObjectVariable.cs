// ----------------------------------------------------------------------------
// GameObjectVariable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 14/12/2021
// ----------------------------------------------------------------------------

using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "GameObject Variable", menuName = "Dependency Library/Variables/GameObject Variable", order = 0)]
    public class GameObjectVariable : ScriptableObject, IDependencyLibVariable<GameObject>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public GameObject Value { get; set; }
        [field: SerializeField] public GameObject DefaultValue { get; set; }

        public void SetValue(GameObject value) => Value = value;
        public void ResetValue() => Value = DefaultValue;
    }
}