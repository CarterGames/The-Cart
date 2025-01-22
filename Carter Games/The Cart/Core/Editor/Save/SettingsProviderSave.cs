using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Core.Save;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
{
	public class SettingsProviderSave : ISettingsProvider
	{
		private static readonly string FoldoutKey = $"{PerUserSettings.UniqueId}_Save_Foldout";


		private static bool IsExpanded
		{
			get => PerUserSettings.GetValue<bool>(FoldoutKey, SettingType.EditorPref);
			set => PerUserSettings.SetValue<bool>(FoldoutKey, SettingType.EditorPref, value);
		}


		private static SerializedObject ObjectRef =>
			ScriptableRef.GetAssetDef<DataAssetCoreRuntimeSettings>().ObjectRef;


		public void OnInspectorSettingsGUI()
		{
			EditorGUILayout.BeginVertical("HelpBox");
            
			EditorGUILayout.LabelField("Internal Save", EditorStyles.boldLabel);
			GeneralUtilEditor.DrawHorizontalGUILine();
            
			EditorGUI.BeginDisabledGroup(true);
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(ObjectRef.Fp("saveMethodTypeDef"), new GUIContent("ISaveMethod"));
			EditorGUI.indentLevel--;
			EditorGUI.EndDisabledGroup();
            
			EditorGUILayout.EndVertical();
		}

		
		public void OnProjectSettingsGUI()
		{
			IsExpanded = EditorGUILayout.Foldout(IsExpanded, new GUIContent("Internal Save"));
			
			if (!IsExpanded) return;

			EditorGUILayout.BeginVertical("Box");
			EditorGUI.indentLevel++;
			
			if (AssemblyHelper.CountClassesOfType<ISaveMethod>() > 1)
			{
				if (GUILayout.Button("Select Save Method"))
				{
					if (!string.IsNullOrEmpty(ObjectRef.Fp("saveMethodTypeDef").Fpr("assembly").stringValue))
					{
						var assemblyString = ObjectRef.Fp("saveMethodTypeDef").Fpr("assembly").stringValue;
						var typeString = ObjectRef.Fp("saveMethodTypeDef").Fpr("type").stringValue;

						SearchProviderSaveMethod.GetProvider().SelectionMade.Add(HandleSelection);
						SearchProviderSaveMethod.GetProvider().Open(ScriptableRef.GetAssetDef<DataAssetCoreRuntimeSettings>().AssetRef.SaveMethodType);
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
			
			EditorGUI.indentLevel--;
			EditorGUILayout.EndVertical();


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