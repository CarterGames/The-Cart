// ----------------------------------------------------------------------------
// RemotePanelAccessor.cs
// 
// Description: A script to access a panel without a direct reference to it. 
// ----------------------------------------------------------------------------

using System.Linq;
using Scarlet.General;
using UnityEngine;

namespace Scarlet.PanelSystem
{
    public class RemotePanelAccessor : MonoBehaviour
    {
        /// <summary>
        /// Gets whether or not the panel is open...
        /// </summary>
        /// <param name="panelId">The panel Id to look for</param>
        /// <returns>Whether the panel is open</returns>
        public virtual bool IsPanelOpen(string panelId)
        {
            var panel = SceneRef.GetComponentsFromScene<Panel>().FirstOrDefault(t => t.PanelID.Equals(panelId));

            if (panel != null) return panel.IsOpen;
            
            Debug.LogError($"Remote Panel Accessor | Unable to find the panel of the Id {panelId}");
            return false;
        }
        
        
        /// <summary>
        /// Opens the panel of the entered Id if it can find it...
        /// </summary>
        /// <param name="panelId">The panel Id to look for</param>
        public virtual void Open(string panelId)
        {
            var panel = SceneRef.GetComponentsFromScene<Panel>().FirstOrDefault(t => t.PanelID.Equals(panelId));

            if (panel != null)
            {
                panel.OpenPanel();
                return;
            }
            
            Debug.LogError($"Remote Panel Accessor | Unable to find the panel of the Id {panelId}");
        }
        
        
        /// <summary>
        /// Closes the panel of the entered Id if it can find it...
        /// </summary>
        /// <param name="panelId">The panel Id to look for</param>
        public virtual void Close(string panelId)
        {
            var panel = SceneRef.GetComponentsFromScene<Panel>().FirstOrDefault(t => t.PanelID.Equals(panelId));

            if (panel != null)
            {
                panel.ClosePanel();
                return;
            }
            
            Debug.LogError($"Remote Panel Accessor | Unable to find the panel of the Id {panelId}");
        }

        
        /// <summary>
        /// Gets the panel of the entered Id if it can find it...
        /// </summary>
        /// <param name="panelId">The panel Id to look for</param>
        public virtual Panel GetPanel(string panelId)
        {
            var panel = SceneRef.GetComponentsFromScene<Panel>().FirstOrDefault(t => t.PanelID.Equals(panelId));

            if (panel != null) return panel;

            Debug.LogError($"Remote Panel Accessor | Unable to find the panel of the Id {panelId}");
            return null;
        }
    }
}