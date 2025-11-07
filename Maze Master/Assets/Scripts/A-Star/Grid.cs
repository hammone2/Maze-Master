using System;
using UnityEngine;

public class Grid<TGridOject>
{
    public event EventHandler<OnGridValueChangedEventArg> OnGridValueChanged;
    public class OnGridValueChangedEventArg : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridOject[,] gridArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridOject>, int, int, TGridOject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridOject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        bool showDebug = false;
        if (showDebug)
        {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    debugTextArray[x, y] = CreateWorldText.createWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

            OnGridValueChanged += (object sender, OnGridValueChangedEventArg eventArg) =>
            {
                debugTextArray[eventArg.x, eventArg.y].text = gridArray[eventArg.x, eventArg.y]?.ToString();
            };
        }

    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition; // vertical (x, y) - Horizontal (x, 0, y)
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetGridOject(int x, int y, TGridOject value)
    {
        if (0 <= x && 0 <= y && x < width && y < height)
        {
            gridArray[x, y] = value;
            if (OnGridValueChanged != null)
                OnGridValueChanged(this, new OnGridValueChangedEventArg { x = x, y = y });
        }
    }

    public void TriggerGridObjectChange(int x, int y)
    {
        if (OnGridValueChanged != null)
            OnGridValueChanged(this, new OnGridValueChangedEventArg { x = x, y = y });
    }

    public void SetGridOject(Vector3 worldPosition, TGridOject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridOject(x, y, value);
    }

    public TGridOject GetGridOject(int x, int y)
    {
        if (0 <= x && 0 <= y && x < width && y < height)
        {
            return gridArray[x, y];
        }
        return default(TGridOject);
    }
    
    public TGridOject GetGridOject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridOject(x, y);
    }
}