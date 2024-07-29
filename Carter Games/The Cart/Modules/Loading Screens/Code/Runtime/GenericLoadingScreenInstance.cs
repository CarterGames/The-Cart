#if CARTERGAMES_CART_MODULE_LOADINGSCREENS

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

using CarterGames.Cart.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Cart.Modules.LoadingScreens
{
    /// <summary>
    /// Handles the generic loading screen instance in the scene.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class GenericLoadingScreenInstance : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [Header("General")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Animator loadingAnim;
        
        [Header("UI Elements")]
        [SerializeField] private Image backgroundGraphic;
        [SerializeField] private TMP_Text loadingTitleLabel;
        [SerializeField] private TMP_Text loadingSubTextLabel;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets if the screen is currently shown or not.
        /// </summary>
        private bool IsShown => canvasGroup.alpha.Equals(1);
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Sets the screen to display the info provided
        /// </summary>
        /// <param name="title">The title to set to.</param>
        /// <param name="subTitle">The subtitle to set to.</param>
        /// <param name="backgroundAlpha">The background alpha to set.</param>
        public void Run(string title, string subTitle = "", float backgroundAlpha = 1f)
        {
            canvasGroup.alpha = 1;
            loadingAnim.enabled = true;
            loadingTitleLabel.SetText(title);
            loadingSubTextLabel.SetText(subTitle);
            backgroundGraphic.color = backgroundGraphic.color.SetAlpha(backgroundAlpha);
        }
        
        
        /// <summary>
        /// Sets the screen to display the info provided
        /// </summary>
        /// <param name="title">The title to set to.</param>
        /// <param name="backgroundAlpha">The background alpha to set.</param>
        public void Run(string title, float backgroundAlpha = 1f)
        {
            canvasGroup.alpha = 1;
            loadingAnim.enabled = true;
            loadingTitleLabel.SetText(title);
            loadingSubTextLabel.SetText(string.Empty);
            backgroundGraphic.color = backgroundGraphic.color.SetAlpha(backgroundAlpha);
        }


        /// <summary>
        /// Stops the display from showing when called.
        /// </summary>
        public void Stop()
        {
            canvasGroup.alpha = 0;
            loadingAnim.enabled = false;
        }


        /// <summary>
        /// Sets the title to the entered value.
        /// </summary>
        /// <param name="text">The test to set to.</param>
        public void SetTitle(string text)
        {
            loadingTitleLabel.SetText(text);
        }
        
        
        /// <summary>
        /// Sets the subtitle to the entered value.
        /// </summary>
        /// <param name="text">The test to set to.</param>
        public void SetSubTitle(string text)
        {
            loadingSubTextLabel.SetText(text);
        }
    }
}

#endif