﻿#if CARTERGAMES_CART_MODULE_LOCALIZATION

/*
 * Copyright (c) 2025 Carter Games
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

using CarterGames.Cart.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Cart.Modules.Localization
{
    /// <summary>
    /// A simple component for a localized UI Image element.
    /// </summary>
    [AddComponentMenu("Carter Games/The Cart/Modules/Localization/Basic Components/LocalizedUIImage")]
    public class LocalizedUIImage : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] [LocalizationId] private string locId;
        [SerializeField] private Image image;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// A reference to the component, assumed on same object or child if auto-finding.
        /// </summary>
        private Image ImageRef => CacheRef.GetOrAssign(ref image, GetComponentInChildren<Image>);

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private void OnEnable()
        {
            LocalizationManager.LanguageChanged.Add(UpdateImage);
            UpdateImage();
        }


        private void OnDestroy()
        {
            LocalizationManager.LanguageChanged.Remove(UpdateImage);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Updates the image sprite when called with the latest value.
        /// </summary>
        private void UpdateImage()
        {
            ImageRef.sprite = LocalizationManager.GetSprite(locId);
        }


        /// <summary>
        /// Forces the display to update when called.
        /// </summary>
        public void ForceUpdateDisplay()
        {
            UpdateImage();
        }
    }
}

#endif