// ----------------------------------------------------------------------------
// ScarletLogs.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 22/09/2022
// ----------------------------------------------------------------------------

using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scarlet.General.Logs
{
    public static class ScarletLogs
    {
        private const string LogPrefix = "<color=#E36B6B><b>Scarlet Library</b></color> | ";
        private const string WarningPrefix = "<color=#D6BA64><b>Warning</b></color> | ";
        private const string ErrorPrefix = "<color=#E77A7A><b>Error</b></color> | ";

        
        /// <summary>
        /// Displays a normal debug message for the build versions asset...
        /// </summary>
        /// <param name="type">The class type to report from...</param>
        /// <param name="message">The message to show...</param>
        /// <param name="editorOnly">Stops the log running outside of the editor (set to false to runtime build logs)...</param>
        public static void Normal(Type type, string message, bool editorOnly = true)
        {
            if (!Application.isEditor && editorOnly) return;
            Debug.Log($"{LogPrefix}<color=#D6BA64>{type.Name}<color>: {message}");
        }
        
        
        /// <summary>
        /// Displays a normal debug message for the build versions asset...
        /// </summary>
        /// <param name="target">The target to show from...</param>
        /// <param name="message">The message to show...</param>
        /// <param name="editorOnly">Stops the log running outside of the editor (set to false to runtime build logs)...</param>
        public static void Normal(string message, Object context, bool editorOnly = true)
        {
            if (!Application.isEditor && editorOnly) return;
            Debug.Log($"{LogPrefix}{context.GetType().Name}: {message}", context);
        }


        /// <summary>
        /// Displays a warning debug message for the build versions asset...
        /// </summary>
        /// <param name="type">The class type to report from...</param>
        /// <param name="message">The message to show...</param>
        /// <param name="editorOnly">Stops the log running outside of the editor (set to false to runtime build logs)...</param>
        public static void Warning(Type type, string message, bool editorOnly = true)
        {
            if (!Application.isEditor && editorOnly) return;
            Debug.LogWarning($"{LogPrefix}{WarningPrefix}<color=#D6BA64>{type.Name}<color>: {message}");
        }
        
        
        /// <summary>
        /// Displays a error debug message for the build versions asset...
        /// </summary>
        /// <param name="type">The class type to report from...</param>
        /// <param name="message">The message to show...</param>
        /// <param name="editorOnly">Stops the log running outside of the editor (set to false to runtime build logs)...</param>
        public static void Error(Type type, string message, bool editorOnly = true)
        {
            if (!Application.isEditor && editorOnly) return;
            Debug.LogError($"{LogPrefix}{ErrorPrefix}<color=#D6BA64>{type.Name}<color>: {message}");
        }
    }
}