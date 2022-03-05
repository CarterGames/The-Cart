// ----------------------------------------------------------------------------
// OpenPanel.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 29/11/2021
// ----------------------------------------------------------------------------

using System.Linq;
using MultiScene.Core;
using UnityEngine;

namespace UI
{
    public class OpenPanel : MonoBehaviour
    {
        public virtual void Open(string panelID)
        {
            MultiSceneElly.GetComponentsFromAllScenes<Panel>().FirstOrDefault(t => t.PanelID.Equals(panelID))?.OpenPanel();
        }
    }
}