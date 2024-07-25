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
using CarterGames.Cart.Core.Logs;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;

namespace CarterGames.Cart.Modules
{
    public sealed class CscFileHandler : IAssetEditorInitialize, IAssetEditorReload
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
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
                if (ScriptableRef.CscObject.Fp("asset").objectReferenceValue != null) return;
                
                ScriptableRef.CscObject.Fp("asset").objectReferenceValue =
                    AssetDatabase.LoadAssetAtPath<DefaultAsset>(paths.First(t => t.Contains(FileName)));

                ScriptableRef.CscObject.ApplyModifiedProperties();
                ScriptableRef.CscObject.Update();

                return;
            }
            
            GenerateFile();
        }
        
        
        private static void GenerateFile()
        {
            File.Create(FilePath);
        }
        

        public static bool HasDefine(IModule module)
        {
            if (ScriptableRef.CscObject.Fp("asset").objectReferenceValue == null)
            {
                TryInitialize();
            }
            
            return LastRead.Contains(module.ModuleDefine);
        }


        public static void AddDefine(IModule module)
        {
            if (HasDefine(module)) return;
            Append($"-define:{module.ModuleDefine}");
            CartLogger.Log<LogCategoryModules>($"Enabled: {module.ModuleName}");
        }
        
        
        public static void AddDefine(List<IModule> modules)
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


        public static void RemoveDefine(IModule module)
        {
            if (!HasDefine(module)) return;
            Remove($"-define:{module.ModuleDefine}");
            CartLogger.Log<LogCategoryModules>($"Disabled: {module.ModuleName}");
        }
        
        
        public static void RemoveDefine(List<IModule> modules)
        {
            var toRemove = new List<string>();
            
            foreach (var module in modules)
            {
                if (HasDefine(module)) continue;
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
            
            EditorUtility.SetDirty(ScriptableRef.CscAsset.Asset);
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
            
            EditorUtility.SetDirty(ScriptableRef.CscAsset.Asset);
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
            
            EditorUtility.SetDirty(ScriptableRef.CscAsset.Asset);
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

                if (parsedEntry.Length <= 1) continue;

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
            
            EditorUtility.SetDirty(ScriptableRef.CscAsset.Asset);
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