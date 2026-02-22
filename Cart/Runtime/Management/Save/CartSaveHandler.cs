using CarterGames.Cart.Data;
using CarterGames.Cart.Management;

namespace CarterGames.Cart.Save
{
	public static class CartSaveHandler
	{
		private static ISaveMethod SaveMethod => CoreSettings!.SaveMethodType;
		

		private static DataAssetCoreRuntimeSettings CoreSettings =>
			DataAccess.GetAsset<DataAssetCoreRuntimeSettings>();


		public static bool HasKey(string key)
		{
			return SaveMethod.HasKey(key);
		}
		
		
		public static T Get<T>(string key, SaveCtx ctx = SaveCtx.ExactValue)
		{
			return SaveMethod.GetValue<T>(key, ctx);
		}
		
		
		public static void Set<T>(string key, T value, SaveCtx ctx = SaveCtx.ExactValue)
		{
			SaveMethod.SetValue(key, value, ctx);
		}
	}
}