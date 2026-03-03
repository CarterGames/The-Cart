#if CARTERGAMES_CART_CRATE_PANELS

/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using UnityEngine;
using UnityEngine.Events;

namespace CarterGames.Cart.Crates.Panels
{
    /// <summary>
    /// A script to manage a UI panel to appear and disappear at will, but with events that you can assign in the inspector.
    /// </summary>
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
            OpenStartedEvt.Remove(PanelOpenStarted);
            OpenCompletedEvt.Remove(PanelOpenComplete);
            CloseStartedEvt.Remove(PanelCloseStarted);
            CloseCompletedEvt.Remove(PanelCloseComplete);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Override Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected override void Initialise()
        {
            base.Initialise();
            
            OpenStartedEvt.Add(PanelOpenStarted);
            OpenCompletedEvt.Add(PanelOpenComplete);
            CloseStartedEvt.Add(PanelCloseStarted);
            CloseCompletedEvt.Add(PanelCloseComplete);
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