// ----------------------------------------------------------------------------
// FloatVariable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using DependencyLibrary.Events;
using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "Float Variable", menuName = "Dependency Library/Variables/Float Variable", order = 0)]
    public class FloatVariable : ScriptableObject, IDependencyLibVariable<float>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public float Value { get; set; }
        [field: SerializeField] public float DefaultValue { get; set; }

        public void SetValue(float value)
        {
            Value = value;
            OnValueChanged?.RaiseEvent(Value);
        }
        
        public void IncrementValue(float value)
        {
            Value += value;
            OnValueChanged?.RaiseEvent(Value);
        }

        public void ResetValue() 
        {
            Value = DefaultValue;
            OnValueChanged?.RaiseEvent(Value);
        }
        
        public FloatEvent OnValueChanged;
    }
}