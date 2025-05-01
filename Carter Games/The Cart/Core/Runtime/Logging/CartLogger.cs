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
using System.Text;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Core.Management;
using UnityEngine;

namespace CarterGames.Cart.Core.Logs
{
    /// <summary>
    /// A custom logger class to aid show logs for common library scripts.
    /// </summary>
    public static class CartLogger
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private const string TypeColorPrefix = "<color=#D6BA64><b>";
        private const string ColorSuffix = "</b></color>";

        private static readonly StringBuilder Builder = new StringBuilder();
        private static readonly StringBuilder MessageBuilder = new StringBuilder();

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
        private static bool ShowLogsOnProductionBuild
        {
            get
            {
                if (Application.isEditor) return true;
                return IsProductionBuild && UtilRuntime.Settings.UseLogsInProductionBuilds;
            }
        }


        /// <summary>
        /// Gets if the log should show or not based on the current settings for the cart logs.
        /// </summary>
        private static bool CanShowLogs
        {
            get
            {
                if (Application.isEditor) return true;
                
                if (IsProductionBuild)
                {
                    return ShowLogsOnProductionBuild && UtilRuntime.Settings.LoggingUseCartLogs;
                }
                
                return UtilRuntime.Settings.LoggingUseCartLogs;
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public static readonly Evt<LogType, string> Logged = new Evt<LogType, string>();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Log Builder Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Creates the category prefix for the log.
        /// </summary>
        /// <param name="additionalContext">The sub-type for the message if used.</param>
        /// <typeparam name="T">The log category type</typeparam>
        /// <returns>The prefix ready for use.</returns>
        private static string GetCategoryPrefix<T>(Type additionalContext = null)
        {
            Builder.Clear();
            
            Builder.Append(TypeColorPrefix);
            Builder.Append(typeof(T).Name);

            if (additionalContext != null)
            {
                Builder.Append("/");
                Builder.Append(additionalContext.Name);
            }
            
            Builder.Append(ColorSuffix);
            Builder.Append(":");

            return Builder.ToString();
        }

        
        /// <summary>
        /// Generates the log message all together to show.
        /// </summary>
        /// <param name="msg">The message to show.</param>
        /// <param name="additionalContext">The sub-type for the message if used.</param>
        /// <typeparam name="T">The log category type</typeparam>
        /// <returns>The formatted log message.</returns>
        private static string CreateLogMessage<T>(string msg, Type additionalContext = null) where T : LogCategory
        {
            MessageBuilder.Clear();
            MessageBuilder.Append(GetCategoryPrefix<T>(additionalContext));
            MessageBuilder.Append(" ");
            MessageBuilder.Append(msg);
            
            return MessageBuilder.ToString();
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Log Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        
        // Normal Logs
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        
        /// <summary>
        /// Displays a normal debug message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        /// <typeparam name="T">The log type to display as.</typeparam>
        public static void Log<T>(string message, UnityEngine.Object ctx = null, bool editorOnlyLog = false) where T : LogCategory
        {
            if (!CanShowLogs) return;
            if (!DataAccess.GetAsset<DataAssetLogCategories>().IsEnabled<T>()) return;
            if (!Application.isEditor && editorOnlyLog) return;

            var formattedLog = CreateLogMessage<T>(message);
            
            Debug.Log(formattedLog, ctx);
            Logged.Raise(LogType.Log, formattedLog);
        }
        
        
        /// <summary>
        /// Displays a normal debug message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="additionalContext">The additional type context for the log. Such as system/class.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        /// <typeparam name="T">The log type to display as.</typeparam>
        public static void Log<T>(string message, Type additionalContext, UnityEngine.Object ctx = null, bool editorOnlyLog = false) where T : LogCategory
        {
            if (!CanShowLogs) return;
            if (!DataAccess.GetAsset<DataAssetLogCategories>().IsEnabled<T>()) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            var formattedLog = CreateLogMessage<T>(message, additionalContext);
            
            Debug.Log(formattedLog);
            Logged.Raise(LogType.Log, formattedLog);
        }
        
        
        // Warning Logs
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        
        /// <summary>
        /// Displays a warning debug message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        /// <typeparam name="T">The log type to display as.</typeparam>
        public static void LogWarning<T>(string message, UnityEngine.Object ctx = null, bool editorOnlyLog = false) where T : LogCategory
        {
            if (!CanShowLogs) return;
            if (!DataAccess.GetAsset<DataAssetLogCategories>().IsEnabled<T>()) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            var formattedLog = CreateLogMessage<T>(message);
            
            Debug.LogWarning(formattedLog);
            Logged.Raise(LogType.Warning, formattedLog);
        }
        
        
        /// <summary>
        /// Displays a warning debug message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="additionalContext">The additional type context for the log. Such as system/class.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        /// <typeparam name="T">The log type to display as.</typeparam>
        public static void LogWarning<T>(string message, Type additionalContext, UnityEngine.Object ctx = null, bool editorOnlyLog = false) where T : LogCategory
        {
            if (!CanShowLogs) return;
            if (!DataAccess.GetAsset<DataAssetLogCategories>().IsEnabled<T>()) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            var formattedLog = CreateLogMessage<T>(message, additionalContext);
            
            Debug.LogWarning(formattedLog);
            Logged.Raise(LogType.Warning, formattedLog);
        }
                
        
        // Error Logs
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        
        /// <summary>
        /// Displays a error debug message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        /// <typeparam name="T">The log type to display as.</typeparam>
        public static void LogError<T>(string message, UnityEngine.Object ctx = null, bool editorOnlyLog = false) where T : LogCategory
        {
            if (!CanShowLogs) return;
            if (!DataAccess.GetAsset<DataAssetLogCategories>().IsEnabled<T>()) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            var formattedLog = CreateLogMessage<T>(message);
            
            Debug.LogError(formattedLog);
            Logged.Raise(LogType.Error, formattedLog);
        }
        
        
        /// <summary>
        /// Displays a error debug message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="additionalContext">The additional type context for the log. Such as system/class.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        /// <typeparam name="T">The log type to display as.</typeparam>
        public static void LogError<T>(string message, Type additionalContext, UnityEngine.Object ctx = null, bool editorOnlyLog = false) where T : LogCategory
        {
            if (!CanShowLogs) return;
            if (!DataAccess.GetAsset<DataAssetLogCategories>().IsEnabled<T>()) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            var formattedLog = CreateLogMessage<T>(message, additionalContext);
            
            Debug.LogError(formattedLog);
            Logged.Raise(LogType.Error, formattedLog);
        }
    }
}