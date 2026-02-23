#if CARTERGAMES_CART_CRATE_LOCALIZATION && UNITY_EDITOR

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

using System.Linq;
using CarterGames.Cart.Data;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization.Editor
{
	public sealed class SettingsProviderLocalization : ISettingsProvider
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		public string MenuName => "Localization";
		
		private SerializedObject AssetObjectRef => AutoMakeDataAssetManager.GetDefine<DataAssetDefinedLanguages>().ObjectRef;

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
				EditorGUIUtility.PingObject(AutoMakeDataAssetManager.GetDefine<DataAssetLocalizationFonts>().AssetRef);
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