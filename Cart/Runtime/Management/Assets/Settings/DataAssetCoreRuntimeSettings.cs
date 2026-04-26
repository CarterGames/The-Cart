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
using CarterGames.Cart.Data;
using CarterGames.Cart.Random;
using CarterGames.Cart.Save;
using UnityEngine;

namespace CarterGames.Cart.Management
{
    /// <summary>
    /// Handles any runtime specific settings for the package.
    /// </summary>
    [Serializable]
    public sealed class DataAssetCoreRuntimeSettings : DataAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        // Rng
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        [SerializeField] private bool isRngExpanded;
        [SerializeField] private AssemblyClassDef rngProviderTypeDef = typeof(UnityRngProvider);
        private IRngProvider cacheRngProvider;
        [SerializeField] private int rngSystemSeed = Guid.NewGuid().GetHashCode();
        [SerializeField] private string rngAleaSeed = Guid.NewGuid().ToString();
        
        // Logging
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        [SerializeField] private bool isLoggingExpanded;
        [SerializeField] private bool loggingUseCartLogs = true;
        [SerializeField] private bool useLogsInProductionBuilds = false;
        [SerializeField] private bool forceShowErrors = true;
        
        // Basic Save
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        [SerializeField] private AssemblyClassDef saveMethodTypeDef = typeof(SaveMethodBasic);
        private ISaveMethod cacheSaveMethod;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Rng
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The current RNG provider for the 
        /// </summary>
        public IRngProvider RngProvider => CacheRef.GetOrAssign(ref cacheRngProvider, rngProviderTypeDef.GetTypeInstance<IRngProvider>);
        public AssemblyClassDef RngProviderAssemblyClassDef => rngProviderTypeDef;

        
        /// <summary>
        /// The System Rng Seed.
        /// </summary>
        public int RngSystemRngSeed
        {
            get => rngSystemSeed;
            set => rngSystemSeed = value;
        }

        
        /// <summary>
        /// The Alea Rng Seed.
        /// </summary>
        public string RngAleaRngSeed
        {
            get => rngAleaSeed;
            set => rngAleaSeed = value;
        }
        
        // Logging
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets if the logs should be shown at all.
        /// </summary>
        public bool LoggingUseCartLogs => loggingUseCartLogs;
        
        
        /// <summary>
        /// Gets if the logs should appear in production builds, by default they will not.
        /// </summary>
        public bool UseLogsInProductionBuilds => useLogsInProductionBuilds;


        /// <summary>
        /// Defines if the setup will force error logs to show even if the category is disabled.
        /// </summary>
        public bool ForceShowErrors => forceShowErrors;
        
        // Basic Save
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public ISaveMethod SaveMethodType =>
            CacheRef.GetOrAssign(ref cacheSaveMethod, saveMethodTypeDef.GetTypeInstance<ISaveMethod>);
    }
}
