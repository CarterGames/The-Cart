/*
 * Copyright (c) 2025 Carter Games
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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management;
using CarterGames.Cart.Management.Editor;
using UnityEngine;

namespace CarterGames.Cart.Crates
{
    public static class CrateManager
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Settings Keys
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static readonly string IsProcessingKey = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_CrateManager_IsProcessing";
        private static readonly string HasPromptedKey = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_CrateManager_HasPrompted";
        
        
        // Colours
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        public static readonly Color UninstallCol = ColorExtensions.HtmlStringToColor("#ff9494");
        public static readonly Color InstallCol = ColorExtensions.HtmlStringToColor("#71ff50");


        // Crates
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static Crate[] allCratesCache;
        private static IReadOnlyDictionary<string, Crate> cratesByNameLookupCache;
        private static IReadOnlyDictionary<string, List<Crate>> cratesByAuthorLookupCache;
        private static ExternalCrate[] allPackagedCratesCache;

        
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

        
        public static IReadOnlyDictionary<string, Crate> CratesLookupByName =>
            CacheRef.GetOrAssign(ref cratesByNameLookupCache, GetLookupByName);

        public static IReadOnlyDictionary<string, List<Crate>> CratesLookupByAuthor =>
            CacheRef.GetOrAssign(ref cratesByAuthorLookupCache, GetLookupByAuthor);
        
        
        public static IEnumerable<ExternalCrate> AllPackagedCrates
        {
            get
            {
                if (!allPackagedCratesCache.IsEmptyOrNull()) return allPackagedCratesCache;
                allPackagedCratesCache = AssemblyHelper.GetClassesOfType<ExternalCrate>(false).ToArray();
                return allPackagedCratesCache;
            }
        }
        
        
        
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

        private static IReadOnlyDictionary<string, Crate> GetLookupByName()
        {
            var byName = new Dictionary<string, Crate>();

            foreach (var crate in AllCrates)
            {
                byName.Add(crate.CrateName, crate);
            }
            
            return byName;
        }
        
        
        private static IReadOnlyDictionary<string, List<Crate>> GetLookupByAuthor()
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
        /// Gets the colourised status icon for the crate's status.
        /// </summary>
        /// <param name="crate">The crate to get the icon for.</param>
        /// <returns>The rich text string for the icon.</returns>
        public static string GetCrateStatusIcon(Crate crate)
        {
            if (!IsEnabled(crate)) return GeneralUtilEditor.CrossIconColourised;
            return GeneralUtilEditor.TickIconColourised;
        }



        public static Crate GetCrateFromName(string crateName)
        {
            return CratesLookupByName.ContainsKey(crateName) 
                ? CratesLookupByName[crateName] 
                : null;
        }
        
        
        public static IEnumerable<Crate> GetCratesFromAuthor(string crateAuthor)
        {
            return CratesLookupByAuthor.ContainsKey(crateAuthor) 
                ? CratesLookupByAuthor[crateAuthor] 
                : null;
        }
        
        
        public static void AddPackage(GitPackageInfo gitPackage)
        {
            var path = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            var jsonString = File.ReadAllText(path);
            int indexOfLastBracket = jsonString.IndexOf("}");
            string dependenciesSubstring = jsonString.Substring(0, indexOfLastBracket);
            var endOfLastPackage = dependenciesSubstring.LastIndexOf("\"");
            string oldValue = jsonString.Substring(endOfLastPackage, indexOfLastBracket - endOfLastPackage);
            jsonString = jsonString.Insert(endOfLastPackage + 1,
                $", \n \"{gitPackage.technicalName}\": \"{gitPackage.packageUrl}\"");
            File.WriteAllText(path, jsonString);
            UnityEditor.PackageManager.Client.Resolve();
        }
        
        
        public static void RemovePackage(GitPackageInfo gitPackage)
        {
            var path = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            var jsonString = File.ReadAllText(path);
            
            jsonString = jsonString.Replace($"    \"{gitPackage.technicalName}\": \"{gitPackage.packageUrl}\",", string.Empty);
            
            File.WriteAllText(path, jsonString);
            UnityEditor.PackageManager.Client.Resolve();
        }
        
        
        public static bool CheckPackageInstalled(string packageName)        
        {
            var path = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            var jsonString = File.ReadAllText(path);
            return jsonString.Contains(packageName);        
        }

        
        public static bool IsPackageInstalled(ExternalCrate crate)
        {
            return CheckPackageInstalled(crate.PackageInfo.technicalName);
        }
    }
}