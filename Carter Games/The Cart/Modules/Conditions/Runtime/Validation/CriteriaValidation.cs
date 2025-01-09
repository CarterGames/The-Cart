#if CARTERGAMES_CART_MODULE_CONDITIONS

/*
 * Copyright (c) 2024 Carter Games
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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Reflection;
using UnityEngine;

namespace CarterGames.Cart.Modules.Conditions
{
    public static class CriteriaValidation
    {
        private static List<Type> criteriaTypesCache;
        public static List<Type> InvalidTypes { get; private set; }
        private static bool HasResult { get; set; }
        private static bool Result { get; set; }
        
        
        public static bool AllCriteriaValid()
        {
            if (HasResult)
            {
                return Result;
            }
            
            if (InvalidTypes == null)
            {
                InvalidTypes = new List<Type>();
            }
            
            if (criteriaTypesCache == null)
            {
                criteriaTypesCache = AssemblyHelper.GetClassesNamesOfType<Criteria>().ToList();
            }
            
            foreach (var type in criteriaTypesCache)
            {
                var hasNonSerialized = ReflectionHelper.GetFieldsWithoutAttribute<NonSerializedAttribute>(type,
                    BindingFlags.NonPublic | BindingFlags.Instance);
                
                var hasSerializedFields = ReflectionHelper.GetFieldsWithAttribute<SerializeField>(type,
                    BindingFlags.NonPublic | BindingFlags.Instance);
                
                var fields = hasNonSerialized.Where(t => !hasSerializedFields.Contains(t));
                
                if (!fields.Any()) continue;
                
                InvalidTypes.Add(type);
            }

            Result = InvalidTypes.Count <= 0;
            HasResult = true;
            return Result;
        }
    }
}

#endif