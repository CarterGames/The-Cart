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

using System.Collections.Generic;
using CarterGames.Cart.Events;

namespace CarterGames.Cart.Crates.Panels
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
        public static readonly Evt<Panel> PanelTrackedEvt = new Evt<Panel>();
        
        
        /// <summary>
        /// Raises when a panel is no longer tracked.
        /// </summary>
        public static readonly Evt<Panel> PanelUnTrackedEvt = new Evt<Panel>();
        
        
        /// <summary>
        /// Raises when a panel is opened.
        /// </summary>
        public static readonly Evt<Panel> PanelOpenedEvt = new Evt<Panel>();


        /// <summary>
        /// Raises when any panel is opened.
        /// </summary>
        public static readonly Evt AnyPanelOpenedEvt = new Evt();


        /// <summary>
        /// Raises when a panel is closed.
        /// </summary>
        public static readonly Evt<Panel> PanelClosedEvt = new Evt<Panel>();


        /// <summary>
        /// Raises when any panel is closed.
        /// </summary>
        public static readonly Evt AnyPanelClosedEvt = new Evt();

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
        public static bool AnyPanelOpen()
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
            PanelTrackedEvt.Raise(panel);
        }


        /// <summary>
        /// Removes a panel from being tracked.
        /// </summary>
        /// <param name="panel">The panel to stop tracking.</param>
        public static void RemovePanel(Panel panel)
        {
            if (!PanelsTracked.ContainsKey(panel.PanelId)) return;
            PanelsTracked.Remove(panel.PanelId);
            PanelUnTrackedEvt.Raise(panel);
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
            PanelOpenedEvt.Raise(panel);
            AnyPanelOpenedEvt.Raise();
        }


        /// <summary>
        /// Tracks the panel entered.
        /// </summary>
        /// <param name="panel">The panel to track.</param>
        public static void MarkPanelClosed(Panel panel)
        {
            if (!ActivePanels.ContainsKey(panel.PanelId)) return;
            ActivePanels.Remove(panel.PanelId);
            PanelClosedEvt.Raise(panel);
            AnyPanelClosedEvt.Raise();
        }
    }
}

#endif