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

using CarterGames.Cart.Editor;

namespace CarterGames.Cart.Crates.DevEnvironments.Editor
{
    public class PreBuildCheckDevEnvironment : PreBuildCheckBase
    {
        public override int Priority => 0;
        
        
        public override PreBuildCheckResult ValidateCanBuild()
        {
            if (Dialogue.Display("Dev Environment",
                    $"Are you sure you want to make a build in the current dev environment: {EnvironmentDetection.CurrentEnvironment}?",
                    "Continue", "Cancel"))
            {
                return PreBuildCheckResult.Succeed();
            }
            else
            {
                return PreBuildCheckResult.Failed(
                    "User cancel due to dev environment not set as desired. Change environment and build again once the project has compiled.");
            }
        }
    }
}

#endif