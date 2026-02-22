#if CARTERGAMES_CART_CRATE_LOCALIZATION && UNITY_EDITOR

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

using System;
using System.Linq;
using CarterGames.Cart.Data;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization.Editor
{
	public sealed class SettingsProviderLocalization : ICrateSettingsProvider
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private static readonly string ExpandedId  = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_Crate_Localization_IsExpanded";

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		public string MenuName => "Localization";
		
		private SerializedObject AssetObjectRef => ScriptableRef.GetAssetDef<DataAssetDefinedLanguages>().ObjectRef;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   ISettingsProvider Implementation
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */


		public void OnProjectSettingsGUI()
		{
			EditorGUILayout.BeginVertical();
			EditorGUILayout.Space(1.5f);

			
			EditorGUILayout.BeginHorizontal();
			EditorGUI.BeginChangeCheck();
			
			
			if (string.IsNullOrEmpty(LocalizationManager.CurrentLanguage.DisplayName))
			{
				GUILayout.Space(15f);
				
				GUI.backgroundColor = Color.green;
				if (GUILayout.Button("Select Language"))
				{
					var current = DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages.FirstOrDefault(t =>
						t.DisplayName.Equals(LocalizationManager.CurrentLanguage.DisplayName));

					SearchProviderLanguages.GetProvider().SelectionMade.Add(OnSelectionMade);
					SearchProviderLanguages.GetProvider().Open(current);

					// For editor errors only
					EditorGUILayout.BeginVertical();
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.BeginHorizontal();
				}
				GUI.backgroundColor = Color.white;
			}
			else
			{
				EditorGUILayout.BeginHorizontal();
				
				EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.TextField(new GUIContent("Current language"), $"{LocalizationManager.CurrentLanguage.DisplayName} ({LocalizationManager.CurrentLanguage.Code})");
				EditorGUI.EndDisabledGroup();
				
				GUI.backgroundColor = Color.yellow;
				if (GUILayout.Button("Edit", GUILayout.Width(55f)))
				{
					var current = DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages.FirstOrDefault(t =>
						t.DisplayName.Equals(LocalizationManager.CurrentLanguage.DisplayName));

					SearchProviderLanguages.GetProvider().SelectionMade.Add(OnSelectionMade);
					SearchProviderLanguages.GetProvider().Open(current);
					
					// For editor errors only
					EditorGUILayout.BeginVertical();
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.BeginHorizontal();
				}
				GUI.backgroundColor = Color.white;
				
				EditorGUILayout.EndHorizontal();
			}
			
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.Space(15f);
			
			EditorGUILayout.HelpBox(
				"Add new supported languages below. Just enter the relevant country codes below for them to be selectable and supported by the localization setup.",
				MessageType.Info);
			
			EditorGUILayout.Space(5f);
			
			EditorGUILayout.BeginVertical();
			
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(AssetObjectRef.Fp("languages"));

			if (EditorGUI.EndChangeCheck())
			{
				AssetObjectRef.ApplyModifiedProperties();
				AssetObjectRef.Update();
			}
			
			EditorGUILayout.EndVertical();
			
			EditorGUILayout.Space(15f);
			
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(17.5f);
			if (GUILayout.Button("Manage Fonts (Select asset)"))
			{
				EditorGUIUtility.PingObject(ScriptableRef.GetAssetDef<DataAssetLocalizationFonts>().AssetRef);
			}
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
		}


		private void OnSelectionMade(SearchTreeEntry entry)
		{
			SearchProviderLanguages.GetProvider().SelectionMade.Remove(OnSelectionMade);
			LocalizationManager.SetLanguage(((Language) entry.userData));
		}
	}
}

#endif