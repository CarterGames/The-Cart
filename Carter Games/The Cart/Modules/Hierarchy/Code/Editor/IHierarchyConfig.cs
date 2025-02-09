using UnityEditor;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    public interface IHierarchyConfig
    {
        string OptionLabel { get; }
        void DrawConfig(SerializedProperty serializedProperty);
    }
}