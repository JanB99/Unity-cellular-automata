using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(CaveGenerator))]
public class CavegeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CaveGenerator generator = (CaveGenerator)target;

        generator.width = EditorGUILayout.IntSlider("Width", generator.width, 1, 100);
        generator.height = EditorGUILayout.IntSlider("Height", generator.height, 1, 100);
        generator.prob = EditorGUILayout.Slider("Probability", generator.prob, 0, 1);

        generator.numSteps = EditorGUILayout.IntField("Number of steps", generator.numSteps);
        generator.numBirth = EditorGUILayout.IntSlider("Birthrate", generator.numBirth, 0, 8);
        generator.starvationNum = EditorGUILayout.IntSlider("Starvationrate", generator.starvationNum, 0, 8);
        generator.overpopNum = EditorGUILayout.IntSlider("Overpopulationrate", generator.overpopNum, 0, 8);

        generator.cube = (GameObject)EditorGUILayout.ObjectField("Object", generator.cube, typeof(GameObject), false);

        if (GUILayout.Button("Generate"))
        {
            DestroyImmediate(GameObject.FindGameObjectWithTag("Respawn"));
            generator.GenerateAutomata();
        }

        if (GUILayout.Button("Next generation"))
        {
            DestroyImmediate(GameObject.FindGameObjectWithTag("Respawn"));
            for (int i = 0; i < generator.numSteps; i++)
            {
                generator.NextGenerationCaves();
            }
            
            generator.InnitObjects();
        }
    }
}
