using System.Collections.Generic;

namespace CarterGames.Cart.Crate
{
    public interface IGitPackageDependency
    {
        List<GitPackageInfo> Packages { get; }
    }
}