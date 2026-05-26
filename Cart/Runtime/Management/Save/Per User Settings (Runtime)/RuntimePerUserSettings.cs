/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using System;
using UnityEngine;

namespace CarterGames.Cart.Save
{
    /// <summary>
    /// Handles all the per user settings for runtime use specifically.
    /// </summary>
    public static class RuntimePerUserSettings
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private const string UniqueIdId = "CarterGames_TheCart_Runtime_UUID";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The unique if for the assets settings to be per project...
        /// </summary>
        /// <remarks>
        /// Saved to player pref to allow settings to be different per project in the same editor version.
        /// </remarks>
        public static string UniqueId => (string)GetOrCreateValue<string>(UniqueIdId, Guid.NewGuid().ToString());
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public static T GetValue<T>(string key, object defaultValue = null)
        {
            return (T) GetOrCreateValue<T>(key, defaultValue);
        }


        private static object GetOrCreateValue<T>(string key, object defaultValue = null)
        {
            if (PlayerPrefs.HasKey(key))
            {
                switch (typeof(T))
                {
                    case var x when x == typeof(bool):
                        return PlayerPrefs.GetInt(key) == 1;
                    case var x when x == typeof(int):
                        return PlayerPrefs.GetInt(key);
                    case var x when x == typeof(float):
                        return PlayerPrefs.GetFloat(key);
                    case var x when x == typeof(string):
                        return PlayerPrefs.GetString(key);
                    case var x when x == typeof(Vector2):
                        return JsonUtility.FromJson<Vector2>(PlayerPrefs.GetString(key));
                    case var x when x == typeof(Color):
                        return JsonUtility.FromJson<Color>(PlayerPrefs.GetString(key));
                    default:
                        return null;
                }
            }

            switch (typeof(T))
            {
                case var x when x == typeof(bool):
                    PlayerPrefs.SetInt(key,
                        defaultValue == null ? 0 : defaultValue.ToString().ToLower() == "true" ? 1 : 0);
                    return PlayerPrefs.GetInt(key) == 1;
                case var x when x == typeof(int):
                    PlayerPrefs.SetInt(key, defaultValue == null ? 0 : (int) defaultValue);
                    return PlayerPrefs.GetInt(key);
                case var x when x == typeof(float):
                    PlayerPrefs.SetFloat(key, defaultValue == null ? 0 : (float) defaultValue);
                    return PlayerPrefs.GetFloat(key);
                case var x when x == typeof(string):
                    PlayerPrefs.SetString(key, (string) defaultValue);
                    return PlayerPrefs.GetString(key);
                case var x when x == typeof(Vector2):
                    PlayerPrefs.SetString(key,
                        defaultValue == null
                            ? JsonUtility.ToJson(Vector2.zero)
                            : JsonUtility.ToJson(defaultValue));
                    return JsonUtility.FromJson<Vector2>(PlayerPrefs.GetString(key));
                case var x when x == typeof(Color):
                    PlayerPrefs.SetString(key,
                        defaultValue == null
                            ? JsonUtility.ToJson(Color.clear)
                            : JsonUtility.ToJson(defaultValue));
                    return JsonUtility.FromJson<Color>(PlayerPrefs.GetString(key));
                default:
                    return null;
            }
        }


        public static void SetValue<T>(string key, object value)
        {
            switch (typeof(T))
            {
                case var x when x == typeof(bool):
                    PlayerPrefs.SetInt(key, ((bool) value) ? 1 : 0);
                    break;
                case var x when x == typeof(int):
                    PlayerPrefs.SetInt(key, (int) value);
                    break;
                case var x when x == typeof(float):
                    PlayerPrefs.SetFloat(key, (float) value);
                    break;
                case var x when x == typeof(string):
                    PlayerPrefs.SetString(key, (string) value);
                    break;
                case var x when x == typeof(Vector2):
                    PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
                    break;
                case var x when x == typeof(Color):
                    PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
                    break;
            }

            PlayerPrefs.Save();
        }
    }
}