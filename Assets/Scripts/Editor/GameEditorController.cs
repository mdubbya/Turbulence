//using UnityEngine;
//using System.Collections;
//using UnityEditor;
//
//[CustomEditor(typeof(GameController))]
//public class GameEditorController : Editor
//{
//	public override void OnInspectorGUI()
//	{
//		DrawDefaultInspector ();
//
//		GameController controller = (GameController)target;
//		if (GUILayout.Button ("Build All"))
//		{
//			controller.Start();
//		}
//		if (GUILayout.Button ("Clear All"))
//		{
//			controller.ClearAllObjects();
//		}
//	}
//}
