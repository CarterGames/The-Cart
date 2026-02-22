using System.Linq;
using CarterGames.Cart.Core.Management;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace CarterGames.Cart.Core.Editor
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