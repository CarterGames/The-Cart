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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Modules.Settings;

namespace CarterGames.Cart.Core.Management.Editor
{
	public static class ModulesScriptableRef
	{
		private static IEnumerable<IScriptableAssetDef<DataAsset>> cacheImplementations;
		private static Dictionary<string ,IScriptableAssetDef<DataAsset>> cacheImplementationLookup;


		private static IEnumerable<IScriptableAssetDef<DataAsset>> Implementations => CacheRef.GetOrAssign(
			ref cacheImplementations,
			AssemblyHelper.GetClassesOfType<IScriptableAssetDef<DataAsset>>(new []
				{Assembly.Load("CarterGames.Cart.Modules")}));


		private static Dictionary<string, IScriptableAssetDef<DataAsset>> ImplementationLookup => 
			CacheRef.GetOrAssign(ref cacheImplementationLookup, GetLookup);



		private static Dictionary<string, IScriptableAssetDef<DataAsset>> GetLookup()
		{
			var lookup = new Dictionary<string, IScriptableAssetDef<DataAsset>>();
			
			foreach (var entry in Implementations)
			{
				// lookup.Add(entry.ContainingNamespace, entry);
			}

			return lookup;
		}
		


		public static IScriptableAssetDef<T> GetAssetRef<T>(string nameSpace) where T : DataAsset
		{
			if (ImplementationLookup.ContainsKey(nameSpace))
			{
				return (IScriptableAssetDef<T>) ImplementationLookup[nameSpace];
			}

			return null;
		}
		
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		// File Names
		/* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
// #if CARTERGAMES_CART_MODULE_GAMETICKER
// 		private static readonly string FileNameGameTickerSettingsAsset =
// 			"[Cart] [GameTicker] Settings Data Asset.asset";
// #endif
//
// #if CARTERGAMES_CART_MODULE_LOADINGSCREENS
// 		private static readonly string FileNameLoadingScreensSettingsAsset =
// 			"[Cart] [Loading Screens] Settings Data Asset.asset";
// #endif
//
// #if CARTERGAMES_CART_MODULE_NOTIONDATA
// 		private static readonly string FileNameNotionDataSettingsAsset =
// 			"[Cart] [Notion Data] Settings Data Asset.asset";
// #endif
// 		
// #if CARTERGAMES_CART_MODULE_RUNTIMECONSOLE
// 		private static readonly string FileNameRuntimeConsoleAsset =
// 			"[Cart] [Runtime Console] Runtime Console Data Asset.asset";
// #endif
// 		
// #if CARTERGAMES_CART_EXTENSION_REMOTECONFIG
// 		private static readonly string FileNameRemoteConfigAsset =
// 			"[Cart] [Remote Config] Remote Config Data Asset.asset";
// #endif
		
		// Assets Initialized Check
		/* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Gets if all the assets needed for the asset to function are in the project at the expected paths.
		/// </summary>
		public static bool HasAllAssets()
		{
			return Implementations.All(HasAsset);
		}


		/// <summary>
		/// Tries to create any missing assets when called.
		/// </summary>
		public static void TryCreateAssets()
		{
			foreach (var element in Implementations)
			{
				// TryCreate(element);
			}
		}


		private static bool HasAsset(IScriptableAssetDef<DataAsset> instance)
		{
			return AssetDatabaseHelper.FileIsInProject<DataAsset>(instance.DataAssetPath);
		}


		// public static void TryCreate(ISettingsDataAssetRef<DataAsset> instance)
		// {
		// 	if (instance.AssetRef != null) return;
		// 	
		// 	FileEditorUtil.CreateSoGetOrAssignAssetCache(
		// 		ref cacheAsset, 
		// 		instance.Filter, 
		// 		instance.Path,
		// 		FileEditorUtil.AssetName, instance.Path);
		// }
	}
}