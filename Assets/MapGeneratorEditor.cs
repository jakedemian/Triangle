using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor {

	public override void OnInspectorGUI() {
		MapGenerator mapGenerator = (MapGenerator)target;

		if (DrawDefaultInspector ()) {
			if (mapGenerator.autoUpdate) {
				mapGenerator.GenerateNewMap ();
			}
		}

		if (GUILayout.Button ("Generate")) {
			mapGenerator.GenerateNewMap ();
		}
	}
}