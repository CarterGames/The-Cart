// ----------------------------------------------------------------------------
// LoadingProgressBar.cs
// 
// Description: A progress bar extension to show the overall progress of the
//              current loading state in the loading system.
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Scarlet.Loading
{
    public class LoadingProgressBar : MonoBehaviour
    {
        [SerializeField] private Image fillImage;


        private void OnEnable() => Initialise();
        private void OnDisable() => Dispose();


        private void Initialise()
        {
            LoadingManager.OnLoadingStarted.Add(SetToEmpty);
            LoadingManager.OnProgressUpdate.Add(SetFill);
            LoadingManager.OnFullyLoaded.Add(SetToFull);
        }


        private void Dispose()
        {
            LoadingManager.OnLoadingStarted.Remove(SetToEmpty);
            LoadingManager.OnProgressUpdate.Remove(SetFill);
            LoadingManager.OnFullyLoaded.Remove(SetToFull);
        }


        private void SetFill(float fillAmount, float maxAmount)
        {
            fillImage.fillAmount = fillAmount;
        }


        private void SetToEmpty()
        {
            fillImage.fillAmount = 0;
        }


        private void SetToFull()
        {
            fillImage.fillAmount = 1;
        }
    }
}