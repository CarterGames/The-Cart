#if CARTERGAMES_CART_MODULE_LOADINGSCREENS

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

using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Events;
using UnityEngine;

namespace CarterGames.Cart.Modules.LoadingScreens
{
    /// <summary>
    /// Handles the generic loading screen system.
    /// </summary>
    public static class GenericLoadingScreen
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static GenericLoadingScreenInstance instance;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Raises when the loading screen is toggled.
        /// </summary>
        public static readonly Evt<bool> Toggled = new Evt<bool>();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets if the loading screen is active or not.
        /// </summary>
        private static bool IsShown { get; set; }


        /// <summary>
        /// Gets the current instance of the loading screen if it is referenced.
        /// </summary>
        private static GenericLoadingScreenInstance ActiveInstance
        {
            get
            {
                if (instance != null) return instance;
                var newInstance = Object.Instantiate(DataAccess.GetAsset<DataAssetSettingsLoadingScreens>().LoadingScreenPrefab);
                Object.DontDestroyOnLoad(newInstance);
                instance = newInstance;
                return instance;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Shows the loading screen when called.
        /// </summary>
        /// <param name="title">The title to display.</param>
        /// <param name="subTitle">The subtitle to show.</param>
        /// <param name="backgroundAlpha">The background alpha to set.</param>
        public static void Show(string title, string subTitle = "", float backgroundAlpha = 1f)
        {
            if (IsShown) return;
            
            ActiveInstance.Run(title, subTitle, Mathf.Clamp01(backgroundAlpha));
            IsShown = true;
            Toggled.Raise(true);
        }


        /// <summary>
        /// Shows the loading screen when called.
        /// </summary>
        /// <param name="title">The title to display.</param>
        /// <param name="backgroundAlpha">The background alpha to set.</param>
        public static void Show(string title, float backgroundAlpha = 1f)
        {
            if (IsShown) return;
            
            ActiveInstance.Run(title, Mathf.Clamp01(backgroundAlpha));
            IsShown = true;
            Toggled.Raise(true);
        }


        /// <summary>
        /// Hides the loading screen when called.
        /// </summary>
        public static void Hide()
        {
            if (!IsShown) return;
            
            ActiveInstance.Stop();
            IsShown = false;
            Toggled.Raise(false);
        }
    }
}

#endif