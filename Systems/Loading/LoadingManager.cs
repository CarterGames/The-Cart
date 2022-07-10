// ----------------------------------------------------------------------------
// LoadingManager.cs
// 
// Description: The manager script for the loading system.
// ----------------------------------------------------------------------------

using System.Linq;
using Scarlet.EventsSystem;
using Scarlet.General;
using UnityEngine;

namespace Scarlet.Loading
{
    public class LoadingManager : MonoBehaviour
    {
        private ILoadingElement[] elements;
        private int itemsCompleted;
        
        
        public bool HasLoaded { get; private set; }
        public float CurrentProgress => elements.Sum(t => t.Progress);
        public int TotalProgress => elements.Length;


        public static readonly Evt OnLoadingStarted = new Evt();
        public static readonly Evt<float, float> OnProgressUpdate = new Evt<float, float>();
        public static readonly Evt OnFullyLoaded = new Evt();


        protected virtual void Start() => Initialise();
        protected virtual void OnDisable() => Dispose();


        public void Initialise()
        {
            elements = SceneRef.GetComponentsFromScene<ILoadingElement>().ToArray();

            foreach (var item in elements)
            {
                item.OnLoaded.Add(OnElementLoaded);
                item.OnProgressMade.Add(BroadcastProgressUpdate);
            }

            foreach (var item in elements)
                item.OnStart.Raise();
            
            OnLoadingStarted.Raise();
        }



        private void Dispose()
        {
            if (elements == null) return;
            if (elements.Length <= 0) return;
            
            foreach (var item in elements)
            {
                item.OnLoaded.Remove(OnElementLoaded);
                item.OnProgressMade.Remove(BroadcastProgressUpdate);
            }
        }
        
        
        private void OnElementLoaded()
        {
            itemsCompleted++;
            CheckIfComplete();
            BroadcastProgressUpdate();
        }


        private void CheckIfComplete()
        {
            if (!itemsCompleted.Equals(elements.Length)) return;
            HasLoaded = true;
            OnFullyLoaded.Raise();
        }
        

        private void BroadcastProgressUpdate()
        {
            OnProgressUpdate.Raise(CurrentProgress, TotalProgress);
        }
    }
}