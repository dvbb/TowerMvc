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

    public Level Level { get { return m_level; } }
    public List<Grid> Road { get { return m_road; } }
    public List<Grid> Grids { get { return m_grid; } }

    public string BackgroundImg
    {
        set
        {
            SpriteRenderer render = transform.Find("Background").GetComponent<SpriteRenderer>();
            StartCoroutine(Tools.LoadImage(value, render));
        }
    }
    public string RoadImg
    {
        set
        {
            SpriteRenderer render = transform.Find("Road").GetComponent<SpriteRenderer>();
            StartCoroutine(Tools.LoadImage(value, render));
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
        this.BackgroundImg = "file://" + Consts.MapDir + level.Background;
        this.RoadImg = "file://" + Consts.MapDir + level.Road;

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
            grid.IsHolder = true;
        }
    }

    private Grid GetGrid(int x, int y)
    {
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
