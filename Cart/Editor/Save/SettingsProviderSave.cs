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

using CarterGames.Cart.Management;
using CarterGames.Cart.Management.Editor;
using CarterGames.Cart.Save;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
	public class SettingsProviderSave : ISettingsProvider
	{
		private static SerializedObject ObjectRef =>
			AutoMakeDataAssetManager.GetDefine<DataAssetCoreRuntimeSettings>().ObjectRef;


		public string MenuName => "Save";

		public void OnProjectSettingsGUI()
		{
			EditorGUILayout.BeginVertical();
			EditorGUILayout.Space(1.5f);
			
			EditorGUILayout.LabelField("Save", EditorStyles.boldLabel);
			
			if (AssemblyHelper.CountClassesOfType<ISaveMethod>() > 1)
			{
				if (GUILayout.Button("Select Save Method"))
				{
					if (!string.IsNullOrEmpty(ObjectRef.Fp("saveMethodTypeDef").Fpr("assembly").stringValue))
					{
						SearchProviderSaveMethod.GetProvider().SelectionMade.Add(HandleSelection);
						SearchProviderSaveMethod.GetProvider().Open(AutoMakeDataAssetManager.GetDefine<DataAssetCoreRuntimeSettings>().AssetRef.SaveMethodType);
					}
					else
					{
						SearchProviderSaveMethod.GetProvider().SelectionMade.Add(HandleSelection);
						SearchProviderSaveMethod.GetProvider().Open();
					}
				}
			}
			else
			{
				EditorGUILayout.HelpBox("Only 1 implementation of ISaveMethod found. You cannot change from the default until there is another to use.", MessageType.Info);
			}
			
			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.PropertyField(ObjectRef.Fp("saveMethodTypeDef").Fpr("assembly"), new GUIContent("Save Method Assembly"));
			EditorGUILayout.PropertyField(ObjectRef.Fp("saveMethodTypeDef").Fpr("type"), new GUIContent("Save Method Type"));
			EditorGUI.EndDisabledGroup();
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();

			return;
			void HandleSelection(SearchTreeEntry entry)
			{
				ObjectRef.Fp("saveMethodTypeDef").Fpr("assembly").stringValue = entry.userData.GetType().Assembly.FullName;
				ObjectRef.Fp("saveMethodTypeDef").Fpr("type").stringValue = entry.userData.GetType().FullName;
				
				ObjectRef.ApplyModifiedProperties();
				ObjectRef.Update();
			}
		}
	}
}