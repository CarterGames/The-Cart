using UnityEditor;

namespace Scarlet.Editor
{
    public static class SerializedPropertyHelper
    {
        public static bool HasProp(this SerializedProperty serializedProperty, string propName)
        {
            return serializedProperty.FindPropertyRelative(propName) != null;
        }
    }
}