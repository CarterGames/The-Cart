using CarterGames.Cart.Management.Editor;

namespace CarterGames.Cart.Crates
{
    public interface ICrateSettingsProvider : ISettingsProvider
    {
        string MenuName { get; }
    }
}