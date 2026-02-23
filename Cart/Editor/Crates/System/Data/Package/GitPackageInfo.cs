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
    /// <summary>
    /// Handles data for an external package that can be imported.
    /// </summary>
    public sealed class GitPackageInfo
    {
        public string displayName;
        public string technicalName;
        public string packageUrl;

        
        public GitPackageInfo(string displayName, string technicalName, string packageUrl)
        {
            this.displayName = displayName;
            this.technicalName = technicalName;
            this.packageUrl = packageUrl;
        }
    }
}