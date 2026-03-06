#if CARTERGAMES_CART_CRATE_CONDITIONS && UNITY_EDITOR

using System.Reflection;
using CarterGames.Cart;
using CarterGames.Cart.Data;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Events;
using CarterGames.Cart.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions.Editor
{
    public static class CriteriaDrawer
    {
	    private static readonly string[] CriteriaIgnoreProperties = new string[5]
	    {
		    "m_Script", "condition", "isExpanded", "excludeFromAssetIndex", "variantId"
	    };
	    
	    
	    public static readonly Evt<SerializedObject> GroupChangedEvt = new Evt<SerializedObject>();
	    public static readonly Evt<SerializedObject> RemovedEvt = new Evt<SerializedObject>();
	    
	    
	    public static void DrawCriteria(DataEditorCriteria criteriaEntry)
	    {
		    var serializedObject = criteriaEntry.Criteria;
		    var conditionObject = criteriaEntry.Condition;
		    
		    
		    // Header & Dropdown for the current criteria.
		    /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
		    EditorGUILayout.BeginVertical(serializedObject.Fp("isExpanded").boolValue ? "HelpBox" : "Box");
		    EditorGUILayout.Space(1.5f);

		    EditorGUI.BeginChangeCheck();
		    EditorGUILayout.BeginHorizontal();

		    var label = (string) (ReflectionHelper
			    .GetProperty(criteriaEntry.Criteria.targetObject.GetType(), "DisplayName", false, false)
			    .GetValue(criteriaEntry.Criteria.targetObject));

		    serializedObject.Fp("isExpanded").boolValue =
			    EditorGUILayout.Foldout(serializedObject.Fp("isExpanded").boolValue,
				    new GUIContent(label));


		    if (EditorApplication.isPlaying && ConditionManager.IsInitialized)
		    {
			    var isValid = typeof(Criteria)
				    .GetProperty("Valid", BindingFlags.NonPublic | BindingFlags.Instance)
				    !.GetValue(serializedObject.targetObject);

			    EditorGUILayout.Toggle((bool)isValid, GUILayout.Width(15));
		    }


		    if (criteriaEntry.CanChangeGroup)
		    {
			    GUI.backgroundColor = Color.cyan;
			    if (GUILayout.Button("Change Group", GUILayout.Width(100)))
			    {
				    void Listener(SearchTreeEntry entry)
				    {
					    ChangeCriteriaGroup(criteriaEntry, (int)entry.userData);
				    }

				    SearchProviderConditionGroups.IsInGroup = true;
				    SearchProviderConditionGroups.GetProvider().SelectionMade.Add(Listener);
				    SearchProviderConditionGroups.GetProvider().Open(conditionObject);
				    
				    // This is really stupid, but it stops errors xD
				    EditorGUILayout.BeginVertical();
				    EditorGUILayout.BeginVertical();
				    EditorGUILayout.BeginVertical();
				    EditorGUILayout.BeginVertical();
				    EditorGUILayout.BeginScrollView(Vector2.zero);
				    return;
			    }
		    }


		    GUI.backgroundColor = Color.red;

		    if (GUILayout.Button("-", GUILayout.Width(25)))
		    {
			    RemoveCriteria(criteriaEntry);
		    }

		    GUI.backgroundColor = Color.white;
		    EditorGUILayout.EndHorizontal();
		    /* ────────────────────────────────────────────────────────────────────────────────────────────────── */


		    // Draw inspector for criteria.
		    /* ────────────────────────────────────────────────────────────────────────────────────────────────── */
		    if (serializedObject.Fp("isExpanded").boolValue)
		    {
			    var iterator = serializedObject.GetIterator();
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
			    serializedObject.ApplyModifiedProperties();
			    serializedObject.Update();
		    }


		    EditorGUILayout.Space(1.5f);
		    EditorGUILayout.EndVertical();
	    }


	    private static void ChangeCriteriaGroup(DataEditorCriteria criteriaEntry, int userEntry)
	    {
		    SearchProviderConditionGroups.GetProvider().SelectionMade.Clear();
		    SearchProviderConditionGroups.IsInGroup = false;
		    
		    AddToExistingGroup(criteriaEntry, userEntry, criteriaEntry.Criteria.targetObject);

		    ConditionsSoCache.ClearCache();
		    ConditionsSaveHandler.TrySetDirty();
		    GroupChangedEvt.Raise(criteriaEntry.Criteria);
	    }
	    
	    
	    public static void AddToExistingGroup(DataEditorCriteria criteriaEntry, int targetGroupIndex, Object toAdd)
	    {
		    // Add to target
		    criteriaEntry.Condition.Fp("criteriaList").GetIndex(targetGroupIndex).Fpr("criteria").InsertAtEnd();
		    criteriaEntry.Condition.Fp("criteriaList").GetIndex(targetGroupIndex).Fpr("criteria").GetLastIndex().objectReferenceValue = toAdd;
		    
		    // Remove from previous
		    var currentIndex = criteriaEntry.Condition.Fp("criteriaList").GetIndex(criteriaEntry.GroupIndex).Fpr("criteria")
			    .GetIndexOf(criteriaEntry.Criteria.targetObject);
							
		    criteriaEntry.Condition.Fp("criteriaList").GetIndex(criteriaEntry.GroupIndex).Fpr("criteria").DeleteAndRemoveIndex(currentIndex);
							
		    // Update
		    criteriaEntry.Condition.ApplyModifiedProperties();
		    criteriaEntry.Condition.Update();
		    
		    ConditionsSoCache.ClearCache();
		    ConditionsSaveHandler.TrySetDirty();
	    }
	    

	    private static void RemoveCriteria(DataEditorCriteria criteriaEntry)
	    {
		    SerializedObjectHelper.RemoveFromObject((DataAsset)criteriaEntry.Criteria.targetObject,
			    criteriaEntry.Condition.Fp("criteriaList").GetIndex(criteriaEntry.GroupIndex).Fpr("criteria"));

		    criteriaEntry.Condition.ApplyModifiedProperties();
		    criteriaEntry.Condition.Update();

		    ConditionsSoCache.ClearCache();
		    ConditionsSaveHandler.TrySetDirty();
		    RemovedEvt.Raise(criteriaEntry.Criteria);
	    }
    }
}

#endif