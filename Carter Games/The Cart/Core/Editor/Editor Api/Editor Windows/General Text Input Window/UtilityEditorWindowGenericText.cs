using CarterGames.Cart.Core.Events;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
{
    public class UtilityEditorWindowGenericText : UtilityEditorWindow
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
        public static string CurrentValue { get; private set; }
        private static string Description { get; set; }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Raises when the value is changed at any time.
        /// </summary>
        public static readonly Evt<string> ValueChangedCtx = new Evt<string>();
        
        
        public static readonly Evt<string> ValueConfirmedCtx = new Evt<string>();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Open Window Method
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
        public static void OpenAndAssignInfo(string windowTitle, string description, string defaultValue = "")
        {
            Description = description;

            if (!string.IsNullOrEmpty(defaultValue))
            {
                CurrentValue = defaultValue;
            }
            
            Open<UtilityEditorWindowGenericText>(windowTitle);
        }
		
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   GUI Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
        private void OnGUI()
        {
            GUILayout.Space(7.5f);
            EditorGUILayout.HelpBox(Description, MessageType.Info);
            GUILayout.Space(1.5f);
            
            EditorGUI.BeginChangeCheck();
            CurrentValue = EditorGUILayout.TextField(CurrentValue);
            if (EditorGUI.EndChangeCheck())
            {
                ValueChangedCtx.Raise(CurrentValue);
            }
        }
    }
}