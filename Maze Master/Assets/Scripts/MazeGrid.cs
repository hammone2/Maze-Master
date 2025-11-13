using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MazeGrid : MonoBehaviour
{
    public PathfindingManager pathfindingManager;
    public AStar aStar;

    public int width = 10;
    public int height = 10;
    public float nodeRadius = 0.5f;
    public LayerMask obstacleLayer;
    public LayerMask startLayer;
    public LayerMask endLayer;

    private Node[,] grid;

    public void CreateGrid()
    {
        Vector2Int startPos = new Vector2Int();
        Vector2Int endPos= new Vector2Int();
        grid = new Node[width, height];
        Debug.Log("Creating grid...");

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 worldPos = new Vector2(x * nodeRadius * 2 + nodeRadius, y * nodeRadius * 2 + nodeRadius);
                bool isWalkable = !Physics2D.OverlapCircle(worldPos, nodeRadius, obstacleLayer);
                grid[x, y] = new Node(new Vector2Int(x, y), isWalkable);

                //check for start and end positions, then add them to the pathfinding manager
                if (Physics2D.OverlapCircle(worldPos, nodeRadius, startLayer))
                {
                    //pathfindingManager.startPosition = new Vector2Int(x, y);
                    startPos = new Vector2Int(x, y);
                }

                if (Physics2D.OverlapCircle(worldPos, nodeRadius, endLayer))
                {
                    //pathfindingManager.targetPosition = new Vector2Int(x, y);
                    endPos = new Vector2Int(x, y);
                }

                Debug.Log($"Node ({x}, {y}) - Walkable: {isWalkable}");
            }
        }

        aStar.SetPoints(startPos, endPos);
    }

    public Node GetNode(Vector2Int position)
    {
        if (position.x >= 0 && position.x < width && position.y >= 0 && position.y < height)
            return grid[position.x, position.y];
        return null;
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 0), //right
            new Vector2Int(-1, 0), //left
            new Vector2Int(0, 1), //up
            new Vector2Int(0, -1) //down
        };

        foreach (var direction in directions)
        {
            Vector2Int neighborPos = node.Position + direction;
            Node neighbor = GetNode(neighborPos);
            if (neighbor != null && neighbor.IsWalkable)
                neighbors.Add(neighbor);
        }

        return neighbors;
    }

    private void OnDrawGizmos()
    {
        if (grid == null) return;

        Gizmos.color = Color.green;
        for (int x = 0; x <= width; x++)
        {
            Gizmos.DrawLine(new Vector2(x * nodeRadius * 2, 0), new Vector2(x * nodeRadius * 2, height * nodeRadius * 2));
        }
        for (int y = 0; y <= height; y++)
        {
            Gizmos.DrawLine(new Vector2(0, y * nodeRadius * 2), new Vector2(width * nodeRadius * 2, y * nodeRadius * 2));
        }

        //draw nodes
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node node = grid[x, y];
                Vector2 worldPos = new Vector2(x * nodeRadius * 2 + nodeRadius, y * nodeRadius * 2 + nodeRadius);

                Color nodeColor = node.IsWalkable ? Color.green : Color.red;

                Gizmos.color = nodeColor;
                Gizmos.DrawSphere(worldPos, nodeRadius);
            }
        }
    }
}
