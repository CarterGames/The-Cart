// ----------------------------------------------------------------------------
// BoolVariable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using DependencyLibrary.Events;
using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "Bool Variable", menuName = "Dependency Library/Variables/Bool Variable", order = 0)]
    public class BoolVariable : ScriptableObject, IDependencyLibVariable<bool>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public bool Value { get; set; }
        [field: SerializeField] public bool DefaultValue { get; set; }

        public void SetValue(bool value)
        {
            Value = value;
            OnValueChanged?.RaiseEvent(Value);
        }
        
        public void ToggleValue()
        {
            Value = !Value;
            OnValueChanged?.RaiseEvent(Value);
        }
        
        public void ResetValue() 
        {
            Value = DefaultValue;
            OnValueChanged?.RaiseEvent(Value);
        }
        
        public BoolEvent OnValueChanged;
    }
}