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

using System;
using System.IO;
using System.Linq;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Management.Editor;
using UnityEngine;

namespace CarterGames.Cart.Crate
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
        private static ICrate[] allCratesCache;

        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// All the modules found in the project.
        /// </summary>
        public static ICrate[] AllCrates
        {
            get
            {
                if (!allCratesCache.IsEmptyOrNull()) return allCratesCache;
                allCratesCache = AssemblyHelper.GetClassesOfType<ICrate>(false).ToArray();
                return allCratesCache;
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
        
        /// <summary>
        /// Gets if the crate is installed.
        /// </summary>
        /// <param name="crate">The crate to check.</param>
        /// <returns>Bool</returns>
        public static bool IsEnabled(ICrate crate)
        {
            return CscFileHandler.HasDefine(crate);
        }
        
        
        /// <summary>
        /// Gets the colourised status icon for the crate's status.
        /// </summary>
        /// <param name="crate">The crate to get the icon for.</param>
        /// <returns>The rich text string for the icon.</returns>
        public static string GetCrateStatusIcon(ICrate crate)
        {
            if (!IsEnabled(crate)) return GeneralUtilEditor.CrossIconColourised;
            return GeneralUtilEditor.TickIconColourised;
        }



        public static ICrate GetCrateFromName(string crateName)
        {
            return AllCrates.FirstOrDefault(t => t.Name.Equals(crateName));
        }
        
        
        public static ICrate GetCrateFromDefine(string crateDefine)
        {
            return AllCrates.FirstOrDefault(t => t.Define.Equals(crateDefine));
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
                $", \n \"{gitPackage.packageName}\": \"{gitPackage.CompletePackageUrl}\"");
            File.WriteAllText(path, jsonString);
            UnityEditor.PackageManager.Client.Resolve();
        }
        
        
        public static void RemovePackage(GitPackageInfo gitPackage)
        {
            var path = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            var jsonString = File.ReadAllLines(path);

            var line = jsonString.FirstOrDefault(t =>
                t.Contains($"    \"{gitPackage.packageName}\": \"{gitPackage.packageUrl}"));

            if (!string.IsNullOrEmpty(line))
            {
                jsonString = jsonString.Where(t => t != line).ToArray();
            }
            
            File.WriteAllLines(path, jsonString);
            UnityEditor.PackageManager.Client.Resolve();
        }
        
        
        public static Version GetVersionInstalled(GitPackageInfo gitPackage)        
        {
            if (!CheckPackageInstalled(gitPackage.packageName))
            {
                if (gitPackage.packageVersion != null)
                {
                    return gitPackage.packageVersion;
                }
                
                return new Version(0, 0, 0);
            }
            
            var path = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            var jsonString = File.ReadAllLines(path);
            
            var line = jsonString.FirstOrDefault(t =>
                t.Contains($"    \"{gitPackage.packageName}\": \"{gitPackage.packageUrl}"));

            var versionStringStart = line.IndexOf(".git#") + 5;
            var versionStringValue = line.Substring(versionStringStart, line.Length - versionStringStart - 2);
            
            return new Version(versionStringValue);
        }
        
        
        public static bool CheckPackageInstalled(string packageName)        
        {
            var path = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            var jsonString = File.ReadAllText(path);
            return jsonString.Contains(packageName);     
        }
    }
}