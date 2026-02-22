using System;
using UnityEngine;

namespace CarterGames.Cart.Save
{
	public class SaveMethodBasic : ISaveMethod
	{
		public bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}
		

		public T GetValue<T>(string key, SaveCtx ctx = SaveCtx.ExactValue)
		{
			switch (ctx)
			{
				case SaveCtx.ExactValue:
					return RuntimePerUserSettings.GetValue<T>(key);
				case SaveCtx.JsonConvert:
					return JsonUtility.FromJson<T>(RuntimePerUserSettings.GetValue<string>(key));
				default:
					throw new ArgumentOutOfRangeException(nameof(ctx), ctx, null);
			}
		}

		
		public void SetValue<T>(string key, T value, SaveCtx ctx = SaveCtx.ExactValue)
		{
			switch (ctx)
			{
				case SaveCtx.ExactValue:
					RuntimePerUserSettings.SetValue<T>(key, value);
					break;
				case SaveCtx.JsonConvert:
					RuntimePerUserSettings.SetValue<string>(key, JsonUtility.ToJson(value));
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(ctx), ctx, null);
			}
		}
	}
}