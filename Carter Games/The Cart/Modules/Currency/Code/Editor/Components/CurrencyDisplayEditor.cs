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
	[CustomEditor(typeof(CurrencyDisplay), true)]
	public class CurrencyDisplayEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			EditorGUILayout.Space(5f);
			
			GeneralUtilEditor.DrawMonoScriptSection(target as CurrencyDisplay);
			
			EditorGUILayout.Space(5f);

			EditorGUILayout.BeginVertical("HelpBox");
			
			EditorGUILayout.Space(1f);
			
			EditorGUILayout.PropertyField(serializedObject.Fp("accountId"));
			
			// if (string.IsNullOrEmpty(serializedObject.Fp("accountId").stringValue))
			// {
			// 	if (GUILayout.Button("Select Account"))
			// 	{
			// 		SearchProviderAccounts.GetProvider().SelectionMade.Add(OnSelectionMade);
			// 		SearchProviderAccounts.GetProvider().Open(serializedObject.Fp("accountId").stringValue);
			// 	}
			// }
			// else
			// {
			// 	EditorGUILayout.BeginHorizontal();
			// 	
			// 	EditorGUI.BeginDisabledGroup(true);
			// 	EditorGUILayout.PropertyField(serializedObject.Fp("accountId"));
			// 	EditorGUI.EndDisabledGroup();
			//
			// 	if (GUILayout.Button("Edit", GUILayout.Width(55)))
			// 	{
			// 		SearchProviderAccounts.GetProvider().SelectionMade.Add(OnSelectionMade);
			// 		SearchProviderAccounts.GetProvider().Open(serializedObject.Fp("accountId").stringValue);
			// 	}
			// 	
			// 	EditorGUILayout.EndHorizontal();
			// }

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(serializedObject.Fp("label"));

			if (serializedObject.Fp("label").objectReferenceValue == null)
			{
				GUI.backgroundColor = Color.yellow;

				if (GUILayout.Button("Try Get Reference", GUILayout.Width(140)))
				{
					serializedObject.Fp("label").objectReferenceValue ??=
						((CurrencyDisplay) target).GetComponentInChildren<Canvas>();

					serializedObject.ApplyModifiedProperties();
					serializedObject.Update();
				}

				GUI.backgroundColor = Color.white;
			}
			
			EditorGUILayout.EndHorizontal();
			
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