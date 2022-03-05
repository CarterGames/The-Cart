// ----------------------------------------------------------------------------
// GenericReference.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 22/10/2021
// ----------------------------------------------------------------------------

namespace DependencyLibrary
{
    /// <summary>
    /// Used to reference any variable
    /// </summary>
    /// <typeparam name="T">The type of reference</typeparam>
    public class GenericReference<T>
    {
        /// <summary>
        /// Should the inspector & reference use the constant value
        /// </summary>
        public bool useConstant = true;
        
        /// <summary>
        /// The constant value to use if required
        /// </summary>
        public T constantValue;
        
        /// <summary>
        /// The variable to use if required
        /// </summary>
        public IDependencyLibVariable<T> variable;

        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GenericReference() {}

        
        /// <summary>
        /// Constructor to set the default values for the reference
        /// </summary>
        /// <param name="value"></param>
        public GenericReference(T value)
        {
            useConstant = true;
            constantValue = value;
        }

        
        /// <summary>
        /// Gets the variable to use in the reference
        /// </summary>
        public virtual IDependencyLibVariable<T> GetVariable => variable;

        
        /// <summary>
        /// Gets the value of the reference is currently using
        /// </summary>
        public virtual T Value =>
            useConstant
                ? constantValue
                : GetVariable.Value;

        
        /// <summary>
        /// Sets the value of the reference to the entered value
        /// </summary>
        /// <param name="value">The value to set to</param>
        public void SetValue(T value)
        {
            if (useConstant)
                constantValue = value;
            else
                GetVariable.SetValue(value);
        }

        
        /// <summary>
        /// Implicit Operator to convert the reference to the value it uses
        /// </summary>
        /// <param name="reference">The reference to use</param>
        /// <returns>The generic type in use</returns>
        public static implicit operator T(GenericReference<T> reference)
            => reference.Value;
    }
}