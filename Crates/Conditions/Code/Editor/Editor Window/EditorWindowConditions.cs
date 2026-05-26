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

using System;
using System.Linq;
using CarterGames.Cart;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions.Editor
{
	public sealed class EditorWindowConditions : StandardEditorWindow
	{
		private static Vector2 ScrollPos
		{
			get => PerUserSettings.GetValue<Vector2>("crate_conditions_editor", SettingType.SessionState, Vector2.zero);
			set => PerUserSettings.SetValue<Vector2>("crate_conditions_editor", SettingType.SessionState, value);
		}
		
		
		
		[MenuItem("Tools/Carter Games/The Cart/[Conditions] Editor Window", priority = 1200)]
		public static void OpenThis()
		{
			ConditionsIndexHandler.UpdateIndex();
			Open<EditorWindowConditions>("Conditions Editor");
		}
		

		private void OnGUI()
		{
			GUILayout.Space(5f);

			if (!CriteriaValidation.AllCriteriaValid())
			{
				var typesString = "";

				for (var i = 0; i < CriteriaValidation.InvalidTypes.Count; i++)
				{
					typesString += "- ";
					typesString += CriteriaValidation.InvalidTypes[i].ToString();

					if (i == CriteriaValidation.InvalidTypes.Count - 1) continue;
					typesString += "\n";
				}
				
				EditorGUILayout.HelpBox(
					"A criteria class has non-serialized fields that are not attributed with [NonSerialized].\nConditions are disabled until this is corrected:\n" + typesString,
					MessageType.Error);
			}
			
			EditorGUI.BeginDisabledGroup(!CriteriaValidation.AllCriteriaValid());
			
			EditorGUILayout.BeginHorizontal();
			GUI.backgroundColor = Color.green;
			if (GUILayout.Button("+ New Condition", GUILayout.Height(22.5f)))
			{
				AddNewCondition();
				ConditionsSoCache.ClearCache();
			}
			GUI.backgroundColor = Color.white;
			
			if (GUILayout.Button("Update Id's Constant Class", GUILayout.Width(200f), GUILayout.Height(22.5f)))
			{
				if (Dialogue.Display("Condition Id Generation", "Are you sure you want to update the ConditionsIds class.", "Generate", "Cancel"))
				{
					ConditionsSaveHandler.ForceSaveChanges();
					ConditionsConstantGenerator.Generate(AssetDatabaseHelper.GetAllInstancesInProject<Condition>());
				}
			}
			EditorGUILayout.EndHorizontal();

			ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos);
			
			GUILayout.Space(5f);

			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.Space(1.5f);
			
			EditorGUILayout.LabelField("Conditions", EditorStyles.boldLabel);
			GeneralUtilEditor.DrawHorizontalGUILine();

			if (ConditionsSoCache.SoLookup.Count > 0)
			{
				var data = ConditionsSoCache.SoLookup.Where(t => !t.targetObject.IsMissingOrNull()).ToArray();

				if (data.Length <= 0)
				{
					EditorGUILayout.LabelField("No conditions in the project, make one to see it here.");
				}
				else
				{
					EditorGUILayout.Space(2.5f);
					
					foreach (var obj in data)
					{
						ConditionDrawer.DrawCondition(obj);
						EditorGUILayout.Space(5f);
					}
				}
			}
			else
			{
				EditorGUILayout.LabelField("No conditions in the project, make one to see it here.");
			}
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndScrollView();
			
			EditorGUI.EndDisabledGroup();
		}
		
		
		private static void AddNewCondition()
		{
			var list = AutoMakeDataAssetManager.GetDefine<ConditionsIndex>().ObjectRef.Fp("assets").Fpr("list");
			list.InsertIndex(list.arraySize);
			
			var condition = CreateInstance<Condition>();
			condition.name = $"{condition.GetType().Name}_{condition.VariantId}";
			
			var path = AutoMakeDataAssetManager.GetDefine<ConditionsIndex>().CompleteDataAssetPath.Replace(AutoMakeDataAssetManager.GetDefine<ConditionsIndex>().CompleteDataAssetPath.Split('/').Last(), string.Empty) + "Conditions/";

			if (!AssetDatabase.IsValidFolder(path))
			{
				FileEditorUtil.CreateToDirectory(path);
			}
			
			AssetDatabase.CreateAsset(condition, path + condition.name + ".asset");
			
			list.GetIndex(list.arraySize - 1).Fpr("key").stringValue = condition.VariantId;
			list.GetIndex(list.arraySize - 1).Fpr("value").objectReferenceValue = condition;

			var conditionObj = new SerializedObject(condition);
			
			conditionObj.Fp("uid").stringValue = condition.VariantId;
			conditionObj.Fp("id").stringValue = condition.VariantId;

			conditionObj.Fp("criteriaList").ClearArray();
			conditionObj.Fp("criteriaList").InsertAtEnd();
			
			conditionObj.Fp("criteriaList").GetLastIndex().Fpr("groupCheckType").intValue = 1;
			conditionObj.Fp("criteriaList").GetLastIndex().Fpr("groupId").stringValue = Guid.NewGuid().ToString();
			conditionObj.Fp("criteriaList").GetLastIndex().Fpr("groupUuid").stringValue = Guid.NewGuid().ToString();
			
			conditionObj.ApplyModifiedProperties();
			conditionObj.Update();
			
			list.serializedObject.ApplyModifiedProperties();
			list.serializedObject.Update();
			
			AssetDatabase.SaveAssets();
		}
	}
}

#endif