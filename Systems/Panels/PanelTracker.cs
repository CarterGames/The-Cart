// ----------------------------------------------------------------------------
// PanelTracker.cs
// 
// Description: Tracks the panels active in the game.
// ----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Scarlet.PanelSystem
{
    public static class PanelTracker
    {
        private static Dictionary<string, Panel> panelsTracked = new Dictionary<string, Panel>();
        
        
        public static void TrackPanel(Panel panel)
        {
            if (panelsTracked.ContainsKey(panel.PanelID)) return;
            panelsTracked.Add(panel.PanelID, panel);
        }

        
        public static void RemovePanel(Panel panel)
        {
            if (!panelsTracked.ContainsKey(panel.PanelID)) return;
            panelsTracked.Remove(panel.PanelID);
        }
        

        public static bool AnyOtherPanelOpen()
        {
            return panelsTracked.Count > 0;
        }


        public static bool TryGetPanel(string id, out Panel panel)
        {
            panel = null;
            if (!panelsTracked.ContainsKey(id)) return false;
            panel = panelsTracked[id];
            return true;
        }

        
        public static bool IsTrackingPanel(string id)
        {
            return panelsTracked.ContainsKey(id);
        }
    }
}