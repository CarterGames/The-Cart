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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarterGames.Cart.Management;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates
{
    /// <summary>
    /// Manages the crates system with API to query crate states.
    /// </summary>
    /// <remarks>
    /// Crates are modular blocks that can be toggled on or off freely to add extra behaviour to the library.
    /// Make your own by inheriting from the <see cref="Crate"/> class.
    /// </remarks>
    public static class CrateManager
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Settings Keys
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static readonly string IsProcessingKey = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_CrateManager_IsProcessing";
        private static readonly string HasPromptedKey = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_CrateManager_HasPrompted";
        
        // Crates
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Initial collections
        private static Crate[] allCratesCache;
        private static ExternalCrate[] allPackagedCratesCache;
        
        // Extra collections
        private static IReadOnlyDictionary<string, Crate> cratesByNameLookupCache;
        private static IReadOnlyDictionary<string, List<Crate>> cratesByAuthorLookupCache;
        private static IReadOnlyDictionary<string, Crate> createsByTechnicalNameLookupCache;
        private static IReadOnlyDictionary<string, ExternalCrate> externalCratesByNameLookupCache;
        private static IReadOnlyDictionary<string, List<ExternalCrate>> externalCratesByAuthorLookupCache;
        private static IReadOnlyDictionary<string, ExternalCrate> externalCreatesByTechnicalNameLookupCache;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// All the crates found in the project.
        /// </summary>
        public static IEnumerable<Crate> AllCrates
        {
            get
            {
                if (!allCratesCache.IsEmptyOrNull()) return allCratesCache;
                
                allCratesCache = AssemblyHelper.GetClassesOfType<Crate>(false)
                    .OrderBy(t => t.CrateAuthor)
                    .ThenBy(t => t.CrateName)
                    .ToArray();
                
                return allCratesCache;
            }
        }

        
        private static IReadOnlyDictionary<string, Crate> CratesLookupByName =>
            CacheRef.GetOrAssign(ref cratesByNameLookupCache, GenerateCrateLookupByName);
        
        private static IReadOnlyDictionary<string, Crate> CratesLookupByTechnicalName =>
            CacheRef.GetOrAssign(ref createsByTechnicalNameLookupCache, GenerateCrateLookupByTechName);

        private static IReadOnlyDictionary<string, List<Crate>> CratesLookupByAuthor =>
            CacheRef.GetOrAssign(ref cratesByAuthorLookupCache, GenerateCrateLookupByAuthor);
        
        private static IReadOnlyDictionary<string, List<Crate>> AllCratesLookupByAuthor =>
            CacheRef.GetOrAssign(ref cratesByAuthorLookupCache, GenerateAllCratesLookupByAuthor);
        
        
        public static IEnumerable<ExternalCrate> AllExternalCrates
        {
            get
            {
                if (!allPackagedCratesCache.IsEmptyOrNull()) return allPackagedCratesCache;
                allPackagedCratesCache = AssemblyHelper.GetClassesOfType<ExternalCrate>(false).ToArray();
                return allPackagedCratesCache;
            }
        }
        
        
        private static IReadOnlyDictionary<string, ExternalCrate> ExternalCratesLookupByName =>
            CacheRef.GetOrAssign(ref externalCratesByNameLookupCache, GenerateExternalCrateLookupByName);
        
        private static IReadOnlyDictionary<string, ExternalCrate> ExternalCratesLookupByTechnicalName =>
            CacheRef.GetOrAssign(ref externalCreatesByTechnicalNameLookupCache, GenerateExternalCrateLookupByTechName());

        private static IReadOnlyDictionary<string, List<ExternalCrate>> ExternalCratesLookupByAuthor =>
            CacheRef.GetOrAssign(ref externalCratesByAuthorLookupCache, GenerateExternalCrateLookupByAuthor);
        
        public static bool AnyCratesInProject => cratesByNameLookupCache.Count > 0;
        
        
        /// <summary>
        /// Gets if the crates are currently being processed.
        /// </summary>
        public static bool IsProcessing
        {
            get => (bool) PerUserSettings.GetOrCreateValue<bool>(IsProcessingKey, SettingType.SessionState, false);
            set => PerUserSettings.SetValue<bool>(IsProcessingKey, SettingType.SessionState, value);
        }
        
        
        /// <summary>
        /// Gets if the user has been prompted or not.
        /// </summary>
        public static bool HasPrompted
        {
            get => (bool) PerUserSettings.GetOrCreateValue<bool>(HasPromptedKey, SettingType.SessionState, false);
            set => PerUserSettings.SetValue<bool>(HasPromptedKey, SettingType.SessionState, value);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static IReadOnlyDictionary<string, Crate> GenerateCrateLookupByName()
        {
            var byName = new Dictionary<string, Crate>();

            foreach (var crate in AllCrates)
            {
                byName.Add(crate.CrateName, crate);
            }
            
            return byName;
        }
        
        
        private static IReadOnlyDictionary<string, ExternalCrate> GenerateExternalCrateLookupByName()
        {
            var byName = new Dictionary<string, ExternalCrate>();

            foreach (var crate in AllExternalCrates)
            {
                byName.Add(crate.CrateName, crate);
            }
            
            return byName;
        }
        
        
        private static IReadOnlyDictionary<string, Crate> GenerateCrateLookupByTechName()
        {
            var byName = new Dictionary<string, Crate>();

            foreach (var crate in AllCrates)
            {
                byName.Add(crate.CrateTechnicalName, crate);
            }
            
            return byName;
        }
        
        
        private static IReadOnlyDictionary<string, ExternalCrate> GenerateExternalCrateLookupByTechName()
        {
            var byName = new Dictionary<string, ExternalCrate>();

            foreach (var crate in AllExternalCrates)
            {
                byName.Add(crate.CrateTechnicalName, crate);
            }
            
            return byName;
        }
        
        
        private static IReadOnlyDictionary<string, List<Crate>> GenerateCrateLookupByAuthor()
        {
            var byAuthor = new Dictionary<string, List<Crate>>();

            foreach (var crate in AllCrates)
            {
                if (!byAuthor.ContainsKey(crate.CrateAuthor))
                {
                    byAuthor.Add(crate.CrateAuthor, new List<Crate>() { crate });
                }
                else
                {
                    byAuthor[crate.CrateAuthor].Add(crate);
                }
            }
            
            return byAuthor;
        }
        
        
        private static IReadOnlyDictionary<string, List<Crate>> GenerateAllCratesLookupByAuthor()
        {
            var byAuthor = new Dictionary<string, List<Crate>>();

            foreach (var entry in CratesLookupByAuthor)
            {
                byAuthor.Add(entry.Key, entry.Value);
            }
            
            foreach (var entry in ExternalCratesLookupByAuthor)
            {
                byAuthor.Add(entry.Key, new List<Crate>(entry.Value));
            }
            
            return byAuthor;
        }


        private static IReadOnlyDictionary<string, List<ExternalCrate>> GenerateExternalCrateLookupByAuthor()
        {
            var byAuthor = new Dictionary<string, List<ExternalCrate>>();

            foreach (var crate in AllExternalCrates)
            {
                if (!byAuthor.ContainsKey(crate.CrateAuthor))
                {
                    byAuthor.Add(crate.CrateAuthor, new List<ExternalCrate>() { crate });
                }
                else
                {
                    byAuthor[crate.CrateAuthor].Add(crate);
                }
            }
            
            return byAuthor;
        }
        
        
        /// <summary>
        /// Gets if the crate is installed.
        /// </summary>
        /// <param name="crate">The crate to check.</param>
        /// <returns>Bool</returns>
        public static bool IsEnabled(Crate crate)
        {
            return CscFileHandler.HasDefine(crate);
        }


        /// <summary>
        /// Gets is a crate is an external crate or not.
        /// </summary>
        /// <param name="crate">The crate to query.</param>
        /// <returns>Boolean</returns>
        public static bool IsExternal(Crate crate)
        {
            return ExternalCratesLookupByName.ContainsKey(crate.CrateName);
        }


        public static ExternalCrate GetAsExternal(Crate crate)
        {
            if (!IsExternal(crate)) return null;
            return ExternalCratesLookupByTechnicalName[crate.CrateTechnicalName];
        }
        

        /// <summary>
        /// Gets a crate class instance from its name.
        /// </summary>
        /// <remarks>
        /// If multiple exist it'll return the first it finds.
        /// </remarks>
        /// <param name="crateName">The name to look for.</param>
        /// <returns>Crate</returns>
        public static Crate GetCrateByName(string crateName)
        {
            if (CratesLookupByName.ContainsKey(crateName))
            {
                return CratesLookupByName[crateName];
            }
            
            if (ExternalCratesLookupByName.ContainsKey(crateName))
            {
                return ExternalCratesLookupByName[crateName];
            }

            return null;
        }
        
        
        /// <summary>
        /// Gets a crate class instance from its tech name.
        /// </summary>
        /// <remarks>
        /// If multiple exist it'll return the first it finds.
        /// </remarks>
        /// <param name="crateTechnicalName">The tech name to look for.</param>
        /// <returns>Crate</returns>
        public static Crate GetCrateByTechnicalName(string crateTechnicalName)
        {
            if (CratesLookupByTechnicalName.ContainsKey(crateTechnicalName))
            {
                return CratesLookupByTechnicalName[crateTechnicalName];
            }
            
            if (ExternalCratesLookupByTechnicalName.ContainsKey(crateTechnicalName))
            {
                return ExternalCratesLookupByTechnicalName[crateTechnicalName];
            }

            return null;
        }
        
        
        /// <summary>
        /// Gets all the crates by a particular author.
        /// </summary>
        /// <remarks>
        /// Is just a sting check, so make sure you enter the author field the same each time. 
        /// </remarks>
        /// <param name="crateAuthor">The author to search for.</param>
        /// <returns>IEnumerable of Crates</returns>
        public static IEnumerable<Crate> GetAllCratesFromAuthor(string crateAuthor)
        {
            if (CratesLookupByAuthor.ContainsKey(crateAuthor))
            {
                return CratesLookupByAuthor[crateAuthor];
            }
            
            if (ExternalCratesLookupByAuthor.ContainsKey(crateAuthor))
            {
                return ExternalCratesLookupByAuthor[crateAuthor];
            }

            return null;
        }


        public static IReadOnlyDictionary<string, List<Crate>> GetAllCratesInProjectByAuthor()
        {
            return AllCratesLookupByAuthor;
        }
        
        
        /// <summary>
        /// Installs the crate entered when called.
        /// </summary>
        /// <param name="crate">The crate to install.</param>
        public static void EnableDefines(Crate crate)
        {
            var toInstall = new List<Crate>();
            
            if (crate.PreRequisites.Length > 0)
            {
                foreach (var preReq in crate.PreRequisites)
                {
                    if (IsEnabled(preReq)) continue;
                    toInstall.Add(preReq);
                }
            }
            
            toInstall.Add(crate);

            if (toInstall.Count > 1)
            {
                CscFileHandler.AddDefine(toInstall);
            }
            else
            {
                CscFileHandler.AddDefine(toInstall[0]);
            }
            
            AssetDatabase.Refresh();
        }
        
        
        public static void DisableDefines(Crate crate)
        {
            var toUninstall = new List<Crate>();
            HasPrompted = false;

            if (AllCrates.Any(t =>
                    t.PreRequisites.Any(x => x.Equals(crate))))
            {
                toUninstall.AddRange(AllCrates
                    .Where(t => t.PreRequisites.Any(x => x.Equals(crate) && IsEnabled(t)))
                    .ToList());
            }

            toUninstall.Add(crate);

            if (toUninstall.Count > 1)
            {
                CscFileHandler.RemoveDefine(toUninstall);
            }
            else
            {
                CscFileHandler.RemoveDefine(toUninstall[0]);
            }
        }
    }
}