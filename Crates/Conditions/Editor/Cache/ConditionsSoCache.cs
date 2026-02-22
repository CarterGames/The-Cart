#if CARTERGAMES_CART_CRATE_CONDITIONS && UNITY_EDITOR

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using CarterGames.Cart;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
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

				if (ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.Fp("assets").Fpr("list").arraySize <= 0)
					return conditionsSoLookup;
				
				for (var i = 0; i < ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.Fp("assets").Fpr("list").arraySize; i++)
				{
					if (ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef
					    .Fp("assets").Fpr("list").GetIndex(i) == null) continue; 
					
					if (ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef
						    .Fp("assets").Fpr("list").GetIndex(i).Fpr("value").objectReferenceValue == null) continue; 
					
					conditionsSoLookup.Add(new SerializedObject(ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef
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