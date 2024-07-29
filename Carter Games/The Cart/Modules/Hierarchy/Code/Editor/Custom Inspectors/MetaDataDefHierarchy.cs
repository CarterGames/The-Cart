using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Core.MetaData.Editor;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    public class MetaDataDefHierarchy : IMetaDefinition
    {
        public string Path => $"{FileEditorUtil.AssetBasePath}/Carter Games/The Cart/Modules/Hierarchy/Data/Meta Data";
    }
}