#if CARTERGAMES_CART_CRATE_CONDITIONS && UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Reflection;
using CarterGames.Cart;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Events;
using CarterGames.Cart.Logs;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions.Editor
{
    public static class CriteriaGroupDrawer
    {
        private static readonly GUIContent GroupContent = new GUIContent("Check Type",
            "Change the state the group needs to be in to be considered \"true\".");


        public static readonly Evt<SerializedObject> NewCriteriaAddedEvt = new Evt<SerializedObject>();
        public static readonly Evt<DataEditorCriteriaGroup> GroupRemovedEvt = new Evt<DataEditorCriteriaGroup>();
        
        
        public static void DrawGroup(DataEditorCriteriaGroup criteriaGroup)
        {
            EditorGUILayout.BeginVertical(criteriaGroup.Group.isExpanded ? "HelpBox" : "Box");
            
            if (!criteriaGroup.IsOnlyGroup)
            {
                DrawFoldout(criteriaGroup);
            }
            
            if (criteriaGroup.IsOnlyGroup || criteriaGroup.Group.isExpanded)
            {
                DrawExpandedView(criteriaGroup);
            }
            
            EditorGUILayout.EndVertical();
        }

        
        
        private static void DrawFoldout(DataEditorCriteriaGroup criteriaGroup)
        {
            EditorGUILayout.BeginHorizontal();
            
            criteriaGroup.Group.isExpanded = EditorGUILayout.Foldout(criteriaGroup.Group.isExpanded,
                new GUIContent(criteriaGroup.Group.Fpr("groupId").stringValue));
            
            DrawStateAndOptions(criteriaGroup);

            EditorGUILayout.EndHorizontal();
        }


        private static void DrawStateAndOptions(DataEditorCriteriaGroup criteriaGroup)
        {
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
            
            if (!criteriaGroup.IsOnlyGroup)
            {
                DrawGroupCheckState(criteriaGroup);
            }
            
            
            // Add new criteria
            GUI.backgroundColor = Color.yellow;
            if (GUILayout.Button("Add Criteria", GUILayout.Width(85)))
            {
                void Listener(SearchTreeEntry entry)
                {
                    AddCriteriaToGroup(criteriaGroup.Condition, criteriaGroup.GroupIndex, (Type) entry.userData);
                }
                
                SearchProviderCriteria.GetProvider().SelectionMade.Clear();
                SearchProviderCriteria.GetProvider().SelectionMade.Add(Listener);
                SearchProviderCriteria.GetProvider().Open();
                
                // For editor gui issues...
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginScrollView(new Vector2());
                EditorGUILayout.BeginScrollView(new Vector2());
                return;
            }
            GUI.backgroundColor = Color.white;
            
            
            // Remove button
            if (!criteriaGroup.IsOnlyGroup)
            {
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("-", GUILayout.Width(25)))
                {
                    RemoveGroup(criteriaGroup);
                    return;
                }
            }
            
            GUI.backgroundColor = Color.white;
            
            EditorGUI.EndDisabledGroup();
        }
        

        private static void DrawExpandedView(DataEditorCriteriaGroup criteriaGroup)
        {
            if (criteriaGroup.IsOnlyGroup)
            {
                EditorGUILayout.BeginHorizontal();
            
                EditorGUILayout.LabelField("");
            
                DrawStateAndOptions(criteriaGroup);

                EditorGUILayout.EndHorizontal();
            }
            
            EditorGUILayout.Space(5f);
            
            // Settings
            if (!criteriaGroup.IsOnlyGroup)
            {
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(criteriaGroup.Group.Fpr("groupId"));
                if (EditorGUI.EndChangeCheck())
                {
                    ConditionsSaveHandler.TrySetDirty();
                }
            }
            
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(criteriaGroup.Group.Fpr("groupCheckType"), GroupContent);
            if (EditorGUI.EndChangeCheck())
            {
                ConditionsSaveHandler.TrySetDirty();
            }
            
            // Criteria
            if (criteriaGroup.Group == null) return;
            if (criteriaGroup.Group.Fpr("criteria").arraySize > 0)
            {
                for (var i = 0; i < criteriaGroup.Group.Fpr("criteria").arraySize; i++)
                {
                    if (!ConditionsSoCache.TryGetSerializedObjectForCriteria(
                            criteriaGroup.Group.Fpr("criteria").GetIndex(i), out var criteriaSo))
                    {
                        CartLogger.Log<LogCategoryConditions>(
                            $"Unable to find criteria for entry\n{criteriaGroup.Group.Fpr("criteria").GetIndex(i).objectReferenceValue.name}");
                        continue;
                    }

                    var entryData = new DataEditorCriteria(
                        criteriaGroup.Condition,
                        criteriaSo,
                        criteriaGroup.GroupIndex,
                        !criteriaGroup.IsOnlyGroup
                    );

                    CriteriaDrawer.DrawCriteria(entryData);
                }
            }
        }
        

        private static void DrawGroupCheckState(DataEditorCriteriaGroup criteriaGroup)
        {
            if (!EditorApplication.isPlaying || !ConditionManager.IsInitialized) return;
            
            var value = ((List<CriteriaGroup>) (typeof(Condition)
                .GetField("criteriaList", BindingFlags.NonPublic | BindingFlags.Instance)
                !.GetValue(criteriaGroup.Condition.targetObject)))[criteriaGroup.GroupIndex];
					
            EditorGUILayout.Toggle(value.IsTrue, GUILayout.Width(15));
        }
        
        
        
        private static void AddCriteriaToGroup(SerializedObject serializedObject, int groupIndex, Type userData)
        {
            SearchProviderCriteria.GetProvider().SelectionMade.Clear();

            var criteria = (Criteria) ScriptableObject.CreateInstance(userData);
            criteria.name = $"{criteria.GetType().Name}_{Guid.NewGuid()}";

            var criteriaObject = new SerializedObject(criteria);
				
            serializedObject.AddToObject(criteria, serializedObject.Fp("criteriaList").GetIndex(groupIndex).Fpr("criteria"));

            criteriaObject.Fp("targetCondition").objectReferenceValue = serializedObject.targetObject;
            criteriaObject.ApplyModifiedProperties();
            criteriaObject.Update();
						
            ConditionsSoCache.ClearCache();
            ConditionsSaveHandler.TrySetDirty();
            NewCriteriaAddedEvt.Raise(serializedObject);
        }


        private static void RemoveGroup(DataEditorCriteriaGroup criteriaGroup)
        {
            if (criteriaGroup.IsOnlyGroup) return;
            
            criteriaGroup.Condition.Fp("criteriaList").DeleteAndRemoveIndex(criteriaGroup.GroupIndex);
					
            criteriaGroup.Condition.ApplyModifiedProperties();
            criteriaGroup.Condition.Update();
					
            ConditionsSoCache.ClearCache();
            ConditionsSaveHandler.TrySetDirty();
            GroupRemovedEvt.Raise(criteriaGroup);
        
            #if !UNITY_2022_2_OR_NEWER
            CartLogger.Log<LogCategoryConditions>("Known issue with Unity (Fixed in 2022.x.x or newer). Please ignore the 2 errors below.");
            #endif
        }
    }
}

#endif