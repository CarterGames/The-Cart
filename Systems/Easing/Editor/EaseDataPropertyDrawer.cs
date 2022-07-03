using UnityEditor;
using UnityEngine;

namespace Scarlet.Easing.Editor
{
    [CustomPropertyDrawer(typeof(EaseData))]
    public class EaseDataPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            BaseEaseDataPropertyDrawer.OnGUILogic(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2 + 3f;
        }
    }


    [CustomPropertyDrawer(typeof(InEaseData))]
    public class InEaseDataPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            BaseEaseDataPropertyDrawer.OnGUILogic(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2 + 3f;
        }
    }
    
    
    [CustomPropertyDrawer(typeof(OutEaseData))]
    public class OutEaseDataPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            BaseEaseDataPropertyDrawer.OnGUILogic(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2 + 3f;
        }
    }
}