#if CARTERGAMES_CART_CRATE_CONDITIONS

/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CarterGames.Cart.Management;
using CarterGames.Cart.Reflection;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
{
    public static class CriteriaValidation
    {
        private static List<Type> criteriaTypesCache;
        public static List<Type> InvalidTypes { get; private set; }
        private static bool HasResult { get; set; }
        private static bool Result { get; set; }
        
        
        public static bool AllCriteriaValid()
        {
            if (HasResult) return Result;
            
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