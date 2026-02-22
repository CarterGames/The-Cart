using System.Linq;
using CarterGames.Cart.Core.Management;

namespace CarterGames.Cart.Core.Editor
{
    public class BuildCheckManager : BuildHandler
    {
        private const string TitleCopy = "Cart Build Check";
        private const string ContinueBuildCopy = "Continue Build";
        private const string ContinueBuildWithoutFixCopy = "Continue Without Fix";
        private const string RunFixCopy = "Run Fix";
        private const string CancelCopy = "Cancel Build";
        private const string CancelAndFixCopy = "Cancel Build & Fix";


        public override int Priority { get; } = 0;


        public override bool OnPreBuild()
        {
            var checks = AssemblyHelper.GetClassesOfType<PreBuildCheckBase>(false).OrderBy(t => t.Priority);

            foreach (var buildCheck in checks)
            {
                var canBuild = true;
                var result = buildCheck.ValidateCanBuild();
                
                if (result.PassedCheck) continue;

                if (result.FixProblemAction != null)
                {
                    // If selected cancel (Run Fix)
                    if (!Dialogue.Display(TitleCopy, result.ErrorMessage, ContinueBuildWithoutFixCopy, RunFixCopy))
                    {
                        canBuild = false;
                        result.FixProblemAction?.Invoke();
                    }
                }
                else if (result.GuideToIssueAction != null)
                {
                    var cancelFinalCopy = result.GuideToIssueAction != null ? CancelAndFixCopy : CancelCopy;
                    
                    // If selected cancel (Run Guide)
                    if (!Dialogue.Display(TitleCopy, result.ErrorMessage, ContinueBuildCopy, cancelFinalCopy))
                    {
                        result.GuideToIssueAction?.Invoke();
                    }
                }
                
                if (canBuild) continue;
                return false;
            }

            return true;
        }
    }
}