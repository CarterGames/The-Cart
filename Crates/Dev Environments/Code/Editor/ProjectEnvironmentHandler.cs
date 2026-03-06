#if CARTERGAMES_CART_CRATE_DEVENVIRONMENTS && UNITY_EDITOR

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

using System.Collections.Generic;

namespace CarterGames.Cart.Crates.DevEnvironments.Editor
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
#if CARTERGAMES_CART_CRATE_PRESS
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
#if CARTERGAMES_CART_CRATE_PRESS
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
#if CARTERGAMES_CART_CRATE_PRESS
                        Press,
#endif
                    };
                    
                    break;
                case DevelopmentEnvironments.Test:
                    
                    toAdd = new List<string>() { Test };
                    toRemove = new List<string>()
                    {
                        Development,
#if CARTERGAMES_CART_CRATE_PRESS
                        Press,
#endif
                    };
                    
	                break;
#if CARTERGAMES_CART_CRATE_PRESS
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