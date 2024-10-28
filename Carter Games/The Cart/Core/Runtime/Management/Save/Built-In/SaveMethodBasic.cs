namespace CarterGames.Cart.Core.Save
{
	public class SaveMethodBasic : ISaveMethod
	{
		public void SaveKeyPair<T>(string key, T value)
		{
			RuntimePerUserSettings.SetValue<T>(key, value);
		}

		public T GetValue<T>(string key)
		{
			return RuntimePerUserSettings.GetValue<T>(key);
		}
		

		public bool TryGetValue<T>(string key, out T value)
		{
			value = GetValue<T>(key);
			return value != null;
		}
	}
}