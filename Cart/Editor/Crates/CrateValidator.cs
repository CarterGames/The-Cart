using System;

namespace CarterGames.Cart.Crates
{
    public static class CrateValidator
    {
        public static bool IsCrateSetupValid(string crateTechnicalName, out string failReason)
        {
            failReason = string.Empty;
            
            if (!CrateManager.TryGetCrateByTechnicalName(crateTechnicalName, out var crate))
            {
                failReason = $"Crate not found under technical name in the project. {crateTechnicalName}";
                return false;
            }

            if (string.IsNullOrEmpty(crate.CrateName))
            {
                failReason = "Crate name is empty.";
                return false;
            }
            
            if (string.IsNullOrEmpty(crate.CrateDescription))
            {
                failReason = "Crate description is empty.";
                return false;
            }
            
            if (string.IsNullOrEmpty(crate.CrateAuthor))
            {
                failReason = "Crate author is empty.";
                return false;
            }

            return true;
        }
    }
}