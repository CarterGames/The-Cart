#if CARTERGAMES_CART_CRATE_MULTISCENE && UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart;
using CarterGames.Cart.Data;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarterGames.Cart.Crates.MultiScene.Editor
{
    public class SceneLoaderToolbar : IToolbarElementRight
    {
        private static GenericMenu scenesMenu;
        
        
        private static void OnEditorDelayCall()
        {
            EditorApplication.delayCall -= OnEditorDelayCall;
            UpdateSceneOptions();
        }

        
        private static void UpdateSceneOptions()
        {
            scenesMenu = new GenericMenu();

            var foundSceneFiles = DataAccess.GetAssets<SceneGroup>();

            if (foundSceneFiles == null) return;
            if (!foundSceneFiles.Any()) return;

            foreach (var sceneEntryPath in foundSceneFiles)
            {
                scenesMenu.AddItem(new GUIContent(sceneEntryPath.name), false, OnSceneGroupSelected, sceneEntryPath);
            }
        }
        

        private static void OnMouseDown()
        {
            scenesMenu.ShowAsContext();
        }


        private static void OnSceneGroupSelected(object sceneGroup)
        {
            if (SceneHelper.GetAllActiveScenes().Any(t => t.isDirty))
            {
                var option = EditorUtility.DisplayDialogComplex("Load Scene Group", "You have unsaved changes in a loaded scene, do you want to save them first?, or continue without saving?",
                    "Save & Continue", "Cancel", "Don't Save & Continue");
                
                switch (option)
                {
                    case 0:
                        EditorSceneManager.SaveOpenScenes();
                        LoadSceneGroupInEditor(((SceneGroup)sceneGroup));
                        break;
                    case 2:
                        LoadSceneGroupInEditor(((SceneGroup)sceneGroup));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                LoadSceneGroupInEditor(((SceneGroup)sceneGroup));
            }
        }
        
        
        /// <summary>
        /// Loads a scene group in the editor when called.
        /// </summary>
        /// <param name="group">The group to load.</param>
        private static void LoadSceneGroupInEditor(SceneGroup group)
        {
            var sceneList = new List<string>();

            for (var i = 0; i < group.scenes.Count; i++)
                sceneList.Add(group.scenes[i].SceneName);

            var paths = GetScenePaths();
            
            if (sceneList.Count <= 0) return;

            for (var i = 0; i < sceneList.Count; i++)
            {
                var scene = sceneList[i];
                var path = paths.FirstOrDefault(t => t.Contains(scene));

                if (i.Equals(0))
                {
                    EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
                }
                else
                {
                    EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
                }
            }

            ScriptableRef.GetAssetDef<MultiSceneSettings>().AssetRef.LastGroup = group;
            MultiSceneEditorEvents.SceneGroups.OnSceneGroupLoadedInEditor.Raise();
        }
        
        
        /// <summary>
        /// Gets the paths for all scenes in the project build settings for use.
        /// </summary>
        /// <returns>A list of the paths of the scenes in the build settings.</returns>
        private static List<string> GetScenePaths()
        {
            var sceneNumber = SceneManager.sceneCountInBuildSettings;
            var arrayOfNames = new string[sceneNumber];
            
            for (var i = 0; i < sceneNumber; i++)
            {
                arrayOfNames[i] = SceneUtility.GetScenePathByBuildIndex(i);
            }

            return arrayOfNames.ToList();
        }
        
        
        public int RightOrder { get; }
        

        public void Initialize()
        {
            EditorApplication.delayCall -= OnEditorDelayCall;
            EditorApplication.delayCall += OnEditorDelayCall;
            
            SettingsProviderMultiScene.ToolbarSettingChangedEvt.Remove(OnRightGUI);
            SettingsProviderMultiScene.ToolbarSettingChangedEvt.Add(OnRightGUI);
        }

        
        public void OnRightGUI()
        {
            if (!ScriptableRef.GetAssetDef<MultiSceneSettings>().ObjectRef.Fp("showToolbar").boolValue) return;
            if (DataAccess.GetAssets<SceneGroup>() == null) return;
            
            EditorGUI.BeginDisabledGroup(EditorApplication.isCompiling || EditorApplication.isPlaying);
            
            var label = "Group Loader";
            
            GUILayout.Space(2.5f);
            
            GUI.backgroundColor = Color.magenta;
            
            if (EditorGUILayout.DropdownButton(new GUIContent(label, EditorArtHandler.GetIcon("crate_multiscene_icon")), FocusType.Passive, GUILayout.Width(label.GUIWidth() + 25f)))
            {
                OnMouseDown();
            }
            
            EditorGUI.EndDisabledGroup();
            
            GUILayout.Space(2.5f);
            GUI.backgroundColor = Color.white;
        }
    }
}

#endif