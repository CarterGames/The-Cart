/*
 * Copyright (c) 2024 Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// An extension class for canvas groups.
    /// </summary>
    public static class CanvasGroupExtensions
    {
        /// <summary>
        /// Sets the canvas group to an active state.
        /// </summary>
        /// <param name="canvasGroup">The canvas group to effect.</param>
        /// <param name="active">Should the canvas be active (true = alpha 1, false = alpha 0)</param>
        /// <param name="interactable">Should the canvas group be interactable?</param>
        /// <param name="blocksRaycasts">Should the canvas group block raycasts?</param>
        /// <param name="ignoreParentGroups">Should the canvas group ignore parent groups?</param>
        public static void SetActive(this CanvasGroup canvasGroup, bool active, bool? interactable = null, bool? blocksRaycasts = null, bool? ignoreParentGroups = null)
        {
            canvasGroup.alpha = active ? 1 : 0;
            canvasGroup.interactable = interactable ?? true;
            canvasGroup.blocksRaycasts = blocksRaycasts ?? true;
            canvasGroup.ignoreParentGroups = ignoreParentGroups ?? false;
        }
    }
}