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

namespace CarterGames.Common.Tooltips
{
    /// <summary>
    /// Handles storing the tooltips in use.
    /// </summary>
    public static class TooltipManager
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static readonly Dictionary<string, ToolTipInstance> AllTips = new Dictionary<string, ToolTipInstance>();
        private static readonly Dictionary<string, ToolTipInstance> VisibleTips = new Dictionary<string, ToolTipInstance>();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Registers a tooltip with the manager when called.
        /// </summary>
        /// <param name="instance">The instance to register.</param>
        public static void RegisterTooltip(ToolTipInstance instance)
        {
            if (AllTips.ContainsKey(instance.Id)) return;
            AllTips.Add(instance.Id, instance);
            instance.Toggled.Add(OnInstanceToggled);
        }

        
        /// <summary>
        /// Un-Registers a tooltip with the manager when called.
        /// </summary>
        /// <param name="instance">The instance to unregister.</param>
        public static void UnregisterTooltip(ToolTipInstance instance)
        {
            if (!AllTips.ContainsKey(instance.Id)) return;
            instance.Toggled.Remove(OnInstanceToggled);
            AllTips.Remove(instance.Id);
        }


        /// <summary>
        /// Runs when any tooltip that is registered is toggled on/off.
        /// </summary>
        /// <param name="instance"></param>
        private static void OnInstanceToggled(ToolTipInstance instance)
        {
            if (instance.IsActive)
            {
                if (VisibleTips.ContainsKey(instance.Id)) return;
                VisibleTips.Add(instance.Id, instance);
            }
            else
            {
                if (!VisibleTips.ContainsKey(instance.Id)) return;
                VisibleTips.Remove(instance.Id);
            }
        }


        /// <summary>
        /// Tries to get a tooltip of the entered Id.
        /// </summary>
        /// <param name="id">The id to find.</param>
        /// <param name="tooltip">The found tooltip.</param>
        /// <returns>If it was successful or not.</returns>
        public static bool TryGetTooltip(string id, out ToolTipInstance tooltip)
        {
            tooltip = GetTooltip(id);
            return tooltip != null;
        }


        /// <summary>
        /// Gets a tooltip of the entered Id.
        /// </summary>
        /// <param name="id">The id to find.</param>
        /// <returns>The found tooltip or null.</returns>
        public static ToolTipInstance GetTooltip(string id)
        {
            if (AllTips.ContainsKey(id))
            {
                return AllTips[id];
            }
            
            return null;
        }
    }
}