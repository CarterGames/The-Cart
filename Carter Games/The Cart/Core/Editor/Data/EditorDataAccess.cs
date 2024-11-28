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

using System.Collections.Generic;
using CarterGames.Cart.Core.Management.Editor;

namespace CarterGames.Cart.Core.Data.Editor
{
	/// <summary>
	/// An editor only data access class, it will make sure the index is up-to date when calling it.
	/// </summary>
	public static class EditorDataAccess
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// The index to check through.
		/// </summary>
		private static DataAssetIndex Index => ScriptableRef.GetAssetDef<DataAssetIndex>().AssetRef;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Gets if an asset exists in the index currently, updates it if not.
		/// </summary>
		/// <typeparam name="T">The type to try and find.</typeparam>
		/// <returns>If it exists in theindex or not.</returns>
		private static bool HasAsset<T>() where T : DataAsset
		{
			if (Index.Lookup.ContainsKey(typeof(T).ToString()))
			{
				if (Index.Lookup[typeof(T).ToString()][0] != null) return true;
			}

			return false;
		}
		
		
        /// <summary>
        /// Gets the Data Asset requested.
        /// </summary>
        /// <typeparam name="T">The asset to get.</typeparam>
        /// <returns>The asset if it exists.</returns>
        public static T GetAsset<T>() where T : DataAsset
        {
	        if (!HasAsset<T>())
	        {
		        DataAssetIndexHandler.UpdateIndex();
	        }
	        
	        return DataAccess.GetAsset<T>();
        }
        
        
        /// <summary>
        /// Gets the Data Asset requested.
        /// </summary>
        /// <typeparam name="T">The asset to get.</typeparam>
        /// <returns>The asset if it exists.</returns>
        public static T GetAsset<T>(string id) where T : DataAsset
        {
	        if (!HasAsset<T>())
	        {
		        DataAssetIndexHandler.UpdateIndex();
	        }
	        
	        return DataAccess.GetAsset<T>(id);
        }
        
        
        /// <summary>
        /// Gets the Data Asset requested.
        /// </summary>
        /// <typeparam name="T">The asset to get.</typeparam>
        /// <returns>The asset if it exists.</returns>
        public static List<T> GetAssets<T>() where T : DataAsset
        {
	        if (!HasAsset<T>())
	        {
		        DataAssetIndexHandler.UpdateIndex();
	        }

	        return DataAccess.GetAssets<T>();
        }


        /// <summary>
        /// Gets all the data assets stored in the index.
        /// </summary>
        /// <returns>All the assets stored.</returns>
        public static List<DataAsset> GetAllAssets() => DataAccess.GetAllAssets();
	}
}