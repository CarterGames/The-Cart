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

namespace CarterGames.Cart.Crates
{
    public static class CrateValidator
    {
        public static bool IsCrateSetupValid(string crateTechnicalName, out string failReason)
        {
            failReason = string.Empty;
            
            if (!CrateManager.TryGetCrateByTechnicalName(crateTechnicalName, out var crate))
            {
                failReason = $"Crate not found under technical name in the project. {crateTechnicalName}.";
                return false;
            }

            if (string.IsNullOrEmpty(crate.CrateName))
            {
                failReason = "Crate name is empty. Please enter a name in the crate define class.";
                return false;
            }
            
            if (string.IsNullOrEmpty(crate.CrateDescription))
            {
                failReason = "Crate description is empty. Please enter a description in the crate define class.";
                return false;
            }
            
            if (string.IsNullOrEmpty(crate.CrateAuthor))
            {
                failReason = "Crate author is empty. Please enter an author in the crate define class.";
                return false;
            }

            return true;
        }
    }
}