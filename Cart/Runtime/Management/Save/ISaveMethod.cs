using System;

namespace CarterGames.Cart.Save
{
	public interface ISaveMethod
	{
		bool HasKey(string key);
		T GetValue<T>(string key, SaveCtx ctx = SaveCtx.ExactValue);
		void SetValue<T>(string key, T value, SaveCtx ctx = SaveCtx.ExactValue);
	}
}