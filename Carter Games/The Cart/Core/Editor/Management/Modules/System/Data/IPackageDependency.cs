using System.Collections.Generic;

namespace CarterGames.Cart.Modules
{
    public interface IPackageDependency
    {
        List<PackageInfo> Packages { get; }
    }
}