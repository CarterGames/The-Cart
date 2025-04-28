using CarterGames.Cart.Core.Save;
using UnityEngine;

namespace CarterGames.Cart.Core.Logs
{
    public class LogCategoryStates
    {
        private static readonly string SaveKey = $"CarterGames_TheCart_Logging_States_{RuntimePerUserSettings.UniqueId}";
        
        
        public static SerializableDictionary<string, bool> CategoryStates
        {
            get
            {
                if ((string.IsNullOrEmpty(RuntimePerUserSettings.GetValue<string>(SaveKey))))
                {
                    RuntimePerUserSettings.SetValue<string>(SaveKey, JsonUtility.ToJson(new LogCategorySaveData()));
                }
                
                return SerializableDictionary<string, bool>
                    .FromKeyPairValueList(JsonUtility.FromJson<LogCategorySaveData>(RuntimePerUserSettings
                        .GetValue<string>(SaveKey)).Data);
            }
            set => RuntimePerUserSettings.SetValue<string>(SaveKey, JsonUtility.ToJson(new LogCategorySaveData(value)));
        }


        public static bool IsEnabled<T>() where T : LogCategory
        {
            if (!CategoryStates.ContainsKey(typeof(T).FullName)) return false;
            return CategoryStates[typeof(T).FullName];
        }
    }
}