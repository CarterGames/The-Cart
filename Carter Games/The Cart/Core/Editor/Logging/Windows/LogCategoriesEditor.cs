using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Logs.Editor.Windows
{
    public class LogCategoriesEditor : UtilityEditorWindow
    {
        private List<KeyValuePair<string, bool>> data = new List<KeyValuePair<string, bool>>();


        [MenuItem("Tools/Carter Games/The Cart/[Logging] Category Window")]
        private static void OpenEditor()
        {
            Open<LogCategoriesEditor>("Log Category Statuses Window");
        }
        
        
        private void OnEnable()
        {
            LogCategoryHandler.UpdateCache();
            RefreshData();
        }


        private void RefreshData()
        {
            data = LogCategoryStates.CategoryStates.OrderBy(t => t.Key).ToList();
        }
        

        private void OnGUI()
        {
            DrawUserDefinedCategories();
            DrawCartDefinedCategories();
        }


        private void DrawUserDefinedCategories()
        {
            EditorGUILayout.BeginVertical();
            EditorGUI.BeginChangeCheck();

            var toShow = data.Where(t => !t.Key.Contains("CarterGames.Cart")).ToList();
            
            if (!toShow.IsEmptyOrNull())
            {
                for (var i = 0; i < toShow.Count; i++)
                {
                    var entry = toShow[i];

                    var style = new GUIStyle("Box")
                    {
                        normal =
                        {
                            background = TextureHelper.SolidColorTexture2D(1,1, i % 2 == 1
                                ? new Color32(50, 50, 50, 0) 
                                : new Color32(56, 56, 56, 255))
                        }
                    };

                    EditorGUILayout.BeginHorizontal(style);
                    
                    EditorGUILayout.LabelField(entry.Key.SplitAndGetLastElement('.'));
                        
                    toShow[i] = new KeyValuePair<string, bool>(entry.Key, EditorGUILayout.Toggle(toShow[i].Value, GUILayout.Width(15)));
                    
                    EditorGUILayout.EndHorizontal();
                }
                
                if (EditorGUI.EndChangeCheck())
                {
                    for (var i = 0; i < data.Count; i++)
                    {
                        foreach (var shownEntry in toShow)
                        {
                            if (shownEntry.Key != data[i].Key) continue;
                            data[i] = shownEntry;
                        }
                    }

                    LogCategoryStates.CategoryStates = SerializableDictionary<string, bool>.FromKeyPairValueList(data);
                    RefreshData();
                }
            }
            
            EditorGUILayout.EndVertical();
        }
        
        
        private void DrawCartDefinedCategories()
        {
            EditorGUILayout.BeginVertical("Box");
            GUILayout.Space(1f);
            
            EditorGUILayout.LabelField("Cart Log Categories", EditorStyles.boldLabel);
            
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.BeginChangeCheck();

            var toShow = data.Where(t => t.Key.Contains("CarterGames.Cart")).ToList();
            
            if (!toShow.IsEmptyOrNull())
            {
                for (var i = 0; i < toShow.Count; i++)
                {
                    var entry = toShow[i];

                    var style = new GUIStyle("Box")
                    {
                        normal =
                        {
                            background = TextureHelper.SolidColorTexture2D(1,1, i % 2 == 1
                                ? new Color32(50, 50, 50, 0) 
                                : new Color32(50, 50, 50, 255))
                        }
                    };

                    EditorGUILayout.BeginHorizontal(style);
                    
                    EditorGUILayout.LabelField(entry.Key.SplitAndGetLastElement('.'));
                        
                    toShow[i] = new KeyValuePair<string, bool>(entry.Key, EditorGUILayout.Toggle(toShow[i].Value, GUILayout.Width(15)));
                    
                    EditorGUILayout.EndHorizontal();
                }
                
                if (EditorGUI.EndChangeCheck())
                {
                    for (var i = 0; i < data.Count; i++)
                    {
                        foreach (var shownEntry in toShow)
                        {
                            if (shownEntry.Key != data[i].Key) continue;
                            data[i] = shownEntry;
                        }
                    }

                    LogCategoryStates.CategoryStates = SerializableDictionary<string, bool>.FromKeyPairValueList(data);
                    RefreshData();
                }
            }
            
            GUILayout.Space(1f);
            EditorGUILayout.EndVertical();
        }
    }
}