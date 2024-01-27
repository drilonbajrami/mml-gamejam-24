using System.Collections.Generic;
using UnityEngine;

public class RecursiveBackTracker : MazeAlgorithm
{
    public void Run(Cell[,] grid, int width, int height)
    {
        Stack<Cell> stack = new Stack<Cell>();
        stack.Push(grid[1, 1]);
        stack.Peek().Visit();

        while (stack.Count != 0) {
            Vector2Int topCellIndex = stack.Peek().Index;
            Cell nCell = GetRandomNonVisitedNeighbour(grid, topCellIndex.x, topCellIndex.y, width, height);

            if (nCell != null) {
                JoinWithNeighbour(stack.Peek(), nCell, grid);
                stack.Push(nCell);
                nCell.Visit();
            }
            else stack.Pop();

        }

        // Start and End points
        grid[1, 0].TurnIntoPassage();
        grid[width - 2, height - 1].TurnIntoPassage();
    }

    private Cell GetRandomNonVisitedNeighbour(Cell[,] grid, int x, int y, int width, int height)
    {
        List<Cell> availableNeighbours = new();

        // NORTH
        int nY = y + 2;
        if (nY < height - 1 && !grid[x, nY].HasBeenVisited)
            availableNeighbours.Add(grid[x, nY]);

        // SOUTH
        nY = y - 2;
        if (nY > 0 && !grid[x, nY].HasBeenVisited)
            availableNeighbours.Add(grid[x, nY]);

        // EAST
        int nX = x + 2;
        if (nX < width - 1 && !grid[nX, y].HasBeenVisited)
            availableNeighbours.Add(grid[nX, y]);

        // WEST
        nX = x - 2;
        if (nX > 0 && !grid[nX, y].HasBeenVisited)
            availableNeighbours.Add(grid[nX, y]);

        return availableNeighbours.Count > 0 ? availableNeighbours[Random.Range(0, availableNeighbours.Count)] : null;
    }

    private void JoinWithNeighbour(Cell currentCell, Cell neighbourCell, Cell[,] grid)
    {
        Vector2Int cIndex = currentCell.Index;
        Vector2Int nIndex = neighbourCell.Index;

        currentCell.TurnIntoPassage();
        neighbourCell.TurnIntoPassage();

        int inbetweenCellX = cIndex.x - (cIndex.x - nIndex.x) / 2;
        int inbetweenCellY = cIndex.y - (cIndex.y - nIndex.y) / 2;
        grid[inbetweenCellX, inbetweenCellY].TurnIntoPassage();
    }
}