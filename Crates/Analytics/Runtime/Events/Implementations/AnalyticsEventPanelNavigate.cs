#if CARTERGAMES_CART_CRATE_ANALYTICS && CARTERGAMES_CART_CRATE_PANELS

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
using CarterGames.Cart.Crates.Panels;

namespace CarterGames.Cart.Crates.Analytics
{
    public class AnalyticsEventPanelNavigate : AnalyticsEvent
    {
        public override string EventName => "panel_navigate";
        
        private string PanelId { get; set; }
        private string PanelState { get; set; }
        
        
        public AnalyticsEventPanelNavigate(Panel panel)
        {
            PanelId = panel.PanelId;
            PanelState = panel.IsOpen ? "open" : "close";
        }
        
        
        protected override List<KeyValuePair<string, object>> MainParameters()
        {
            return new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("panel_id", PanelId),
                new KeyValuePair<string, object>("state", PanelState),
            };
        }
    }
}

#endif