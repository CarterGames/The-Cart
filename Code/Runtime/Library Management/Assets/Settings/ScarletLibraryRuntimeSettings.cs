/*
 * Copyright (c) 2018-Present Carter Games
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

using System;
using Scarlet.Random;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scarlet.Management
{
    /// <summary>
    /// Handles any runtime specific settings for the package.
    /// </summary>
    [CreateAssetMenu(fileName = "Runtime Settings Asset", menuName = "Scarlet Library/Management/Setttings (Runtime)")]
    public sealed class ScarletLibraryRuntimeSettings : ScarletLibraryAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        // Rng
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        [SerializeField] private bool isRngExpanded;
        [SerializeField] private RngProviders rngRngProvider;
        [SerializeField] private int rngSystemSeed = Guid.NewGuid().GetHashCode();
        [SerializeField] private string rngAleaSeed = Guid.NewGuid().ToString();
        
        // Logging
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        [SerializeField] private bool isLoggingExpanded;
        [SerializeField] private bool loggingUseScarletLogs = true;
        [SerializeField] private bool useLogsInProductionBuilds = false;
        
        // Game Ticks
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        [SerializeField] private bool isGameTicksExpanded;
        [SerializeField] private int gameTickTicksPerSecond = 5;
        [SerializeField] private bool gameTickUseUnscaledTime;
        [SerializeField] private bool gameTickUseGlobalTicker = true;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Rng
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The current RNG provider for the 
        /// </summary>
        public RngProviders RngRngProvider => rngRngProvider;

        
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
        public bool LoggingUseScarletLogs => loggingUseScarletLogs;
        
        
        /// <summary>
        /// Gets if the logs should appear in production builds, by default they will not.
        /// </summary>
        public bool UseLogsInProductionBuilds => useLogsInProductionBuilds;
        
        // Game Ticks
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The number of ticks per second.
        /// </summary>
        public int GameTickTicksPerSecond => gameTickTicksPerSecond;
        
        
        /// <summary>
        /// Should the tick system be in unscaled time?
        /// </summary>
        public bool GameTickUseUnscaledTime => gameTickUseUnscaledTime;
        
        
        /// <summary>
        /// Should the global ticker be initialized & used at runtime?
        /// </summary>
        public bool GameTickUseGlobalTicker => gameTickUseGlobalTicker;
    }
}
