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
using CarterGames.Cart.Events;
using UnityEngine;
using UnityEngine.Networking;

namespace CarterGames.Cart.Management.Editor
{
    /// <summary>
    /// Handles checking for the latest version.
    /// </summary>
    public static class VersionChecker
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The download URL for the latest version.
        /// </summary>
        public static string DownloadURL => VersionInfo.DownloadBaseUrl + Versions.Data.Version;
        

        /// <summary>
        /// Gets if the latest version is this version.
        /// </summary>
        public static bool IsLatestVersion => Versions.Data.VersionNumber.Equals(new Version(VersionInfo.ProjectVersionNumber));
        
        
        /// <summary>
        /// Gets if the version here is higher that the latest version.
        /// </summary>
        public static bool IsNewerVersion => new Version(VersionInfo.ProjectVersionNumber).CompareTo(Versions.Data.VersionNumber) > 0;
        
        
        /// <summary>
        /// Gets the version data downloaded.
        /// </summary>
        public static VersionPacket Versions { get; private set; }

        
        /// <summary>
        /// The latest version string.
        /// </summary>
        public static string LatestVersionNumberString => Versions.Data.Version;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Raises when the data has been downloaded.
        /// </summary>
        public static Evt ResponseReceived { get; private set; } = new Evt();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the latest version data when called.
        /// </summary>
        public static void GetLatestVersions()
        {
            RequestLatestVersionData();
        }


        /// <summary>
        /// Makes the web request & handles the response.
        /// </summary>
        private static void RequestLatestVersionData()
        {
            var request = UnityWebRequest.Get(VersionInfo.ValidationUrl);
            var async = request.SendWebRequest();

            async.completed += (a) =>
            {
                if (request.result != UnityWebRequest.Result.Success) return;

                Versions = JsonUtility.FromJson<VersionPacket>(request.downloadHandler.text);
                ResponseReceived.Raise();
            };
        }
    }
}