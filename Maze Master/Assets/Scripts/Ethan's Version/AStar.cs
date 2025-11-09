using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public MazeGrid grid;
    public Vector2Int startPos;
    public Vector2Int targetPos;

    private List<Node> openList = new List<Node>();
    private List<Node> closedList = new List<Node>();

    public void FindPath(Vector2Int start, Vector2Int target)
    {
        openList.Clear();
        closedList.Clear();

        Node startNode = grid.GetNode(start);
        Node targetNode = grid.GetNode(target);

        if (startNode == null)
        {
            Debug.LogError("Start node is invalid!");
            return;
        }

        if (targetNode == null)
        {
            Debug.LogError("Target node is invalid!");
            return;
        }

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = GetNodeWithLowestFCost(openList);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Node neighbor in grid.GetNeighbors(currentNode))
            {
                if (closedList.Contains(neighbor)) continue;

                int tentativeGCost = currentNode.GCost + GetDistance(currentNode, neighbor);

                if (tentativeGCost < neighbor.GCost || !openList.Contains(neighbor))
                {
                    neighbor.GCost = tentativeGCost;
                    neighbor.HCost = GetDistance(neighbor, targetNode);
                    neighbor.Parent = currentNode;

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }
    }

    private void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();

        // Do something with the path (e.g., move an object along the path)
        // For example, you can visualize the path or move an object
        VisualizePath(path);
    }

    private void VisualizePath(List<Node> path)
    {
        // For visualization, we can draw a line or instantiate something at the path positions
        foreach (Node node in path)
        {
            Debug.Log(node.Position);
        }

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = path.Count;

        for (int i = 0; i < path.Count; i++)
        {
            Vector3 worldPos = new Vector3(path[i].Position.x * grid.nodeRadius * 2 + grid.nodeRadius, path[i].Position.y * grid.nodeRadius * 2 + grid.nodeRadius, -2f);
            lineRenderer.SetPosition(i, worldPos);
        }
    }

    private Node GetNodeWithLowestFCost(List<Node> nodes)
    {
        Node lowestFCostNode = nodes[0];
        foreach (Node node in nodes)
        {
            if (node.FCost < lowestFCostNode.FCost || (node.FCost == lowestFCostNode.FCost && node.HCost < lowestFCostNode.HCost))
            {
                lowestFCostNode = node;
            }
        }
        return lowestFCostNode;
    }

    private int GetDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.Position.x - b.Position.x);
        int distY = Mathf.Abs(a.Position.y - b.Position.y);
        return distX + distY; // Manhattan Distance for 2D grid
    }
}
