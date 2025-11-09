using UnityEngine;

public class Node
{
    public Vector2Int Position;  // Position on the grid
    public bool IsWalkable;      // Can this node be traversed?
    public int GCost;            // Cost from start node to current node
    public int HCost;            // Heuristic cost (estimated cost from current to target)
    public int FCost => GCost + HCost;  // FCost is the total cost (G + H)
    public Node Parent;          // Parent node to reconstruct the path

    public Node(Vector2Int position, bool isWalkable)
    {
        Position = position;
        IsWalkable = isWalkable;
    }
}