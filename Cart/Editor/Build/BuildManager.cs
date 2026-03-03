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

using System.Linq;
using CarterGames.Cart.Management;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace CarterGames.Cart.Editor
{
    public class BuildManager : IPostprocessBuildWithReport
    {
        [InitializeOnLoadMethod]
        private static void Initialize() 
        {
            BuildPlayerWindow.RegisterBuildPlayerHandler(CartBuildHandler);
        }


        private static void CartBuildHandler(BuildPlayerOptions options)
        {
            var handlers = AssemblyHelper.GetClassesOfType<BuildHandler>(false)
                .OrderBy(t => t.Priority)
                .ToArray();

            var forceCancelBuild = false;
            
            foreach (var handler in handlers)
            {
                var preBuildResult = handler.OnPreBuild();
                
                if (preBuildResult) continue;
                forceCancelBuild = true;
                break;
            }
            
            if (forceCancelBuild)
            {
                return;
            }

            foreach (var handler in handlers)
            {
                handler.OnBuildStarted();
            }
            
            BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
        }


        public int callbackOrder => 0;
        
        
        public void OnPostprocessBuild(BuildReport report)
        {
            var handlers = AssemblyHelper.GetClassesOfType<BuildHandler>(false)
                .OrderBy(t => t.Priority)
                .ToArray();
            
            foreach (var handler in handlers)
            {
                handler.OnPostBuild();
                handler.OnBuildCompleted();
            }
        }
    }
}