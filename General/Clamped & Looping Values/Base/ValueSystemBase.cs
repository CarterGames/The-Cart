// ----------------------------------------------------------------------------
// ValueSystemBase.cs
// 
// Description: A base class for the looping & clamped value system.
// ----------------------------------------------------------------------------

using System;
using UnityEngine;

namespace Scarlet.General
{
    [Serializable]
    public abstract class ValueSystemBase<T>
    {
        [SerializeField] protected T minValue;
        [SerializeField] protected T maxValue;
        [SerializeField] protected T currentValue;
        
        /// <summary>
        /// The value currently at.
        /// </summary>
        public T Value => currentValue;
        
        /// <summary>
        /// The min value in this value.
        /// </summary>
        public T MinValue => minValue;
        
        /// <summary>
        /// The max value in this value.
        /// </summary>
        public T MaxValue => maxValue;
        
        
        protected virtual T ValueChecks(T adjustment)
        {
            return currentValue;
        }
    }
}