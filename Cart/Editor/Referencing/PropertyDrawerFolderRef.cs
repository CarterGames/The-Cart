/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using System.IO;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    [CustomPropertyDrawer(typeof(FolderRef))]
    public class PropertyDrawerFolderRef : PropertyDrawer
    {
        private SerializedProperty guid;
        private Object obj;
        
        
        private bool IsInitialized { get; set; }

        
        private void Init(SerializedProperty property) 
        {
            IsInitialized = true;
            guid = property.Fpr("GUID");
            obj = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guid.stringValue));
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!IsInitialized)
            {
                Init(property);
            }

            var guiContent = EditorGUIUtility.ObjectContent(obj, typeof(DefaultAsset));
            var rect = EditorGUI.PrefixLabel(position, label);

            var textFieldRect = rect;
            textFieldRect.width -= 19f;

            var textFieldStyle = new GUIStyle("TextField")
            {
                imagePosition = obj ? ImagePosition.ImageLeft : ImagePosition.TextOnly
            };

            if (GUI.Button(textFieldRect, guiContent, textFieldStyle) && obj)
            {
                EditorGUIUtility.PingObject(obj);
            }

            var path = string.Empty;
            
            if (textFieldRect.Contains(Event.current.mousePosition))
            {
                if (Event.current.type == EventType.DragUpdated)
                {
                    var reference = DragAndDrop.objectReferences[0];
                    path = AssetDatabase.GetAssetPath(reference);

                    DragAndDrop.visualMode = Directory.Exists(path)
                        ? DragAndDropVisualMode.Copy
                        : DragAndDropVisualMode.Rejected;

                    Event.current.Use();
                }
                else if (Event.current.type == EventType.DragPerform)
                {
                    var reference = DragAndDrop.objectReferences[0];
                    path = AssetDatabase.GetAssetPath(reference);

                    if (Directory.Exists(path))
                    {
                        obj = reference;
                        guid.stringValue = AssetDatabase.AssetPathToGUID(path);
                    }

                    Event.current.Use();
                }
            }

            var objectFieldRect = rect;
            objectFieldRect.x = textFieldRect.xMax + 1f;
            objectFieldRect.width = 19f;

            if (!GUI.Button(objectFieldRect, "", GUI.skin.GetStyle("IN ObjectField"))) return;
            
            path = EditorUtility.OpenFolderPanel("Select a folder", "Assets", "");
            
            if (path.Contains(Application.dataPath))
            {
                path = "Assets" + path.Substring(Application.dataPath.Length);
                obj = AssetDatabase.LoadAssetAtPath(path, typeof(DefaultAsset));
                guid.stringValue = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(obj));
            }
            else
            {
                Debug.LogError("The path must be in the Assets folder");
            }
        }
    }
}