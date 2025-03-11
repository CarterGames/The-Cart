#if CARTERGAMES_CART_MODULE_DEVENVIRONMENTS && UNITY_EDITOR

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

using System.Collections.Generic;

namespace CarterGames.Cart.Modules.DevEnvironments.Editor
{
    /// <summary>
    /// Handles switching dev environments.
    /// </summary>
    public static class ProjectEnvironmentHandler
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private const string Development = "DEV_ENVIRONMENT_DEVELOPMENT";
        private const string Test = "DEV_ENVIRONMENT_TEST";
#if CARTERGAMES_CART_MODULE_PRESS
        private const string Press = "DEV_ENVIRONMENT_PRESS";
#endif

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Switched the dev environment when called.
        /// </summary>
        /// <param name="environment">The setup to change to.</param>
        public static void SetEnvironment(DevelopmentEnvironments environment)
        {
            var toAdd = new List<string>();
            var toRemove = new List<string>();
            
            switch (environment)
            {
                case DevelopmentEnvironments.Development:

                    toAdd = new List<string>() { Development };
                    toRemove = new List<string>()
                    {
                        Test,
#if CARTERGAMES_CART_MODULE_PRESS
                        Press,
#endif
                    };
                    
                    break;
                case DevelopmentEnvironments.Release:
                    
                    toAdd = new List<string>();
                    toRemove = new List<string>()
                    {
                        Development,
                        Test,
#if CARTERGAMES_CART_MODULE_PRESS
                        Press,
#endif
                    };
                    
                    break;
                case DevelopmentEnvironments.Test:
                    
                    toAdd = new List<string>() { Test };
                    toRemove = new List<string>()
                    {
                        Development,
#if CARTERGAMES_CART_MODULE_PRESS
                        Press,
#endif
                    };
                    
	                break;
#if CARTERGAMES_CART_MODULE_PRESS
                case DevelopmentEnvironments.Press:

                    toAdd = new List<string>() { Press };
                    toRemove = new List<string>()
                    {
                        Test,
                        Development,
                    };

                    break;
#endif
                default:
                    break;
            }
            
            CscFileHandler.EditDefines(toAdd, toRemove);
        }
    }
}

#endif