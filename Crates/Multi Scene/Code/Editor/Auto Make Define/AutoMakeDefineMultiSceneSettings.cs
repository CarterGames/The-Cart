#if CARTERGAMES_CART_CRATE_MULTISCENE && UNITY_EDITOR

using CarterGames.Cart.Editor;

namespace CarterGames.Cart.Crates.MultiScene.Editor
{
    public sealed class AutoMakeDefineMultiSceneSettings : AutoMakeDataAssetDefineBase<MultiSceneSettings>
    {
        public override string DataAssetFileName => "[Cart] [MultiScene] Settings Data Asset.asset";
        
        public override string DataAssetPath => $"Crates/{DataAssetFileName}";
    }
}

#endif