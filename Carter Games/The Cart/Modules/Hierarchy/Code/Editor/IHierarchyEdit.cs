using UnityEngine;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    public interface IHierarchyEdit
    {
        int Order { get; }
        void OnHierarchyDraw(int instanceId, Rect rect);
    }
}