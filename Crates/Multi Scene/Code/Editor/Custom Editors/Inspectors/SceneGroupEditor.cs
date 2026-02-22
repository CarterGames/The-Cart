#if CARTERGAMES_CART_CRATE_MULTISCENE && UNITY_EDITOR

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CarterGames.Cart;
using CarterGames.Cart.Data.Editor;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Logs;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CarterGames.Cart.Crates.MultiScene.Editor
{
    /// <summary>
    /// Custom Inspector for the SceneGroup scriptable object.
    /// </summary>
    [CustomEditor(typeof(SceneGroup), true)]
    public sealed class SceneGroupEditor : InspectorDataAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */      
        
        private SerializedProperty scenes;

        private SceneGroup sceneGroupRef;

        private string[] allGroupOptions;
        private string[] buildSettingsOptions;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        protected override bool ShowVariantIdOption => false;
        protected override bool ShowAssetIndexOptions => false;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */          
        
        private void OnEnable()
        {
            sceneGroupRef = target as SceneGroup;
            
            scenes = serializedObject.Fp("scenes");
            
            buildSettingsOptions = EditorSceneHelper.ScenesInBuildSettings.ToDisplayOptions();
            
            EditorSceneHelper.UpdateCaches();
            EditorSceneHelper.OnCacheUpdate.Add(UpdateSceneNames);
        }

        
        private void OnDisable()
        {
            scenes = null;
            EditorSceneHelper.OnCacheUpdate.Remove(UpdateSceneNames);
        }


        protected override void DrawInspectorGUI()
        {
            base.DrawInspectorGUI();
            
            GUILayout.Space(5f);
            DrawHelpBox();
            GUILayout.Space(5f);
            DrawToolsSection();
            GUILayout.Space(5f);
            DrawScenesSection();
            GUILayout.Space(5f);
            DrawDangerZoneSection();
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Draw Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        
        /// <summary>
        /// Updates the scene names options.
        /// </summary>
        private void UpdateSceneNames()
        {
            buildSettingsOptions = EditorSceneHelper.ScenesInBuildSettings.ToDisplayOptions();
        }

        
        /// <summary>
        /// Draws the script field for this editor.
        /// </summary>
        private void DrawScriptSection()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromScriptableObject(target as SceneGroup), typeof(SceneGroup), false);
            GUI.enabled = true;
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
        

        /// <summary>
        /// Draws a help box with some important info to show.
        /// </summary>
        private void DrawHelpBox()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.HelpBox("Scene groups control a \"scene\" in the multi-scene setup, each group is its own scene collection which can be loaded and unloaded easily.", MessageType.Info);
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the tool buttons section.
        /// </summary>
        private void DrawToolsSection()
        {
            GUILayout.Space(1.5f);
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Tools", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();

            EditorGUI.BeginDisabledGroup(!sceneGroupRef.IsValid);
            
            if (GUILayout.Button("Load Scenes"))
            {
                if (SceneHelper.GetAllActiveScenes().Any(t => t.isDirty))
                {
                    var option = EditorUtility.DisplayDialogComplex("Load Scene Group", "You have unsaved changes in a loaded scene, do you want to save them first?, or continue without saving?",
                        "Save & Continue", "Cancel", "Don't Save & Continue");
                
                    switch (option)
                    {
                        case 0:
                            EditorSceneManager.SaveOpenScenes();
                            LoadSceneGroupInEditor();
                            break;
                        case 2:
                            LoadSceneGroupInEditor();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    LoadSceneGroupInEditor();
                }
            }
            
            EditorGUI.EndDisabledGroup();
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            GUILayout.Space(1.5f);
        }


        /// <summary>
        /// Draws the scene field section.
        /// </summary>
        private void DrawScenesSection()
        {
            GUILayout.Space(1.5f);
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Scenes", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();

            scenes ??= serializedObject.Fp("scenes");
            
            // Shows the base field button if there are no entries in the scene group...
            if (scenes.arraySize <= 0)
            {
                GUI.backgroundColor = Color.green;
                
                if (GUILayout.Button("Add Main Scene"))
                {
                    CallAddBaseField();
                }
                
                GUI.backgroundColor = Color.white;
            }
            else if (scenes.arraySize > 0)
            {
                EditorGUILayout.BeginVertical("Box");
                RenderBaseSceneField();
                EditorGUILayout.EndVertical();
                
                GUILayout.Space(2.5f);
                
                EditorGUILayout.BeginVertical("Box");
                RenderAdditiveSceneFields();
                EditorGUILayout.EndVertical();
            }

            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            GUILayout.Space(1.5f);
        }


        /// <summary>
        /// Draws the danger zone section.
        /// </summary>
        private void DrawDangerZoneSection()
        {
            GUILayout.Space(1.5f);
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Danger Zone", EditorStyles.boldLabel);
            
            GeneralUtilEditor.DrawHorizontalGUILine();
            GUI.backgroundColor = Color.red;

            if (GUILayout.Button("Reset Group"))
            {
                CallResetGroup();
            }

            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            
            GUI.backgroundColor = Color.white;
        }
        

        /// <summary>
        /// Adds the base scene to the editor
        /// </summary>
        private void CallAddBaseField()
        {
            scenes.InsertIndex(0);
        }
        

        /// <summary>
        /// Renders the base scene grouping...
        /// </summary>
        private void RenderBaseSceneField()
        {
            GUI.backgroundColor = Color.green;
            
            // EditorGUILayout.LabelField("Main Scene", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(scenes.GetIndex(0), new GUIContent("Primary scene"));
            
            GUI.backgroundColor = Color.white;
        }


        /// <summary>
        /// Renders the additive scenes into a grouping...
        /// </summary>
        private void RenderAdditiveSceneFields()
        {
            GUI.backgroundColor = Color.yellow;
            
            EditorGUILayout.LabelField("Additive Scene(s)", EditorStyles.boldLabel);

            if (scenes.arraySize <= 1)
            {
                if (sceneGroupRef.IsValid)
                {
                    GUI.backgroundColor = Color.yellow;

                    if (GUILayout.Button("Add Additive Scene"))
                    {
                        CallAddNewAdditiveScene();
                    }

                    GUI.backgroundColor = Color.white;
                }
            }
            else
            {
                for (var i = 1; i < scenes.arraySize; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    GUI.backgroundColor = Color.yellow;

                    EditorGUILayout.PropertyField(scenes.GetIndex(i), new GUIContent($"Additive scene {i}"));
                    
                    GUI.backgroundColor = Color.green;

                    if (GUILayout.Button("+", GUILayout.Width(" + ".GUIWidth())))
                    {
                        CallAddNewAdditiveScene(scenes);
                    }

                    GUI.backgroundColor = Color.red;

                    if (GUILayout.Button("-", GUILayout.Width(" - ".GUIWidth())))
                    {
                        CallRemoveElementAtIndex(scenes, i);
                    }

                    GUI.backgroundColor = Color.white;

                    EditorGUILayout.EndHorizontal();
                }
            }

            GUI.backgroundColor = Color.white;
        }


        /// <summary>
        /// Removed the element at the index entered...
        /// </summary>
        /// <param name="i">The element to edit</param>
        private void CallRemoveElementAtIndex(SerializedProperty prop, int i)
        {
            prop.DeleteIndex(i);
        }

        
        /// <summary>
        /// Adds a new element to the scenes list that is blank at the element entered.
        /// </summary>
        private void CallAddNewAdditiveScene(SerializedProperty prop)
        {
            scenes.InsertAtEnd();
            scenes.GetLastIndex().Fpr("sceneAssetRef").objectReferenceValue = null;
            scenes.GetLastIndex().Fpr("isDirty").boolValue = false;
            scenes.GetLastIndex().Fpr("scenePath").stringValue = string.Empty;
        }
        
        
        /// <summary>
        /// Adds a new element to the scenes list that is blank.
        /// </summary>
        private void CallAddNewAdditiveScene()
        {
            scenes.InsertAtEnd();
            scenes.GetLastIndex().Fpr("sceneAssetRef").objectReferenceValue = null;
            scenes.GetLastIndex().Fpr("isDirty").boolValue = false;
            scenes.GetLastIndex().Fpr("scenePath").stringValue = string.Empty;
            scenes.serializedObject.ApplyModifiedProperties();
            scenes.serializedObject.Update();
        }

        
        /// <summary>
        /// Resets the scenes list to a new list.
        /// </summary>
        private void CallResetGroup()
        {
            if (!EditorUtility.DisplayDialog("Clear Scene Group",
                    "Are you sure that you want to clear this scene group? This action cannot be undone once performed.",
                    "Yes", "No")) return;
            
            typeof(SceneGroup).GetMethod("ClearAsset", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(target, null);
            serializedObject.Update();
        }
        
        
        /// <summary>
        /// Loads the scene group in the editor on call.
        /// </summary>
        private void LoadSceneGroupInEditor()
        {
            var sceneList = new List<string>();

            for (var i = 0; i < scenes.arraySize; i++)
            {
                var path = scenes.GetIndex(i).Fpr("scenePath").stringValue;
                
                if (path.Length <= 0)
                {
                    CartLogger.LogError<MultiSceneLogs>(
                        "Unable to load group in editor as a scene doesn't have a valid path to load from...");
                    return;
                }
                
                sceneList.Add(scenes.GetIndex(i).Fpr("scenePath").stringValue);
            }
            
            if (sceneList.Count <= 0) return;

            for (var i = 0; i < sceneList.Count; i++)
            {
                var _scene = sceneList[i];

                if (i.Equals(0))
                {
                    EditorSceneManager.OpenScene(_scene, OpenSceneMode.Single);
                }
                else
                {
                    EditorSceneManager.OpenScene(_scene, OpenSceneMode.Additive);
                }
            }
            
            ScriptableRef.GetAssetDef<MultiSceneSettings>().AssetRef.LastGroup = target as SceneGroup;
            MultiSceneEditorEvents.SceneGroups.OnSceneGroupLoadedInEditor.Raise();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Utility Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */    
        
        private string[] UnusedSceneOptions(string currentlySelected)
        {
            var list = new List<string>();

            foreach (var sceneName in buildSettingsOptions)
            {
                if (currentlySelected != null)
                {
                    if (sceneName == currentlySelected)
                    {
                        list.Add(sceneName);
                        continue;
                    }
                }
                
                for (var i = 0; i < scenes.arraySize; i++)
                {
                    if (scenes.GetIndex(i).Fpr("sceneName").stringValue == sceneName) goto SkipAdd;
                }
                
                list.Add(sceneName);
                SkipAdd: ;
            }

            return list.ToArray();
        }
    }
}

#endif