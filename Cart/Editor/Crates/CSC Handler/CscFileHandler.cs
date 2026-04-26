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
using System.Linq;
using System.Text;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Logs;
using CarterGames.Cart.Management.Editor;
using UnityEditor;

namespace CarterGames.Cart.Crates
{
    public sealed class CscFileHandler : IAssetEditorInitialize, IAssetEditorReload
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static object Lock = new object();
        
        private const string FileName = "csc.rsp";
        private static readonly string FilePath = $"Assets/{FileName}";
        private const string Filter = "csc t:defaultasset";
        private static string lastDataCache;


        private static readonly StringBuilder Builder = new StringBuilder();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static string LastRead
        {
            get
            {
                if (lastDataCache != null) return lastDataCache;
                lastDataCache = GetFileContents();
                return lastDataCache;
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IAssetEditorInitialize Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public int InitializeOrder { get; }
        
        public void OnEditorInitialized()
        {
            TryInitialize();
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IAssetEditorReload Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public void OnEditorReloaded()
        {
            TryInitialize();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static void TryInitialize()
        {
            var assets = AssetDatabase.FindAssets(Filter, new[] {"Assets"});
            var paths = new List<string>();
            
            foreach (var bbb in assets)
            {
                paths.Add(AssetDatabase.GUIDToAssetPath(bbb));
            }

            if (paths.Any(t => t.Contains(FileName)))
            {
                if (AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().ObjectRef.Fp("asset").objectReferenceValue != null) return;
                
                AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().ObjectRef.Fp("asset").objectReferenceValue =
                    AssetDatabase.LoadAssetAtPath<DefaultAsset>(paths.First(t => t.Contains(FileName)));
                
                AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().ObjectRef.ApplyModifiedProperties();
                AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().ObjectRef.Update();

                return;
            }
            
            GenerateFile();
        }
        
        
        private static void GenerateFile()
        {
            File.Create(FilePath).Dispose();
        }


        private static bool HasDefine(string define)
        {
            lock (Lock)
            {
                if (AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().ObjectRef.Fp("asset").objectReferenceValue == null)
                {
                    TryInitialize();
                }

                return LastRead.Contains(define);
            }
        }
        

        public static bool HasDefine(Crate crate)
        {
            if (crate == null) return false;
            return HasDefine(crate.CrateDefine);
        }
        
        
        public static void AddDefine(string define)
        {
            lock (Lock)
            {
                if (HasDefine(define)) return;
                Append($"-define:{define}");
            }
        }


        public static void AddDefine(Crate crate)
        {
            lock (Lock)
            {
                if (HasDefine(crate)) return;
                Append($"-define:{crate.CrateDefine}");
                CartLogger.Log<CartLogs>($"Enabled: {crate.CrateName}");
            }
        }
        
        
        public static void AddDefine(List<string> defines)
        {
            lock (Lock)
            {
                var toAdd = new List<string>();

                foreach (var define in defines)
                {
                    if (HasDefine(define)) continue;
                    toAdd.Add($"-define:{define}");
                }

                Append(toAdd);
            }
        }
        
        
        public static void AddDefine(List<Crate> crates)
        {
            lock (Lock)
            {
                var toAdd = new List<string>();

                foreach (var crate in crates)
                {
                    if (HasDefine(crate)) continue;
                    toAdd.Add($"-define:{crate.CrateDefine}");
                }

                Append(toAdd);

                foreach (var crate in crates)
                {
                    CartLogger.Log<CartLogs>($"Enabled: {crate.CrateName}");
                }
            }
        }

        
        public static void RemoveDefine(string define)
        {
            lock (Lock)
            {
                if (!HasDefine(define)) return;
                Remove($"-define:{define}");
            }
        }
        

        public static void RemoveDefine(Crate crate)
        {
            lock (Lock)
            {
                if (!HasDefine(crate)) return;
                Remove($"-define:{crate.CrateDefine}");
                CartLogger.Log<CartLogs>($"Disabled: {crate.CrateName}");
            }
        }
        
        
        public static void RemoveDefine(List<string> defines)
        {
            lock (Lock)
            {
                var toRemove = new List<string>();
            
                foreach (var define in defines)
                {
                    if (HasDefine(define)) continue;
                    toRemove.Add($"-define:{define}");
                }
            
                Remove(toRemove);
            }
        }
        
        
        public static void RemoveDefine(List<Crate> crates)
        {
            var toRemove = new List<string>();
            
            foreach (var crate in crates)
            {
                if (!HasDefine(crate)) continue;
                toRemove.Add($"-define:{crate.CrateDefine}");
            }
            
            Remove(toRemove);
            
            foreach (var crate in crates)
            {
                CartLogger.Log<CartLogs>($"Disabled: {crate.CrateName}");
            }
        }


        private static void Append(string toAdd)
        {
            var write = new StreamWriter(FilePath);
            Builder.Clear();
            
            foreach (var entry in LastRead.Split('-'))
            {
                var parsedEntry = $"-{entry.Trim()}";
                
                if (parsedEntry.Length <= 1) continue;
                Builder.Append(parsedEntry);
                Builder.AppendLine();
            }
            
            Builder.Append(toAdd);
            
            write.Write(Builder.ToString());
            write.Close();
            
            EditorUtility.SetDirty(AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().AssetRef);
            AssetDatabase.Refresh();
        }


        private static void Append(List<string> toAdd)
        {
            var write = new StreamWriter(FilePath);
            Builder.Clear();
            
            foreach (var entry in LastRead.Split('-'))
            {
                var parsedEntry = $"-{entry.Trim()}";
                
                if (parsedEntry.Length <= 1) continue;
                Builder.Append(parsedEntry);
                Builder.AppendLine();
            }
            
            foreach (var entry in toAdd)
            {
                Builder.Append(entry);
                
                if (entry.Equals(toAdd.Last())) continue;
                Builder.AppendLine();
            }
            
            write.Write(Builder.ToString());
            write.Close();
            
            EditorUtility.SetDirty(AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().AssetRef);
            AssetDatabase.Refresh();
        }
        
        
        private static void Remove(string line)
        {
            var write = new StreamWriter(FilePath);
            Builder.Clear();

            for (var i = 0; i < LastRead.Split('-').Length; i++)
            {
                var entry = LastRead.Split('-')[i];
                var parsedEntry = $"-{entry.Trim()}";

                if (parsedEntry.Length <= 1) continue;
                if (parsedEntry.Equals(line)) continue;

                Builder.Append(parsedEntry);
                if (i.Equals(LastRead.Split('-').Length - 1)) continue;
                Builder.AppendLine();
            }
            
            write.Write(Builder.ToString());
            write.Close();
            
            EditorUtility.SetDirty(AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().AssetRef);
            AssetDatabase.Refresh();
        }
        
        
        private static void Remove(List<string> lines)
        {
            var write = new StreamWriter(FilePath);
            Builder.Clear();

            for (var i = 0; i < LastRead.Split('-').Length; i++)
            {
                var entry = LastRead.Split('-')[i];
                var parsedEntry = $"-{entry.Trim()}";
                
                if (parsedEntry.Equals("-")) continue;
                
                foreach (var line in lines)
                {
                    if (!parsedEntry.Equals(line)) continue;
                    goto SkipEntryAsRemoving;
                }

                Builder.Append(parsedEntry);
                if (i.Equals(LastRead.Split('-').Length - 1)) continue;
                Builder.AppendLine();
                
                SkipEntryAsRemoving: ;
            }
            
            write.Write(Builder.ToString());
            write.Close();
            
            EditorUtility.SetDirty(AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().AssetRef);
            AssetDatabase.Refresh();
        }


        public static void EditDefines(List<string> toAdd, List<string> toRemove)
        {
            lock (Lock)
            {
                if (lastDataCache == null)
                {
                    lastDataCache = GetFileContents();
                }

                var totalRemoved = 0;
                var write = new StreamWriter(FilePath);
                Builder.Clear();

                for (var i = 0; i < LastRead.Split('-').Length; i++)
                {
                    var entry = LastRead.Split('-')[i];
                    var parsedEntry = $"-{entry.Trim()}";

                    if (parsedEntry.Length <= 1) continue;

                    if (toRemove.Count > 0)
                    {
                        foreach (var line in toRemove)
                        {
                            if (!parsedEntry.Equals($"-define:{line}")) continue;
                            totalRemoved++;
                            goto SkipEntryAsRemoving;
                        }
                    }

                    Builder.Append(parsedEntry);
                    if (i.Equals(LastRead.Split('-').Length - 1)) continue;
                    Builder.AppendLine();

                    SkipEntryAsRemoving: ;
                }

                if (toAdd.Count > 0)
                {
                    foreach (var entry in toAdd)
                    {
                        if (toRemove.Count == 0 || totalRemoved == 0)
                        {
                            Builder.AppendLine();
                        }

                        Builder.Append($"-define:{entry}");

                        if (entry.Equals(toAdd.Last())) continue;
                        Builder.AppendLine();
                    }
                }

                write.Write(Builder.ToString());
                write.Close();
            }
            

            EditorUtility.SetDirty(AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().AssetRef);
            AssetDatabase.Refresh();
        }


        private static string GetFileContents()
        {
            var reader = new StreamReader(FilePath);
            var text = reader.ReadToEnd();
            reader.Close();

            return text;
        }
    }
}