#if CARTERGAMES_CART_CRATE_CONDITIONS && UNITY_EDITOR

using UnityEditor;

namespace CarterGames.Cart.Crates.Conditions.Editor
{
    public class DataEditorCriteriaGroup
    {
        public SerializedObject Condition { get; private set; }
        public SerializedProperty Group { get; private set; }
        public int GroupIndex { get; private set; }
        public bool IsOnlyGroup { get; private set; }

        
        public DataEditorCriteriaGroup(SerializedObject condition, SerializedProperty group, int groupIndex, bool soloGroup)
        {
            Condition = condition;
            Group = group;
            GroupIndex = groupIndex;
            IsOnlyGroup = soloGroup;
        }
    }
}

#endif