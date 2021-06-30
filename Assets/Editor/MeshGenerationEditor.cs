using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshGeneration))]
public class MeshGenerationEditor : Editor
{

    public override void OnInspectorGUI()
    {
        MeshGeneration generation = (MeshGeneration)target;

        generation.width = EditorGUILayout.IntSlider("Width", generation.width, 1, 100);
        generation.length = EditorGUILayout.IntSlider("Length", generation.length, 1, 100);
        generation.height = EditorGUILayout.Slider("Height", generation.height, 0.01f, 5f);
        generation.noiseFreq = EditorGUILayout.Slider("noiseFreq", generation.noiseFreq, 1, 500);

        if (GUILayout.Button("Generate"))
        {
            generation.Generate();
        }
    }

}
