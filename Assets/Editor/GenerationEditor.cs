using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Generation))]
public class GenerationEditor : Editor
{

    public override void OnInspectorGUI()
    {
        Generation generation = (Generation)target;

        generation.cube = (GameObject)EditorGUILayout.ObjectField("Object", generation.cube, typeof(GameObject), false);
        generation.width = EditorGUILayout.IntSlider("Width", generation.width, 1, 100);
        generation.height = EditorGUILayout.IntSlider("Height", generation.height, 1, 100);
        generation.sinHeight = EditorGUILayout.Toggle("Sin wave height", generation.sinHeight);
        generation.cosHeight = EditorGUILayout.Toggle("Cos wave height", generation.cosHeight);
        generation.tanHeight = EditorGUILayout.Toggle("Tan wave height", generation.tanHeight);
        generation.randomHeight = EditorGUILayout.Toggle("Random height", generation.randomHeight);

        if (GUILayout.Button("Generate"))
        {
            DestroyImmediate(GameObject.FindGameObjectWithTag("Respawn"));
            generation.Generate();
        }
    }

    }
