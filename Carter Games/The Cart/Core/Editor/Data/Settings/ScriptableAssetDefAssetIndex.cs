using System;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Modules.Settings;
using UnityEditor;

namespace CarterGames.Cart.Core.Data.Editor
{
	public class ScriptableAssetDefAssetIndex : IScriptableAssetDef<DataAssetIndex>
	{
		private static DataAssetIndex cache;
		private static SerializedObject objCache;
		
		public Type AssetType => typeof(DataAssetIndex);
		public string DataAssetFileName => "[Cart] Data Asset Index.asset";
		public string DataAssetFilter => $"t:{typeof(DataAssetIndex).FullName}";
		public string DataAssetPath => $"{ScriptableRef.FullPathResources}{DataAssetFileName}";

		public DataAssetIndex AssetRef => ScriptableRef.GetOrCreateAsset(this, ref cache);
		public SerializedObject ObjectRef => ScriptableRef.GetOrCreateAssetObject(this, ref objCache);
		public void TryCreate()
		{
			ScriptableRef.GetOrCreateAsset(this, ref cache);
		}
	}
}