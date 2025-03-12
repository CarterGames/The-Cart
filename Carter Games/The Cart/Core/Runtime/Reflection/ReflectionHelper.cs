/*
 * Copyright (c) 2025 Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CarterGames.Cart.Core.Reflection
{
    /// <summary>
    /// A helper class for getting values from any script via reflection.
    /// </summary>
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
        /// Gets The field requested in the class type.
        /// </summary>
        /// <param name="fieldName">The field name (This is CaSe SeNsItIvE).</param>
        /// <param name="target">The target object to set to, leave null for static types.</param>
        /// <param name="value">The value to set to.</param>
        /// <param name="isPublic">Tells the system to not look through public methods if set to false.</param>
        /// <param name="isStatic">Tells the system to check static fields if true.</param>
        /// <returns>The FieldInfo found or null if nothing found.</returns>
        public static void SetField<T>(string fieldName, T target, object value, bool? isPublic = true, bool? isStatic = false)
        {
            var flags = 
                (isPublic != null && isPublic.Value ? BindingFlags.Public : BindingFlags.NonPublic) | 
                (isStatic != null && isStatic.Value ? BindingFlags.Static : BindingFlags.Instance);

            typeof(T).GetField(fieldName, flags)?.SetValue(target, value);
        }


        /// <summary>
        /// Gets fields with particular attribute on a class type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="bindingFlags">Any specific binding flags to check.</param>
        /// <typeparam name="T">The attribute type to find.</typeparam>
        /// <returns>Any FieldInfo's that match the request.</returns>
        public static FieldInfo[] GetFieldsWithAttribute<T>(Type type, BindingFlags bindingFlags) where T : Attribute
        {
            var fields = type.GetFields(bindingFlags);
            var filedInfos = new List<FieldInfo>();

            foreach (var f in fields)
            {
                if (f.GetCustomAttribute(attributeType: typeof(T)) == null) continue;
                filedInfos.Add(f);
            }

            return filedInfos.ToArray();
        }
        
        
        /// <summary>
        /// Gets fields without particular attribute on a class type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="bindingFlags">Any specific binding flags to check.</param>
        /// <typeparam name="T">The attribute type to find not on the fields.</typeparam>
        /// <returns>Any FieldInfo's that match the request.</returns>
        public static FieldInfo[] GetFieldsWithoutAttribute<T>(Type type, BindingFlags bindingFlags) where T : Attribute
        {
            var fields = type.GetFields(bindingFlags);
            var filedInfos = new List<FieldInfo>();

            foreach (var f in fields)
            {
                if (f.GetCustomAttribute(attributeType: typeof(T)) != null) continue;
                filedInfos.Add(f);
            }

            return filedInfos.ToArray();
        }
        
        
        /// <summary>
        /// Gets The field requested in the class type & casts the value it gets into the try requested.
        /// </summary>
        /// <param name="fieldName">The field name (This is CaSe SeNsItIvE).</param>
        /// <param name="target">The target object to get from, set to null if not in use.</param>
        /// <param name="isPublic">Tells the system to not look through public methods if set to false.</param>
        /// <param name="isStatic">Tells the system to check static fields if true.</param>
        /// <typeparam name="TFrom">The type to reflect from.</typeparam>
        /// <typeparam name="TAs">The type to return.</typeparam>
        /// <returns>The found field reference.</returns>
        public static TAs GetFieldValue<TFrom, TAs>(string fieldName, object target, bool isPublic = false, bool isStatic = false)
        {
            var flags =
                (isPublic ? BindingFlags.Public : BindingFlags.NonPublic) |
                (isStatic ? BindingFlags.Static : BindingFlags.Instance);

            return (TAs)typeof(TFrom).GetField(fieldName, flags)?.GetValue(target == null || isStatic ? null : target);
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