using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Management;

namespace CarterGames.Cart.Core.Save
{
	public static class CartSaveHandler
	{
		private static ISaveMethod SaveMethod => CoreSettings.SaveMethodType;

		private static DataAssetCartGlobalRuntimeSettings CoreSettings =>
			DataAccess.GetAsset<DataAssetCartGlobalRuntimeSettings>();
		
		
		public static T Get<T>(string key)
		{
			return SaveMethod.GetValue<T>(key);
		}
		
		
		public static void Set<T>(string key, T value)
		{
			SaveMethod.SaveKeyPair(key, value);
		}
	}
}