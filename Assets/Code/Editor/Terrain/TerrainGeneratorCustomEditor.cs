using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Terrain))]
[CanEditMultipleObjects]
public class TerrainGeneratorCustomEditor : Editor
{
  public override void OnInspectorGUI(){
    serializedObject.Update ();
    base.OnInspectorGUI();
    Terrain terrain = target as Terrain;
    EditorGUILayout.BeginVertical();
    if (GUILayout.Button("generate on scene"))
    {
      terrain.terrainGenerator.GenerateTerrain();
    }
    EditorGUILayout.EndVertical();
    serializedObject.ApplyModifiedProperties ();
  }
  private void OnGUI(){

  }
}