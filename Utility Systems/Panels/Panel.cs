// ----------------------------------------------------------------------------
// BasePanel.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 19/10/2021
// ----------------------------------------------------------------------------

using System.Collections;
using System.Linq;
using Fumb.General;
using JTools;
using MultiScene.Core;
using Tracking;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Panel : MonoBehaviour, IMultiSceneAwake
    {
        [SerializeField] protected string panelID;
        [SerializeField] protected float fadeSpeed = 4;

        protected Canvas canvas;
        protected GraphicRaycaster graphicRaycaster;

        [SerializeField] protected GameObject panel;
        
        [SerializeField] public UnityEvent onPanelOpenStart;
        [SerializeField] public UnityEvent onPanelOpenComplete;
        [SerializeField] public UnityEvent onPanelCloseStart;
        [SerializeField] public UnityEvent onPanelCloseComplete;

        private CameraTouchToMove cameraController;
        private CanvasGroup canvasGroup;
        private bool hasCamera;
        
        protected Transform PanelObject => panel.transform;

        public string PanelID => panelID;
        
        public bool IsOpen { get; private set; }
        
        
        protected virtual void Awake()
        {
            if (gameObject.TryGetComponentInParent<Canvas>(out var _canvas))
                canvas = _canvas;

            if (gameObject.TryGetComponentInParent<GraphicRaycaster>(out var _gr))
                graphicRaycaster = _gr;

            canvasGroup ??= GetComponentInChildren<CanvasGroup>();
            canvasGroup.alpha = 0;
            onPanelCloseComplete.AddListener(OnPanelClosed);
        }


        public virtual void OnMultiSceneAwake()
        {
            if (MultiSceneManager.IsSceneInGroup("GarageBaseScene"))
            {
                cameraController = MultiSceneElly.GetComponentFromScene<CameraTouchToMove>("PlayerCamera");
                hasCamera = cameraController != null;
            }
        }


        protected virtual void OnDisable()
        {
            onPanelCloseComplete.RemoveListener(OnPanelClosed);
        }


        public void CallOpenOtherPanel(string panelID)
        {
            MultiSceneElly.GetComponentsFromAllScenes<Panel>().FirstOrDefault(t => t.PanelID.Equals(panelID))?.OpenPanel();
        }

        
        public void OpenPanel()
        {
            if (IsOpen) return;
            IsOpen = true;
            
            if (hasCamera)
                cameraController.EnableTouchInput(false);
            
            PanelOpenSequence();
        }


        private void PanelOpenSequence()
        {
            TweenAnimationHelper.TweenPanelOpen(panel.gameObject, gameObject);
            StartCoroutine(Co_CanvasGroupFade(true));
        }


        public virtual void OnPanelOpenStarted()
        {
            if (canvas == null) return;
            canvas.enabled = true;
            
            onPanelOpenStart?.Invoke();
        }

        public virtual void OnPanelOpenComplete()
        {
            if (graphicRaycaster != null)
                graphicRaycaster.enabled = true;
            
            onPanelOpenComplete?.Invoke();
        }


        public void ClosePanel() => ClosePanel(false);
        
        public void ClosePanel(bool force)
        {
            // Tutorial Lock
            if (!force)
            {
                if (!Telemetry.HasCompletedTutorial) return;
            }
            
            TweenAnimationHelper.TweenPanelClose(panel.gameObject, gameObject);
            StartCoroutine(Co_CanvasGroupFade(false));
            
            if (hasCamera)
                cameraController.EnableTouchInput(true);
        }
        

        public virtual void OnPanelCloseStarted()
        {
            if (graphicRaycaster != null)
                graphicRaycaster.enabled = false;
            
            onPanelCloseStart?.Invoke();
        }
        
        public virtual void OnPanelCloseComplete()
        {
            if (canvas != null)
                canvas.enabled = false;
            
            onPanelCloseComplete?.Invoke();
        }

        private void OnPanelClosed()
        {
            IsOpen = false;
        }


        private IEnumerator Co_CanvasGroupFade(bool fadeIn)
        {
            if (fadeIn)
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                
                while (canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha += fadeSpeed * Time.unscaledDeltaTime;
                    yield return null;
                }
            }
            else
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                
                while (canvasGroup.alpha > 0)
                {
                    canvasGroup.alpha -= fadeSpeed * Time.unscaledDeltaTime;
                    yield return null;
                }
            }
        }
    }
}