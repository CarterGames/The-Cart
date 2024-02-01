using UnityEngine;

namespace CarterGames.Cart.Editor.Hierarchy
{
    public static class HierarchyMeta
    {
        public struct Settings
        {
            public static readonly GUIContent HeaderPrefix =
                new GUIContent(
                    "Header prefix",
                    "The prefix used to define header hierarchy blocks.");


            public static readonly GUIContent SeparatorPrefix =
                new GUIContent(
                    "Separator prefix",
                    "The prefix used to define separator hierarchy blocks.");
        
        
            public static readonly GUIContent TextAlign =
                new GUIContent(
                    "Text alignment",
                    "The method at which the text will align to.");
        
        
            public static readonly GUIContent FullWidth =
                new GUIContent(
                    "Full width?",
                    "Should the bars always be full width?");
        
        
            public static readonly GUIContent HeaderBackgroundColor =
                new GUIContent(
                    "Header BG color",
                    "The default header background color.");
        
        
            public static readonly GUIContent TextColor =
                new GUIContent(
                    "Header text color",
                    "The default header text color.");
        }


        public struct HierarchyInspector
        {
            public static readonly GUIContent BackgroundCol =
                new GUIContent("Header Color:", "The color the header block should be rendered as.");

            public static readonly GUIContent FullWidth = new GUIContent("Full Width?",
                "Defines if the header takes up the full width regardless of child/parenting.");

            public static readonly GUIContent Label = new GUIContent("Label:", "The text shown on the header.");

            public static readonly GUIContent LabelCol =
                new GUIContent("Color:", "The color the header text is displayed in.");

            public static readonly GUIContent BoldStyle =
                new GUIContent("Bold Label?", "Should the label appear in a bold style?");

            public static readonly GUIContent Alignment =
                new GUIContent("Alignment:", "Choose how the header label should be aligned.");
        }
        
        
        public struct SeparatorInspector
        {
            public static readonly GUIContent BackgroundCol = new GUIContent("Separator Color:",
                "The color the separator block should be rendered as.");

            public static readonly GUIContent FullWidth = new GUIContent("Full Width?",
                "Defines if the separator takes up the full width regardless of child/parenting.");
        }
    }
}