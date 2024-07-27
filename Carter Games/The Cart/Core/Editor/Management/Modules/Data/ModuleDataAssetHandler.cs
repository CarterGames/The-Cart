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

using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;

namespace CarterGames.Cart.Modules
{
    /// <summary>
    /// A base class for modules to use data assets without issues.
    /// </summary>
    /// <typeparam name="TAssetType">The asset type.</typeparam>
    public abstract class ModuleDataAssetHandler<TAssetType> where TAssetType : DataAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private readonly string filterModuleDataAsset = $"t:{typeof(TAssetType).FullName}";
        
        private TAssetType cacheModuleDataAsset;
        private SerializedObject objectCacheModuleDataAsset;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The file name to use.
        /// </summary>
        protected abstract string FileNameModuleDataAsset { get; }
        
        
        /// <summary>
        /// The full path for the data asset.
        /// </summary>
        private string FullPathModuleDataAsset => $"{ScriptableRef.FullPathData}Modules/{FileNameModuleDataAsset}.asset";
        
        
        /// <summary>
        /// The asset reference.
        /// </summary>
        protected TAssetType ModuleAsset =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref cacheModuleDataAsset, filterModuleDataAsset, FullPathModuleDataAsset, FileEditorUtil.AssetName, $"{ScriptableRef.PathData}Modules/{FileNameModuleDataAsset}.asset");
        
        
        /// <summary>
        /// The module asset as an object.
        /// </summary>
        protected SerializedObject ObjectModuleAsset =>
            FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref objectCacheModuleDataAsset, ModuleAsset);


        /// <summary>
        /// 
        /// </summary>
        internal void TryCreateIfNotInProject()
        {
            if (AssetDatabaseHelper.FileIsInProject<TAssetType>(FullPathModuleDataAsset)) return;
            
            if (cacheModuleDataAsset == null)
            {
                FileEditorUtil.CreateSoGetOrAssignAssetCache(
                    ref cacheModuleDataAsset, 
                    filterModuleDataAsset, 
                    FullPathModuleDataAsset,
                    FileEditorUtil.AssetName, $"{ScriptableRef.PathData}Modules/{FileNameModuleDataAsset}.asset");
            }
        }
    }
}