#if CARTERGAMES_CART_CRATE_MULTISCENE && UNITY_EDITOR

using CarterGames.Cart.Management.Editor;

namespace CarterGames.Cart.Crates.MultiScene.Editor
{
    public sealed class EditorArtMultiSceneIcon : IEditorArtDefine
    {
        public string Uid => "crate_multiscene_icon";
        public string InternalPath => "Crates/Multi Scene/Art/File Icons/T_MultiScene_Transparent_Logo.png";
    }
}

#endif