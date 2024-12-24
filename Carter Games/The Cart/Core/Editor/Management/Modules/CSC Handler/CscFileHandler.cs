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
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Logs;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules
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
                if (ScriptableRef.GetAssetDef<DataAssetCsc>().ObjectRef.Fp("asset").objectReferenceValue != null) return;
                
                ScriptableRef.GetAssetDef<DataAssetCsc>().ObjectRef.Fp("asset").objectReferenceValue =
                    AssetDatabase.LoadAssetAtPath<DefaultAsset>(paths.First(t => t.Contains(FileName)));
                
                ScriptableRef.GetAssetDef<DataAssetCsc>().ObjectRef.ApplyModifiedProperties();
                ScriptableRef.GetAssetDef<DataAssetCsc>().ObjectRef.Update();

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
                if (ScriptableRef.GetAssetDef<DataAssetCsc>().ObjectRef.Fp("asset").objectReferenceValue == null)
                {
                    TryInitialize();
                }

                return LastRead.Contains(define);
            }
        }
        

        public static bool HasDefine(IModule module)
        {
            if (module == null) return false;
            return HasDefine(module.ModuleDefine);
        }
        
        
        public static void AddDefine(string define)
        {
            lock (Lock)
            {
                if (HasDefine(define)) return;
                Append($"-define:{define}");
            }
        }


        public static void AddDefine(IModule module)
        {
            lock (Lock)
            {
                if (HasDefine(module)) return;
                Append($"-define:{module.ModuleDefine}");
                CartLogger.Log<LogCategoryModules>($"Enabled: {module.ModuleName}");
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
        
        
        public static void AddDefine(List<IModule> modules)
        {
            lock (Lock)
            {
                var toAdd = new List<string>();

                foreach (var module in modules)
                {
                    if (HasDefine(module)) continue;
                    toAdd.Add($"-define:{module.ModuleDefine}");
                }

                Append(toAdd);

                foreach (var module in modules)
                {
                    CartLogger.Log<LogCategoryModules>($"Enabled: {module.ModuleName}");
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
        

        public static void RemoveDefine(IModule module)
        {
            lock (Lock)
            {
                if (!HasDefine(module)) return;
                Remove($"-define:{module.ModuleDefine}");
                CartLogger.Log<LogCategoryModules>($"Disabled: {module.ModuleName}");
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
        
        
        public static void RemoveDefine(List<IModule> modules)
        {
            var toRemove = new List<string>();
            
            foreach (var module in modules)
            {
                if (!HasDefine(module)) continue;
                toRemove.Add($"-define:{module.ModuleDefine}");
            }
            
            Remove(toRemove);
            
            foreach (var module in modules)
            {
                CartLogger.Log<LogCategoryModules>($"Disabled: {module.ModuleName}");
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
            
            EditorUtility.SetDirty(ScriptableRef.GetAssetDef<DataAssetCsc>().AssetRef);
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
            
            EditorUtility.SetDirty(ScriptableRef.GetAssetDef<DataAssetCsc>().AssetRef);
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
            
            EditorUtility.SetDirty(ScriptableRef.GetAssetDef<DataAssetCsc>().AssetRef);
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
            
            EditorUtility.SetDirty(ScriptableRef.GetAssetDef<DataAssetCsc>().AssetRef);
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
            

            EditorUtility.SetDirty(ScriptableRef.GetAssetDef<DataAssetCsc>().AssetRef);
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