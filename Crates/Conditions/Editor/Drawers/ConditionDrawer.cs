#if CARTERGAMES_CART_CRATE_CONDITIONS && UNITY_EDITOR

using System;
using System.Reflection;
using CarterGames.Cart;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Events;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions.Editor
{
    public static class ConditionDrawer
    {
        public static readonly Evt<SerializedObject> NewCriteriaCroupAddedEvt = new Evt<SerializedObject>();
        
        
        
        public static void DrawCondition(SerializedObject serializedObject)
        {
            if (serializedObject.Fp("isExpanded").boolValue)
            {
                EditorGUILayout.BeginVertical("HelpBox");
            }
            else
            {
                EditorGUILayout.BeginVertical();
            }
            
            // Foldout.
            DrawFoldout(serializedObject);
            
            EditorGUILayout.Space(5f);
            
            // Expanded view
            if (serializedObject.Fp("isExpanded").boolValue)
            {
                // Draw info
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(serializedObject.Fp("uid"));
                EditorGUI.EndDisabledGroup();
                
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(serializedObject.Fp("id"));
                if (EditorGUI.EndChangeCheck())
                {
                    ConditionsSaveHandler.TrySetDirty();
                }
                
                EditorGUILayout.Space(1.5f);
                
                GeneralUtilEditor.DrawHorizontalGUILine();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Criteria", EditorStyles.boldLabel);

                if (GUILayout.Button("+ New Group", GUILayout.Width(100)))
                {
                    MakeNewCriteriaGroup(serializedObject);
                }
                
                EditorGUILayout.EndHorizontal();
                GeneralUtilEditor.DrawHorizontalGUILine();

                // Criteria Groups
                if (serializedObject.Fp("criteriaList").arraySize > 0)
                {
                    for (var i = 0; i < serializedObject.Fp("criteriaList").arraySize; i++)
                    {
                        var groupData = new DataEditorCriteriaGroup(
                            serializedObject,
                            serializedObject.Fp("criteriaList").GetIndex(i),
                            i,
                            serializedObject.Fp("criteriaList").arraySize <= 1
                            );

                        CriteriaGroupDrawer.DrawGroup(groupData);
                    }
                }
            }
            
            EditorGUILayout.EndVertical();
        }


        private static void DrawFoldout(SerializedObject serializedObject)
        {
            EditorGUILayout.BeginHorizontal();
            
            serializedObject.Fp("isExpanded").boolValue = EditorGUILayout.Foldout(serializedObject.Fp("isExpanded").boolValue,
                new GUIContent(serializedObject.Fp("id").stringValue));
            
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
            DrawConditionCheckState(serializedObject);
            
            // Remove button
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("-", GUILayout.Width(25)))
            {
                RemoveCondition(serializedObject);
                return;
            }
            GUI.backgroundColor = Color.white;
            
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
        
        
        private static void DrawConditionCheckState(SerializedObject serializedObject)
        {
            if (!EditorApplication.isPlaying || !ConditionManager.IsInitialized) return;
            
            var isValid = typeof(Condition)
                .GetProperty("IsTrue", BindingFlags.Public | BindingFlags.Instance)
                !.GetValue(serializedObject.targetObject);
					
            EditorGUILayout.Toggle((bool) isValid, GUILayout.Width(15));
        }
        
        
        private static void MakeNewCriteriaGroup(SerializedObject serializedObject)
        {
            // New group.
            serializedObject.Fp("criteriaList").InsertIndex(serializedObject.Fp("criteriaList").arraySize);

            var newGrp = serializedObject.Fp("criteriaList").GetLastIndex();
            
            newGrp.Fpr("criteria").InsertIndex(0);
            newGrp.Fpr("groupCheckType").intValue = 1;
            newGrp.Fpr("groupId").stringValue = Guid.NewGuid().ToString();
            newGrp.Fpr("groupUuid").stringValue = Guid.NewGuid().ToString();
            newGrp.Fpr("criteria").ClearArray();
							
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
            
            NewCriteriaCroupAddedEvt.Raise(serializedObject);
        }


        private static void RemoveCondition(SerializedObject serializedObject)
        {
            var index = 0;
				
            for (var i = 0; i < ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.Fp("assets").Fpr("list").arraySize; i++)
            {
                if (ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.Fp("assets").Fpr("list").GetIndex(i)
                        .Fpr("value").objectReferenceValue == serializedObject.targetObject)
                {
                    index = i;
                    break;
                }
            }
				
            ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.Fp("assets").Fpr("list").DeleteIndex(index);
            ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.ApplyModifiedProperties();
            ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.Update();

            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(serializedObject.targetObject));
            AssetDatabase.SaveAssets();
				
            ConditionsSoCache.ClearCache();
        }
    }
}

#endif