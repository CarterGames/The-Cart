// ----------------------------------------------------------------------------
// LoadingProgressText.cs
// 
// Description: A text extension to show the overall progress of the
//              current loading state in the loading system.
// ----------------------------------------------------------------------------

using System.Text;
using TMPro;
using UnityEngine;

namespace Scarlet.Loading
{
    public class LoadingProgressText : MonoBehaviour
    {
        [SerializeField] private ProgressTextStyle textStyle;
        [SerializeField] private TMP_Text progressText;

        private readonly StringBuilder builder = new StringBuilder();
        
        
        protected virtual void OnEnable() => Initialise();
        protected virtual void OnDisable() => Dispose();


        private void Initialise()
        {
            LoadingManager.OnProgressUpdate.Add(UpdateProgressText);
        }


        private void Dispose()
        {
            LoadingManager.OnProgressUpdate.Remove(UpdateProgressText);
        }


        private void UpdateProgressText(float currentProgress, float maxProgress)
        {
            switch (textStyle)
            {
                case ProgressTextStyle.Percentage:
                    
                    builder.Clear();
                    builder.Append(Mathf.FloorToInt((currentProgress / maxProgress) * 100));
                    builder.Append("%");

                    break;
                case ProgressTextStyle.OutOfX:
                    
                    builder.Clear();
                    builder.Append(Mathf.FloorToInt(currentProgress));
                    builder.Append(" / ");
                    builder.Append(Mathf.FloorToInt(maxProgress));

                    break;
            }

            progressText.text = builder.ToString();
        }
    }
}