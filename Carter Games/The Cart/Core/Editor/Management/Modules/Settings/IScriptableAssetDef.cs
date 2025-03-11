using System;
using CarterGames.Cart.Core.Data;
using UnityEditor;

namespace CarterGames.Cart.Modules.Settings
{
	public interface IScriptableAssetDef<out T> where T : DataAsset
	{
		Type AssetType { get; }
		string DataAssetFileName { get; }
		string DataAssetFilter { get; }
		string DataAssetPath { get; }
		T AssetRef { get; }
		SerializedObject ObjectRef { get; }
		void TryCreate();
		void OnCreated();
	}
}