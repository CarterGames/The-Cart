#if CARTERGAMES_CART_MODULE_CURRENCY && UNITY_EDITOR

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using CarterGames.Cart.Core.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Modules.Currency.Editor
{
	[CustomEditor(typeof(CurrencyDependantButton), true)]
	public class CurrencyDependantButtonEditor : UnityEditor.Editor
	{
		private static readonly string[] ToExcludeFromDraw = new string[10]
		{
			"m_Script", "accountId", "setAmount", "amount", "transactionType", "button", "buttonGraphic", "buttonLabel",
			"affordableLabelColor", "unaffordableLabelColor"
		};


		public override void OnInspectorGUI()
		{
			EditorGUILayout.Space(5f);
			
			GeneralUtilEditor.DrawMonoScriptSection(target as CurrencyDependantButton);
			
			EditorGUILayout.Space(5f);

			EditorGUI.BeginChangeCheck();
			
			DrawAccountOptions();
			EditorGUILayout.Space(5f);
			DrawDisplayOptions();
			EditorGUILayout.Space(5f);
			DrawRemainingOptions();
			
			if (EditorGUI.EndChangeCheck())
			{
				serializedObject.ApplyModifiedProperties();
				serializedObject.Update();
			}
		}


		private void DrawAccountOptions()
		{
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1f);
			
			if (string.IsNullOrEmpty(serializedObject.Fp("accountId").stringValue))
			{
				if (GUILayout.Button("Select Account"))
				{
					SearchProviderAccounts.GetProvider().SelectionMade.Add(OnSelectionMade);
					SearchProviderAccounts.GetProvider().Open(serializedObject.Fp("accountId").stringValue);
				}
			}
			else
			{
				EditorGUILayout.BeginHorizontal();
				
				EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.PropertyField(serializedObject.Fp("accountId"));
				EditorGUI.EndDisabledGroup();

				if (GUILayout.Button("Edit", GUILayout.Width(55)))
				{
					SearchProviderAccounts.GetProvider().SelectionMade.Add(OnSelectionMade);
					SearchProviderAccounts.GetProvider().Open(serializedObject.Fp("accountId").stringValue);
				}
				
				EditorGUILayout.EndHorizontal();
			}
			
			EditorGUILayout.PropertyField(serializedObject.Fp("setAmount"), new GUIContent("Pre-defined Amount"));

			if (serializedObject.Fp("setAmount").boolValue)
			{
				EditorGUILayout.PropertyField(serializedObject.Fp("amount"));
				EditorGUILayout.PropertyField(serializedObject.Fp("transactionType"));
			}
			
			EditorGUILayout.Space(1f);
			EditorGUILayout.EndVertical();
		}


		private void DrawDisplayOptions()
		{
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1f);
			
			EditorGUILayout.PropertyField(serializedObject.Fp("button"));
			EditorGUILayout.PropertyField(serializedObject.Fp("buttonGraphic"));
			
			EditorGUILayout.Space(2.5f);
			
			EditorGUILayout.PropertyField(serializedObject.Fp("buttonLabel"));
			EditorGUILayout.PropertyField(serializedObject.Fp("affordableLabelColor"));
			EditorGUILayout.PropertyField(serializedObject.Fp("unaffordableLabelColor"));
			
			EditorGUILayout.Space(1f);
			EditorGUILayout.EndVertical();
		}


		private void DrawRemainingOptions()
		{
			if (serializedObject.TotalProperties().Equals(ToExcludeFromDraw.Length)) return;
			
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1f);
			
			DrawPropertiesExcluding(serializedObject, ToExcludeFromDraw);
			
			EditorGUILayout.Space(1f);
			EditorGUILayout.EndVertical();
		}


		private void OnSelectionMade(SearchTreeEntry entry)
		{
			SearchProviderAccounts.GetProvider().SelectionMade.Remove(OnSelectionMade);
				
			serializedObject.Fp("accountId").stringValue = (string) entry.userData;
			serializedObject.ApplyModifiedProperties();
			serializedObject.Update();
		}
	}
}

#endif