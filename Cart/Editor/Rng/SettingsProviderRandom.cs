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
            ScriptableRef.GetAssetDef<DataAssetCoreRuntimeSettings>().ObjectRef;
        
        private static DataAssetCoreRuntimeSettings AssetRef =>
            ScriptableRef.GetAssetDef<DataAssetCoreRuntimeSettings>().AssetRef;
        

        
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
                var seededProvider = AssetRef.RngProviderAssemblyClassDef.GetDefinedType<ISeededRngProvider>();

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