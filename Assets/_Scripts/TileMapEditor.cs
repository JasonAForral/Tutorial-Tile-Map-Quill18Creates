using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(TileMap))]
public class TileMapEditor : Editor {

   // public SerializedProperty tileCountX;
    //private float v = 0.5f;

    void OnEnable ()
    {
        //tileCountX = serializedObject.FindProperty("tileCountX");
    }

    public override void OnInspectorGUI ()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        //EditorGUILayout.IntSlider(tileCountX, 1, 100);

        //EditorGUILayout.BeginVertical();
        //v = EditorGUILayout.Slider(v, 0f, 1.0f);
        //EditorGUILayout.EndVertical();

        if (GUILayout.Button("Regenerate"))
        {
            TileMap tileMap = (TileMap)target;
            tileMap.BuildMesh();
        }
    }
}
