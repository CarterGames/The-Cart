using System;
using CarterGames.Cart.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    [Serializable]
    public sealed class HierarchyHeaderSeparatorConfig : IHierarchyConfig
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private string headerPrefix = "<---";
        [SerializeField] private string separatorPrefix = "--->";
        [SerializeField] private HierarchyTitleTextAlign textAlign = HierarchyTitleTextAlign.Center;
        [SerializeField] private bool fullWidth = false;
        [SerializeField] private Color headerBackgroundColor = Color.gray;
        [SerializeField] private Color textColor = Color.white;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public string OptionLabel => "Headers & Separators";
        public string HeaderPrefix => headerPrefix;
        public string SeparatorPrefix => separatorPrefix;
        public HierarchyTitleTextAlign HeaderTextAlign => textAlign;
        public bool HeaderFullWidth => fullWidth;
        public Color HeaderBackgroundColor => headerBackgroundColor;
        public Color HeaderTextColor => textColor;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public void DrawConfig(SerializedProperty serializedProperty)
        {
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(serializedProperty.Fpr("headerPrefix"));
            EditorGUILayout.PropertyField(serializedProperty.Fpr("separatorPrefix"));
            EditorGUILayout.PropertyField(serializedProperty.Fpr("textAlign"));
            EditorGUILayout.PropertyField(serializedProperty.Fpr("fullWidth"));
            EditorGUILayout.PropertyField(serializedProperty.Fpr("headerBackgroundColor"));
            EditorGUILayout.PropertyField(serializedProperty.Fpr("textColor"));

            if (EditorGUI.EndChangeCheck())
            {
                serializedProperty.serializedObject.ApplyModifiedProperties();
                serializedProperty.serializedObject.Update();
            }
        }
    }
}