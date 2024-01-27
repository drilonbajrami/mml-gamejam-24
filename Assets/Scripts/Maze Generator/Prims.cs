using System.Collections.Generic;
using UnityEngine;

public class Prims : MazeAlgorithm
{
    public void Run(Cell[,] grid, int width, int height)
    {
        // List of frontier cells to be processed
        List<Cell> frontiers = new();

        // Pick the starting cell based on wall mode and turn it into passage
        Cell currentCell = grid[1, 1];
        currentCell.TurnIntoPassage();

        // Get the index of the current cell and its neighbours
        Vector2Int index = currentCell.Index;
        GetNeighbours(grid, index.x, index.y, frontiers, width, height);

        while (frontiers.Count > 0) {
            // Pick random frontier cell
            currentCell = frontiers[Random.Range(0, frontiers.Count)];
            // Turn current cell into passage and 
            currentCell.TurnIntoPassage();
            // Get its index
            index = currentCell.Index;
            // Join a random available neighbour
            JoinRandomPassageNeighbour(grid, index.x, index.y, width, height);
            // Remove current cell from frontier list
            frontiers.Remove(currentCell);
            // Find its neighbours
            GetNeighbours(grid, index.x, index.y, frontiers, width, height);
        }

        //// Start and End points
        grid[1, 0].TurnIntoPassage();
        grid[width - 2, height - 1].TurnIntoPassage();
    }

    private void GetNeighbours(Cell[,] grid, int x, int y, List<Cell> frontierCells, int width, int height)
    {
        // NORTH
        int nY = y + 2; // mode = 0 (SLIM) || mode = 1 (THICK)
        if (nY < height - 1 && grid[x, nY].IsBlocked && !grid[x, nY].IsBeingProcessed) {
            grid[x, nY].Process();
            frontierCells.Add(grid[x, nY]);
        }

        // SOUTH
        nY = y - 2;
        if (nY > 0 && grid[x, nY].IsBlocked && !grid[x, nY].IsBeingProcessed) {
            grid[x, nY].Process();
            frontierCells.Add(grid[x, nY]);
        }

        // EAST
        int nX = x + 2;
        if (nX < width - 1 && grid[nX, y].IsBlocked && !grid[nX, y].IsBeingProcessed) {
            grid[nX, y].Process();
            frontierCells.Add(grid[nX, y]);
        }

        // WEST
        nX = x - 2;
        if (nX > 0 && grid[nX, y].IsBlocked && !grid[nX, y].IsBeingProcessed) {
            grid[nX, y].Process();
            frontierCells.Add(grid[nX, y]);
        }
    }

    private void JoinRandomPassageNeighbour(Cell[,] grid, int x, int y, int width, int height)
    {
        List<Vector2Int> availablePassCells = new();

        // NORTH
        int nY = y + 2;
        if (nY < height - 1 && !grid[x, nY].IsBlocked)
            availablePassCells.Add(new Vector2Int(x, nY));

        // SOUTH
        nY = y - 2;
        if (nY > 0 && !grid[x, nY].IsBlocked)
            availablePassCells.Add(new Vector2Int(x, nY));

        // WEST
        int nX = x + 2;
        if (nX < width - 1 && !grid[nX, y].IsBlocked)
            availablePassCells.Add(new Vector2Int(nX, y));

        // EAST
        nX = x - 2;
        if (nX > 0 && !grid[nX, y].IsBlocked)
            availablePassCells.Add(new Vector2Int(nX, y));

        if (availablePassCells.Count > 0) {
            int index = Random.Range(0, availablePassCells.Count);
            Vector2Int nIndex = availablePassCells[index];

            int midX = x - (x - nIndex.x) / 2;
            int midY = y - (y - nIndex.y) / 2;
            grid[x, y].TurnIntoPassage();
            grid[midX, midY].TurnIntoPassage();
        }
    }
}