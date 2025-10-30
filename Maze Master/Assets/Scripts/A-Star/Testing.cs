using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] int gridWidth;
    [SerializeField] int gridHeight;
    [SerializeField] float cellSize;
    [SerializeField] Vector3 positionPlacement;

    private Grid grid;

    void Start()
    {
        grid = new Grid(gridWidth, gridHeight, cellSize, positionPlacement);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(GetMouseWorldPosition.getMouseWorldPosition(), 56);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(GetMouseWorldPosition.getMouseWorldPosition()));
        }
    }
}
