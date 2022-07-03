// ----------------------------------------------------------------------------
// CanvasGroupExtension.cs
// 
// Description: An extension class for canvas groups.
// ----------------------------------------------------------------------------

using UnityEngine;

namespace Scarlet.General
{
    /// <summary>
    /// An extension class for canvas groups.
    /// </summary>
    public static class CanvasGroupExtension
    {
        /// <summary>
        /// Sets the canvas group to an active state.
        /// </summary>
        /// <param name="canvasGroup">The canvas group to effect.</param>
        /// <param name="alpha">The alpha value to assign.</param>
        /// <param name="interactable">Should the canvas group be interactable?</param>
        /// <param name="blocksRaycasts">Should the canvas group block raycasts?</param>
        /// <param name="ignoreParentGroups">Should the canvas group ignore parent groups?</param>
        public static void SetActive(this CanvasGroup canvasGroup, int? alpha = null, bool? interactable = null, bool? blocksRaycasts = null, bool? ignoreParentGroups = null)
        {
            canvasGroup.alpha = alpha ?? canvasGroup.alpha;
            canvasGroup.interactable = interactable ?? true;
            canvasGroup.blocksRaycasts = blocksRaycasts ?? true;
            canvasGroup.ignoreParentGroups = ignoreParentGroups ?? false;
        }
    }
}