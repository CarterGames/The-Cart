using System;

namespace CarterGames.Cart.Editor
{
    public class PreBuildCheckResult
    {
        public bool PassedCheck { get; private set; }
        public string ErrorMessage { get; private set; }
        
        public Action FixProblemAction { get; private set; }
        public Action GuideToIssueAction { get; private set; }


        public static PreBuildCheckResult Succeed()
        {
            return new PreBuildCheckResult()
            {
                PassedCheck = true,
            };
        }
        
        
        public static PreBuildCheckResult Failed(string errorMessage)
        {
            return new PreBuildCheckResult()
            {
                PassedCheck = false,
                ErrorMessage = errorMessage
            };
        }
        
        
        public static PreBuildCheckResult FailedWithFix(string errorMessage, Action fixAction)
        {
            return new PreBuildCheckResult()
            {
                PassedCheck = false,
                ErrorMessage = errorMessage,
                FixProblemAction = fixAction
            };
        }
        
        
        public static PreBuildCheckResult FailedWithGuide(string errorMessage, Action guideAction)
        {
            return new PreBuildCheckResult()
            {
                PassedCheck = false,
                ErrorMessage = errorMessage,
                GuideToIssueAction = guideAction
            };
        }
    }
}