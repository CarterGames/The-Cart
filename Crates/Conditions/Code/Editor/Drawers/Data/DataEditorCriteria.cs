#if CARTERGAMES_CART_CRATE_CONDITIONS && UNITY_EDITOR

using UnityEditor;

namespace CarterGames.Cart.Crates.Conditions.Editor
{
    public class DataEditorCriteria
    {
        public SerializedObject Condition { get; private set; }
        public SerializedObject Criteria { get; private set; }
        public int GroupIndex { get; private set; }
        public bool CanChangeGroup { get; private set; }

        
        public DataEditorCriteria(SerializedObject condition, SerializedObject criteria, int groupIndex, bool canChangeGroup)
        {
            Condition = condition;
            Criteria = criteria;
            GroupIndex = groupIndex;
            CanChangeGroup = canChangeGroup;
        }
    }
}

#endif