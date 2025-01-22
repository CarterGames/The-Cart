#if CARTERGAMES_CART_MODULE_PANELS

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

using System.Collections.Generic;
using CarterGames.Cart.Core.Events;

namespace CarterGames.Cart.Modules.Panels
{
    /// <summary>
    /// Tracks the panels active in the game.
    /// </summary>
    public static class PanelTracker
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static readonly Dictionary<string, Panel> PanelsTracked = new Dictionary<string, Panel>();
        private static readonly Dictionary<string, Panel> ActivePanels = new Dictionary<string, Panel>();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Raises when a panel is tracked.
        /// </summary>
        public static readonly Evt<Panel> PanelTracked = new Evt<Panel>();
        
        
        /// <summary>
        /// Raises when a panel is no longer tracked.
        /// </summary>
        public static readonly Evt<Panel> PanelUnTracked = new Evt<Panel>();
        
        
        /// <summary>
        /// Raises when a panel is opened.
        /// </summary>
        public static readonly Evt<Panel> PanelOpened = new Evt<Panel>();


        /// <summary>
        /// Raises when any panel is opened.
        /// </summary>
        public static readonly Evt AnyPanelOpened = new Evt();


        /// <summary>
        /// Raises when a panel is closed.
        /// </summary>
        public static readonly Evt<Panel> PanelClosed = new Evt<Panel>();


        /// <summary>
        /// Raises when any panel is closed.
        /// </summary>
        public static readonly Evt AnyPanelClosed = new Evt();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Utility Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Checks to see if a panel is being tracked.
        /// </summary>
        /// <param name="id">The panel id to find.</param>
        /// <returns>If the panel is being tracked or not.</returns>
        public static bool IsTrackingPanel(string id)
        {
            return PanelsTracked.ContainsKey(id);
        }


        /// <summary>
        /// Checks to see if a panel is open.
        /// </summary>
        /// <param name="id">The panel id to find.</param>
        /// <returns>If the panel is being tracked or not.</returns>
        public static bool IsPanelOpen(string id)
        {
            return ActivePanels.ContainsKey(id);
        }


        /// <summary>
        /// Checks to see if any panel is opened.
        /// </summary>
        /// <returns>If there is any panel is open.</returns>
        public static bool AnyOtherPanelOpen()
        {
            return ActivePanels.Count > 0;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Tracks the panel entered.
        /// </summary>
        /// <param name="panel">The panel to track.</param>
        public static void TrackPanel(Panel panel)
        {
            if (PanelsTracked.ContainsKey(panel.PanelId)) return;
            PanelsTracked.Add(panel.PanelId, panel);
            PanelTracked.Raise(panel);
        }


        /// <summary>
        /// Removes a panel from being tracked.
        /// </summary>
        /// <param name="panel">The panel to stop tracking.</param>
        public static void RemovePanel(Panel panel)
        {
            if (!PanelsTracked.ContainsKey(panel.PanelId)) return;
            PanelsTracked.Remove(panel.PanelId);
            PanelUnTracked.Raise(panel);
        }


        /// <summary>
        /// Tries to get the panel of the entered id.
        /// </summary>
        /// <param name="id">The panel id to find.</param>
        /// <param name="panel">The panel found.</param>
        /// <returns>If it was successful.</returns>
        public static bool TryGetPanel(string id, out Panel panel)
        {
            panel = GetPanel(id);
            return panel != null;
        }


        /// <summary>
        /// Get the panel of the entered id.
        /// </summary>
        /// <param name="id">The panel id to find.</param>
        /// <returns>The panel found or null if none were found.</returns>
        public static Panel GetPanel(string id)
        {
            if (!PanelsTracked.ContainsKey(id)) return null;
            return PanelsTracked[id];
        }


        /// <summary>
        /// Tracks the panel entered.
        /// </summary>
        /// <param name="panel">The panel to track.</param>
        public static void MarkPanelOpened(Panel panel)
        {
            if (ActivePanels.ContainsKey(panel.PanelId)) return;
            ActivePanels.Add(panel.PanelId, panel);
            PanelOpened.Raise(panel);
            AnyPanelOpened.Raise();
        }


        /// <summary>
        /// Tracks the panel entered.
        /// </summary>
        /// <param name="panel">The panel to track.</param>
        public static void MarkPanelClosed(Panel panel)
        {
            if (!ActivePanels.ContainsKey(panel.PanelId)) return;
            ActivePanels.Remove(panel.PanelId);
            PanelClosed.Raise(panel);
            AnyPanelClosed.Raise();
        }
    }
}

#endif