#if CARTERGAMES_CART_MODULE_CONDITIONS && UNITY_EDITOR

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
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;

namespace CarterGames.Cart.Modules.Conditions.Editor
{
	public static class ConditionsSoCache
	{
		private static List<SerializedObject> conditionsSoLookup; // Conditions
		private static Dictionary<SerializedObject, List<SerializedObject>> conditionCriteriaSoLookup; // Key, condition, Value, Base And Criteria List
		private static Dictionary<SerializedObject, List<SerializedProperty>> conditionGroupsSoLookup; // Key, condition, Value, List CriteriaGroup
		private static Dictionary<SerializedProperty, List<SerializedObject>> conditionGroupsCriteriaSoLookup; // Key, CriteriaGroup, Value, Criteria List entries


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


		public static Dictionary<SerializedObject, List<SerializedObject>> BaseAndCriteriaLookup
		{
			get
			{
				if (!conditionCriteriaSoLookup.IsEmptyOrNull()) return conditionCriteriaSoLookup;
				conditionCriteriaSoLookup = new Dictionary<SerializedObject, List<SerializedObject>>();
				
				
				for (var i = 0; i < SoLookup.Count; i++)
				{
					var entry = new List<SerializedObject>();
					
					for (var j = 0; j < SoLookup[i].Fp("baseAndGroup").arraySize; j++)
					{
						if (SoLookup[i].Fp("baseAndGroup").GetIndex(j).objectReferenceValue == null) continue;
						entry.Add(new SerializedObject(SoLookup[i].Fp("baseAndGroup").GetIndex(j).objectReferenceValue));
					}
					
					conditionCriteriaSoLookup.Add(SoLookup[i], entry);
				}
				
				return conditionCriteriaSoLookup;
			}
		}
		
		
		public static Dictionary<SerializedObject, List<SerializedProperty>> GroupLookup
		{
			get
			{
				if (!conditionGroupsSoLookup.IsEmptyOrNull()) return conditionGroupsSoLookup;
				conditionGroupsSoLookup = new Dictionary<SerializedObject, List<SerializedProperty>>();
				
				for (var i = 0; i < SoLookup.Count; i++)
				{
					var entries = new List<SerializedProperty>();
					
					for (var j = 0; j < SoLookup[i].Fp("criteriaList").arraySize; j++)
					{
						if (SoLookup[i].Fp("criteriaList").GetIndex(j) == null) continue;
						entries.Add(SoLookup[i].Fp("criteriaList").GetIndex(j));
					}
					
					conditionGroupsSoLookup.Add(SoLookup[i], entries);
				}
				
				return conditionGroupsSoLookup;
			}
		}
		
		
		public static Dictionary<SerializedProperty, List<SerializedObject>> GroupCriteriaLookup
		{
			get
			{
				if (!conditionGroupsCriteriaSoLookup.IsEmptyOrNull()) return conditionGroupsCriteriaSoLookup;
				conditionGroupsCriteriaSoLookup = new Dictionary<SerializedProperty, List<SerializedObject>>();
				

				foreach (var data in GroupLookup) // Each group on the condition
				{
					for (var j = 0; j < data.Value.Count; j++)	// Each groups criteria
					{					
						var criteriaEntries = new List<SerializedObject>();
						
						for (var i = 0; i < data.Value[j].Fpr("criteria").arraySize; i++) // Each criteria list entry.
						{
							if (data.Value[j].Fpr("criteria").GetIndex(i).objectReferenceValue == null) continue;
							criteriaEntries.Add(new SerializedObject(data.Value[j].Fpr("criteria").GetIndex(i).objectReferenceValue));
						}
						
						conditionGroupsCriteriaSoLookup.Add(data.Value[j], criteriaEntries);
					}
				}
				
				
				return conditionGroupsCriteriaSoLookup;
			}
		}


		
		[MenuItem("Tools/Carter Games/The Cart/[Conditions] Clear Cache", priority = 1205)]
		public static void ClearCache()
		{
			conditionsSoLookup?.Clear();
			conditionCriteriaSoLookup?.Clear();
			conditionGroupsSoLookup?.Clear();
			conditionGroupsCriteriaSoLookup?.Clear();
		}
	}
}

#endif