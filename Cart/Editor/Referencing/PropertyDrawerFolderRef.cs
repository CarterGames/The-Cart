/*
 * Copyright (c) 2025 Carter Games
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
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