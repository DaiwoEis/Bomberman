using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Map))]
public class MapGenerator : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (GUILayout.Button("Generate map"))
        {
            Map map = target as Map;
            map.GenerateMap();
        }
    }
}
