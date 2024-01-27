using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject Floor;
    public GameObject CellContainer;

    public int mazeSize = 17;
    public float wallWidth = 1f;
    public float wallHeight = 1.5f;

    public GameObject Player;

    [Space(20)]
    [Header("Input")]
    public GameObject CellPrefab;
    public Wall WallPrefab;
    public Cell[,] grid;

    public List<Wall> walls = new List<Wall>();

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
        grid = GenerateCells(mazeSize, CellPrefab, CellContainer.transform);
        selectedAlgorithm.Run(grid, mazeSize, mazeSize);

        GenerateWalls();

        Player.transform.position = grid[1, 0].transform.position;
    }

    /// <summary>
    /// Returns a grid of cells with max given dimensions.
    /// </summary>
    private Cell[,] GenerateCells(int mazeSize, GameObject cellPrefab, Transform parent)
    {
        Cell[,] grid = new Cell[mazeSize, mazeSize];

        Floor.transform.localScale = new Vector3(mazeSize * wallWidth / 10f, 1f, mazeSize * wallWidth / 10f);
        Floor.transform.localPosition = new Vector3(mazeSize * wallWidth / 2f, 0f, mazeSize * wallWidth / 2f);

        for (int z = 0; z < mazeSize; z++)
            for (int x = 0; x < mazeSize; x++) {
                GameObject cellGO = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity, parent);
                cellGO.transform.localScale = new Vector3(wallWidth, wallHeight, wallWidth);
                cellGO.transform.localPosition = new Vector3(x * wallWidth + wallWidth / 2f, cellGO.transform.localScale.y / 2f, z * wallWidth + wallWidth / 2f);
                grid[x, z] = cellGO.GetComponent<Cell>();
                grid[x, z].SetIndex(x, z);
                grid[x, z].SetName($"{x}:{z}");
            }

        return grid;
    }

    private void ClearGrid()
    {
        if(CellContainer.transform.childCount > 0)
            for(int i = CellContainer.transform.childCount - 1; i >= 0; i--)
                Destroy(CellContainer.transform.GetChild(i).gameObject);
    }

    private void GenerateWalls()
    {
        walls.Clear();

        for(int z = 0; z < mazeSize; z++)
            for(int x = 0; x < mazeSize; x++) {
                if (grid[x, z].IsBlocked) {
                    UpdateWallCellFlags(grid, x, z, mazeSize, mazeSize);
                    walls.AddRange(grid[x, z].SpawnWalls(WallPrefab));
                }  
            }
    }

    private void UpdateWallCellFlags(Cell[,] grid, int x, int y, int width, int height)
    {
        // Set flags based on which direction does this wall cell have a passage neighbour cell.
        Cell wallCell = grid[x, y];
        wallCell.North = y + 1 < height && grid[x, y + 1].IsPassage();
        wallCell.South = y - 1 > -1 && grid[x, y - 1].IsPassage();
        wallCell.East = x + 1 < width && grid[x + 1, y].IsPassage();
        wallCell.West = x - 1 > -1 && grid[x - 1, y].IsPassage();
    }

    public void Quit() => Application.Quit();
}