/*
 * Copyright (c) 2018-Present Carter Games
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

using Scarlet.General.Reflection;
using Scarlet.Random;
using UnityEditor;
using UnityEngine;

namespace Scarlet.Editor
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
            UtilEditor.SettingsObject.FindProperty("isRngExpanded").boolValue =
                EditorGUILayout.Foldout(UtilEditor.SettingsObject.FindProperty("isRngExpanded").boolValue, "Rng");

            
            if (!UtilEditor.SettingsObject.FindProperty("isRngExpanded").boolValue) return;


            EditorGUILayout.BeginVertical();
            EditorGUI.indentLevel++;
            
            
            // Draw the provider enum field on the GUI...
            EditorGUILayout.PropertyField(UtilEditor.SettingsObject.FindProperty("rngRngProvider"), new GUIContent("Rng Provider"));

            
            var rngProvider = UtilEditor.SettingsObject.FindProperty("rngRngProvider");
            
            
            // If set to a provider that doesn't have a seed, return...
            // Currently this is only 0 - (Unity Random)
            if (rngProvider.intValue <= 0)
            {
                EditorGUI.indentLevel--;
                EditorGUILayout.EndVertical();
                return;
            }
            

            EditorGUILayout.BeginHorizontal();
            
        
            var systemSeedProperty = UtilEditor.SettingsObject.FindProperty("rngSystemSeed");
            var aleaSeedProperty = UtilEditor.SettingsObject.FindProperty("rngAleaSeed");

            
            // System Seed Field
            if (rngProvider.intValue == 1)
            {
                EditorGUILayout.PropertyField(systemSeedProperty, new GUIContent("System Seed"));
            }
                
            
            // Alea Seed Field
            if (rngProvider.intValue == 2)
            {
                EditorGUILayout.PropertyField(aleaSeedProperty, new GUIContent("Alea Seed"));
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
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the inspector version of the settings.
        /// </summary>
        /// <param name="serializedObject">The target object</param>
        public static void DrawInspector(SerializedObject serializedObject)
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Random", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isRngExpanded"));
            EditorGUI.EndDisabledGroup();
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rngRngProvider"), new GUIContent("Rng Provider"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rngSystemSeed"), new GUIContent("System Seed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rngAleaSeed"), new GUIContent("Alea Seed"));
            
            EditorGUILayout.EndVertical();
        }
    }
}