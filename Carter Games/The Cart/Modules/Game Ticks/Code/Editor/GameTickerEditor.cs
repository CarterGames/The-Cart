#if CARTERGAMES_CART_MODULE_GAMETICKER

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

namespace CarterGames.Cart.Modules.GameTicks.Editor
{
    [CustomEditor(typeof(GameTicker))]
    public class GameTickerEditor : UnityEditor.Editor, IMeta
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IMeta Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the path for the metadata of the module.
        /// </summary>
        public string MetaDataPath => $"{ScriptableRef.AssetBasePath}/Carter Games/The Cart/Modules/Game Ticks/Data/Meta Data/";
        
        
        /// <summary>
        /// Gets the metadata of the module.
        /// </summary>
        public MetaData MetaData => Meta.GetData(MetaDataPath, "GameTickerMeta");
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Custom editor
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public override void OnInspectorGUI()
        {
            GeneralUtilEditor.DrawMonoScriptSection(target as GameTicker);

            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1.5f);
            
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.Fp("syncState"), MetaData.Content("inspector_syncState"));
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }
            
            
            if (serializedObject.Fp("syncState").intValue == 0)
            {
                EditorGUILayout.PropertyField(serializedObject.Fp("ticksPerSecond"), MetaData.Content("inspector_tickRate"));
            }
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}

#endif