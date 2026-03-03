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

namespace CarterGames.Cart.Editor
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