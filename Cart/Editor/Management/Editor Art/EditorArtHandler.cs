using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Management.Editor
{
    /// <summary>
    /// Handles the art for the editor setup. Without needing exact references.
    /// </summary>
    public static class EditorArtHandler
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static readonly Dictionary<string, string> DefinesLookup = new Dictionary<string, string>();
        private static readonly Dictionary<string, Texture2D> CacheLookup = new Dictionary<string, Texture2D>();
        private static IEditorArtDefine[] CacheDefines = null;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the asset path for any art asset for the asset from this script.
        /// </summary>
        /// <param name="pathEnd"></param>
        /// <returns></returns>
        private static string GetPathToAsset(string pathEnd)
        {
            string basePath = Path.GetFullPath(Path.Combine(GetCurrentFileName(), $"../../../../../{pathEnd}")).Replace(@"\", "/");;
            
            // Needed as otherwise the art gets a null ref in the package manager version.
            if (Directory.Exists("Assets/Carter Games/The Cart"))
            {
                return "Assets/" + basePath.Split(new string[1] { "/Assets/" }, StringSplitOptions.None)[1];
            }
            else
            {
                return "Art/" + basePath.Split(new string[1] { "/Art/" }, StringSplitOptions.None)[1];
            }
        }
        
        
        /// <summary>
        /// Gets the file name for the location of this class.
        /// </summary>
        /// <param name="fileName">The path for this file.</param>
        /// <returns>String</returns>
        private static string GetCurrentFileName([System.Runtime.CompilerServices.CallerFilePath] string fileName = null)
        {
            return fileName;
        }


        /// <summary>
        /// Gets an art icon from its constant id.
        /// </summary>
        /// <param name="id">The id to get</param>
        /// <returns>Texture2D</returns>
        public static Texture2D GetIcon(string id)
        {
            if (CacheDefines == null)
            {
                CacheDefines = AssemblyHelper.GetClassesOfType<IEditorArtDefine>(false).ToArray();

                foreach (var entry in CacheDefines)
                {
                    DefinesLookup.Add(entry.Uid, entry.InternalPath);
                }
            }
            
            if (CacheLookup.ContainsKey(id))
            {
                if (CacheLookup[id] == null)
                {
                    CacheLookup[id] = AssetDatabase.LoadAssetAtPath<Texture2D>(GetPathToAsset(DefinesLookup[id]));
                }
                    
                return CacheLookup[id];
            }
            
            if (!DefinesLookup.ContainsKey(id)) return null;
            CacheLookup.Add(id, AssetDatabase.LoadAssetAtPath<Texture2D>(GetPathToAsset(DefinesLookup[id])));
            return CacheLookup[id];
        }
    }
}