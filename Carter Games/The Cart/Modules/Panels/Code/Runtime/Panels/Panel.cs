#if CARTERGAMES_CART_MODULE_PANELS

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

using System.Collections.Generic;
using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Core.Logs;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Cart.Modules.Panels
{
    /// <summary>
    /// A script to manage a UI panel to appear and disappear at will.
    /// </summary>
    public class Panel : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField][Tooltip("The id the panel is referred to.")] protected string panelId;
        [SerializeField][Tooltip("The object the panel is on.")] protected GameObject panelObject;

        [Space] 
        [SerializeField] private List<PanelTransition> transitions;


        [SerializeField] protected Canvas canvas;
        [SerializeField] protected GraphicRaycaster graphicRaycaster;

        private int completedTransitions;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The object the panel is contained on.
        /// </summary>
        public Transform PanelObject => panelObject.transform;


        /// <summary>
        /// The ID of the panel.
        /// </summary>
        public string PanelId => panelId;


        /// <summary>
        /// Returns if the panel is open.
        /// </summary>
        public bool IsOpen { get; private set; }


        /// <summary>
        /// Gets if the panel is transitioning or not.
        /// </summary>
        public bool IsTransitioning { get; private set; }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Runs when the panel begins to open.
        /// </summary>
        public readonly Evt OpenStartedEvt = new Evt();
        
        
        /// <summary>
        /// Runs when the panel has opened.
        /// </summary>
        public readonly Evt OpenCompletedEvt = new Evt();
        
        
        /// <summary>
        /// Runs when the panel begins to close.
        /// </summary>
        public readonly Evt CloseStartedEvt = new Evt();
        
        
        /// <summary>
        /// Runs when the panel has closed.
        /// </summary>
        public readonly Evt CloseCompletedEvt = new Evt();
        
        
        /// <summary>
        /// Raises when the transitions are completed.
        /// </summary>
        private readonly Evt TransitionsCompletedEvt = new Evt();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected virtual void OnEnable()
        {
            Initialise();
        }


        private void OnDestroy()
        {
            PanelTracker.RemovePanel(this);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Initialises the panel with any references it needs to get & sets it up in its default state.
        /// </summary>
        protected virtual void Initialise()
        {
            canvas ??= transform.GetComponentInChildren<Canvas>(true);
            graphicRaycaster ??= transform.GetComponentInChildren<GraphicRaycaster>(true);

            if (!IsValid())
            {
                CartLogger.LogError<LogCategoryPanels>($"Panel on {gameObject.name} is not setup correctly", typeof(Panel));
                return;
            }
            
            PanelTracker.TrackPanel(this);
        }


        /// <summary>
        /// Opens the panel.
        /// </summary>
        public void OpenPanel()
        {
            if (IsOpen) return;
            IsOpen = true;
            PanelOpenSequence();
        }


        /// <summary>
        /// Runs the logic to close the panel.
        /// </summary>
        public void ClosePanel()
        {
            if (!IsOpen) return;
            PanelCloseSequence();
        }


        /// <summary>
        /// Runs the sequence to open the panel.
        /// </summary>
        private void PanelOpenSequence()
        {
            IsTransitioning = true;
            PanelOpenStarted();

            TransitionsCompletedEvt.Add(PanelOpenComplete);
            
            foreach (var transition in transitions)
            {
                transition.CompletedEvt.Remove(OnTransitionCompleted);
                transition.CompletedEvt.Add(OnTransitionCompleted);
                
                transition.TransitionIn();
            }
            
            PanelTracker.MarkPanelOpened(this);
        }


        /// <summary>
        /// Runs the sequence to close the panel.
        /// </summary>
        private void PanelCloseSequence()
        {
            IsTransitioning = true;
            PanelCloseStarted();
            
            TransitionsCompletedEvt.Add(PanelCloseComplete);
            
            foreach (var transition in transitions)
            {
                transition.CompletedEvt.Remove(OnTransitionCompleted);
                transition.CompletedEvt.Add(OnTransitionCompleted);
                
                transition.TransitionOut();
            }
            
            IsOpen = false;
            PanelTracker.MarkPanelClosed(this);
        }


        /// <summary>
        /// Runs logic for the start of opening a panel.
        /// </summary>
        private void PanelOpenStarted()
        {
            if (canvas != null)
            {
                canvas.enabled = true;
            }
            
            OpenStartedEvt.Raise();
        }


        /// <summary>
        /// Runs logic for the completion of opening a panel.
        /// </summary>
        private void PanelOpenComplete()
        {
            if (graphicRaycaster != null)
            {
                graphicRaycaster.enabled = true;
            }
            
            OpenCompletedEvt.Raise();
        }


        /// <summary>
        /// Runs logic for the start of closing a panel.
        /// </summary>
        private void PanelCloseStarted()
        {
            if (graphicRaycaster == null) return;
            graphicRaycaster.enabled = false;
            CloseStartedEvt.Raise();
        }


        /// <summary>
        /// Runs logic for the completion of closing a panel.
        /// </summary>
        private void PanelCloseComplete()
        {
            if (canvas == null) return;
            canvas.enabled = false;
            CloseCompletedEvt.Raise();
        }


        /// <summary>
        /// Runs when any transition attached to the panel completes.
        /// </summary>
        private void OnTransitionCompleted()
        {
            completedTransitions++;

            if (!completedTransitions.Equals(transitions.Count)) return;
            
            TransitionsCompletedEvt.Raise();
            TransitionsCompletedEvt.Clear();

            completedTransitions = 0;
            IsTransitioning = false;
        }


        /// <summary>
        /// Gets if the panel is setup correctly for use or not. 
        /// </summary>
        /// <returns>bool</returns>
        public bool IsValid()
        {
            return canvas != null && panelObject != null && !string.IsNullOrEmpty(panelId);
        }
    }
}

#endif