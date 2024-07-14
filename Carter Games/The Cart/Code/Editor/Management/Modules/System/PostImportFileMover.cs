using System.Linq;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules
{
    public static class PostImportFileMover
    {
        private static string CorrectPathForModule(IModule module)
        {
            return module.ModuleInstallPath;
        }
        
        
        private static string CurrentPathForModule(IModule module)
        {
            var path = "Assets";
            path += module.ModuleInstallPath.Replace(ScriptableRef.AssetBasePath, string.Empty);
            return path;
        }


        public static bool IsInRightPath(IModule module)
        {
            return AssetDatabase.IsValidFolder(CorrectPathForModule(module)) && (CurrentPathForModule(module) == CorrectPathForModule(module));
        }


        public static void UpdateFileLocation(IModule module)
        {
            if (IsInRightPath(module)) return;
            
            AssetDatabase.StartAssetEditing();
            
            AssetDatabase.MoveAsset(CurrentPathForModule(module), CorrectPathForModule(module));
            
            if (ShouldDeleteExtraPaths())
            {
                AssetDatabase.DeleteAsset("Assets/Carter Games/The Cart/Modules");
                AssetDatabase.DeleteAsset("Assets/Carter Games/The Cart/");
                AssetDatabase.DeleteAsset("Assets/Carter Games/");
            }
            
            AssetDatabase.StopAssetEditing();
        }


        private static bool ShouldDeleteExtraPaths()
        {
            return !ScriptableRef.AssetBasePath.Equals("Assets");
        }
    }
}