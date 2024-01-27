using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public Vector2Int Index { get; private set; }
    public bool IsBlocked { get; private set; }
    public bool IsBeingProcessed { get; private set; }
    public bool HasBeenVisited { get; private set; }
    public int Set { get; set; }

    private SpriteRenderer spriteRenderer;

    //[SerializeField] private LineRenderer NorthWall;
    //[SerializeField] private LineRenderer SouthWall;
    //[SerializeField] private LineRenderer EastWall;
    //[SerializeField] private LineRenderer WestWall;

    void Start()
    {
        Index = new Vector2Int((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f));
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.enabled) {
            //spriteRenderer.sprite = SpriteLibrary.CellSprite;
            //spriteRenderer.material = SpriteLibrary.Wall;
        }

        //NorthWall.material = SpriteLibrary.Wall;
        //SouthWall.material = SpriteLibrary.Wall;
        //EastWall.material = SpriteLibrary.Wall;
        //WestWall.material = SpriteLibrary.Wall;

        gameObject.SetActive(false);
    }

    //public bool HasNorthWall() => NorthWall.gameObject.activeSelf;
    //public bool HasSouthWall() => SouthWall.gameObject.activeSelf;
    //public bool HasWestWall() => WestWall.gameObject.activeSelf;
    //public bool HasEastWall() => EastWall.gameObject.activeSelf;
    public bool IsPassage() => !IsBlocked;

    public Vector3 GetPosition() => transform.position;
    public void SetName(string name) => gameObject.name = name;
    public void Disable() => gameObject.SetActive(false);
    public void Visit() => HasBeenVisited = true;

    public void Reset(bool slim)
    {
        IsBlocked = true;
        IsBeingProcessed = false;
        HasBeenVisited = false;
        gameObject.SetActive(true);
        WallMode(slim);
        //if (spriteRenderer != null) spriteRenderer.material = SpriteLibrary.Wall;
    }

    public void MakePath()
    {
        spriteRenderer.enabled = true;
        //spriteRenderer.material = SpriteLibrary.Path;
    }

    private void WallMode(bool condition)
    {
        spriteRenderer.enabled = !condition;
        //NorthWall.gameObject.SetActive(condition);
        //SouthWall.gameObject.SetActive(condition);
        //EastWall.gameObject.SetActive(condition);
        //WestWall.gameObject.SetActive(condition);
    }

    public void Process()
    {
        IsBeingProcessed = true;
        Mark();
    }

    public void Mark()
    {
        spriteRenderer.enabled = true;
        //spriteRenderer.material = SpriteLibrary.Frontier;
    }

    public void Unmark(bool slimMode)
    {
        //if (slimMode) spriteRenderer.enabled = false;
        //else spriteRenderer.material = SpriteLibrary.Passage;
    }

    public void TurnIntoPassage()
    {
        IsBlocked = false;
        //if (spriteRenderer != null) spriteRenderer.material = SpriteLibrary.Passage;
    }

    public void RemoveWallWithNeighbour(int x, int y)
    {
        //if (Index.x < x) EastWall.gameObject.SetActive(false);
        //else if (Index.x > x) WestWall.gameObject.SetActive(false);
        //else if (Index.y < y) NorthWall.gameObject.SetActive(false);
        //else SouthWall.gameObject.SetActive(false);

        IsBlocked = false;
        spriteRenderer.enabled = false;
    }

    public void RemoveWallWithNeighbour(MazeCell neighbourCell)
    {
        //if (Index.x < neighbourCell.Index.x) EastWall.gameObject.SetActive(false);
        //else if (Index.x > neighbourCell.Index.x) WestWall.gameObject.SetActive(false);
        //else if (Index.y < neighbourCell.Index.y) NorthWall.gameObject.SetActive(false);
        //else SouthWall.gameObject.SetActive(false);

        IsBlocked = false;
        spriteRenderer.enabled = false;
    }
}



public interface ICell
{
    #region Properties
    public Vector2Int Index { get; }
    public bool IsBlocked { get; }
    #endregion

    public Vector3 GetPosition();
    public void SetName(string name);
    public void Reset(bool mazeMode);
    public void Disable();
    public void Visit();
    public void Process();
    public void Mark();
    public void Unmark(bool slimMode);
    public void TurnIntoPassage();
    public void RemoveWallWithNeighbour(int neighbourX, int neighbourY);
    public void RemoveWallWithNeighbour(ICell cell);
    public void MakePath();

    public bool HasNorthWall();
    public bool HasSouthWall();
    public bool HasWestWall();
    public bool HasEastWall();
    public bool IsPassage();
}
