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

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    public static class PackageManifestHandler
    {
        private static JToken GetDependencies()
        {
            var path = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            return JObject.Parse(File.ReadAllText(path));
        }


        private static void UpdateData(JToken data)
        {
            var path = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            File.WriteAllText(path, data.ToString());
        }
        
        
        public static bool IsInstalled(string key)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(GetDependencies()["dependencies"]?.ToString() ?? string.Empty).ContainsKey(key);
        }
        
        
        public static void AddEntry(string key, string value)
        {
            if (IsInstalled(key)) return;

            var data = GetDependencies();
            var lookup = JsonConvert.DeserializeObject<Dictionary<string, string>>(data["dependencies"]?.ToString() ?? string.Empty);
            
            lookup.Add(key, value);
            data["dependencies"] = JToken.FromObject(lookup);

            UpdateData(data);
        }


        public static void RemoveEntry(string key)
        {
            if (!IsInstalled(key)) return;

            var data = GetDependencies();
            var lookup = JsonConvert.DeserializeObject<Dictionary<string, string>>(data["dependencies"]?.ToString() ?? string.Empty);
            
            lookup.Remove(key);
            data["dependencies"] = JToken.FromObject(lookup);
            
            UpdateData(data);
        }
    }
}