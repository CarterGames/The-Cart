using System;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Modules.Settings;
using UnityEditor;

namespace CarterGames.Cart.Core.MetaData.Editor
{
	public class AssetDefMetaData : IScriptableAssetDef<DataAssetMetaData>
	{
		private static DataAssetMetaData cache;
		private static SerializedObject objCache;

		public Type AssetType => typeof(DataAssetMetaData);
		public string DataAssetFileName => "[Cart] Meta Data Asset.asset";
		public string DataAssetFilter => $"t:{typeof(DataAssetMetaData).FullName} name={DataAssetFileName}";
		public string DataAssetPath => $"{ScriptableRef.FullPathData}/{DataAssetFileName}";

		public DataAssetMetaData AssetRef => ScriptableRef.GetOrCreateAsset(this, ref cache);
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