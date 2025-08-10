using System;
using System.Collections.Generic;

namespace CarterGames.Cart.Core.Editor
{
    [Serializable]
    public class ProjectSetupTemplate
    {
        public string name;
        public List<string> paths;

        
        public static ProjectSetupTemplate Copy(ProjectSetupTemplate defaultFolderStructure)
        {
            return new ProjectSetupTemplate()
            {
                name = defaultFolderStructure.name,
                paths = new List<string>(defaultFolderStructure.paths)
            };
        }
    }
}