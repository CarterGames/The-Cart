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
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Modules.Settings;
using UnityEditor;

namespace CarterGames.Cart.Core.Data.Editor
{
	/// <summary>
	/// Defines the data asset index in the library.
	/// </summary>
	public sealed class ScriptableAssetDefAssetIndex : IScriptableAssetDef<DataAssetIndex>
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static DataAssetIndex cache;
		private static SerializedObject objCache;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   IScriptableAssetDef
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// The type of the asset.
		/// </summary>
		public Type AssetType => typeof(DataAssetIndex);
		
		
		/// <summary>
		/// The name for the file.
		/// </summary>
		public string DataAssetFileName => "[Cart] Data Asset Index.asset";
		
		
		/// <summary>
		/// The filter for the type in the project.
		/// </summary>
		public string DataAssetFilter => $"t:{typeof(DataAssetIndex).FullName} name={DataAssetFileName}";
		
		
		/// <summary>
		/// The path to make the asset at.
		/// </summary>
		public string DataAssetPath => $"{ScriptableRef.FullPathResources}/{DataAssetFileName}";

		
		/// <summary>
		/// The asset reference of the asset.
		/// </summary>
		public DataAssetIndex AssetRef => ScriptableRef.GetOrCreateAsset(this, ref cache);
		
		
		/// <summary>
		/// The object reference of the asset.
		/// </summary>
		public SerializedObject ObjectRef => ScriptableRef.GetOrCreateAssetObject(this, ref objCache);
		
		
		/// <summary>
		/// Tries to create the asset when called.
		/// </summary>
		public void TryCreate()
		{
			ScriptableRef.GetOrCreateAsset(this, ref cache);
		}

		
		/// <summary>
		/// Runs when the asset is created.
		/// </summary>
		public void OnCreated()
		{
			DataAssetIndexHandler.UpdateIndex();
		}
	}
}