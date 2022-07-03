// ----------------------------------------------------------------------------
// ReflectionHelper.cs
// 
// Description: A helper class for getting values from any script via reflection.
// ----------------------------------------------------------------------------

using System;
using System.Reflection;

namespace Scarlet.General
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// Gets The field requested in the class type.
        /// </summary>
        /// <param name="type">The type to get from (use an instance of type if you want a particular item.</param>
        /// <param name="fieldName">The field name (This is CaSe SeNsItIvE).</param>
        /// <param name="isPublic">Tells the system to not look through public methods if set to false.</param>
        /// <param name="isStatic">Tells the system to check static fields if true.</param>
        /// <returns>The FieldInfo found or null if nothing found.</returns>
        public static FieldInfo GetField(Type type, string fieldName, bool? isPublic = true, bool? isStatic = false)
        {
            var flags = 
                (isPublic != null && isPublic.Value ? BindingFlags.Public : BindingFlags.NonPublic) | 
                (isStatic != null && isStatic.Value ? BindingFlags.Static : BindingFlags.Instance);

            return type.GetField(fieldName, flags);
        }
        
        /// <summary>
        /// Gets The property requested in the class type.
        /// </summary>
        /// <param name="type">The type to get from (use an instance of type if you want a particular item.</param>
        /// <param name="propertyName">The property name (This is CaSe SeNsItIvE).</param>
        /// <param name="isPublic">Tells the system to not look through public methods if set to false.</param>
        /// <param name="isStatic">Tells the system to check static fields if true.</param>
        /// <returns>The PropertyInfo found or null if nothing found.</returns>
        public static PropertyInfo GetProperty(Type type, string propertyName, bool? isPublic = true, bool? isStatic = false)
        {
            var flags = 
                (isPublic != null && isPublic.Value ? BindingFlags.Public : BindingFlags.NonPublic) | 
                (isStatic != null && isStatic.Value ? BindingFlags.Static : BindingFlags.Instance);

            return type.GetProperty(propertyName, flags);
        }
        
        /// <summary>
        /// Gets The method requested in the class type.
        /// </summary>
        /// <param name="type">The type to get from (use an instance of type if you want a particular item.</param>
        /// <param name="methodName">The method name (This is CaSe SeNsItIvE).</param>
        /// <param name="isPublic">Tells the system to not look through public methods if set to false.</param>
        /// <param name="isStatic">Tells the system to check static fields if true.</param>
        /// <returns>The MethodInfo found or null if nothing found.</returns>
        public static MethodInfo GetMethod(Type type, string methodName, bool? isPublic = true, bool? isStatic = false)
        {
            var flags = 
                (isPublic != null && isPublic.Value ? BindingFlags.Public : BindingFlags.NonPublic) | 
                (isStatic != null && isStatic.Value ? BindingFlags.Static : BindingFlags.Instance);

            return type.GetMethod(methodName, flags);
        }
    }
}