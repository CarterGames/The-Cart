using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Modules.Settings;

namespace CarterGames.Cart.Core.Editor.Extensions
{
	public interface ICartLibraryExtension
	{
		string Name { get; }
		ISettingsProvider SettingsProvider { get; }
		IScriptableAssetDef<DataAsset> ScriptableAssetDef { get; }
	}
}