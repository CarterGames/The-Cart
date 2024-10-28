/*
 * Copyright (c) 2024 Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using UnityEngine;

namespace CarterGames.Cart.Core.Save
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