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
			ScriptableRef.GetAssetDef<DataAssetCoreRuntimeSettings>().ObjectRef;

		
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