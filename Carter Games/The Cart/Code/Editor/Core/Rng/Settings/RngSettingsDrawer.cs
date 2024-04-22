/*
 * Copyright (c) 2024 Carter Games
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

using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Core.MetaData.Editor;
using CarterGames.Cart.Core.Reflection;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Random.Editor
{
    /// <summary>
    /// Handles the settings drawing for the random system.
    /// </summary>
    public static class RngSettingsDrawer
    {
        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public static void DrawSettings()
        {
            PerUserSettings.RuntimeSettingsRngExpanded = EditorGUILayout.Foldout(PerUserSettings.RuntimeSettingsRngExpanded, Meta.Rng.Content(Meta.SectionTitle));

            
            if (!PerUserSettings.RuntimeSettingsRngExpanded) return;


            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(1.5f);
            EditorGUI.indentLevel++;
            
            
            // Draw the provider enum field on the GUI...
            EditorGUILayout.PropertyField(UtilEditor.SettingsObject.Fp("rngRngProvider"), Meta.Rng.Content("provider"));

            
            var rngProvider = UtilEditor.SettingsObject.Fp("rngRngProvider");
            
            
            // If set to a provider that doesn't have a seed, return...
            // Currently this is only 0 - (Unity Random)
            if (rngProvider.intValue <= 0)
            {
                EditorGUI.indentLevel--;
                EditorGUILayout.Space(1.5f);
                EditorGUILayout.EndVertical();
                return;
            }
            

            EditorGUILayout.BeginHorizontal();
            
        
            var systemSeedProperty = UtilEditor.SettingsObject.Fp("rngSystemSeed");
            var aleaSeedProperty = UtilEditor.SettingsObject.Fp("rngAleaSeed");

            
            // System Seed Field
            if (rngProvider.intValue == 1)
            {
                EditorGUILayout.PropertyField(systemSeedProperty, Meta.Rng.Content("systemSeed"));
            }
                
            
            // Alea Seed Field
            if (rngProvider.intValue == 2)
            {
                EditorGUILayout.PropertyField(aleaSeedProperty, Meta.Rng.Content("aleaSeed"));
            }


            // Draws the button to copy the seed...
            if (GUILayout.Button("Copy", GUILayout.Width(65)))
            {
                if (rngProvider.intValue == 1)
                {
                    systemSeedProperty.intValue.ToString().CopyToClipboard();
                }
                if (rngProvider.intValue == 2)
                {
                    aleaSeedProperty.stringValue.CopyToClipboard();
                }
                    
                Dialogue.Display("Seed Copy", "The seed has been added to your clipboard", "Continue");
            }

            
            // Draws the button the regenerate the seed. 
            if (GUILayout.Button("Regenerate", GUILayout.Width(100)))
            {
                ReflectionHelper.GetField(typeof(RngSettingsDrawer), "providerCache", false, true).SetValue(null, null);
                var seededProvider = (ISeededRngProvider) ReflectionHelper.GetProperty(typeof(RngSettingsDrawer), "Provider", false, true).GetValue(null);

                Dialogue.Display("Regen Seed", "Are you sure you want to regen the Seed?", "Yes",
                    "Cancel", seededProvider.GenerateSeed);
            }

            
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the inspector version of the settings.
        /// </summary>
        /// <param name="serializedObject">The target object</param>
        public static void DrawInspector(SerializedObject serializedObject)
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField(Meta.Rng.Labels[Meta.SectionTitle], EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("rngRngProvider"), Meta.Rng.Content("provider"));
            EditorGUILayout.PropertyField(serializedObject.Fp("rngSystemSeed"), Meta.Rng.Content("systemSeed"));
            EditorGUILayout.PropertyField(serializedObject.Fp("rngAleaSeed"), Meta.Rng.Content("aleaSeed"));
            
            EditorGUILayout.EndVertical();
        }
    }
}