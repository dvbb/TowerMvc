using System;
using System.Collections.Generic;
using UnityEngine;

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

public class UILevel : View
{
    #region Field
    private float MapWidth;
    private float MapHeight;

    private float GridWidth;
    private float GridHeight;

    private LevelInfo m_level;
    private List<Grid> m_grid = new List<Grid>();
    private List<Grid> m_path = new List<Grid>();

    // Drag
    private bool isDragging;
    private GameObject prefab;
    #endregion
    public override string Name => Consts.V_Level;

    public const int RowCount = 7;
    public const int ColCount = 12;
    public const int TotalGrid = RowCount * ColCount;
    public Vector3 mapZeorVector => new Vector3(-MapWidth / 2, -MapHeight / 2);
    public Vector3 gridOffSet => new Vector3(GridWidth / 2, GridHeight / 2);

    public event EventHandler<GridClickEventArgs> onGridClicked;

    public LevelInfo LevelInfo { get { return m_level; } }
    public List<Grid> Path { get { return m_path; } }
    public List<Grid> Grids { get { return m_grid; } }
    public List<GameObject> Nodes = new List<GameObject>();

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
            StartCoroutine(Tools.LoadImage(value, render, 45));
        }
    }

    private void Awake()
    {
        CalculateMapAndGrid();

        if (gameObject.scene.name != "MapBuilder")
        {
            string fileName = Tools.GetLevelFile(LevelModel.Instance.LevelIndex).FullName;
            Tools.ParseXml(fileName, ref m_level);
            LoadLevel(m_level);
        }
        else
        {
            onGridClicked += OnGridClick;
        }

    }

    private void Update()
    {
        EditGrid();

        if (isDragging && prefab != null)
        {
            var position = GetWorldPosition();
            prefab.transform.position = position;
        }
    }



    #region Data processing

    public void Clear()
    {
        m_level = null;
        ClearRoad();
        ClearHolder();
    }
    public void ClearRoad() => m_path.Clear();

    public void ClearHolder()
    {
        foreach (Grid grid in m_grid)
        {
            grid.IsHolder = false;
        }
    }

    public void Init()
    {
        m_grid.Clear();
        m_path.Clear();

        for (int y = 0; y < RowCount; y++) // 7 row  0 < y < 7
            for (int x = 0; x < ColCount; x++) // 12 column  0 < x < 12
                m_grid.Add(new Grid(x, y)); //(0,0) (0,1) ... (0,11) (1,0) ... (7,0)
    }

    public void LoadLevel(LevelInfo level)
    {
        Init();

        this.m_level = level;
        this.BackgroundImg = "UI/Maps/" + level.Background;
        this.RoadImg = "UI/Maps/" + level.Road;

        //Debug.Log("m_grid Count: " + m_grid.Count);
        //Debug.Log("Path count: " + level.Path.Count);
        //Debug.Log("Holder count: " + level.Holder.Count);

        for (int i = 0; i < level.Path.Count; i++)
        {
            Point point = level.Path[i];
            Grid grid = GetGrid(point.X, point.Y);
            m_path.Add(grid);
        }
        for (int i = 0; i < level.Holder.Count; i++)
        {
            Point point = level.Holder[i];
            Grid grid = GetGrid(point.X, point.Y);
            if (grid != null)
                grid.IsHolder = true;
        }

        // Draw holder
        GenerateNodes();
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

    #region Click event

    private Grid GetGridByMouse()
    {
        Vector3 worldPosition = GetWorldPosition();
        Grid grid = GetGridByWorldPosition(worldPosition);
        return grid;
    }

    private Vector3 GetWorldPosition()
    {
        // Screeen => Viewport => World
        Vector3 viewPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 worldPosition = Camera.main.ViewportToWorldPoint(viewPosition);
        worldPosition.z = 0;
        return worldPosition;
    }

    private Grid GetGridByWorldPosition(Vector3 worldPosition)
    {
        int x = (int)((worldPosition.x + MapWidth / 2) / GridWidth);
        int y = (int)((worldPosition.y + MapHeight / 2) / GridHeight);
        return GetGrid(x, y);
    }

    public void GenerateNodes()
    {
        if (gameObject.scene.name == "MapBuilder" || LevelInfo == null)
            return;

        var prefab = Resources.Load("Prefabs/Map/Node") as GameObject;
        foreach (var item in m_grid)
        {
            if (item.IsHolder)
            {
                Vector3 position = GetGridPosition(item.X, item.Y);
                var node = Instantiate(prefab, position, Quaternion.identity);
                node.transform.SetParent(transform);
                node.SetActive(false);
                Nodes.Add(node);
            }
        }
    }

    public void ShowNodes()
    {
        foreach (var item in Nodes)
        {
            item.SetActive(true);
        }
    }

    public void HideNodes()
    {
        foreach (var item in Nodes)
        {
            item.SetActive(false);
        }
    }
    #endregion

    #region Event Regresion

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        AttentionEvents.Add(Consts.E_ShowNode);
        AttentionEvents.Add(Consts.E_HideNode);
        AttentionEvents.Add(Consts.E_StartCardDrag);
        AttentionEvents.Add(Consts.E_EndCardDrag);
    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Consts.E_ShowNode:
                ShowNodes();
                break;
            case Consts.E_HideNode:
                HideNodes();
                break;
            case Consts.E_StartCardDrag:
                isDragging = true;
                Card card = obj as Card;
                prefab = Instantiate(Resources.Load(card.prefabPath) as GameObject);
                break;
            case Consts.E_EndCardDrag:
                isDragging = false;

                // prefab == null means that drag cancelled
                if (prefab == null)
                    return;

                // Get prefab's grid
                Grid grid = GetGridByWorldPosition(prefab.transform.position);
                if (grid.IsHolder)
                {
                    prefab.transform.position = GetGridPosition(grid.X, grid.Y);
                }
                else
                {
                    Destroy(prefab);
                }
                prefab = null;
                break;
            default:
                break;
        }
    }

    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        // Draw Grid in map scene
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

        // Draw holder
        foreach (var item in m_grid)
        {
            if (item.IsHolder)
            {
                Vector3 position = GetGridPosition(item.X, item.Y);
                Gizmos.DrawIcon(position, "Holder.png", true);
            }
        }

        // Draw navigation line
        Gizmos.color = Color.red;
        for (int i = 0; i < m_path.Count; i++)
        {
            if (i == 0)
            {
                Vector3 position = GetGridPosition(m_path[i].X, m_path[i].Y);
                Gizmos.DrawIcon(position, "Spawn.png", true);
            }
            if (m_path.Count > 1 && i == m_path.Count - 1)
            {
                Vector3 position = GetGridPosition(m_path[i].X, m_path[i].Y);
                Gizmos.DrawIcon(position, "Home.png", true);
            }
            if (m_path.Count > 1 && i != 0)
            {
                Vector3 from = GetGridPosition(m_path[i - 1].X, m_path[i - 1].Y);
                Vector3 to = GetGridPosition(m_path[i].X, m_path[i].Y);
                Gizmos.DrawLine(from, to);
            }
        }
    }

    private Vector3 GetGridPosition(int x, int y)
    {
        int index = x + y * ColCount;
        if (index < m_grid.Count)
        {
            return mapZeorVector + gridOffSet + new Vector3(m_grid[index].X * GridWidth, m_grid[index].Y * GridHeight);
        }
        return Vector3.zero;
    }


    #endregion

    #region Editor method

    private void EditGrid()
    {
        if (gameObject.scene.name != "MapBuilder" || LevelInfo == null)
            return;

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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Grid grid = GetGridByMouse();
            if (grid != null)
            {
                GridClickEventArgs arg = new GridClickEventArgs(1, grid);
                if (onGridClicked != null)
                    OnGridClick(this, arg);
            }
        }
    }

    private void OnGridClick(object sender, GridClickEventArgs args)
    {
        // Set Turret position
        if (args.MouseBtnId == 0 && !m_path.Contains(args.Grid))
        {
            args.Grid.IsHolder = !args.Grid.IsHolder;
        }

        // Set Road
        if (args.MouseBtnId == 1 && !args.Grid.IsHolder)
        {
            m_path.Add(args.Grid);
        }
    }

    #endregion
}
