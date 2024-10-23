using System;

namespace CarterGames.Cart.Core.Save
{
	public interface ISaveMethod
	{
		void SaveKeyPair<T>(string key, T value);
		T GetValue<T>(string key);
		bool TryGetValue<T>(string key, out T value);
	}
}