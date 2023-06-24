/*
 * Copyright (c) 2018-Present Carter Games
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

using UnityEditor;

namespace Scarlet.Editor.Setup
{
    /// <summary>
    /// A script to handle the creation of a basic project folder structure.
    /// </summary>
    public static class ProjectFolderSetup
    {
        /// <summary>
        /// Static | Adds the button to call to run the setup.
        /// </summary>
        [MenuItem("Tools/Scarlet Library/Setup Basic Folder Structure", priority = 2000)]
        public static void RunSetupProject()
        {
            CreateBasicProjectFolderStructure();
        }


        /// <summary>
        /// Creates the basic folder structure when called.
        /// </summary>
        private static void CreateBasicProjectFolderStructure()
        {
            TryCreateFolder("_Assets", "Assets");
            TryCreateFolder("_Project", "Assets");
            TryCreateFolder("Art", "Assets/_Project");
            TryCreateFolder("2D", "Assets/_Project/Art");
            TryCreateFolder("3D", "Assets/_Project/Art");
            TryCreateFolder("UI", "Assets/_Project/Art");
            TryCreateFolder("Code", "Assets/_Project");
            TryCreateFolder("Editor", "Assets/_Project/Code");
            TryCreateFolder("Runtime", "Assets/_Project/Code");
            TryCreateFolder("Data","Assets/_Project");
            TryCreateFolder("Prefabs","Assets/_Project");
            TryCreateFolder("World", "Assets/_Project/Prefabs");
            TryCreateFolder("UI", "Assets/_Project/Prefabs");
            TryCreateFolder("Scenes", "Assets/_Project");
            TryCreateFolder("UI", "Assets/_Project/Scenes");
            TryCreateFolder("Menu", "Assets/_Project/Scenes");
            TryCreateFolder("Game", "Assets/_Project/Scenes");
            TryCreateFolder("_Dev", "Assets");
            
            AssetDatabase.SaveAssets();   
            AssetDatabase.Refresh();

            Dialogue.Display("Folder Structure Generator", "The folder structure has been generated", "Continue");
        }
        
        
        /// <summary>
        /// Tries to create a folder using asset database. If it already exists it'll skip it. 
        /// </summary>
        /// <param name="newFolderName">The name of the new folder.</param>
        /// <param name="parentPath">The path that goes to the new folder, without any / at the end of the path.</param>
        private static void TryCreateFolder(string newFolderName, string parentPath)
        {
            if (AssetDatabase.IsValidFolder(parentPath + "/" + newFolderName)) return;
            AssetDatabase.CreateFolder(parentPath, newFolderName);
        }
    }
}