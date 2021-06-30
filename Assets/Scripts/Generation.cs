using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Generation : MonoBehaviour
{

    public GameObject cube;
    public int width = 5;
    public int height = 5;
    public bool randomHeight = false;
    public bool sinHeight = false;
    public bool cosHeight = false;
    public bool tanHeight = false;


    public void Generate()
    {
        GameObject parent = new GameObject("Generated Objects");
        parent.tag = "Respawn";

        for (int i = -width / 2; i < width / 2; i++) {
            for (int j = -height / 2; j < height / 2; j++)
            {
                float blockHeight = 1f;
                
                blockHeight += randomHeight ? Random.Range(0.5f, 5) : 0;
                blockHeight += sinHeight ? Mathf.Sin(i) + 1f : 0;
                blockHeight += cosHeight ? Mathf.Cos(j) + 1f : 0;
                blockHeight += tanHeight ? Mathf.Tan(i + j) : 0;
                
                for (float k = 0.5f; k < blockHeight; k += 0.5f)
                {
                    GameObject o = Instantiate(cube, new Vector3(i, k, j), Quaternion.identity);
                    o.transform.parent = parent.transform;
                }
            }
        }

    }
}
