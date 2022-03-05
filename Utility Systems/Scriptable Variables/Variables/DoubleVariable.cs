// ----------------------------------------------------------------------------
// DoubleVariable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using DependencyLibrary.Events;
using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "Double Variable", menuName = "Dependency Library/Variables/Double Variable", order = 0)]
    public class DoubleVariable : ScriptableObject, IDependencyLibVariable<double>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public double Value { get; set; }
        [field: SerializeField] public double DefaultValue { get; set; }

        public void SetValue(double value)
        {
            Value = value;
            OnValueChanged?.RaiseEvent(Value);
        }
        
        public void IncrementValue(double value)
        {
            Value += value;
            OnValueChanged?.RaiseEvent(Value);
        }

        public void ResetValue() 
        {
            Value = DefaultValue;
            OnValueChanged?.RaiseEvent(Value);
        }
        
        public DoubleEvent OnValueChanged;
    }
}