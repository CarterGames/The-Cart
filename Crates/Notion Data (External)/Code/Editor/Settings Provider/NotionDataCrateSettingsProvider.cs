#if CARTERGAMES_CART_CRATE_NOTIONDATA && UNITY_EDITOR

using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.NotionData.Editor
{
    public class NotionDataCrateSettingsProvider : ISettingsProvider
    {
        public string MenuName => "Notion Data";
        
        
        public void OnProjectSettingsGUI()
        {
            EditorGUILayout.HelpBox("Notion Data's settings are in their own tab. Use the button below to open them here.", MessageType.Info);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(17.5f);
            
            if (GUILayout.Button("Open Settings"))
            {
                SettingsService.OpenProjectSettings("Carter Games/Assets/Notion Data");
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }
}

#endif