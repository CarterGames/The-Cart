using CarterGames.Cart.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Setup.Editor
{
	public class UtilityWindowProjectSetup : UtilityEditorWindow
	{
		private static string[] defaultFolderStructure = new string[]
		{
			"_Assets",
			"_Project",
			"_Scratch",
			"_Project/Art",
			"_Project/Art/2D",
			"_Project/Art/3D",
			"_Project/Art/UI",
			"_Project/Audio",
			"_Project/Code",
			"_Project/Code/Editor",
			"_Project/Code/Runtime",
			"_Project/Data",
			"_Project/Prefabs",
			"_Project/Prefabs/World",
			"_Project/Prefabs/UI",
			"_Project/Scenes",
			"_Project/Scenes/Menu",
			"_Project/Scenes/Game",
		};
		
		
		[MenuItem("Tools/Carter Games/The Cart/Core/Setup Folder Structure", priority = 131)]
		private static void OpenDisplay()
		{
			Open<UtilityWindowProjectSetup>("Project Setup");
		}

		
		private void OnGUI()
		{
			EditorGUILayout.Space(5f);
			
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);
			
			
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
		}


		private void CreatePath(string path)
		{
			
		}
	}
}