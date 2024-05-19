using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridController : Singleton<GridController>
{
    public int cols;
    public int rows;
    public Vector3 initialPos;

    public GameObject[] grids;
    public GameObject[,] gridArray;


    private void Start()
    {
        gridArray = new GameObject[cols, rows];
        SpawnGrid();
    }

    void SpawnGrid()
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject grid = Instantiate(grids[(i + j) % 2], new Vector3(i, j, 0) - initialPos, Quaternion.identity);
                gridArray[i, j] = grid;
                grid.GetComponent<Grid>().x = i;
                grid.GetComponent<Grid>().y = j;
                grid.transform.SetParent(this.transform);
            }
        }
    }

    public Grid TestDirection(int x, int y, int direction)
    {
        switch(direction)
        {
            case 4:
                if (x - 1 > -1 && gridArray[x - 1, y] && gridArray[x - 1, y].GetComponent<Grid>().jewelContainedType == gridArray[x ,y].GetComponent<Grid>().jewelContainedType)
                {
                    return gridArray[x - 1, y].GetComponent<Grid>();
                }
                else
                {
                    return null;
                }
            case 3:
                if (x + 1 < cols && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<Grid>().jewelContainedType == gridArray[x, y].GetComponent<Grid>().jewelContainedType)
                {
                    return gridArray[x + 1, y].GetComponent<Grid>();
                }
                else
                {
                    return null;
                }

            case 2:
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<Grid>().jewelContainedType == gridArray[x, y].GetComponent<Grid>().jewelContainedType)
                {
                    return gridArray[x, y - 1].GetComponent<Grid>();
                }
                else
                {
                    return null;
                }

            case 1:
                if (y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<Grid>().jewelContainedType == gridArray[x, y].GetComponent<Grid>().jewelContainedType)
                {
                    return gridArray[x, y + 1].GetComponent<Grid>();
                }
                else
                {
                    return null;
                }
        }
        return null;
    }

}
