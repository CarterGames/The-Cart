// ----------------------------------------------------------------------------
// LoadingTimer.cs
// 
// Description: A class to add a set time to the loading system.
//              Useful to set a default load time if the user has a fast machine.
// ----------------------------------------------------------------------------

using System.Collections;
using Scarlet.EventsSystem;
using UnityEngine;

namespace Scarlet.Loading
{
    public class LoadingTimer : MonoBehaviour, ILoadingElement
    {
        [SerializeField] private float waitTime;
        
        private Coroutine waitRoutine;


        public float Progress { get; set; } = 0f;
        public Evt OnStart { get; set; } = new Evt();
        public Evt OnProgressMade { get; set; } = new Evt();
        public Evt OnLoaded { get; set; } = new Evt();
        
        
        protected virtual void OnEnable() => Initialise();
        protected virtual void OnDisable() => Dispose();


        private void Initialise()
        {
            LoadingManager.OnLoadingStarted.Add(StartTimer);
        }


        private void Dispose()
        {
            LoadingManager.OnLoadingStarted.Remove(StartTimer);
        }


        public void StartTimer()
        {
            waitRoutine = StartCoroutine(Co_TimerRoutine());
        }


        public void StopTimer()
        {
            if (waitRoutine == null) return;
            StopCoroutine(waitRoutine);
            waitRoutine = null;
        }
        
        
        private IEnumerator Co_TimerRoutine()
        {
            OnStart.Raise();
            var duration = 0f;

            while (duration < waitTime)
            {
                duration += Time.unscaledDeltaTime;
                Progress = duration / waitTime;
                OnProgressMade.Raise();
                yield return null;
            }

            Progress = 1f;
            OnLoaded.Raise();
            waitRoutine = null;
        }
    }
}