using CarterGames.Cart.Data.Notion;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    public static class NotionDataSettingsDrawer
    {
        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public static void DrawSettings()
        {
            PerUserSettings.EditorDataExpanded =
                EditorGUILayout.Foldout(PerUserSettings.EditorDataExpanded, "Data");
        
            
            if (!PerUserSettings.EditorDataExpanded) return;
        
        
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(1.5f);
            EditorGUI.indentLevel++;
            
            PerUserSettings.EditorDataNotionExpanded =
                EditorGUILayout.Foldout(PerUserSettings.EditorDataNotionExpanded, "Notion");

            if (PerUserSettings.EditorDataNotionExpanded)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.Space(1.5f);
                EditorGUI.indentLevel++;
                
                // Draw the provider enum field on the GUI...
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(UtilEditor.SettingsObject.Fp("notionApiKey"), NotionMetaData.DefaultApiKey);
                
                
                if (EditorGUI.EndChangeCheck())
                {
                    UtilEditor.SettingsObject.ApplyModifiedProperties();
                    UtilEditor.SettingsObject.Update();
                }
                
                EditorGUI.indentLevel--;
                EditorGUILayout.Space(1.5f);
                EditorGUILayout.EndVertical();
            }
        
        
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the inspector version of the settings.
        /// </summary>
        /// <param name="serializedObject">The target object</param>
        public static void DrawInspector(SerializedObject serializedObject)
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Data - Notion", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("notionApiKey"), NotionMetaData.DefaultApiKey);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }

            EditorGUILayout.EndVertical();
        }
    }
}