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

using System.Linq;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Conditions.Editor
{
	public sealed class EditorWindowConditions : StandardEditorWindow
	{
		private static Vector2 ScrollPos
		{
			get => PerUserSettings.GetValue<Vector2>("module_conditions_editor", SettingType.SessionState, Vector2.zero);
			set => PerUserSettings.SetValue<Vector2>("module_conditions_editor", SettingType.SessionState, value);
		}
		
		
		
		[MenuItem("Tools/Carter Games/The Cart/Modules/Conditions/Editor", priority = 1200)]
		private static void OpenThis()
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
				ConditionEditorHelper.AddNewCondition();
				ConditionsSoCache.ClearCache();
			}
			GUI.backgroundColor = Color.white;
			
			if (GUILayout.Button("Update Id's Constant Class", GUILayout.Width(200f), GUILayout.Height(22.5f)))
			{
				if (Dialogue.Display("Condition Id Generation", "Are you sure you want to update the ConditionsIds class.", "Generate", "Cancel"))
				{
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
					foreach (var obj in data)
					{
						ConditionEditor.DrawCondition(obj, this);
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
	}
}

#endif