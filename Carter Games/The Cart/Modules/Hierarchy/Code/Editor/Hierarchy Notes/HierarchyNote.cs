using UnityEngine;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    public class HierarchyNote : MonoBehaviour
    {
        [SerializeField] private string iconRefName = "sv_icon_dot1_pix16_gizmo";
        [SerializeField, Multiline] private string note;
        
        public string Icon => iconRefName;
        public string Text => note;
    }
}