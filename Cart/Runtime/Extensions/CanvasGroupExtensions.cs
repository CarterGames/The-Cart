/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using UnityEngine;

namespace CarterGames.Cart
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