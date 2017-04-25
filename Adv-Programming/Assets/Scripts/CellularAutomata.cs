﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellularAutomata : MonoBehaviour
{
    [Range (1,100)]
    public int probability;
    public int gridWidth;
    public int gridHeight;
    public int[,] grid;

    public GameObject wall;
	// Use this for initialization
	void Start ()
    {
        grid = new int[gridWidth, gridHeight];

        GenerateGrid();

        for (int i = 0; i < 4; i++)
        {
            SmoothGrid();
        }

        //SpawnObjects();
        StartMarchingSquares();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    void GenerateGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                int randNumber = Random.Range(0, 100);
                if (randNumber <= probability && x != 0 && x != grid.GetLength(0) - 1 && y != 0 && y != grid.GetLength(1) - 1)
                {
                    grid[x, y] = 0;
                }
                else
                {
                    grid[x, y] = 1;
                }
            }
        }
    }

    void SmoothGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (x != 0 && x != grid.GetLength(0) -1 && y != 0 && y != grid.GetLength(1) -1)
                {
                    if (GetAmountOfNeighbours(x, y) > 4)
                    {
                        grid[x, y] = 1;
                    }
                    else if (GetAmountOfNeighbours(x, y) < 4)
                    {
                        grid[x, y] = 0;
                    } 
                }
            }
        }
    }

    int GetAmountOfNeighbours(int gridX, int gridY)
    {
        int neighbours = 0;

        for (int x = gridX - 1; x <= gridX + 1; x++)
        {
            for (int y = gridY - 1; y <= gridY + 1; y++)
            {
                if (grid[x, y] == 1)
                {
                    neighbours++;
                }
            }
        }

        return neighbours;
    }

    void StartMarchingSquares()
    {
        MarchingSquares marchingSquares = GetComponent<MarchingSquares>();
        marchingSquares.GenerateMesh(grid, 1);
    }

    void SpawnObjects()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y] == 0)
                {
                    Vector3 spawnPos = new Vector3(x, 0, y);
                    Instantiate(wall, spawnPos, Quaternion.identity);
                }
            }
        }
    }
}
