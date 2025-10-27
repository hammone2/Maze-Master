using UnityEngine;
using System.Collections.Generic;

public class PathfindAlgorithm : MonoBehaviour
{
    public List<Cell> cells = new List<Cell>();
    public Cell startPoint;
    public Cell endPoint;

    //private bool mazeSolved = false;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Cell cell = transform.GetChild(i).GetComponent<Cell>();
            cells.Add(cell);

            if (cell.gameObject.name == "MazeEnd")
                endPoint = cell;

            if (cell.gameObject.name == "MazeStart")
                startPoint = cell;
        }
    }
}
