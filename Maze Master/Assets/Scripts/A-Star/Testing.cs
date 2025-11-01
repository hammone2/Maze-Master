using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] int gridWidth;
    [SerializeField] int gridHeight;
    [SerializeField] float cellSize;
    [SerializeField] Vector3 positionPlacement;

    private Grid<StringGridObject> grid;

    void Start()
    {
        grid = new Grid<StringGridObject>(gridWidth, gridHeight, cellSize, positionPlacement, (Grid<StringGridObject> g, int x, int y) => new StringGridObject(g, x, y));
    }

    void Update()
    {
        Vector3 position = GetMouseWorldPosition.getMouseWorldPosition();
        // if (Input.GetMouseButtonDown(0))
        // {
        //     StringGridObject mapGridObject = grid.GetGridOject(position);
        //     if (mapGridObject != null)
        //     {
        //         mapGridObject.Addvalue(5); // IntMapFGridObject

        //     }
        //     //grid.SetGridOject(GetMouseWorldPosition.getMouseWorldPosition(), true);
        // }

        if (Input.GetKeyDown(KeyCode.A)) { grid.GetGridOject(position).AddLetter("A"); }
        if (Input.GetKeyDown(KeyCode.B)) { grid.GetGridOject(position).AddLetter("B"); }
        if (Input.GetKeyDown(KeyCode.C)) { grid.GetGridOject(position).AddLetter("C"); }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { grid.GetGridOject(position).AddNumber("1"); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { grid.GetGridOject(position).AddNumber("2"); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { grid.GetGridOject(position).AddNumber("3"); }

        if (Input.GetMouseButtonDown(1)) { Debug.Log(grid.GetGridOject(GetMouseWorldPosition.getMouseWorldPosition())); }
    }
}

internal class IntMapGridObject
{
    private const int MIN = 0;
    private const int MAX = 100;
    private Grid<IntMapGridObject> grid;
    private int x;
    private int y;
    private int value;

    public IntMapGridObject(Grid<IntMapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void Addvalue(int Addvalue)
    {
        value += Addvalue;
        Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChange(x, y);
    }

    public float GetNormalized()
    {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}

internal class StringGridObject
{

    private Grid<StringGridObject> grid;
    private int x;
    private int y;

    private string letters;
    private string numbers;

    public StringGridObject(Grid<StringGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        letters = "";
        numbers = "";
    }

    public void AddLetter(string letter)
    {
        letters += letter;
        grid.TriggerGridObjectChange(x, y);
    }

    public void AddNumber(string number)
    {
        numbers += number;
        grid.TriggerGridObjectChange(x, y);
    }

    public override string ToString()
    {
        return letters + "\n" + numbers;
    }
}