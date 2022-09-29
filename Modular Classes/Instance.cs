// ----------------------------------------------------------------------------
// Instance.cs
// 
// Description: A modular class to handle an instance of another class.
//              Ideal for static instances of a class that also needs to be
//              instanced more than once. 
// ----------------------------------------------------------------------------

using System;
using Scarlet.General.Logs;
using UnityEngine;

namespace Scarlet.ModularComponents
{
    /// <summary>
    /// A modular class to handle an instance of another class.
    /// </summary>
    /// <typeparam name="T">The type to instance.</typeparam>
    public class Instance<T> where T : UnityEngine.Object
    {
        /// <summary>
        /// The instance value.
        /// </summary>
        public T Value { get; }


        /// <summary>
        /// Initialises the instance to the value entered. 
        /// </summary>
        /// <param name="typeValue">The instance reference to use.</param>
        /// <param name="dontDestroy">Should the instance be using DoNoDestroyOnLoad().</param>
        public Instance(T typeValue, bool dontDestroy = false)
        {
            Value = typeValue;

            if (!dontDestroy) return;
            UnityEngine.Object.DontDestroyOnLoad(Value);
        }
        
        
        /// <summary>
        /// Initialises the instance via the function entered. 
        /// </summary>
        /// <param name="typeInstance">The function to get the instance reference.</param>
        /// <param name="dontDestroy">Should the instance be using DoNoDestroyOnLoad().</param>
        public Instance(Func<T> typeInstance, bool dontDestroy = false)
        {
            Value = typeInstance.Invoke();

            if (!dontDestroy) return;
            UnityEngine.Object.DontDestroyOnLoad(Value);
        }

        
        /// <summary>
        /// Implicitly returns the instance so you don't need to use Instance.Value all the time.
        /// </summary>
        /// <param name="instance">The instance to use.</param>
        /// <returns>The instance saved.</returns>
        public static implicit operator T(Instance<T> instance)
        {
            if (instance.Value == null)
                ScarletLogs.Error(typeof(Instance<>),"Instance is null. Please ensure the instance is initialised and all.");
            
            return instance.Value;
        }
    }
}