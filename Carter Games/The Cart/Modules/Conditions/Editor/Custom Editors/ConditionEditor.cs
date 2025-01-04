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
using System.Reflection;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Modules.Conditions.Editor
{
	public static class ConditionEditor
	{
		private static readonly string[] CriteriaIgnoreProperties = new string[5]
		{
			"m_Script", "condition", "isExpanded", "excludeFromAssetIndex", "variantId"
		};
		
		
		
		public static void DrawCondition(SerializedObject condition, EditorWindow window)
		{
			EditorGUILayout.BeginVertical(condition.Fp("isExpanded").boolValue ? "HelpBox" : "Box");
			EditorGUI.BeginChangeCheck();
			
			
			// Condition Dropdown Option
			/* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
			EditorGUILayout.BeginHorizontal();
			
			condition.Fp("isExpanded").boolValue = EditorGUILayout.Foldout(condition.Fp("isExpanded").boolValue,
				new GUIContent(condition.Fp("id").stringValue));
			
			EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
			
			if (EditorApplication.isPlaying)
			{
				var isValid = typeof(Condition)
					.GetProperty("IsValid", BindingFlags.Public | BindingFlags.Instance)
					!.GetValue(condition.targetObject);
					
				EditorGUILayout.Toggle((bool) isValid, GUILayout.Width(15));
			}

			GUI.backgroundColor = Color.red;
			if (GUILayout.Button("-", GUILayout.Width(25)))
			{
				var index = 0;
				
				for (var i = 0; i < ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.Fp("assets").Fpr("list").arraySize; i++)
				{
					if (ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.Fp("assets").Fpr("list").GetIndex(i)
						    .Fpr("value").objectReferenceValue == condition.targetObject)
					{
						index = i;
						break;
					}
				}
				
				ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.Fp("assets").Fpr("list").DeleteIndex(index);
				ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.ApplyModifiedProperties();
				ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.Update();

				AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(condition.targetObject));
				AssetDatabase.SaveAssets();
				
				ConditionsSoCache.ClearCache();
				window.Repaint();
				return;
			}
			GUI.backgroundColor = Color.white;
			
			EditorGUI.EndDisabledGroup();
			EditorGUILayout.EndHorizontal();
			/* ────────────────────────────────────────────────────────────────────────────────────────────────────── */

			
			// When open
			if (condition.Fp("isExpanded").boolValue)
			{
				GeneralUtilEditor.DrawHorizontalGUILine();

				
				// Condition Header
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
				EditorGUILayout.BeginHorizontal();
				
				// Id editing...
				EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.PropertyField(condition.Fp("id"));
				EditorGUI.EndDisabledGroup();
				
				if (GUILayout.Button("Edit", GUILayout.Width(60)))
				{
					UtilityEditorWindowGenericText.ValueChangedCtx.Clear();
					UtilityEditorWindowGenericText.ValueChangedCtx.Add(AssignValueToProp);
					UtilityEditorWindowGenericText.OpenAndAssignInfo("Change Condition Name", "Assign a name for the condition to reference it by.", condition.Fp("id").stringValue);
					return;

					void AssignValueToProp(string value)
					{
						condition.Fp("id").stringValue = value;
						condition.ApplyModifiedProperties();
						condition.Update();
					}
				}
				
				EditorGUILayout.EndHorizontal();
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
				
				
				// Criteria (No Group)
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
				if (ConditionsSoCache.BaseAndCriteriaLookup.ContainsKey(condition))
				{
					DrawUngroupedCriteria(ConditionsSoCache.BaseAndCriteriaLookup[condition], condition, window);
				}
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */

				
				// Criteria (Grouped)
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
				if (ConditionsSoCache.GroupLookup.ContainsKey(condition))
				{
					var data = ConditionsSoCache.GroupLookup[condition].ToArray();
					
					for (var i = 0; i < data.Length; i++)
					{
						if (!ConditionsSoCache.GroupCriteriaLookup.ContainsKey(data[i])) continue;
						DrawGroupedCriteria(i, ConditionsSoCache.GroupCriteriaLookup[data[i]], condition, window);
					}
				}
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */

				
				// Add Criteria
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
				GUI.backgroundColor = Color.yellow;
				if (GUILayout.Button("Add Criteria"))
				{
					void Listener(SearchTreeEntry entry)
					{
						SearchProviderCriteria.GetProvider().SelectionMade.Remove(Listener);

						var criteria = (Criteria) ScriptableObject.CreateInstance((Type) entry.userData);
						criteria.name = $"{criteria.GetType().Name}_{Guid.NewGuid()}";

						var criteriaObject = new SerializedObject(criteria);
				
						condition.AddToObject(criteria, condition.Fp("baseAndGroup"));

						criteriaObject.Fp("targetCondition").objectReferenceValue = condition.targetObject;
						criteriaObject.ApplyModifiedProperties();
						criteriaObject.Update();
						
						ConditionsSoCache.ClearCache();

						window.Repaint();
					}

					SearchProviderCriteria.GetProvider().SelectionMade.Add(Listener);
					SearchProviderCriteria.GetProvider().Open();
				}
				GUI.backgroundColor = Color.white;
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
			}
			
			
			if (EditorGUI.EndChangeCheck())
			{
				if (condition != null)
				{
					condition.ApplyModifiedProperties();
					condition.Update();
				}
			}
			
			
			EditorGUILayout.EndVertical();
		}
		
		
		
		private static void DrawUngroupedCriteria(List<SerializedObject> criteria, SerializedObject conditionObject, EditorWindow window)
		{
			if (criteria.Count <= 0) return;
			
			EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
			
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.Space(1.5f);

			
			// Foreach criteria in the condition that isn't grouped.
			/* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
			foreach (var criteriaEntry in criteria.ToArray())
			{
				EditorGUILayout.BeginVertical(criteriaEntry.Fp("isExpanded").boolValue ? "HelpBox" : "Box");
				EditorGUILayout.Space(1.5f);
				
				EditorGUI.BeginChangeCheck();

				
				// Criteria Header & Dropdown
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
				EditorGUILayout.BeginHorizontal();
				criteriaEntry.Fp("isExpanded").boolValue =
					EditorGUILayout.Foldout(criteriaEntry.Fp("isExpanded").boolValue, new GUIContent(criteriaEntry.targetObject.GetType().Name.Replace("Criteria", string.Empty).SplitCapitalsWithSpace()));
				
				
				if (EditorApplication.isPlaying)
				{
					var isValid = typeof(Criteria)
						.GetProperty("IsCriteriaValid", BindingFlags.Public | BindingFlags.Instance)
						!.GetValue(criteriaEntry.targetObject);
					
					EditorGUILayout.Toggle((bool) isValid, GUILayout.Width(15));
				}
				
				GUI.backgroundColor = Color.cyan;
				if (GUILayout.Button("Add To Group", GUILayout.Width(100)))
				{
					void Listener(SearchTreeEntry entry)
					{
						SearchProviderConditionGroups.GetProvider().SelectionMade.Remove(Listener);
						SearchProviderConditionGroups.IsInGroup = false;

						if ((int)entry.userData < 0)
						{
							ConditionEditorHelper.AddToNewGroup(conditionObject, criteriaEntry.targetObject);
						}
						else
						{
							ConditionEditorHelper.AddToExistingGroup(conditionObject, (int) entry.userData,
								criteriaEntry.targetObject);
						}
						
						ConditionsSoCache.ClearCache();
						window.Repaint();
					}
					
					SearchProviderConditionGroups.IsInGroup = false;
					SearchProviderConditionGroups.GetProvider().SelectionMade.Add(Listener);
					SearchProviderConditionGroups.GetProvider().Open(conditionObject);
				}
				
				GUI.backgroundColor = Color.red;
				if (GUILayout.Button("-", GUILayout.Width(25)))
				{
					SerializedObjectHelper.RemoveFromObject((DataAsset) criteriaEntry.targetObject, conditionObject.Fp("baseAndGroup"));
					
					conditionObject.ApplyModifiedProperties();
					conditionObject.Update();
					
					ConditionsSoCache.ClearCache();
					window.Repaint();
				}
				GUI.backgroundColor = Color.white;
				
				EditorGUILayout.EndHorizontal();
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
				
				
				// Criteria Inspector
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
				if (criteriaEntry.Fp("isExpanded").boolValue)
				{
					var iterator = criteriaEntry.GetIterator();
					GeneralUtilEditor.DrawHorizontalGUILine();
					
					if (iterator.NextVisible(true))
					{
						while (iterator.NextVisible(false))
						{
							if (iterator.hasChildren)
							{
								if (iterator.hasMultipleDifferentValues) continue;
							}

							if (CriteriaIgnoreProperties.Contains(iterator.name)) continue;
							EditorGUILayout.PropertyField(iterator);
						}
					}
				}
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
				
				
				
				if (EditorGUI.EndChangeCheck())
				{
					criteriaEntry.ApplyModifiedProperties();
					criteriaEntry.Update();
				}
				
				EditorGUILayout.Space(1.5f);
				EditorGUILayout.EndVertical();
			}
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
			
			EditorGUI.EndDisabledGroup();
		}
		
		
		
		
		private static void DrawGroupedCriteria(int groupIndex, List<SerializedObject> criteria, SerializedObject conditionObject, EditorWindow window)
		{
			if (criteria == null) return;
			if (criteria.Count <= 0) return;
			
			EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
			
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.Space(1.5f);

			
			// Header
			/* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
			EditorGUILayout.BeginHorizontal();

			var label = conditionObject.Fp("criteriaList").GetIndex(groupIndex).Fpr("groupId").stringValue;
			
			EditorGUILayout.LabelField(label, EditorStyles.boldLabel, GUILayout.Width(label.GUIWidth()));
			EditorGUILayout.LabelField(conditionObject.Fp("criteriaList").GetIndex(groupIndex).Fpr("groupUuid").intValue.ToString(), EditorStyles.miniLabel);

			if (GUILayout.Button("Edit Group Label", GUILayout.Width(110)))
			{
				UtilityEditorWindowGenericText.ValueChangedCtx.Clear();
				UtilityEditorWindowGenericText.ValueChangedCtx.Add(AssignValueToProp);
				UtilityEditorWindowGenericText.OpenAndAssignInfo("Change Criteria Group Label", "Assign a label for the criteria, this is not used in code in anyway, purely cosmetic.", conditionObject.Fp("criteriaList").GetIndex(groupIndex).Fpr("groupId").stringValue);
				return;

				void AssignValueToProp(string value)
				{
					conditionObject.Fp("criteriaList").GetIndex(groupIndex).Fpr("groupId").stringValue = value;
					conditionObject.ApplyModifiedProperties();
					conditionObject.Update();
				}
			}
			
			if (EditorApplication.isPlaying)
			{
				var value = (List<CriteriaGroup>) (typeof(Condition)
					.GetField("criteriaList", BindingFlags.NonPublic | BindingFlags.Instance)
					!.GetValue(conditionObject.targetObject));
					
				EditorGUILayout.Toggle(value[groupIndex].IsValid, GUILayout.Width(15));
			}
			
			
			GUI.backgroundColor = new Color32(255, 140, 0, 255);
			
			
			if (GUILayout.Button(((CriteriaGroupCheckType) conditionObject.Fp("criteriaList").GetIndex(groupIndex)
				    .Fpr("groupCheckType").intValue).ToString(), GUILayout.Width(50)))
			{
				var edit = conditionObject.Fp("criteriaList").GetIndex(groupIndex)
					.Fpr("groupCheckType").intValue == 1
					? 2
					: 1;

				conditionObject.Fp("criteriaList").GetIndex(groupIndex)
					.Fpr("groupCheckType").intValue = edit;
				
				conditionObject.ApplyModifiedProperties();
				conditionObject.Update();
			}
			
			
			GUI.backgroundColor = Color.white;
			EditorGUILayout.EndHorizontal();
			/* ────────────────────────────────────────────────────────────────────────────────────────────────────── */

			
			// Foreach criteria in the group.
			/* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
			foreach (var criteriaEntry in criteria.ToArray())
			{
				// Header & Dropdown for the current criteria.
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
				EditorGUILayout.BeginVertical(criteriaEntry.Fp("isExpanded").boolValue ? "HelpBox" : "Box");
				EditorGUILayout.Space(1.5f);
				
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.BeginHorizontal();
				
				
				criteriaEntry.Fp("isExpanded").boolValue =
					EditorGUILayout.Foldout(criteriaEntry.Fp("isExpanded").boolValue, new GUIContent(criteriaEntry.targetObject.GetType().Name.Replace("Criteria", string.Empty).SplitCapitalsWithSpace()));
				
				
				if (EditorApplication.isPlaying)
				{
					var isValid = typeof(Criteria)
						.GetProperty("Valid", BindingFlags.Public | BindingFlags.Instance)
						!.GetValue(criteriaEntry.targetObject);
					
					EditorGUILayout.Toggle((bool) isValid, GUILayout.Width(15));
				}
				
				
				GUI.backgroundColor = Color.cyan;
				if (GUILayout.Button("Change Group", GUILayout.Width(100)))
				{
					void Listener(SearchTreeEntry entry)
					{
						SearchProviderConditionGroups.GetProvider().SelectionMade.Remove(Listener);
						SearchProviderConditionGroups.IsInGroup = false;

						if ((int)entry.userData < 0)
						{
							ConditionEditorHelper.AddToNewGroup(conditionObject, criteriaEntry.targetObject);
						}
						else if ((int) entry.userData == 0)
						{
							ConditionEditorHelper.AddToUngrouped(conditionObject, criteriaEntry.targetObject);
						}
						else
						{
							ConditionEditorHelper.AddToExistingGroup(conditionObject, (int) entry.userData,
								criteriaEntry.targetObject);
						}
						
						ConditionsSoCache.ClearCache();
						window.Repaint();
					}

					SearchProviderConditionGroups.IsInGroup = true;
					SearchProviderConditionGroups.GetProvider().SelectionMade.Add(Listener);
					SearchProviderConditionGroups.GetProvider().Open(conditionObject);
				}
				
				
				GUI.backgroundColor = Color.red;
				
				if (GUILayout.Button("-", GUILayout.Width(25)))
				{
					SerializedObjectHelper.RemoveFromObject((DataAsset) criteriaEntry.targetObject, conditionObject.Fp("criteriaList").GetIndex(groupIndex).Fpr("criteria"));

					if (conditionObject.Fp("criteriaList").GetIndex(groupIndex).Fpr("criteria").arraySize <= 0)
					{
						conditionObject.Fp("criteriaList").DeleteAndRemoveIndex(groupIndex);
					}
					
					conditionObject.ApplyModifiedProperties();
					conditionObject.Update();
					
					ConditionsSoCache.ClearCache();
					window.Repaint();
				}
				
				GUI.backgroundColor = Color.white;
				EditorGUILayout.EndHorizontal();
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
				
				
				// Draw inspector for criteria.
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
				if (criteriaEntry.Fp("isExpanded").boolValue)
				{
					var iterator = criteriaEntry.GetIterator();
					GeneralUtilEditor.DrawHorizontalGUILine();

					if (iterator.NextVisible(true))
					{
						while (iterator.NextVisible(false))
						{
							if (CriteriaIgnoreProperties.Contains(iterator.name)) continue;
							EditorGUILayout.PropertyField(iterator);
						}
					}
				}
				/* ────────────────────────────────────────────────────────────────────────────────────────────────── */
				
				
				if (EditorGUI.EndChangeCheck())
				{
					criteriaEntry.ApplyModifiedProperties();
					criteriaEntry.Update();
				}
				
				
				EditorGUILayout.Space(1.5f);
				EditorGUILayout.EndVertical();
			}
			/* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
			
			EditorGUI.EndDisabledGroup();
		}
	}
}

#endif