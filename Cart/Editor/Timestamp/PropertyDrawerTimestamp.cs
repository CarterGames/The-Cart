using System;
using CarterGames.Cart.Runtime;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    [CustomPropertyDrawer(typeof(Timestamp))]
    public class PropertyDrawerTimestamp : PropertyDrawer
    {
        private static GUIStyle labelStyle;
        
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property.Fpr("value"), label);

            DateTime dt;
            
            if (property.Fpr("dateTime").Fpr("year").intValue >= 0)
            {
                try
                {
                    dt = DateTimeHelper.ParseUnixEpochUtc(property.Fpr("value").longValue);
                }
#pragma warning disable 0168
                catch (Exception e)
#pragma warning restore
                {
                    dt = DateTimeHelper.UnixEpoch;
                }
           
            }
            else
            {
                dt = new DateTime(
                    property.Fpr("dateTime").Fpr("year").intValue,
                    property.Fpr("dateTime").Fpr("month").intValue,
                    property.Fpr("dateTime").Fpr("day").intValue,
                    property.Fpr("dateTime").Fpr("hour").intValue,
                    property.Fpr("dateTime").Fpr("minute").intValue,
                    property.Fpr("dateTime").Fpr("second").intValue,
                    property.Fpr("dateTime").Fpr("millisecond").intValue);
            }
            
            DrawTypeLabel(position, dt, property.Fpr("value").longValue);
            
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.UpdateIfRequiredOrScript();
            
            EditorGUI.EndProperty();
        }


        private static void DrawTypeLabel(Rect position, DateTime time, long value)
        {
            InitializeStyleIfNeeded();
            
            int controlID;
            string displayString;
            
            if (value < 0)
            {
                controlID = GUIUtility.GetControlID(FocusType.Passive) - 1;
                displayString = $"(Invalid)";
            }
            else
            {
                controlID = GUIUtility.GetControlID(FocusType.Passive) - 1;
                displayString = $"({time:yyyy/MM/dd hh:mm:ss})";
            }
            
            DrawInterfaceNameLabel(position, displayString, controlID);
        }


        private static void DrawInterfaceNameLabel(Rect position, string displayString, int controlID)
        {
            if (Event.current.type != EventType.Repaint) return;
            
            const int additionalLeftWidth = 3;
            const int verticalIndent = 1;

            var content = EditorGUIUtility.TrTextContent(displayString);
            var size = labelStyle.CalcSize(content);
            var labelPos = position;

            labelPos.width = size.x + additionalLeftWidth;
            labelPos.x += position.width - labelPos.width - 5;
            labelPos.height -= verticalIndent * 2;
            labelPos.y += verticalIndent;
            labelStyle.Draw(labelPos, EditorGUIUtility.TrTextContent(displayString), controlID,
                DragAndDrop.activeControlID == controlID, false);
        }


        private static void InitializeStyleIfNeeded()
        {
            if (labelStyle != null) return;

            var style = new GUIStyle(EditorStyles.label)
            {
                font = EditorStyles.objectField.font,
                fontSize = EditorStyles.objectField.fontSize,
                fontStyle = EditorStyles.objectField.fontStyle,
                alignment = TextAnchor.MiddleRight,
                normal = new GUIStyleState()
                {
                    textColor = Color.grey
                }
            };
            
            labelStyle = style;
        }
    }
}