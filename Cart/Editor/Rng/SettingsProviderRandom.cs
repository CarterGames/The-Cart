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
using CarterGames.Cart.Management;
using CarterGames.Cart.Management.Editor;
using CarterGames.Cart.Random;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    /// <summary>
    /// Handles the settings drawing for the random system.
    /// </summary>
    public class SettingsProviderRandom : ISettingsProvider
    {
        private static SerializedObject ObjectRef =>
            AutoMakeDataAssetManager.GetDefine<DataAssetCoreRuntimeSettings>().ObjectRef;
        
        private static DataAssetCoreRuntimeSettings AssetRef =>
            AutoMakeDataAssetManager.GetDefine<DataAssetCoreRuntimeSettings>().AssetRef;
        

        public string MenuName => "Random";
        
        public void OnProjectSettingsGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Random", EditorStyles.boldLabel);
            
            var rngProvider = UtilEditor.SettingsObject.Fp("rngProviderTypeDef").Fpr("type").stringValue;

            EditorGUILayout.BeginHorizontal();
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField(new GUIContent("Current Provider"), ObjectRef.Fp("rngProviderTypeDef").Fpr("type").stringValue.Split('.').Last().Replace("RngProvider", string.Empty));
            EditorGUI.EndDisabledGroup();
            
            if (GUILayout.Button("Select Provider", GUILayout.Width(167.5f)))
            {
                SearchProviderRandom.GetProvider().SelectionMade.Add(OnSelectionMade);
                SearchProviderRandom.GetProvider().Open(AssetRef.RngProvider);
                
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
            }
            
            EditorGUILayout.EndHorizontal();
            

            void OnSelectionMade(SearchTreeEntry entry)
            {
                ObjectRef.Fp("rngProviderTypeDef").Fpr("assembly").stringValue = entry.userData.GetType().Assembly.FullName;
                ObjectRef.Fp("rngProviderTypeDef").Fpr("type").stringValue = entry.userData.GetType().FullName;
				
                ObjectRef.ApplyModifiedProperties();
                ObjectRef.Update();
            }
            
            
            // If set to a provider that doesn't have a seed, return...
            // Currently this is only 0 - (Unity Random)
            if (rngProvider == typeof(UnityRngProvider).FullName)
            {
                EditorGUILayout.Space(1.5f);
                EditorGUILayout.EndVertical();
                return;
            }
            

            EditorGUILayout.BeginHorizontal();
            
        
            var systemSeedProperty = UtilEditor.SettingsObject.Fp("rngSystemSeed");
            var aleaSeedProperty = UtilEditor.SettingsObject.Fp("rngAleaSeed");

            
            // System Seed Field
            if (rngProvider == typeof(SystemRngProvider).FullName)
            {
                EditorGUILayout.PropertyField(systemSeedProperty, new GUIContent("Seed", "Defines the seed to use for random numbers."));
            }
                
            
            // Alea Seed Field
            if (rngProvider == typeof(AleaRngProvider).FullName)
            {
                EditorGUILayout.PropertyField(aleaSeedProperty, new GUIContent("Seed", "Defines the seed to use for random numbers."));
            }


            // Draws the button to copy the seed...
            if (GUILayout.Button("Copy", GUILayout.Width(65)))
            {
                if (rngProvider == typeof(SystemRngProvider).FullName)
                {
                    systemSeedProperty.intValue.ToString().CopyToClipboard();
                }
                if (rngProvider == typeof(AleaRngProvider).FullName)
                {
                    aleaSeedProperty.stringValue.CopyToClipboard();
                }
                    
                Dialogue.Display("Seed Copy", "The seed has been added to your clipboard", "Continue");
            }

            
            // Draws the button the regenerate the seed. 
            if (GUILayout.Button("Regenerate", GUILayout.Width(100)))
            {
                var seededProvider = AssetRef.RngProviderAssemblyClassDef.GetTypeInstance<ISeededRngProvider>();

                Dialogue.Display("Regen Seed", "Are you sure you want to regen the Seed?", "Yes",
                    "Cancel", seededProvider.GenerateSeed);

                ObjectRef.ApplyModifiedProperties();
                ObjectRef.Update();
            }

            
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}