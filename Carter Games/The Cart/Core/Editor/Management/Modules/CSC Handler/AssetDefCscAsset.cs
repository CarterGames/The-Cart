using System;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Modules.Settings;
using UnityEditor;

namespace CarterGames.Cart.Modules.Editor
{
	public class AssetDefCscAsset : IScriptableAssetDef<DataAssetCsc>
	{
		private static DataAssetCsc cache;
		private static SerializedObject objCache;

		public Type AssetType => typeof(DataAssetCsc);
		public string DataAssetFileName => "[Cart] Csc Data Asset.asset";
		public string DataAssetFilter => $"t:{typeof(DataAssetCsc).FullName} name={DataAssetFileName}";
		public string DataAssetPath => $"{ScriptableRef.FullPathData}/{DataAssetFileName}";

		public DataAssetCsc AssetRef => ScriptableRef.GetOrCreateAsset(this, ref cache);
		public SerializedObject ObjectRef => ScriptableRef.GetOrCreateAssetObject(this, ref objCache);
		
		public void TryCreate()
		{
			ScriptableRef.GetOrCreateAsset(this, ref cache);
		}
		
		/// <summary>
		/// Runs when the asset is created.
		/// </summary>
		public void OnCreated() { }
	}
}