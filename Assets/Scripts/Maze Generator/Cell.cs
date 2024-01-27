using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int Index { get; set; }
    [field: SerializeField] public bool IsBlocked { get; private set; }
    public bool IsBeingProcessed { get; private set; }
    public bool HasBeenVisited { get; private set; }

    // Keep track of what side does this cell have passage neighbours.
    public bool North = false;
    public bool South = false;
    public bool East = false;
    public bool West = false;

    private void Awake()
    {
        IsBlocked = true;
        IsBeingProcessed = false;
        HasBeenVisited = false;
    }

    public void SetName(string name) => gameObject.name = name;

    public void SetIndex(int x, int z) => Index = new Vector2Int(x, z);

    public bool IsPassage() => !IsBlocked;

    public void Visit() => HasBeenVisited = true;

    public void Process() => IsBeingProcessed = true;

    public void TurnIntoPassage()
    {
        IsBlocked = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public List<Wall> SpawnWalls(Wall wallPrefab)
    {
        List<Wall> walls = new();

        if(North) {
            Wall wall = Instantiate(wallPrefab, transform);
            wall.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0.5f), Quaternion.LookRotation(transform.forward * -1, transform.up));
            walls.Add(wall);
        }

        if(South) {
            Wall wall = Instantiate(wallPrefab, transform);
            wall.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -0.5f), Quaternion.LookRotation(transform.forward, transform.up));
            walls.Add(wall);
        }

        if(East) {
            Wall wall = Instantiate(wallPrefab, transform);
            wall.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0.5f, 0, 0), Quaternion.LookRotation(transform.right * -1, transform.up));
            walls.Add(wall);
        }

        if(West) {
            Wall wall = Instantiate(wallPrefab, transform);
            wall.gameObject.transform.SetLocalPositionAndRotation(new Vector3(-0.5f, 0, 0), Quaternion.LookRotation(transform.right, transform.up));
            walls.Add(wall);
        }

        GetComponent<MeshRenderer>().enabled = false;
        return walls;
    }
}