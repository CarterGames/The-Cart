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

using CarterGames.Cart.Logs;
using UnityEngine;

namespace CarterGames.Cart.Crates.Panels
{
    /// <summary>
    /// A script to access a panel without a direct reference to it. 
    /// </summary>
    public class RemotePanelAccessor : MonoBehaviour
    {
        /// <summary>
        /// Gets the panel of the entered Id if it can find it...
        /// </summary>
        /// <param name="panelId">The panel Id to look for</param>
        public Panel Get(string panelId)
        {
            if (PanelTracker.TryGetPanel(panelId, out var panel))
            {
                return panel;
            }

            CartLogger.LogError<LogCategoryPanels>($"Unable to find the panel of the Id {panelId}", typeof(RemotePanelAccessor));
            return null;
        }


        /// <summary>
        /// Opens the panel of the entered Id if it can find it...
        /// </summary>
        /// <param name="panelId">The panel Id to look for</param>
        public void Open(string panelId)
        {
            if (PanelTracker.TryGetPanel(panelId, out var panel))
            {
                panel.OpenPanel();
                return;
            }

            CartLogger.LogError<LogCategoryPanels>($"Unable to find the panel of the Id {panelId}", typeof(RemotePanelAccessor));
        }


        /// <summary>
        /// Closes the panel of the entered Id if it can find it...
        /// </summary>
        /// <param name="panelId">The panel Id to look for</param>
        public void Close(string panelId)
        {
            if (PanelTracker.TryGetPanel(panelId, out var panel))
            {
                panel.ClosePanel();
                return;
            }
            
            CartLogger.LogError<LogCategoryPanels>($"Unable to find the panel of the Id {panelId}", typeof(RemotePanelAccessor));
        }
    }
}

#endif