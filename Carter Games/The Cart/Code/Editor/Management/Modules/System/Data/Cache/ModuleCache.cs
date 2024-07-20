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

using System.Linq;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules
{
    [CreateAssetMenu(fileName = "Module Cache Asset", menuName = "Carter Games/The Cart/Modules/Cache Asset")]
    public sealed class ModuleCache : DataAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] private SerializableDictionary<string, TextAsset> installedModuleReceipts;
        [SerializeField] private TextAsset manifest;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The manifest for all the modules.
        /// </summary>
        public ModuleManifestWrapper Manifest
        {
            get
            {
                var so = new SerializedObject(this);
                
                if (so.Fp("manifest").objectReferenceValue == null)
                {
                    so.Fp("manifest").objectReferenceValue = AssetDatabase.LoadAssetAtPath<TextAsset>(
                        $"{ScriptableRef.AssetBasePath}/Carter Games/The Cart/Code/Editor/Core/Modules/System/Data/Manifest/ModuleManifest.json");
                    so.ApplyModifiedProperties();
                    so.Update();
                }

                if (so.Fp("manifest").objectReferenceValue == null)
                {
                    return null;
                }
                
                return JsonUtility.FromJson<ModuleManifestWrapper>(manifest.text);
            }
        }


        /// <summary>
        /// Gets the installed info for the modules.
        /// </summary>
        public SerializableDictionary<string, TextAsset> InstalledModulesInfo => installedModuleReceipts;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Adds a module info to the cache when called.
        /// </summary>
        /// <param name="module">The module to add.</param>
        /// <param name="asset">The asset.</param>
        public void AddInstalledModuleInfo(IModule module, TextAsset asset)
        {
            if (installedModuleReceipts.ContainsKey(module.Namespace))
            {
                installedModuleReceipts[module.Namespace] = asset;
                EditorUtility.SetDirty(this);
                return;
            }
            
            installedModuleReceipts.Add(module.Namespace, asset);
            EditorUtility.SetDirty(this);
        }


        /// <summary>
        /// Updates the info when called.
        /// </summary>
        /// <param name="module">The module to update info for.</param>
        /// <param name="asset">The test asset to read and update from.</param>
        public void UpdateInstalledModuleInfo(IModule module, TextAsset asset)
        {
            if (!installedModuleReceipts.ContainsKey(module.Namespace)) return;
            installedModuleReceipts[module.Namespace] = asset;
            EditorUtility.SetDirty(this);
        }


        /// <summary>
        /// Removes a module info from the cache.
        /// </summary>
        /// <param name="module">The module to remove.</param>
        public void RemoveInstalledInfo(IModule module)
        {
            if (!installedModuleReceipts.Keys.Any(t => t.Equals(module.ModuleName))) return;
            
            var so = new SerializedObject(this);
            var index = installedModuleReceipts.Keys.ToList().FindIndex(t => t.Equals(module.ModuleName));
            so.Fp("installedModuleReceipts").Fpr("list").DeleteIndex(index);
            so.ApplyModifiedProperties();
            so.Update();
        }
    }
}