// ----------------------------------------------------------------------------
// DependencyLibraryPreBuildProcess.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 22/10/2021
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace DependencyLibrary.Editor
{
    public class DependencyLibraryPreBuildProcess : IPreprocessBuildWithReport
    {
        /// <summary>
        /// Lists all the types that can be a variable
        /// </summary>
        /// <remarks>If you want to add another variable, it'll need to be added here for it to reset xD</remarks>
        private static List<string> ValidTypes = new List<string>
        {
            "Bool",
            "Color",
            "Double",
            "Float",
            "Int",
            "Long",
            "Quaternion",
            "Short",
            "Sprite",
            "String",
            "Vector2",
            "Vector3",
            "Vector4"
        };


        /// <summary>
        /// Legit just needed for the build process interface xD
        /// </summary>
        public int callbackOrder { get; }
        
        
        /// <summary>
        /// The menu item to reset to values manually for testing purposes xD
        /// </summary>
        [MenuItem("Tools/Dependency Library/Reset All To Default Values")]
        public static void ResetValues()
        {
            ResetAllValuesToDefault();
        }


        /// <summary>
        /// Runs when you press to make any build.
        /// </summary>
        /// <param name="report">The build report</param>
        public void OnPreprocessBuild(BuildReport report)
        {
            // Calls to reset al the values...
            ResetAllValuesToDefault();
        }

        
        /// <summary>
        /// Resets all the Scriptable Objects in the project that are a part of this system to their default values.
        /// </summary>
        private static void ResetAllValuesToDefault()
        {
            // Loops through all the types...
            foreach (var typeName in ValidTypes)
            {
                // Sets the filter to find variables of that type & Finds all the assets of said type...
                var _typeToFind = $"t:{typeName}Variable";
                var _allFound = AssetDatabase.FindAssets(_typeToFind, null);

                // Loops through each variable to edit them...
                foreach (var _found in _allFound)
                {
                    // Sets the path of the asset & gets the reset interface...
                    var _path = AssetDatabase.GUIDToAssetPath(_found);
                    var _loadedAsset = (IDependencyResetOnBuild) AssetDatabase.LoadAssetAtPath(_path, typeof(IDependencyResetOnBuild));
                    
                    // Calls to reset xD
                    _loadedAsset?.ResetValue();
                }
            }
        }
    }
}