using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] int gridWidth;
    [SerializeField] int gridHeight;
    [SerializeField] float cellSize;

    private Grid grid;

    void Start()
    {
        grid = new Grid(gridWidth, gridHeight, cellSize);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            grid.SetValue(GetMouseWorldPosition.getMouseWorldPosition(), 56);
        }
    }
}
