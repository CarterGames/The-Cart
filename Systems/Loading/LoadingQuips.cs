// ----------------------------------------------------------------------------
// LoadingQuips.cs
// 
// Description: A text extension to show random text lines while the
//              loading system is loading.
// ----------------------------------------------------------------------------

using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scarlet.Loading
{
    public class LoadingQuips : MonoBehaviour
    {
        [SerializeField] private TMP_Text quipsText;
        [SerializeField] private string[] quips;
        [SerializeField] private float startDelay;
        [SerializeField] private float loopDelay;

        private WaitForSecondsRealtime startWait;
        private WaitForSecondsRealtime loopWait;

        private Coroutine startRoutine;
        private Coroutine loopRoutine;

        private string lastQuip;
        
        private bool IsLoading { get; set; }

        protected virtual void OnEnable() => Initialise();
        protected virtual void OnDisable() => Dispose();


        private void Initialise()
        {
            LoadingManager.OnLoadingStarted.Add(SetToLoading);
            LoadingManager.OnLoadingStarted.Add(StartQuips);
            LoadingManager.OnFullyLoaded.Add(SetToNotLoading);
            LoadingManager.OnFullyLoaded.Add(StopQuips);
            
            startWait = new WaitForSecondsRealtime(startDelay);
            loopWait = new WaitForSecondsRealtime(loopDelay);
        }


        private void Dispose()
        {
            LoadingManager.OnLoadingStarted.Remove(SetToLoading);
            LoadingManager.OnLoadingStarted.Remove(StartQuips);
            LoadingManager.OnFullyLoaded.Remove(SetToNotLoading);
            LoadingManager.OnFullyLoaded.Remove(StopQuips);
            
            if (startRoutine != null)
            {
                StopCoroutine(startRoutine);
                startRoutine = null;
            }

            if (loopRoutine == null) return;
            StopCoroutine(loopRoutine);
            loopRoutine = null;
        }


        private void SetToLoading() => IsLoading = true;
        private void SetToNotLoading() => IsLoading = false;
        
        
        private IEnumerator Co_StartRoutine()
        {
            yield return startWait;
            loopRoutine = StartCoroutine(Co_LoopRoutine());
            startRoutine = null;
        }

        
        private IEnumerator Co_LoopRoutine()
        {
            var options = quips.Where(t => !t.Equals(lastQuip)).ToArray();
            var index = Random.Range(0, options.Length);

            lastQuip = options[index];
            quipsText.text = options[index];

            yield return loopWait;

            if (IsLoading)
            {
                loopRoutine = StartCoroutine(Co_LoopRoutine());
                yield break;
            }
            
            StopQuips();
        }


        private void StartQuips()
        {
            startRoutine = StartCoroutine(Co_StartRoutine());
        }


        private void StopQuips()
        {
            quipsText.text = string.Empty;
            
            if (loopRoutine == null) return;
            StopCoroutine(loopRoutine);
            loopRoutine = null;
        }
    }
}