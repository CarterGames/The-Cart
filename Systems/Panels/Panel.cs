// ----------------------------------------------------------------------------
// Panel.cs
// 
// Description: A script to manage a UI panel to appear and disappear at will.
// ----------------------------------------------------------------------------

using System.Collections;
using Scarlet.Easing;
using Scarlet.EventsSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Scarlet.PanelSystem
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Panel : MonoBehaviour
    {
        [SerializeField][Tooltip("The id the panel is referred to.")] protected string panelID;
        [SerializeField][Tooltip("The speed the panel canvas group fades as.")] protected float fadeSpeed = 4;
        [Space]
        [SerializeField][Tooltip("The ease used to reveal the panel.")] protected OutEaseData revealEase;
        [SerializeField][Tooltip("The ease used to hide the panel.")] protected InEaseData hideEase;
        [Space]
        [SerializeField][Tooltip("The object the panel is on.")] protected GameObject panelObject;
        
        
        protected Canvas canvas;
        protected GraphicRaycaster graphicRaycaster;
        protected CanvasGroup canvasGroup;

        
        /// <summary>
        /// Runs when the panel begins to open...
        /// </summary>
        public readonly Evt OnPanelOpenStarted = new Evt();
        
        
        /// <summary>
        /// Runs when the panel has opened...
        /// </summary>
        public readonly Evt OnPanelOpenComplete = new Evt();
        
        
        /// <summary>
        /// Runs when the panel begins to close...
        /// </summary>
        public readonly Evt OnPanelCloseStarted = new Evt();
        
        
        /// <summary>
        /// Runs when the panel has closed...
        /// </summary>
        public readonly Evt OnPanelCloseComplete = new Evt();

        
        /// <summary>
        /// The object the panel is contained on...
        /// </summary>
        protected Transform PanelObject => panelObject.transform;

        
        /// <summary>
        /// The ID of the panel...
        /// </summary>
        public string PanelID => panelID;
        
        
        /// <summary>
        /// Returns if the panel is open...
        /// </summary>
        public bool IsOpen { get; private set; }
        
        
        protected virtual void Awake()
        {
            Initialise();
        }


        /// <summary>
        /// Initialises the panel with any references it needs to get & sets it up in its default state...
        /// </summary>
        protected virtual void Initialise()
        {
            var _canvas = transform.GetComponentInChildren<Canvas>(true);
            canvas = _canvas;

            var _graphicsRaycaster = transform.GetComponentInChildren<GraphicRaycaster>(true);
            graphicRaycaster = _graphicsRaycaster;

            canvasGroup ??= GetComponentInChildren<CanvasGroup>();
            canvasGroup.alpha = 0;
        }


        /// <summary>
        /// Opens the panel...
        /// </summary>
        public void OpenPanel()
        {
            if (IsOpen) return;
            IsOpen = true;
            PanelOpenSequence();
        }


        /// <summary>
        /// Runs the logic to close the panel...
        /// </summary>
        public void ClosePanel()
        {
            if (!IsOpen) return;
            PanelCloseSequence();
        }


        /// <summary>
        /// Runs the sequence to open the panel...
        /// </summary>
        private void PanelOpenSequence()
        {
            StopAllCoroutines();
            PanelOpenStarted();
            StartCoroutine(Co_CanvasGroupFade(true));
            StartCoroutine(Co_PanelReveal());
            PanelTracker.TrackPanel(this);
        }
        
        
        /// <summary>
        /// Runs the sequence to close the panel...
        /// </summary>
        private void PanelCloseSequence()
        {
            StopAllCoroutines();
            PanelCloseStarted();
            StartCoroutine(Co_CanvasGroupFade(false));
            StartCoroutine(Co_PanelHide());
            IsOpen = false;
            PanelTracker.RemovePanel(this);
        }


        /// <summary>
        /// Runs logic for the start of opening a panel...
        /// </summary>
        private void PanelOpenStarted()
        {
            if (canvas == null) return;
            canvas.enabled = true;
            OnPanelOpenStarted.Raise();
        }

        
        /// <summary>
        /// Runs logic for the completion of opening a panel...
        /// </summary>
        private void PanelOpenComplete()
        {
            if (graphicRaycaster == null) return;
            graphicRaycaster.enabled = true;
            OnPanelOpenComplete.Raise();
        }


        /// <summary>
        /// Runs logic for the start of closing a panel...
        /// </summary>
        private void PanelCloseStarted()
        {
            if (graphicRaycaster == null) return;
            graphicRaycaster.enabled = false;
            OnPanelCloseStarted.Raise();
        }
        
        
        /// <summary>
        /// Runs logic for the completion of closing a panel...
        /// </summary>
        private void PanelCloseComplete()
        {
            if (canvas == null) return;
            canvas.enabled = false;
            OnPanelCloseComplete.Raise();
        }


        /// <summary>
        /// The coroutine to reveal the panel...
        /// </summary>
        private IEnumerator Co_PanelReveal()
        {
            var _elapsedTime = 0d;

            while (_elapsedTime < revealEase.easeDuration)
            {
                _elapsedTime += Time.unscaledDeltaTime;
                var _progress = _elapsedTime / revealEase.easeDuration;
                PanelObject.transform.localScale = Vector3.one * (float)Ease.ReadValue(revealEase, _progress);
                yield return null;
            }

            PanelObject.transform.localScale = Vector3.one;
            PanelOpenComplete();
        }


        /// <summary>
        /// The coroutine to hide the panel...
        /// </summary>
        private IEnumerator Co_PanelHide()
        {
            var _elapsedTime = 0d;

            while (_elapsedTime < hideEase.easeDuration)
            {
                _elapsedTime += Time.unscaledDeltaTime;
                var _progress = _elapsedTime / hideEase.easeDuration;
                var _easeValue = 1 - (float)Ease.ReadValue(hideEase, _progress);
                if (_easeValue >= 0) PanelObject.transform.localScale = Vector3.one * _easeValue;
                yield return null;
            }

            PanelObject.transform.localScale = Vector3.zero;
            PanelCloseComplete();
        }


        /// <summary>
        /// The coroutine to fade the canvas group on the panel...
        /// </summary>
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