using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject Container;
    public GameObject WallContainer;

    private const int MAX_SIZE = 17;
    public int mazeSize = 17;

    [Space(20)]
    [Header("Input")]
    public GameObject CellPrefab;
    public Wall WallPrefab;
    public Cell[,] grid;

    private List<MazeAlgorithm> algorithms;
    private MazeAlgorithm selectedAlgorithm;

    private void Awake()
    {
        algorithms = new List<MazeAlgorithm>
        {
            new Prims(),
            new RecursiveBackTracker(),
        };

        selectedAlgorithm = algorithms[1];
    }

    void Start() => GenerateMaze();

    /// <summary> 
    /// Listener method for 'Generate' button onClick event
    /// </summary>
    private void GenerateMaze()
    {
        ClearGrid();
        grid = GenerateCells(mazeSize, CellPrefab, Container.transform);
        selectedAlgorithm.Run(grid, mazeSize, mazeSize);

        UpdateCellNeighbourFlags();
    }

    /// <summary>
    /// Returns a grid of cells with max given dimensions.
    /// </summary>
    private Cell[,] GenerateCells(int mazeSize, GameObject cellPrefab, Transform parent)
    {
        Cell[,] grid = new Cell[mazeSize, mazeSize];

        for (int z = 0; z < mazeSize; z++)
            for (int x = 0; x < mazeSize; x++) {
                GameObject cellGO = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity, parent);
                cellGO.transform.position = new Vector3(x + 0.5f, transform.localScale.y / 2f, z + 0.5f);
                grid[x, z] = cellGO.GetComponent<Cell>();
                grid[x, z].SetIndex(x, z);
                grid[x, z].SetName($"{x}:{z}");
            }

        return grid;
    }

    private void ClearGrid()
    {
        if(Container.transform.childCount > 0)
            for(int i = Container.transform.childCount - 1; i >= 0; i--)
                Destroy(Container.transform.GetChild(i).gameObject);
    }

    public void Quit() => Application.Quit();

    private List<Cell> GetWallCells()
    {
        List<Cell> cells = new();

        return cells;
    }

    private void UpdateCellNeighbourFlags()
    {
        for(int z = 0; z < mazeSize; z++)
            for(int x = 0; x < mazeSize; x++) {
                if (grid[x, z].IsBlocked) {
                    UpdateFlagsForCell(grid, x, z, mazeSize, mazeSize);
                    grid[x, z].SpawnWalls(WallPrefab, WallContainer.transform);
                }  
            }
    }

    private void UpdateFlagsForCell(Cell[,] grid, int x, int y, int width, int height)
    {
        grid[x, y].North = y + 1 < height && grid[x, y + 1].IsPassage();
        grid[x, y].South = y - 1 > -1 && grid[x, y - 1].IsPassage();
        grid[x, y].East = x + 1 < width && grid[x + 1, y].IsPassage();
        grid[x, y].West = x - 1 > -1 && grid[x - 1, y].IsPassage();
    }
}