#if CARTERGAMES_CART_MODULE_NOTIONDATA

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

using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Core.MetaData.Editor;
using UnityEditor;

namespace CarterGames.Cart.Modules.NotionData.Editor
{
    /// <summary>
    /// Handles the settings provider for the notion data.
    /// </summary>
    public sealed class SettingsProviderNotionData : ModuleDataAssetHandler<DataAssetRuntimeSettingsNotionData>, ISettingsProvider, IMeta
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   ModuleDataAssetHandler Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected override string FileNameModuleDataAsset => "[Cart] [Notion Data] Runtime Settings Data Asset";

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IMeta Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the path for the metadata of the module.
        /// </summary>
        public string MetaDataPath => $"{ScriptableRef.AssetBasePath}/Carter Games/The Cart/Modules/Notion/Data/Meta Data/";
        
        
        /// <summary>
        /// Gets the metadata of the module.
        /// </summary>
        public MetaData MetaData => Meta.GetData(MetaDataPath, "NotionData");
        
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   ISettingsProvider Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Draws the inspector version of the settings.
        /// </summary>
        /// <param name="serializedObject">The target object</param>
        public void OnInspectorSettingsGUI(SerializedObject serializedObject)
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField(MetaData.Labels["sectionTitle"], EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(ObjectModuleAsset.Fp("notionApiKey"), MetaData.Content("notion_defaultAPIKey"));

            if (EditorGUI.EndChangeCheck())
            {
                ObjectModuleAsset.ApplyModifiedProperties();
                ObjectModuleAsset.Update();
            }

            EditorGUILayout.EndVertical();
        }

        
        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public void OnProjectSettingsGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(1.5f);
            
            EditorSettingsNotionData.EditorDataNotionExpanded =
                EditorGUILayout.Foldout(EditorSettingsNotionData.EditorDataNotionExpanded, MetaData.Content("notionTitle"));
            
            if (EditorSettingsNotionData.EditorDataNotionExpanded)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.Space(1.5f);
                EditorGUI.indentLevel++;
                
                // Draw the provider enum field on the GUI...
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(ObjectModuleAsset.Fp("notionApiKey"), MetaData.Content("notion_defaultAPIKey"));
                
                
                if (EditorGUI.EndChangeCheck())
                {
                    ObjectModuleAsset.ApplyModifiedProperties();
                    ObjectModuleAsset.Update();
                }
                
                EditorGUI.indentLevel--;
                EditorGUILayout.Space(1.5f);
                EditorGUILayout.EndVertical();
            }
            
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif