/*
 * Copyright (c) 2018-Present Carter Games
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
using CarterGames.Common.Events;

namespace CarterGames.Common.Panels
{
    /// <summary>
    /// Tracks the panels active in the game.
    /// </summary>
    public static class PanelTracker
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Dictionary<string, Panel> panelsTracked = new Dictionary<string, Panel>();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Raises when any panel is opened.
        /// </summary>
        public static readonly Evt OnAnyPanelOpened = new Evt();
        
        
        /// <summary>
        /// Raises when any panel is closed.
        /// </summary>
        public static readonly Evt OnAnyPanelClosed = new Evt();

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
            return panelsTracked.ContainsKey(id);
        }
        
        
        /// <summary>
        /// Checks to see if any panel is opened.
        /// </summary>
        /// <returns>If there is any panel is open.</returns>
        public static bool AnyOtherPanelOpen()
        {
            return panelsTracked.Count > 0;
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
            if (panelsTracked.ContainsKey(panel.PanelID)) return;
            panelsTracked.Add(panel.PanelID, panel);
            OnAnyPanelOpened.Raise();
        }

        
        /// <summary>
        /// Removes a panel from being tracked.
        /// </summary>
        /// <param name="panel">The panel to stop tracking.</param>
        public static void RemovePanel(Panel panel)
        {
            if (!panelsTracked.ContainsKey(panel.PanelID)) return;
            panelsTracked.Remove(panel.PanelID);
            OnAnyPanelClosed.Raise();
        }
        

        /// <summary>
        /// Tries to get the panel of the entered id.
        /// </summary>
        /// <param name="id">The panel id to find.</param>
        /// <param name="panel">The panel found.</param>
        /// <returns>If it was successful.</returns>
        public static bool TryGetPanel(string id, out Panel panel)
        {
            panel = null;
            
            if (!panelsTracked.ContainsKey(id)) return false;
            panel = panelsTracked[id];
            return true;
        }
    }
}