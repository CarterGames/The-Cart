// ----------------------------------------------------------------------------
// IDependencyLibVariable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 22/10/2021
// ----------------------------------------------------------------------------

namespace DependencyLibrary
{
    /// <summary>
    /// An interface to make a dependency library variable of any type
    /// </summary>
    /// <typeparam name="T">The type for the variable to be</typeparam>
    public interface IDependencyLibVariable<T>
    {
        /// <summary>
        /// The description field for the dev to write stuff about the variable
        /// </summary>
        string DevDescription { get; set; }
        
        /// <summary>
        /// A generic for the default value of the variable
        /// </summary>
        T DefaultValue { get; set; }
        
        /// <summary>
        /// A generic for the actual value of the variable
        /// </summary>
        T Value { get; set; }
        
        /// <summary>
        /// A method for the dev to set the value to whatever was entered
        /// </summary>
        /// <param name="value">A passthrough for the value to set to</param>
        void SetValue(T value);
    }
}