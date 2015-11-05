using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ComponentBuilder))]
public class GameEditorController : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();

		ComponentBuilder builder = (ComponentBuilder)target;
		if (GUILayout.Button ("Build"))
		{
			builder.Awake();
		}
		if(GUILayout.Button ("Rebuild"))
		{
			builder.Rebuild();
		}
	}
}
