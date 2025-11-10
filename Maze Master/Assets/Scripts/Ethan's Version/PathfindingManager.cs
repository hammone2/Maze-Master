using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    public AStar aStar;
    public MazeGrid grid;

    void Start()
    {
        if (grid == null || aStar == null)
        {
            Debug.LogError("Grid or AStar reference is missing.");
            return;
        }

        InitializeGrid();
    }

    // Check if the position is within the grid bounds
    bool IsValidPosition(Vector2Int position)
    {
        return position.x >= 0 && position.x < grid.width && position.y >= 0 && position.y < grid.height;
    }

    public void InitializeGrid()
    {
        grid.CreateGrid();
    }
}
