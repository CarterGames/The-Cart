﻿/*
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
using Scarlet.Utility;
using UnityEngine;

namespace Scarlet.Logs
{
    /// <summary>
    /// A custom logger class to aid show logs for scarlet library scripts.
    /// </summary>
    public static class ScarletLogs
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private const string LogPrefix = "<color=#E36B6B><b>Scarlet Library</b></color> | ";
        private const string WarningPrefix = "<color=#D6BA64><b>Warning</b></color> | ";
        private const string ErrorPrefix = "<color=#E77A7A><b>Error</b></color> | ";

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Displays a normal debug message for the build versions asset...
        /// </summary>
        /// <param name="type">The class type to report from...</param>
        /// <param name="message">The message to show...</param>
        /// <param name="editorOnly">Stops the log running outside of the editor (set to false to runtime build logs)...</param>
        public static void Normal(Type type, string message, bool editorOnly = true)
        {
            if (!UtilRuntime.Settings.LoggingUseScarletLogs) return;
            if (!Application.isEditor && editorOnly) return;
            Debug.Log($"{LogPrefix}<color=#D6BA64>{type.Name}</color>: {message}");
        }


        /// <summary>
        /// Displays a warning debug message for the build versions asset...
        /// </summary>
        /// <param name="type">The class type to report from...</param>
        /// <param name="message">The message to show...</param>
        /// <param name="editorOnly">Stops the log running outside of the editor (set to false to runtime build logs)...</param>
        public static void Warning(Type type, string message, bool editorOnly = true)
        {
            if (!UtilRuntime.Settings.LoggingUseScarletLogs) return;
            if (!Application.isEditor && editorOnly) return;
            Debug.LogWarning($"{LogPrefix}{WarningPrefix}<color=#D6BA64>{type.Name}</color>: {message}");
        }
        
        
        /// <summary>
        /// Displays a error debug message for the build versions asset...
        /// </summary>
        /// <param name="type">The class type to report from...</param>
        /// <param name="message">The message to show...</param>
        /// <param name="editorOnly">Stops the log running outside of the editor (set to false to runtime build logs)...</param>
        public static void Error(Type type, string message, bool editorOnly = true)
        {
            if (!UtilRuntime.Settings.LoggingUseScarletLogs) return;
            if (!Application.isEditor && editorOnly) return;
            Debug.LogError($"{LogPrefix}{ErrorPrefix}<color=#D6BA64>{type.Name}</color>: {message}");
        }
    }
}