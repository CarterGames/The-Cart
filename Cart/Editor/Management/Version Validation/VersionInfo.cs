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

namespace CarterGames.Cart.Management.Editor
{
    /// <summary>
    /// The info used in the version validation system.
    /// </summary>
    public static class VersionInfo
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The Url to request the versions from.
        /// </summary>
        public const string ValidationUrl = "https://carter.games/validation/versions.json";
        
        
        /// <summary>
        /// The download Url for the latest version of this package.
        /// </summary>
        public const string DownloadBaseUrl = "https://github.com/CarterGames/The-Cart/releases/tag/";
        
        
        /// <summary>
        /// The key of the package to get from the JSON blob.
        /// </summary>
        public const string Key = "The Cart";


        /// <summary>
        /// The version string for the package.
        /// </summary>
        public static string ProjectVersionNumber => CartVersionData.VersionNumber;
    }
}