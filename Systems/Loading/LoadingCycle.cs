// ----------------------------------------------------------------------------
// LoadingCycle.cs
// 
// Description: A script to help implement logic while the loading is happening.
// ----------------------------------------------------------------------------

using System.Collections;
using Scarlet.EventsSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Scarlet.Loading
{
    public class LoadingCycle : MonoBehaviour
    {
        [SerializeField, Range(1, 20)] private float cycleDelay = 1f;
        [Space] 
        [SerializeField] private UnityEvent OnStartEvent;
        [SerializeField] private UnityEvent OnCycledEvent;
        [SerializeField] private UnityEvent OnCompleteEvent;


        private WaitForSecondsRealtime cycleWait;
        private Coroutine cycleRoutine;

        private bool IsLoading { get; set; }
        
        
        public readonly Evt OnStart = new Evt();
        public readonly Evt OnCycled = new Evt();
        public readonly Evt OnComplete = new Evt();
        
        
        protected virtual void OnEnable() => Initialise();
        protected virtual void OnDisable() => Dispose();


        private void Initialise()
        {
            cycleWait = new WaitForSecondsRealtime(cycleDelay);
            
            LoadingManager.OnLoadingStarted.Add(OnLoadingStart);
            OnCycled.Add(OnLoadingCycle);
            LoadingManager.OnFullyLoaded.Add(OnLoadingComplete);
        }


        private void Dispose()
        {
            LoadingManager.OnLoadingStarted.Remove(OnLoadingStart);
            OnCycled.Remove(OnLoadingCycle);
            LoadingManager.OnFullyLoaded.Remove(OnLoadingComplete);
            
            if (cycleRoutine == null) return;
            StopCoroutine(cycleRoutine);
            cycleRoutine = null;
        }


        private void OnLoadingStart()
        {
            IsLoading = true;
            cycleRoutine = StartCoroutine(Co_CycleRoutine());
            OnStart.Raise();
            OnStartEvent?.Invoke();
        }
        
        private void OnLoadingCycle() => OnCycledEvent?.Invoke();
        
        
        private void OnLoadingComplete()
        {
            IsLoading = false;
            OnComplete.Raise();
            OnCompleteEvent?.Invoke();
        }


        private IEnumerator Co_CycleRoutine()
        {
            yield return cycleWait;
            OnCycled.Raise();
            
            if (!IsLoading)
            {
                cycleRoutine = null;
                yield break;
            }
            
            cycleRoutine = StartCoroutine(Co_CycleRoutine());
        }
    }
}