using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Grid
{
    public int X;
    public int Y;
    public bool IsHolder;
    public object Data;

    public Grid(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class GridClickEventArgs : EventArgs
{
    public int MouseBtnId;
    public Grid Grid;

    public GridClickEventArgs(int mouseBtn, Grid grid)
    {
        MouseBtnId = mouseBtn;
        Grid = grid;
    }
}

public class Map : MonoBehaviour
{
    #region Field
    private float MapWidth;
    private float MapHeight;

    private float GridWidth;
    private float GridHeight;

    private Level m_level;
    private List<Grid> m_grid = new List<Grid>();
    private List<Grid> m_road = new List<Grid>();
    #endregion
    public const int RowCount = 7;
    public const int ColCount = 12;
    public const int TotalGrid = RowCount * ColCount;

    public event EventHandler<GridClickEventArgs> onGridClicked;

    public Level Level { get { return m_level; } }
    public List<Grid> Road { get { return m_road; } }
    public List<Grid> Grids { get { return m_grid; } }

    public string BackgroundImg
    {
        set
        {
            SpriteRenderer render = transform.Find("Background").GetComponent<SpriteRenderer>();
            if (render.sprite == null)
                StartCoroutine(Tools.LoadImage(value, render));
        }
    }
    public string RoadImg
    {
        set
        {
            SpriteRenderer render = transform.Find("Road").GetComponent<SpriteRenderer>();
            if (render.sprite == null)
                StartCoroutine(Tools.LoadImage(value, render));
        }
    }

    private void Awake()
    {
        onGridClicked += OnGridClick;
        for (int x = 0; x < RowCount; x++) // 7 row
            for (int y = 0; y < ColCount; y++) // 12 column
                m_grid.Add(new Grid(x, y));

        Level level = new Level();
        Tools.ParseXml("D:\\repo\\TowerMvc\\Assets\\Resources\\UI\\Levels\\Level1.xml", ref level);
        LoadLevel(level);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Grid grid = GetGridByMouse();
            if (grid != null)
            {
                GridClickEventArgs arg = new GridClickEventArgs(0, grid);
                if (onGridClicked != null)
                    OnGridClick(this, arg);
            }
        }
    }

    #region Data processing

    public void Clear()
    {
        m_level = null;
        m_road.Clear();
        foreach (Grid grid in m_grid)
        {
            grid.IsHolder = false;
        }
    }

    public void LoadLevel(Level level)
    {
        Clear();
        this.m_level = level;
        this.BackgroundImg = "UI/Maps/" + level.Background;
        this.RoadImg = "UI/Maps/" + level.Road;

        for (int i = 0; i < level.Path.Count; i++)
        {
            Point point = level.Path[i];
            Grid grid = GetGrid(point.X, point.Y);
            m_road.Add(grid);
        }
        for (int i = 0; i < level.Holder.Count; i++)
        {
            Point point = level.Holder[i];
            Grid grid = GetGrid(point.X, point.Y);
            if (grid != null)
                grid.IsHolder = true;
        }
    }

    private Grid GetGrid(int x, int y)
    {
        Debug.Log(x + " " + y);
        int index = x + y * ColCount;
        if (index >= 0 && index < m_grid.Count)
        {
            return m_grid[index];
        }
        else
        {
            Debug.Log("m_grid index error");
            return null;
        }
    }

    #endregion

    #region Click event

    private Grid GetGridByMouse()
    {
        Vector3 worldPosition = GetWorldPosition();
        Grid grid = GetGridByWorldPosition(worldPosition);
        return grid;
    }

    private Vector3 GetWorldPosition()
    {
        Vector3 viewPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 worldPosition = Camera.main.ViewportToWorldPoint(viewPosition);
        return worldPosition;
    }

    private Grid GetGridByWorldPosition(Vector3 worldPosition)
    {
        int x = (int)((worldPosition.x + MapWidth / 2) / GridWidth);
        int y = (int)((worldPosition.y + MapHeight / 2) / GridHeight);
        return GetGrid(x, y);
    }

    #endregion

    #region Event Regresion

    private void OnGridClick(object sender, GridClickEventArgs args)
    {
        if (gameObject.scene.name != "Map" || Level == null)
            return;

        // Set Turret position
        if (args.MouseBtnId == 0 && !m_road.Contains(args.Grid))
        {
            args.Grid.IsHolder = !args.Grid.IsHolder;
        }

        // Set Road
        if (args.MouseBtnId == 1 && !args.Grid.IsHolder)
        {

        }
    }

    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        CalculateMapAndGrid();

        for (int row = 0; row < RowCount; row++)
        {
            Vector2 from = new Vector2(-MapWidth / 2, -MapHeight / 2 + row * GridHeight);
            Vector2 to = new Vector2(-MapWidth / 2 + MapWidth, -MapHeight / 2 + row * GridHeight);
            Gizmos.DrawLine(from, to);
        }
        for (int column = 0; column < ColCount; column++)
        {
            Vector2 from = new Vector2(-MapWidth / 2 + column * GridWidth, -MapHeight / 2);
            Vector2 to = new Vector2(-MapWidth / 2 + +column * GridWidth, -MapHeight / 2 + MapHeight);
            Gizmos.DrawLine(from, to);
        }
    }

    private void CalculateMapAndGrid()
    {
        // Game screen vector
        Vector3 leftDown = new Vector3(0, 0);
        Vector3 rightUp = new Vector3(1, 1);

        Vector3 v1 = Camera.main.ViewportToWorldPoint(leftDown);
        Vector3 v2 = Camera.main.ViewportToWorldPoint(rightUp);

        MapHeight = Mathf.Abs(v1.y - v2.y);
        MapWidth = Mathf.Abs(v1.x - v2.x);

        GridHeight = MapHeight / RowCount;
        GridWidth = MapWidth / ColCount;
    }
    #endregion
}
