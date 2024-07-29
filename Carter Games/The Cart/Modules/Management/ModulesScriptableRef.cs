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
using UnityEditor;

namespace CarterGames.Cart.Core.Management.Editor
{
	public static class ModulesScriptableRef
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		// File Names
		/* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
#if CARTERGAMES_CART_MODULE_GAMETICKER
		private static readonly string FileNameGameTickerSettingsAsset =
			"[Cart] [GameTicker] Settings Data Asset.asset";
#endif

#if CARTERGAMES_CART_MODULE_LOADINGSCREENS
		private static readonly string FileNameLoadingScreensSettingsAsset =
			"[Cart] [Loading Screens] Settings Data Asset.asset";
#endif

#if CARTERGAMES_CART_MODULE_NOTIONDATA
		private static readonly string FileNameNotionDataSettingsAsset =
			"[Cart] [Notion Data] Settings Data Asset.asset";
#endif

		
		// Asset Paths
		/* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
#if CARTERGAMES_CART_MODULE_GAMETICKER
		private static readonly string FullPathGameTickerSettingsAsset =
			$"{ScriptableRef.FullPathData}Modules/{FileNameGameTickerSettingsAsset}";
#endif

#if CARTERGAMES_CART_MODULE_LOADINGSCREENS
		private static readonly string FullPathLoadingScreensSettingsAsset =
			$"{ScriptableRef.FullPathData}Modules/{FileNameLoadingScreensSettingsAsset}";
#endif

#if CARTERGAMES_CART_MODULE_NOTIONDATA
		private static readonly string FullPathNotionDataSettingsAsset =
			$"{ScriptableRef.FullPathData}Modules/{FileNameNotionDataSettingsAsset}";
#endif

		
		// Asset Filters
		/* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
#if CARTERGAMES_CART_MODULE_GAMETICKER
		private static readonly string FilterGameTickerSettingsAsset =
			$"t:{typeof(Modules.GameTicks.DataAssetSettingsGameTicker).FullName}";
#endif
		
#if CARTERGAMES_CART_MODULE_LOADINGSCREENS
		private static readonly string FilterLoadingScreensSettingsAsset =
			$"t:{typeof(Modules.LoadingScreens.DataAssetSettingsLoadingScreens).FullName}";
#endif
		
#if CARTERGAMES_CART_MODULE_NOTIONDATA
		private static readonly string FilterNotionDataSettingsAsset =
			$"t:{typeof(Modules.NotionData.DataAssetSettingsNotionData).FullName}";
#endif


		// Asset Caches
		/* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
#if CARTERGAMES_CART_MODULE_GAMETICKER
		private static Modules.GameTicks.DataAssetSettingsGameTicker cacheGameTickerSettingsAsset;
#endif
		
#if CARTERGAMES_CART_MODULE_LOADINGSCREENS
		private static Modules.LoadingScreens.DataAssetSettingsLoadingScreens cacheLoadingScreensSettingsAsset;
#endif
		
#if CARTERGAMES_CART_MODULE_NOTIONDATA
		private static Modules.NotionData.DataAssetSettingsNotionData cacheNotionDataSettingsAsset;
#endif


		// SerializedObject Caches
		/* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
#if CARTERGAMES_CART_MODULE_GAMETICKER
		private static SerializedObject objectCacheGameTickerSettingsAsset;
#endif
		
#if CARTERGAMES_CART_MODULE_LOADINGSCREENS
		private static SerializedObject objectCacheLoadingScreensSettingsAsset;
#endif
		
#if CARTERGAMES_CART_MODULE_NOTIONDATA
		private static SerializedObject objectCacheNotionDataSettingsAsset;
#endif


		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */


		// Asset Properties
		/* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
#if CARTERGAMES_CART_MODULE_GAMETICKER
		public static Modules.GameTicks.DataAssetSettingsGameTicker GameTickerSettingsAsset =>
			FileEditorUtil.CreateSoGetOrAssignAssetCache(ref cacheGameTickerSettingsAsset,
				FilterGameTickerSettingsAsset, FullPathGameTickerSettingsAsset,
				FileEditorUtil.AssetName,
				$"{ScriptableRef.PathData}Modules/{FileNameGameTickerSettingsAsset}");
#endif
		
#if CARTERGAMES_CART_MODULE_LOADINGSCREENS
		public static Modules.LoadingScreens.DataAssetSettingsLoadingScreens LoadingScreensSettingsAsset =>
			FileEditorUtil.CreateSoGetOrAssignAssetCache(ref cacheLoadingScreensSettingsAsset,
				FilterLoadingScreensSettingsAsset, FullPathLoadingScreensSettingsAsset,
				FileEditorUtil.AssetName,
				$"{ScriptableRef.PathData}Modules/{FileNameLoadingScreensSettingsAsset}");
#endif
		
#if CARTERGAMES_CART_MODULE_NOTIONDATA
		public static Modules.NotionData.DataAssetSettingsNotionData NotionDataSettingsAsset =>
			FileEditorUtil.CreateSoGetOrAssignAssetCache(ref cacheNotionDataSettingsAsset,
				FilterNotionDataSettingsAsset, FullPathNotionDataSettingsAsset,
				FileEditorUtil.AssetName,
				$"{ScriptableRef.PathData}Modules/{FileNameNotionDataSettingsAsset}");
#endif


		// Object Properties
		/* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
#if CARTERGAMES_CART_MODULE_GAMETICKER
		public static SerializedObject GameTickerSettingsObject =>
			FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref objectCacheGameTickerSettingsAsset,
				GameTickerSettingsAsset);
#endif
		
#if CARTERGAMES_CART_MODULE_LOADINGSCREENS
		public static SerializedObject LoadingScreensSettingsObject =>
			FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref objectCacheLoadingScreensSettingsAsset,
				LoadingScreensSettingsAsset);
#endif
		
#if CARTERGAMES_CART_MODULE_NOTIONDATA
		public static SerializedObject NotionDataSettingsObject =>
			FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref objectCacheNotionDataSettingsAsset,
				NotionDataSettingsAsset);
#endif

		// Assets Initialized Check
		/* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Gets if all the assets needed for the asset to function are in the project at the expected paths.
		/// </summary>
		public static bool HasAllAssets()
		{
			var list = new List<bool>();

#if CARTERGAMES_CART_MODULE_GAMETICKER
			list.Add(AssetDatabaseHelper.FileIsInProject<Modules.GameTicks.DataAssetSettingsGameTicker>(
				FullPathGameTickerSettingsAsset));
#endif
			
#if CARTERGAMES_CART_MODULE_LOADINGSCREENS
			list.Add(AssetDatabaseHelper.FileIsInProject<Modules.LoadingScreens.DataAssetSettingsLoadingScreens>(
				FullPathLoadingScreensSettingsAsset));
#endif
			
#if CARTERGAMES_CART_MODULE_NOTIONDATA
			list.Add(AssetDatabaseHelper.FileIsInProject<Modules.NotionData.DataAssetSettingsNotionData>(
				FullPathNotionDataSettingsAsset));
#endif

			return list.All(t => t);
		}


		/// <summary>
		/// Tries to create any missing assets when called.
		/// </summary>
		public static void TryCreateAssets()
		{
#if CARTERGAMES_CART_MODULE_GAMETICKER
			if (cacheGameTickerSettingsAsset == null)
			{
				FileEditorUtil.CreateSoGetOrAssignAssetCache(
					ref cacheGameTickerSettingsAsset,
					FilterGameTickerSettingsAsset,
					FullPathGameTickerSettingsAsset,
					FileEditorUtil.AssetName,
					$"{ScriptableRef.PathData}Modules/{FileNameGameTickerSettingsAsset}");
			}
#endif
			
#if CARTERGAMES_CART_MODULE_LOADINGSCREENS
			if (cacheLoadingScreensSettingsAsset == null)
			{
				FileEditorUtil.CreateSoGetOrAssignAssetCache(
					ref cacheLoadingScreensSettingsAsset,
					FilterLoadingScreensSettingsAsset,
					FullPathLoadingScreensSettingsAsset,
					FileEditorUtil.AssetName,
					$"{ScriptableRef.PathData}Modules/{FileNameLoadingScreensSettingsAsset}");
			}
#endif
			
#if CARTERGAMES_CART_MODULE_NOTIONDATA
			if (cacheNotionDataSettingsAsset == null)
			{
				FileEditorUtil.CreateSoGetOrAssignAssetCache(
					ref cacheNotionDataSettingsAsset,
					FilterNotionDataSettingsAsset,
					FullPathNotionDataSettingsAsset,
					FileEditorUtil.AssetName,
					$"{ScriptableRef.PathData}Modules/{FileNameNotionDataSettingsAsset}");
			}
#endif
		}
	}
}