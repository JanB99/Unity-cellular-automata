using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGeneration : MonoBehaviour
{

    public int width; 
    public int length;
    public float height;
    public float noiseFreq;


    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    private void Start()
    {
        
    }

    public void Generate()
    {   
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[(width + 1) * (length + 1)];
        

        int index = 0;
        for (int i = 0; i < width + 1; i++) {
            for (int j = 0; j < length + 1; j++)
            {

                float y = NoiseStep(i, j, noiseFreq, 2);
                y += NoiseStep(i, j, noiseFreq / 2, 2);
                y += NoiseStep(i, j, noiseFreq / 4, 2);

                //float y = Mathf.PerlinNoise(nx/100, ny) * height;
                vertices[index] = new Vector3(i, y, j);
                index++;
            }
        }

        triangles = new int[width * length * 6];
        int tris = 0;
        int vert = 0;
        for (int j = 0; j < length; j++) {
            
            for (int i = 0; i < width; i++)
            {
                triangles[tris] = vert;
                triangles[tris + 1] = vert + 1;
                triangles[tris + 2] = vert + width + 1; 
                triangles[tris + 3] = vert + width + 1; 
                triangles[tris + 4] = vert + 1; 
                triangles[tris + 5] = vert + width + 2;
                tris += 6;
                vert++;
            }
            vert++;
        }
        

        //for (int i = 0; i < triangles.Length; i++)
        //{
        //    Debug.Log(i + " - " + triangles[i]);
        //}

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    float NoiseStep(int x, int y, float freq, float exp) {
        float nx = Time.time * x / freq;
        float ny = Time.time * y / freq;
        float noise = Mathf.PerlinNoise(nx, ny) * height;
        noise += 0.5f * Mathf.PerlinNoise(nx / 2, ny / 2) * height;
        noise += 0.25f * Mathf.PerlinNoise(nx / 4, ny / 4) * height;
        noise += 0.125f * Mathf.PerlinNoise(nx / 8, ny / 8) * height;
        noise = Mathf.Pow(noise, exp);
        return noise;
    }

    public void OnDrawGizmos()
    {
        if (vertices == null) return;

        //for (int i = 0; i < vertices.Length; i++) {
        //    Gizmos.DrawSphere(vertices[i], .1f);
        //}

    }

}
