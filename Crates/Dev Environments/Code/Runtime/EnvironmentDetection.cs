#if CARTERGAMES_CART_CRATE_DEVENVIRONMENTS

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

namespace CarterGames.Cart.Crates.DevEnvironments
{
	/// <summary>
	/// Use to get the current environment.
	/// </summary>
    public static class EnvironmentDetection
    {
	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Properties
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    /// <summary>
	    /// Gets the current environment.
	    /// </summary>
        public static DevelopmentEnvironments CurrentEnvironment
        {
            get
            {
#if DEV_ENVIRONMENT_DEVELOPMENT
                return DevelopmentEnvironments.Development;
#elif DEV_ENVIRONMENT_TEST
                return DevelopmentEnvironments.Test;
#elif DEV_ENVIRONMENT_PRESS
                return DevelopmentEnvironments.Press;
#else
                return DevelopmentEnvironments.Release;
#endif
            }
        }
    }
}

#endif