using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CaveGenerator : MonoBehaviour
{

    public GameObject cube;
    public int width;
    public int height;
    public float res;
    public float prob;

    public int numSteps;
    public int numBirth;
    public int starvationNum;
    public int overpopNum;

    public GameObject automata;

    public class Cell {
        public bool alive;
        public Vector3 loc;

        public Cell(bool alive, Vector3 loc) {
            this.alive = alive;
            this.loc = loc;
        }
    }

    private Cell[,] cells;

    public void GenerateAutomata() {

        cells = new Cell[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                cells[i, j] = new Cell(Random.Range(0f, 1f) < prob, new Vector3(i, 0, j));
            }
        }


        InnitObjects();
    }

    public void InnitObjects() {

        automata = new GameObject("Generated Objects");
        automata.transform.parent = transform;
        automata.tag = "Respawn";

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (cells[i, j].alive)
                {
                    GameObject o = Instantiate(cube, cells[i, j].loc, Quaternion.identity);
                    o.transform.parent = automata.transform;
                }
            }
        }
    }

    public void NextGenerationGameOfLife() {

        Cell[,] newGeneration = new Cell[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {

                Cell c = cells[i, j];

                int numNeighbors = CountNeighbors(i, j);

                if (c.alive && numNeighbors < 2) {
                    newGeneration[i, j] = new Cell(false, new Vector3(i, 0, j));
                    continue;
                }

                if (c.alive && numNeighbors == 2 || numNeighbors == 3)
                {
                    newGeneration[i, j] = new Cell(true, new Vector3(i, 0, j));
                    continue;
                }

                if (c.alive && numNeighbors > 3)
                {
                    newGeneration[i, j] = new Cell(false, new Vector3(i, 0, j));
                    continue;
                }

                if (!c.alive && numNeighbors == 3)
                {
                    newGeneration[i, j] = new Cell(true, new Vector3(i, 0, j));
                    continue;
                }

                newGeneration[i, j] = cells[i, j];
            }
        }

        cells = newGeneration;
    }

    public void NextGenerationCaves()
    {

        Cell[,] newGeneration = new Cell[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {

                Cell c = cells[i, j];

                int numNeighbors = CountNeighbors(i, j);

                //if (c.alive && numNeighbors < starvationNum)
                //{
                //    newGeneration[i, j] = new Cell(false, new Vector3(i, 0, j));
                //    continue;
                //}

                ////if (c.alive && numNeighbors == 2 || numNeighbors == 3)
                ////{
                ////    newGeneration[i, j] = new Cell(true, new Vector3(i, 0, j));
                ////    continue;
                ////}

                //if (c.alive && numNeighbors > overpopNum)
                //{
                //    newGeneration[i, j] = new Cell(false, new Vector3(i, 0, j));
                //    continue;
                //}

                //if (!c.alive && numNeighbors == numBirth)
                //{
                //    newGeneration[i, j] = new Cell(true, new Vector3(i, 0, j));
                //    continue;
                //}

                if (c.alive)
                {
                    if (numNeighbors < starvationNum)
                    {
                        newGeneration[i, j] = new Cell(false, new Vector3(i, 0, j));
                    }
                    else {
                        newGeneration[i, j] = new Cell(true, new Vector3(i, 0, j));
                    }
                }
                else {
                    if (numNeighbors > numBirth)
                    {
                        newGeneration[i, j] = new Cell(true, new Vector3(i, 0, j));
                    }
                    else
                    {
                        newGeneration[i, j] = new Cell(false, new Vector3(i, 0, j));
                    }
                }

                //newGeneration[i, j] = cells[i, j];
            }
        }

        cells = newGeneration;
    }


    public int CountNeighbors(int i, int j) {

        int numAliveNeihbors = 0;

        for (int k = -1; k < 2; k++)
        {
            for (int l = -1; l < 2; l++)
            {
                if (k == 0 && l == 0) continue;
                if (i + k >= 0 && i + k < width && j + l >= 0 && j + l < height) {
                    if (cells[i + k, j + l].alive) {
                        numAliveNeihbors++;
                    }
                }
            }
        }
        //Debug.Log(numAliveNeihbors);

        return numAliveNeihbors;
    }
}
