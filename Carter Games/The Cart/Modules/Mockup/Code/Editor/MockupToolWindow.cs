#if CARTERGAMES_CART_MODULE_MOCKUPS && UNITY_EDITOR

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

using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CarterGames.Cart.Modules.Mockup.Editor
{
    public class MockupToolWindow : EditorWindow
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private const string ObjIdKey = "RootObj_InstanceId";

        private const string RootObjName = "~[Mockup Tool] - Root Object";
        private const string ImageObjName = "~[Mockup] - Visual";

        private const string GalleryIconName = "T_Editor_GalleryIcon_Large";
        private const string EditIconName = "T_Editor_EditIcon_Large";

        private const string WindowTitle = "Mockup Tool";

        private static string rootObjectId;
        private static GameObject rootObj;
        private static Canvas mockupCanvas;
        private static CanvasScaler mockupCanvasScaler;
        private static CanvasGroup mockupCanvasGroup;
        private static Image mockupImage;

        private static Texture2D galleryIconCache;
        private static Texture2D editIconCache;
        private static readonly Vector2 WindowSize = new Vector2(400, 135);

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets the gallery icon for the tool.
        /// </summary>
        private static Texture2D GalleryIcon => FileEditorUtil.GetOrAssignCache(ref galleryIconCache, GalleryIconName);


        /// <summary>
        /// Gets the edit icon for the tool.
        /// </summary>
        private static Texture2D EditIcon => FileEditorUtil.GetOrAssignCache(ref editIconCache, EditIconName);


        private void OnGUI()
        {
            GetReferences();

            GUILayout.Space(5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Mockup Image", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            mockupImage.sprite = (Sprite) EditorGUILayout.ObjectField(mockupImage.sprite, typeof(Sprite), false);
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            
            GUILayout.Space(2.5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            mockupCanvasGroup.alpha = EditorGUILayout.Slider(new GUIContent("Alpha:"), mockupCanvasGroup.alpha,0,1);

            if (mockupImage.sprite != null)
            {
                if (mockupCanvasScaler.referenceResolution != mockupImage.sprite.rect.size)
                {
                    mockupCanvasScaler.referenceResolution = mockupImage.sprite.rect.size;
                    mockupImage.rectTransform.sizeDelta = mockupImage.sprite.rect.size;
                }
            }
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;

            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            
            GetReferences();
        }


        [MenuItem("Tools/Carter Games/The Cart/Modules/Mockup/Toggle Tool", priority = 1400)]
        private static void ToggleTool()
        {
            PerUserSettings.MockupEnabled = !PerUserSettings.MockupEnabled;

            if (!PerUserSettings.MockupEnabled)
            {
                DestroyImmediate(rootObj);
                rootObjectId = string.Empty;
            }
            
            SceneView.RepaintAll();
        }


        private static void OpenOrCreateWindow()
        {
            var window = GetWindow<MockupToolWindow>(true);
            
            window.titleContent = new GUIContent(WindowTitle);
            window.minSize = WindowSize;
            window.maxSize = WindowSize;
                
            window.Show();
        }


        private static void ShowWindow(bool toggle = true)
        {
            var didCreate =  GenerateCanvas();
            GetReferences();

            if (!toggle)
            {
                OpenOrCreateWindow();
                return;
            }
            
            
            if (!didCreate)
            {
                if (!rootObj.activeInHierarchy)
                {
                    rootObj.SetActive(true);

                    OpenOrCreateWindow();
                }
                else
                {
                    rootObj.SetActive(false);

                    GetWindow<MockupToolWindow>(true).Close();
                }
            }
            else
            {
                rootObj.SetActive(true);

                OpenOrCreateWindow();
            }
        }


        private static void OnSceneGUI(SceneView sceneView)
        {
            if (!PerUserSettings.MockupEnabled) return;
            
            Handles.BeginGUI();

            if (string.IsNullOrEmpty(SessionState.GetString(ObjIdKey, rootObjectId)))
            {
                GUI.backgroundColor = Color.green;
            }
            else
            {
                GetReferences();
                
                if (rootObj == null)
                {
                    GUI.backgroundColor = Color.green;
                }
                else
                {
                    GUI.backgroundColor = rootObj.activeInHierarchy ? Color.red : Color.green;
                }
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();
            
            
            if (GUILayout.Button(GalleryIcon,GUILayout.Width(32.5f), GUILayout.Height(27.5f)))
            {
                rootObjectId = SessionState.GetString(ObjIdKey, rootObjectId);
                ShowWindow();
            }

            GUI.backgroundColor = Color.white;

            if (!HasOpenInstances<MockupToolWindow>())
            {
                if (rootObj != null)
                {
                    if (rootObj.activeInHierarchy)
                    {
                        if (GUILayout.Button(EditIcon, GUILayout.Width(32.5f), GUILayout.Height(27.5f)))
                        {
                            rootObjectId = SessionState.GetString(ObjIdKey, rootObjectId);
                            ShowWindow(false);
                        }
                    }
                }
            }
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            
            Handles.EndGUI();
        }


        private static void OnPlayModeStateChanged(PlayModeStateChange newMode)
        {
            if (newMode == PlayModeStateChange.EnteredPlayMode)
            {
                OnEnterPlayMode();
            }

            if (newMode == PlayModeStateChange.ExitingPlayMode)
            {
                OnExitPlayMode();
            }
        }


        private static bool GenerateCanvas()
        {
            if (string.IsNullOrEmpty(rootObjectId))
            {
                CreateRootObjectAndComponents();
            }
            else
            {
                for (var i = 0; i < SceneManager.sceneCount; i++)
                {
                    foreach (var obj in SceneManager.GetSceneAt(i).GetRootGameObjects())
                    {
                        if (!obj.TryGetComponent(typeof(MockupObjectHelper), out _)) continue;
                        
                        rootObj = obj;
                            
                        mockupCanvas = rootObj.GetComponent<Canvas>();
                        mockupCanvasScaler = rootObj.GetComponent<CanvasScaler>();
                        mockupCanvasGroup = rootObj.GetComponent<CanvasGroup>();
                        mockupImage = rootObj.GetComponentInChildren<Image>();

                        return false;
                    }
                }

                CreateRootObjectAndComponents();
            }

            return true;
        }


        private static void CreateRootObjectAndComponents()
        {
            rootObj = new GameObject(RootObjName);
            rootObj.AddComponent<MockupObjectHelper>();
            rootObj.AddComponent<Canvas>();
            rootObj.AddComponent<CanvasScaler>();
            rootObj.AddComponent<CanvasGroup>();

            rootObj.tag = "EditorOnly";
            rootObj.hideFlags = HideFlags.HideInHierarchy | HideFlags.NotEditable | HideFlags.DontSaveInBuild;

            var childObj = new GameObject("Container");
            childObj.transform.SetParent(rootObj.transform);
            childObj.AddComponent<RectTransform>();
            
            childObj.tag = "EditorOnly";
            childObj.hideFlags = HideFlags.HideInHierarchy | HideFlags.NotEditable | HideFlags.DontSaveInBuild;
            
            childObj.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            childObj.GetComponent<RectTransform>().anchorMax = Vector2.one;
            childObj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            
            var imageObj = new GameObject(ImageObjName);
            imageObj.transform.SetParent(childObj.transform);
            imageObj.AddComponent<Image>();
            
            imageObj.tag = "EditorOnly";
            imageObj.hideFlags = HideFlags.HideInHierarchy | HideFlags.NotEditable | HideFlags.DontSaveInBuild;
            
                
            mockupCanvas = rootObj.GetComponent<Canvas>();
            mockupCanvasScaler = rootObj.GetComponent<CanvasScaler>();
            mockupCanvasGroup = rootObj.GetComponent<CanvasGroup>();
            mockupImage = rootObj.GetComponentInChildren<Image>();

            mockupCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            mockupCanvas.sortingOrder = int.MaxValue;
            
            mockupCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            mockupCanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            mockupCanvasScaler.matchWidthOrHeight = 1;

            mockupCanvasGroup.interactable = false;
            mockupCanvasGroup.blocksRaycasts = false;
            mockupCanvasGroup.alpha = .5f;
            
            mockupImage.rectTransform.sizeDelta = mockupCanvasScaler.referenceResolution;
            mockupImage.raycastTarget = false;

            SceneVisibilityManager.instance.TogglePicking(rootObj, true);

            rootObjectId = rootObj.GetComponent<MockupObjectHelper>().Uuid;
            SessionState.SetString(ObjIdKey, rootObjectId);
        }


        private static void GetReferences()
        {
            if (rootObj != null) return;
            
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                foreach (var obj in SceneManager.GetSceneAt(i).GetRootGameObjects())
                {
                    if (!obj.TryGetComponent(typeof(MockupObjectHelper), out _)) continue;
                        
                    rootObj = obj;
                            
                    mockupCanvas = rootObj.GetComponent<Canvas>();
                    mockupCanvasScaler = rootObj.GetComponent<CanvasScaler>();
                    mockupCanvasGroup = rootObj.GetComponent<CanvasGroup>();
                    mockupImage = rootObj.GetComponentInChildren<Image>();

                    return;
                }
            }
        }


        private static void OnEnterPlayMode()
        {
            if (!PerUserSettings.MockupEnabled) return;
            rootObj.SetActive(false);
        }


        private static void OnExitPlayMode()
        {
            if (!PerUserSettings.MockupEnabled) return;
            rootObj.SetActive(true);
        }
    }
}

#endif