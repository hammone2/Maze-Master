using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    public AStar aStar;
    public MazeGrid grid;
    public bool isRandom = false;

    [SerializeField] private GameObject startTileObj;
    [SerializeField] private GameObject endTileObj;
    [SerializeField] private GameObject wallTileObj;
    [SerializeField] private Transform map;

    void Start()
    {
        if (grid == null || aStar == null)
        {
            Debug.LogError("Grid or AStar reference is missing.");
            return;
        }

        if (isRandom)
        {
            GenerateMaze();
            Invoke("InitializeGrid", 0.2f);
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

    private void GenerateMaze()
    {
        int width = grid.width;
        int height = grid.height;
        float nodeRadius = grid.nodeRadius;


        //place end points on the edges
        Vector2Int startPos = GetRandomEdgePosition(grid.width);
        Vector2 startPosition = new Vector2(startPos.x * nodeRadius * 2, startPos.y * nodeRadius * 2);
        GameObject startTile = Instantiate(startTileObj, startPosition, Quaternion.identity);
        startTile.transform.SetParent(map);

        Vector2Int endPos = GetRandomEdgePosition(grid.width);
        Vector2 endPosition = new Vector2(endPos.x * nodeRadius * 2, endPos.y * nodeRadius * 2);
        GameObject endTile = Instantiate(endTileObj, endPosition, Quaternion.identity);
        endTile.transform.SetParent(map);


        //place walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int gridPos = new Vector2Int(x, y);
                if (gridPos == startPos || gridPos == endPos)
                    continue;


                bool isPlacing = Random.value > 0.75f;
                if (!isPlacing)
                    continue;


                Vector2 worldPos = new Vector2(x * nodeRadius * 2, y * nodeRadius * 2);
                GameObject wallTile = Instantiate(wallTileObj, worldPos, Quaternion.identity);
                wallTile.transform.SetParent(map);
            }
        }
    }

    Vector2Int GetRandomEdgePosition(int gridSize)
    {
        int max = gridSize - 1;

        // Decide if the object goes on horizontal or vertical edge
        bool horizontalEdge = Random.value > 0.5f;

        int x, y;

        if (horizontalEdge)
        {
            // Top or bottom row
            y = Random.value > 0.5f ? 0 : max;
            x = Random.Range(0, gridSize);
        }
        else
        {
            // Left or right column
            x = Random.value > 0.5f ? 0 : max;
            y = Random.Range(0, gridSize);
        }

        return new Vector2Int(x, y);
    }
}
