using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Core.MetaData.Editor;

namespace CarterGames.Cart.Modules.GameTicks.Editor
{
    public class MetaDataDefGameTicker : IMetaDefinition
    {
        public string Path => $"{FileEditorUtil.AssetBasePath}/Carter Games/The Cart/Modules/Game Ticks/Data/Meta Data";
    }
}