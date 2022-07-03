// ----------------------------------------------------------------------------
// PanelE.cs
// 
// Description: A script to manage a UI panel to appear and disappear at will,
//              But with events that you can assign in the inspector.
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

namespace Scarlet.PanelSystem
{
    public class PanelE : Panel
    {
        [Space]
        [SerializeField] private UnityEvent onPanelOpenStart;
        [SerializeField] private UnityEvent onPanelOpenComplete;
        [SerializeField] private UnityEvent onPanelCloseStart;
        [SerializeField] private UnityEvent onPanelCloseComplete;


        protected override void Initialise()
        {
            base.Initialise();
            
            OnPanelOpenStarted.Add(PanelOpenStarted);
            OnPanelOpenComplete.Add(PanelOpenComplete);
            OnPanelCloseStarted.Add(PanelCloseStarted);
            OnPanelCloseComplete.Add(PanelCloseComplete);
        }


        private void OnDisable()
        {
            OnPanelOpenStarted.Remove(PanelOpenStarted);
            OnPanelOpenComplete.Remove(PanelOpenComplete);
            OnPanelCloseStarted.Remove(PanelCloseStarted);
            OnPanelCloseComplete.Remove(PanelCloseComplete);
        }


        private void PanelOpenStarted()
        {
            onPanelOpenStart?.Invoke();
        }

        
        private void PanelOpenComplete()
        {
            onPanelOpenComplete?.Invoke();
        }


        private void PanelCloseStarted()
        {
            onPanelCloseStart?.Invoke();
        }


        private void PanelCloseComplete()
        {
            onPanelCloseComplete?.Invoke();
        }
    }
}