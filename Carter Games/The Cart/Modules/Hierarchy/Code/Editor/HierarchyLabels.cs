
using CarterGames.Cart.Core;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    public class HierarchyLabels : IHierarchyEdit
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IHierarchyEdit
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */


        public int Order => 0;
        
        
        public void OnHierarchyDraw(int instanceId, Rect rect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;

            if (gameObject == null) return;
            if (!gameObject.CompareTag("EditorOnly")) return;

            switch (gameObject.name)
            {
                case var x when x.Contains(EditorSettingsHierarchy.HeaderPrefix):
                    DrawHeaderItemLabel(rect, gameObject, gameObject.name.Replace(EditorSettingsHierarchy.HeaderPrefix, string.Empty));
                    break;
                case var x when x.Contains(EditorSettingsHierarchy.SeparatorPrefix):
                    DrawSeparatorItemLabel(rect, gameObject);
                    break;
                default:
                    break;
            }
        }
        
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Menu Items
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        
        /// <summary>
        /// Creates a new empty gameObject setup to be a header with no name.
        /// </summary>
        [MenuItem("GameObject/Hierarchy/Create Header", false, 1)] 
        private static void CreateHeader()
        {
            var gameObject = new GameObject(EditorSettingsHierarchy.HeaderPrefix);
            gameObject.tag = "EditorOnly";
            
            if (Selection.activeTransform != null)
            {
                gameObject.transform.SetParent(Selection.activeTransform);
            }
            
            Selection.activeObject = gameObject;
        }


        /// <summary>
        /// Creates a new empty gameObject setup to be a separator.
        /// </summary>
        [MenuItem("GameObject/Hierarchy/Create Separator", false, 2)] 
        private static void CreateSeparator()
        {
            var gameObject = new GameObject(EditorSettingsHierarchy.SeparatorPrefix);
            gameObject.tag = "EditorOnly";
            
            if (Selection.activeTransform != null)
            {
                gameObject.transform.SetParent(Selection.activeTransform);
            }
            
            Selection.activeObject = gameObject;
        }


        /// <summary>
        /// Creates a new empty gameObject setup to be a header with no name.
        /// </summary>
        [MenuItem("GameObject/Hierarchy/Create Customizable Header", false, 3)] 
        private static void CreateCustomizableHeader()
        {
            var gameObject = new GameObject(EditorSettingsHierarchy.HeaderPrefix);
            gameObject.tag = "EditorOnly";
            gameObject.AddComponent<HierarchyHeaderSettings>();

            if (Selection.activeTransform != null)
            {
                gameObject.transform.SetParent(Selection.activeTransform);
            }
            
            Selection.activeObject = gameObject;
        }


        /// <summary>
        /// Creates a new empty gameObject setup to be a header with no name.
        /// </summary>
        [MenuItem("GameObject/Hierarchy/Create Customizable Separator", false, 4)] 
        private static void CreateCustomizableSeparator()
        {
            var gameObject = new GameObject(EditorSettingsHierarchy.SeparatorPrefix);
            gameObject.tag = "EditorOnly";
            gameObject.AddComponent<HierarchySeparatorSettings>();
            
            if (Selection.activeTransform != null)
            {
                gameObject.transform.SetParent(Selection.activeTransform);
            }
            
            Selection.activeObject = gameObject;
        }
        
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        
        /// <summary>
        /// Draws a header when called.
        /// </summary>
        /// <param name="rect">The rect to draw on.</param>
        /// <param name="gameObject">The gameObject to edit.</param>
        /// <param name="label">The label to display on the gameObject.</param>
        private static void DrawHeaderItemLabel(Rect rect, GameObject gameObject, string label)
        {
            var style = new GUIStyle();
            
            var hasSettings = gameObject.TryGetComponent<HierarchyHeaderSettings>(out var settings);
            style.normal.background = TextureHelper.SolidColorTexture2D(1,1, hasSettings ? settings.BackgroundColor : EditorSettingsHierarchy.HeaderBackgroundColor);

            var textStyle = hasSettings ? settings.BoldLabel ? new GUIStyle(EditorStyles.boldLabel) : new GUIStyle() : new GUIStyle(EditorStyles.boldLabel);

            AlignText(textStyle, hasSettings ? settings.TextAlign : EditorSettingsHierarchy.TextAlign);
                
            textStyle.normal.textColor = hasSettings ? settings.LabelColor : EditorSettingsHierarchy.TextColor;

            // Adjusts the rect to fill the hierarchy blank space between the normal label field space.
            rect = AdjustRect(rect, gameObject, hasSettings ? settings.FullWidth : EditorSettingsHierarchy.FullWidth);

            if (Event.current.type == EventType.Repaint)
            {
                style.Draw(rect, false, false, false, false);
            }
            
            var iconContent = HierarchyEditor.GetObjectContent(gameObject, typeof(GameObject));
            var itemContent = new GUIContent(hasSettings ? settings.Label : label, iconContent.image);
            
            EditorGUI.LabelField(rect, itemContent, textStyle);
        }


        /// <summary>
        /// Draws a separator when called.
        /// </summary>
        /// <param name="rect">The rect to draw on.</param>
        /// <param name="gameObject">The gameObject to edit.</param>
        private static void DrawSeparatorItemLabel(Rect rect, GameObject gameObject)
        {
            var style = new GUIStyle();
            var hasSettings = gameObject.TryGetComponent<HierarchySeparatorSettings>(out var settings);
            
            style.normal.background = TextureHelper.SolidColorTexture2D(1,1,hasSettings 
                ? settings.BackgroundColor 
                : EditorGUIUtility.isProSkin
                    ? new Color32(46, 46, 46, 255)
                    : new Color32(184, 184, 184, 255));

            // Adjusts the rect to fill the hierarchy blank space between the normal label field space.
            rect = AdjustRect(rect, gameObject, hasSettings ? settings.FullWidth : EditorSettingsHierarchy.FullWidth);
            
            if (Event.current.type == EventType.Repaint)
            {
                style.Draw(rect, false, false, false, false);
            }
            
            var iconContent = HierarchyEditor.GetObjectContent(gameObject, typeof(GameObject));
            var itemContent = new GUIContent(string.Empty, iconContent.image);

            EditorGUI.LabelField(rect, itemContent);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Utility Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Adjusts the rect based on the settings & parent depth.
        /// </summary>
        /// <param name="rect">The rect to edit.</param>
        /// <param name="gameObject">The target object.</param>
        private static Rect AdjustRect(Rect rect, GameObject gameObject, bool fullWidth)
        {
            if (gameObject.transform.parent == null)
            {
                rect.x -= 29;
                rect.width += 46;
            }
            else
            {
                if (fullWidth)
                {
                    rect.x -= 29 + (13.5f * gameObject.transform.GetParentCount());
                    rect.width += 46 + (13.5f * gameObject.transform.GetParentCount());
                }
                else
                {
                    rect.x -= 29 - (13 * gameObject.transform.GetParentCount());
                    rect.width += 46 - (13 * gameObject.transform.GetParentCount()); 
                }
            }

            return rect;
        }


        /// <summary>
        /// Aligns the text of a header when called.
        /// </summary>
        /// <param name="style">The style to edit.</param>
        /// <param name="alignment">The alignment to set to.</param>
        private static void AlignText(GUIStyle style, HierarchyTitleTextAlign alignment)
        {
            style.alignment = alignment switch
            {
                HierarchyTitleTextAlign.Center => TextAnchor.UpperCenter,
                HierarchyTitleTextAlign.Left => TextAnchor.UpperLeft,
                HierarchyTitleTextAlign.Right => TextAnchor.UpperRight,
                _ => TextAnchor.UpperCenter
            };
            
            switch (alignment)
            {
                case HierarchyTitleTextAlign.Left:
                    style.padding.left = 5;
                    break;
                case HierarchyTitleTextAlign.Right:
                    style.padding.right = 5;
                    break;
                case HierarchyTitleTextAlign.Center:
                default:
                    break;
            }
        }
    }
}