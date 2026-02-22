using System.Collections.Generic;

namespace CarterGames.Cart.Crates
{
    public interface IPackageDependency
    {
        List<GitPackageInfo> Packages { get; }
    }
}