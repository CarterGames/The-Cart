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

namespace CarterGames.Cart.Modules.Localization
{
    /// <summary>
    /// A simple component for a localized SpriteRenderer element.
    /// </summary>
    [AddComponentMenu("Carter Games/The Cart/Modules/Localization/Basic Components/LocalizedSpriteRenderer")]
    public class LocalizedSpriteRenderer : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] [LocalizationId] private string locId;
        [SerializeField] private SpriteRenderer spriteRenderer;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// A reference to the component, assumed on same object or child if auto-finding.
        /// </summary>
        private SpriteRenderer RendererRef => CacheRef.GetOrAssign(ref spriteRenderer, GetComponentInChildren<SpriteRenderer>);

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private void OnEnable()
        {
            LocalizationManager.LanguageChanged.Add(UpdateSprite);
            UpdateSprite();
        }


        private void OnDestroy()
        {
            LocalizationManager.LanguageChanged.Remove(UpdateSprite);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Updates the sprite when called with the latest value.
        /// </summary>
        private void UpdateSprite()
        {
            RendererRef.sprite = LocalizationManager.GetSprite(locId);
        }


        /// <summary>
        /// Forces the display to update when called.
        /// </summary>
        public void ForceUpdateDisplay()
        {
            UpdateSprite();
        }
    }
}

#endif