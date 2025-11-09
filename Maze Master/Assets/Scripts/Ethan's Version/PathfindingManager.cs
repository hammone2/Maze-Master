using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    public AStar aStar;
    public MazeGrid grid;
    public Vector2Int startPosition;
    public Vector2Int targetPosition;

    void Start()
    {
        if (grid == null || aStar == null)
        {
            Debug.LogError("Grid or AStar reference is missing.");
            return;
        }

        // Log the start and target positions
        Debug.Log($"Start Position: {startPosition}, Target Position: {targetPosition}");

        // Ensure the start and target positions are within bounds
        if (!IsValidPosition(startPosition) || !IsValidPosition(targetPosition))
        {
            Debug.LogError("Start or target position is out of bounds!");
            return;
        }

        grid.CreateGrid();
        aStar.FindPath(startPosition, targetPosition);
    }

    // Check if the position is within the grid bounds
    bool IsValidPosition(Vector2Int position)
    {
        return position.x >= 0 && position.x < grid.width && position.y >= 0 && position.y < grid.height;
    }
}
