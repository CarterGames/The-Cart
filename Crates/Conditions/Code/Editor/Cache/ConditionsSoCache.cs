#if CARTERGAMES_CART_CRATE_CONDITIONS && UNITY_EDITOR

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

using System.Collections.Generic;
using CarterGames.Cart.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions.Editor
{
	public static class ConditionsSoCache
	{
		private static List<SerializedObject> conditionsSoLookup; // Conditions
		private static Dictionary<Object, SerializedObject> criteriaLookup;
		
		
		public static Dictionary<Object, SerializedObject> CriteriaLookup
		{
			get
			{
				if (!criteriaLookup.IsEmptyOrNull()) return criteriaLookup;
				criteriaLookup = new Dictionary<Object, SerializedObject>();

				// For each condition
				for (var i = 0; i < SoLookup.Count; i++)
				{
					// For each group
					for (var j = 0; j < SoLookup[i].Fp("criteriaList").arraySize; j++)
					{
						// For each criteria in the group.
						for (var k = 0; k < SoLookup[i].Fp("criteriaList").GetIndex(j).Fpr("criteria").arraySize; k++)
						{
							var prop = SoLookup[i].Fp("criteriaList").GetIndex(j).Fpr("criteria").GetIndex(k);
							var so = new SerializedObject(prop.objectReferenceValue);
							
							criteriaLookup.Add(prop.objectReferenceValue, so);
						}
					}
				}
				
				return criteriaLookup;
			}
		}


		public static bool TryGetSerializedObjectForCriteria(SerializedProperty property, out SerializedObject criteriaSerializedObject)
		{
			return CriteriaLookup.TryGetValue(property.objectReferenceValue, out criteriaSerializedObject);
		}
		
		
		public static List<SerializedObject> SoLookup
		{
			get
			{
				if (!conditionsSoLookup.IsEmptyOrNull()) return conditionsSoLookup;
				conditionsSoLookup = new List<SerializedObject>();

				if (AutoMakeDataAssetManager.GetDefine<ConditionsIndex>().ObjectRef.Fp("assets").Fpr("list").arraySize <= 0)
					return conditionsSoLookup;
				
				for (var i = 0; i < AutoMakeDataAssetManager.GetDefine<ConditionsIndex>().ObjectRef.Fp("assets").Fpr("list").arraySize; i++)
				{
					if (AutoMakeDataAssetManager.GetDefine<ConditionsIndex>().ObjectRef
					    .Fp("assets").Fpr("list").GetIndex(i) == null) continue; 
					
					if (AutoMakeDataAssetManager.GetDefine<ConditionsIndex>().ObjectRef
						    .Fp("assets").Fpr("list").GetIndex(i).Fpr("value").objectReferenceValue == null) continue; 
					
					conditionsSoLookup.Add(new SerializedObject(AutoMakeDataAssetManager.GetDefine<ConditionsIndex>().ObjectRef
						.Fp("assets").Fpr("list").GetIndex(i).Fpr("value").objectReferenceValue));
				}

				return conditionsSoLookup;
			}
		}


		
		[MenuItem("Tools/Carter Games/The Cart/[Conditions] Clear Cache", priority = 1205)]
		public static void ClearCache()
		{
			conditionsSoLookup?.Clear();
			criteriaLookup?.Clear();
		}
	}
}

#endif