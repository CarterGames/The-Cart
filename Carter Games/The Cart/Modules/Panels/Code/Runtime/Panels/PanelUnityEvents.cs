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

using UnityEngine;
using UnityEngine.Events;

namespace CarterGames.Cart.Modules.Panels
{
    /// <summary>
    /// A script to manage a UI panel to appear and disappear at will, but with events that you can assign in the inspector.
    /// </summary>
    [AddComponentMenu("Carter Games/The Cart/Modules/Panels/Panel (Unity Events)")]
    public class PanelUnityEvents : Panel
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] private bool eventsExpanded;
        [SerializeField] private UnityEvent onPanelOpenStart;
        [SerializeField] private UnityEvent onPanelOpenComplete;
        [SerializeField] private UnityEvent onPanelCloseStart;
        [SerializeField] private UnityEvent onPanelCloseComplete;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private void OnDisable()
        {
            OpenStarted.Remove(PanelOpenStarted);
            OpenCompleted.Remove(PanelOpenComplete);
            CloseStarted.Remove(PanelCloseStarted);
            CloseCompleted.Remove(PanelCloseComplete);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Override Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected override void Initialise()
        {
            base.Initialise();
            
            OpenStarted.Add(PanelOpenStarted);
            OpenCompleted.Add(PanelOpenComplete);
            CloseStarted.Add(PanelCloseStarted);
            CloseCompleted.Add(PanelCloseComplete);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private void PanelOpenStarted()
        {
            onPanelOpenStart?.Invoke();
        }


        private void PanelOpenComplete()
        {
            onPanelOpenComplete?.Invoke();
        }


        private void PanelCloseStarted()
        {
            onPanelCloseStart?.Invoke();
        }


        private void PanelCloseComplete()
        {
            onPanelCloseComplete?.Invoke();
        }
    }
}

#endif