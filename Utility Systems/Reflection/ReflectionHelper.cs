// ----------------------------------------------------------------------------
// ReflectionHelper.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 08/02/2022
// ----------------------------------------------------------------------------

using System;
using System.Reflection;

namespace JTools
{
    public static class ReflectionHelper
    {
        public static FieldInfo GetField(Type type, string fieldName, bool? isPublic = true, bool? isStatic = false)
        {
            var flags = 
                (isPublic != null && isPublic.Value ? BindingFlags.Public : BindingFlags.NonPublic) | 
                (isStatic != null && isStatic.Value ? BindingFlags.Static : BindingFlags.Instance);

            return type.GetField(fieldName, flags);
        }
        
        public static PropertyInfo GetProperty(Type type, string fieldName, bool? isPublic = true, bool? isStatic = false)
        {
            var flags = 
                (isPublic != null && isPublic.Value ? BindingFlags.Public : BindingFlags.NonPublic) | 
                (isStatic != null && isStatic.Value ? BindingFlags.Static : BindingFlags.Instance);

            return type.GetProperty(fieldName, flags);
        }
    }
}