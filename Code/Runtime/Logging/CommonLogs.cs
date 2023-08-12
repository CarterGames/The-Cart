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

using CarterGames.Common.Utility;
using UnityEngine;

namespace CarterGames.Common.Logs
{
    /// <summary>
    /// A custom logger class to aid show logs for common library scripts.
    /// </summary>
    public static class CommonLogs
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private const string LogPrefix = "<color=#E36B6B><b>Common</b></color> | ";
        private const string WarningPrefix = "<color=#D6BA64><b>Warning</b></color> | ";
        private const string ErrorPrefix = "<color=#E77A7A><b>Error</b></color> | ";

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets if the build is a production build.
        /// </summary>
        private static bool IsProductionBuild => !Application.isEditor && !Debug.isDebugBuild;

        
        /// <summary>
        /// Gets if the system should show logs (in a production build) based on the current settings.
        /// </summary>
        private static bool ShowLogsOnProductionBuild => IsProductionBuild && UtilRuntime.Settings.UseLogsInProductionBuilds;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Displays a normal debug message for the build versions asset...
        /// </summary>
        /// <param name="message">The message to show...</param>
        /// <param name="editorOnly">Stops the log running outside of the editor (set to false to runtime build logs)...</param>
        public static void Normal<T>(string message, bool editorOnly = true)
        {
            if (!UtilRuntime.Settings.LoggingUseCommonLogs) return;
            if (!ShowLogsOnProductionBuild) return;
            Debug.Log($"{LogPrefix}<color=#D6BA64>{typeof(T).Name}</color>: {message}");
        }


        /// <summary>
        /// Displays a warning debug message for the build versions asset...
        /// </summary>
        /// <param name="message">The message to show...</param>
        /// <param name="editorOnly">Stops the log running outside of the editor (set to false to runtime build logs)...</param>
        public static void Warning<T>(string message, bool editorOnly = true)
        {
            if (!UtilRuntime.Settings.LoggingUseCommonLogs) return;
            if (!ShowLogsOnProductionBuild) return;
            Debug.LogWarning($"{LogPrefix}{WarningPrefix}<color=#D6BA64>{typeof(T).Name}</color>: {message}");
        }
        
        
        /// <summary>
        /// Displays a error debug message for the build versions asset...
        /// </summary>
        /// <param name="message">The message to show...</param>
        /// <param name="editorOnly">Stops the log running outside of the editor (set to false to runtime build logs)...</param>
        public static void Error<T>(string message, bool editorOnly = true)
        {
            if (!UtilRuntime.Settings.LoggingUseCommonLogs) return;
            if (!ShowLogsOnProductionBuild) return;
            Debug.LogError($"{LogPrefix}{ErrorPrefix}<color=#D6BA64>{typeof(T).Name}</color>: {message}");
        }
    }
}