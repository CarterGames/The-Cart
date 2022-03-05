// ----------------------------------------------------------------------------
// IDependencyResetOnBuild.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

namespace DependencyLibrary
{
    /// <summary>
    /// Used to ensure that values reset when the developer makes a build
    /// </summary>
    public interface IDependencyResetOnBuild
    {
        /// <summary>
        /// The method called to reset the value to default
        /// </summary>
        void ResetValue();
    }
}