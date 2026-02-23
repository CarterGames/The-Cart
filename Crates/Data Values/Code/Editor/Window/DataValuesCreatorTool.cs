#if CARTERGAMES_CART_CRATE_DATAVALUES && UNITY_EDITOR

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
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Crates.DataValues.Editor.Window
{
	/// <summary>
	/// Handles the creator tool for data values.
	/// </summary>
	public sealed class DataValuesCreatorTool : EditorWindow
	{
		private const string KeyId = "dataValueCreator_newKey";
		private const string TypeId = "dataValueCreator_newType";

		private const string LocationId = "dataValueCreator_newLocation";
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private DataValuesSearchProvider search;
		private SearchTreeEntry selectedEntry;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private static string Key
		{
			get => (string) PerUserSettings.GetOrCreateValue<string>(KeyId, SettingType.SessionState);
			set => PerUserSettings.SetValue<string>(KeyId, SettingType.SessionState, value);
		}


		private static string TypeName
		{
			get => (string) PerUserSettings.GetOrCreateValue<string>(TypeId, SettingType.SessionState);
			set => PerUserSettings.SetValue<string>(TypeId, SettingType.SessionState, value);
		}


		private static string Location
		{
			get => (string) PerUserSettings.GetOrCreateValue<string>(LocationId, SettingType.SessionState);
			set => PerUserSettings.SetValue<string>(LocationId, SettingType.SessionState, value);
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private void OnGUI()
		{
			EditorGUILayout.Space(5f);
			
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);
			
			EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
			
			GeneralUtilEditor.DrawHorizontalGUILine();
			
			DrawKeyField();
			DrawTypeSelect();
			DrawSelectLocation();
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();

			EditorGUILayout.Space(5f);
			
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);

			DrawCreateSection();
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Menu Items
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Makes a menu item for the creator to open.
		/// </summary>
		[MenuItem("Tools/Carter Games/The Cart/[Data Values] Data Value Asset Creator", priority = 1300)]
		private static void ShowWindow()
		{
			var window = GetWindow<DataValuesCreatorTool>(true);
			window.titleContent = new GUIContent("Data Value Creator");
			window.Show();
		}


		private void DrawKeyField()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUI.BeginChangeCheck();

			EditorGUILayout.LabelField("Key:", GUILayout.Width(45f));
			
			var value = EditorGUILayout.TextField((string) PerUserSettings.GetOrCreateValue<string>(KeyId, SettingType.SessionState));
			
			if (EditorGUI.EndChangeCheck())
			{
				PerUserSettings.SetValue<string>(KeyId, SettingType.SessionState, value);
			}
			
			EditorGUILayout.EndHorizontal();
		}


		private void DrawTypeSelect()
		{
			if (search == null)
			{
				search = CreateInstance<DataValuesSearchProvider>();
			}

			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.LabelField("Type:", GUILayout.Width(45f));
			
			if (selectedEntry != null)
			{
				if (selectedEntry.userData != null)
				{
					EditorGUILayout.LabelField(((Type) selectedEntry.userData).Name);

					if (GUILayout.Button("Change Type", GUILayout.Width(100)))
					{
						DataValuesSearchProvider.ToExclude.Clear();

						DataValuesSearchProvider.OnSearchTreeSelectionMade.Clear();

						if (selectedEntry != null)
						{
							DataValuesSearchProvider.OnSearchTreeSelectionMade.Remove(OnSelectionMade);
							DataValuesSearchProvider.OnSearchTreeSelectionMade.Add(OnSelectionMade);
						}

						SearchWindow.Open(
							new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), search);
					}
				}
				else
				{
					if (GUILayout.Button("Select Type"))
					{
						DataValuesSearchProvider.ToExclude.Clear();
                            
						DataValuesSearchProvider.OnSearchTreeSelectionMade.Clear();
						
						DataValuesSearchProvider.OnSearchTreeSelectionMade.Remove(OnSelectionMade);
						DataValuesSearchProvider.OnSearchTreeSelectionMade.Add(OnSelectionMade);
				
						SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), search);
					}
				}
			}
			else
			{
				if (GUILayout.Button("Select Type"))
				{
					DataValuesSearchProvider.ToExclude.Clear();
                            
					DataValuesSearchProvider.OnSearchTreeSelectionMade.Clear();
					
					DataValuesSearchProvider.OnSearchTreeSelectionMade.Remove(OnSelectionMade);
					DataValuesSearchProvider.OnSearchTreeSelectionMade.Add(OnSelectionMade);
				
					SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), search);
				}
			}
			
			EditorGUILayout.EndHorizontal();
			

		}


		private void DrawSelectLocation()
		{
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.LabelField("Path:", GUILayout.Width(45f));
			
			
			if (!string.IsNullOrEmpty((string) PerUserSettings.GetOrCreateValue<string>(LocationId, SettingType.SessionState)))
			{
				EditorGUILayout.BeginHorizontal();
				
				EditorGUILayout.TextField(
					(string) PerUserSettings.GetOrCreateValue<string>(LocationId, SettingType.SessionState));
				
				if (GUILayout.Button("Change Path", GUILayout.Width(100)))
				{
					PerUserSettings.SetValue<string>(LocationId, SettingType.SessionState,
						EditorUtility.SaveFilePanelInProject("Save New Data Value Asset", $"New Data Value Asset",
							"asset", ""));
				}
				
				EditorGUILayout.EndHorizontal();
			}
			else
			{
				if (GUILayout.Button("Select Path"))
				{
					PerUserSettings.SetValue<string>(LocationId, SettingType.SessionState,
						EditorUtility.SaveFilePanelInProject("Save New Data Value Asset", $"New Data Value Asset",
							"asset", ""));
				}
			}
			
			EditorGUILayout.EndHorizontal();
		}


		private void DrawCreateSection()
		{
			EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(Key) || 
			                             string.IsNullOrEmpty(TypeName) || 
			                             string.IsNullOrEmpty(Location));
			
			if (GUILayout.Button("Create"))
			{
				CreateAsset(Key, Type.GetType(TypeName));

				Key = string.Empty;
				TypeName = string.Empty;
				Location = string.Empty;
			}
			
			EditorGUI.EndDisabledGroup();
		}


		private void OnSelectionMade(SearchTreeEntry entry)
		{
			selectedEntry = entry;
			TypeName = ((Type)entry.userData).FullName;
		}


		private void CreateAsset(string key, Type type)
		{
			AssetDatabase.CreateAsset(CreateInstance(type), Location);
			
			var instance = (DataValueAsset) AssetDatabase.LoadAssetAtPath(Location, type);
			var instanceSo = new SerializedObject(instance);
			
			instanceSo.Fp("key").stringValue = key;

			instanceSo.ApplyModifiedProperties();
			instanceSo.Update();
			
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
}

#endif