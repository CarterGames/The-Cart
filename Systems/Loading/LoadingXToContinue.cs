// ----------------------------------------------------------------------------
// LoadingXToContinue.cs
// 
// Description: A class to add functionality for when the loading has completed.
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

namespace Scarlet.Loading
{
    public class LoadingXToContinue : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnLoadingComplete;

        
        protected virtual void OnEnable() => Initialise();
        protected virtual void OnDisable() => Dispose();


        private void Initialise()
        {
            LoadingManager.OnFullyLoaded.Add(CallLoadingCompleted);
            LoadingManager.OnFullyLoaded.Add(LoadingCompleted);
        }


        private void Dispose()
        {
            LoadingManager.OnFullyLoaded.Remove(CallLoadingCompleted);
            LoadingManager.OnFullyLoaded.Remove(LoadingCompleted);
        }


        private void CallLoadingCompleted()
        {
            OnLoadingComplete?.Invoke();
        }


        protected virtual void LoadingCompleted() { }
    }
}