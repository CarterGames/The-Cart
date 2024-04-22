/*
 * Copyright (c) 2024 Carter Games
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
        private const string WarningPrefix = "<color=#D6BA64><b>Warning</b></color> | ";
        private const string ErrorPrefix = "<color=#E77A7A><b>Error</b></color> | ";

        private static readonly StringBuilder Builder = new StringBuilder();

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


        /// <summary>
        /// Gets if the log should show or not based on the current settings for the cart logs.
        /// </summary>
        private static bool CanShowLogs
        {
            get
            {
                if (IsProductionBuild)
                {
                    return ShowLogsOnProductionBuild && UtilRuntime.Settings.LoggingUseCartLogs;
                }
                else
                {
                    return UtilRuntime.Settings.LoggingUseCartLogs;
                }
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Log Builder Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Creates the severity prefix for the log message.
        /// </summary>
        /// <param name="logType">The severity of the log message.</param>
        /// <returns>The prefix if there is one.</returns>
        private static string GetSeverityPrefix(LogType logType)
        {
            if (logType == LogType.Log)
            {
                return string.Empty;
            }
            
            Builder.Clear();

            switch (logType)
            {
                case LogType.Error:
                case LogType.Assert:
                case LogType.Exception:
                    Builder.Append(ErrorPrefix);
                    break;
                case LogType.Warning:
                    Builder.Append(WarningPrefix);
                    break;
            }

            return Builder.ToString();
        }
        
        
        /// <summary>
        /// Makes the location log prefix if needed.
        /// </summary>
        /// <param name="context">The main type string.</param>
        /// <param name="additionalContext">The sub-type string.</param>
        /// <returns>The prepared type prefix string.</returns>
        private static string GetLogLocationString(string context, string additionalContext = null)
        {
            if (string.IsNullOrEmpty(context) && string.IsNullOrEmpty(additionalContext)) return string.Empty; 
            
            Builder.Clear();
            Builder.Append(TypeColorPrefix);
            Builder.Append(context);
            
            if (string.IsNullOrEmpty(additionalContext))
            {
                Builder.Append(ColorSuffix);
                Builder.Append(":");
                
                return Builder.ToString();
            }
            
            Builder.Append("/");
            Builder.Append(additionalContext);
            Builder.Append(ColorSuffix);
            Builder.Append(":");
                
            return Builder.ToString();
        }

        
        /// <summary>
        /// Generates the log message all together to show.
        /// </summary>
        /// <param name="logType">The severity of the log message.</param>
        /// <param name="target">The target type for the message in string form.</param>
        /// <param name="msg">The message to show.</param>
        /// <param name="additionalContext">The sub-type for the message if used.</param>
        /// <returns>The formatted log message.</returns>
        private static string CreateLogMessage(LogType logType, string target, string msg, Type additionalContext)
        {
            var severityPrefix = GetSeverityPrefix(logType);
            var prefix = GetLogLocationString(target, additionalContext == null ? string.Empty : additionalContext.Name);

            Builder.Clear();
            Builder.Append(severityPrefix);
            Builder.Append(prefix);
            
            if (!string.IsNullOrEmpty(severityPrefix) || !string.IsNullOrEmpty(prefix))
            {
                Builder.Append(" ");
            }
            
            Builder.Append(msg);
            
            return Builder.ToString();
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
        public static void Log(string message, bool editorOnlyLog = false)
        {
            if (!CanShowLogs) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            Debug.Log(CreateLogMessage(LogType.Log, string.Empty, message, null));
        }
        
        
        /// <summary>
        /// Creates a normal log message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="typeContext">The additional type context for the log. Such as system/class.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        /// <typeparam name="T">The type context for the log.</typeparam>
        public static void Log<T>(string message, Type typeContext = null, bool editorOnlyLog = false)
        {
            if (!CanShowLogs) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            Debug.Log(CreateLogMessage(LogType.Log, typeof(T).Name, message, typeContext));
        }


        /// <summary>
        /// Displays a normal debug message.
        /// </summary>
        /// <param name="type">The type this log is to show.</param>
        /// <param name="message">The message to write.</param>
        /// <param name="typeContext">The additional type context for the log. Such as system/class.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        public static void Log(Type type, string message, Type typeContext = null, bool editorOnlyLog = false)
        {
            if (!CanShowLogs) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            Debug.Log(CreateLogMessage(LogType.Log, type.Name, message, typeContext));
        }
        
        
        // Warning Logs
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        
        /// <summary>
        /// Displays a warning debug message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        public static void LogWarning(string message, bool editorOnlyLog = false)
        {
            if (!CanShowLogs) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            Debug.Log(CreateLogMessage(LogType.Warning, string.Empty, message, null));
        }
        
        
        /// <summary>
        /// Creates a warning log message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="typeContext">The additional type context for the log. Such as system/class.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        /// <typeparam name="T">The type context for the log.</typeparam>
        public static void LogWarning<T>(string message, Type typeContext = null, bool editorOnlyLog = false)
        {
            if (!CanShowLogs) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            Debug.Log(CreateLogMessage(LogType.Warning, typeof(T).Name, message, typeContext));
        }


        /// <summary>
        /// Displays a warning debug message.
        /// </summary>
        /// <param name="type">The type this log is to show.</param>
        /// <param name="message">The message to write.</param>
        /// <param name="typeContext">The additional type context for the log. Such as system/class.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        public static void LogWarning(Type type, string message, Type typeContext = null, bool editorOnlyLog = false)
        {
            if (!CanShowLogs) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            Debug.Log(CreateLogMessage(LogType.Warning, type.Name, message, typeContext));
        }
                
        
        // Error Logs
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        
        /// <summary>
        /// Displays a error debug message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        public static void LogError(string message, bool editorOnlyLog = false)
        {
            if (!CanShowLogs) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            Debug.Log(CreateLogMessage(LogType.Error, string.Empty, message, null));
        }
        
        
        /// <summary>
        /// Creates a error log message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="typeContext">The additional type context for the log. Such as system/class.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        /// <typeparam name="T">The type context for the log.</typeparam>
        public static void LogError<T>(string message, Type typeContext = null, bool editorOnlyLog = false)
        {
            if (!CanShowLogs) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            Debug.Log(CreateLogMessage(LogType.Error, typeof(T).Name, message, typeContext));
        }


        /// <summary>
        /// Displays a error debug message.
        /// </summary>
        /// <param name="type">The type this log is to show.</param>
        /// <param name="message">The message to write.</param>
        /// <param name="typeContext">The additional type context for the log. Such as system/class.</param>
        /// <param name="editorOnlyLog">Should this log be in the editor only.</param>
        public static void LogError(Type type, string message, Type typeContext = null, bool editorOnlyLog = false)
        {
            if (!CanShowLogs) return;
            if (!Application.isEditor && editorOnlyLog) return;
            
            Debug.Log(CreateLogMessage(LogType.Error, type.Name, message, typeContext));
        }
    }
}