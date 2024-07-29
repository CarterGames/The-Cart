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

using CarterGames.Cart.Core.Logs;
using UnityEngine;

namespace CarterGames.Cart.Modules.Panels
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