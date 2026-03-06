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

using System;
using UnityEngine;

namespace CarterGames.Cart.Management
{
    /// <summary>
    /// A copy of the Json data for each entry stored on the server.
    /// </summary>
    [Serializable]
    public sealed class VersionData
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private string key;
        [SerializeField] private string version;
        [SerializeField] private string releaseDate;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The key for the entry.
        /// </summary>
        public string Key
        {
            get => key;
            set => key = value;
        }
        
        
        /// <summary>
        /// The version for the entry.
        /// </summary>
        public string Version
        {
            get => version;
            set => version = value;
        }        
        
        
        /// <summary>
        /// The release date for the entry.
        /// </summary>
        public string ReleaseDate
        {
            get => releaseDate;
            set => releaseDate = value;
        }


        /// <summary>
        /// The version number for the entry.
        /// </summary>
        public Version VersionNumber => new Version(Version);
    }
}