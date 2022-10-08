using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarManager : MonoBehaviour
{
    [SerializeField] private Tilemap walkableTilemap;
    [SerializeField] private Transform idie;
    [SerializeField] private Transform highlight;
    [SerializeField] private float moveTime;
    [SerializeField] private float numToMove;
    //Note: In C#, variables without an access modifier are private by default
    Vector3Int[,] walkableArea;
    Astar astar;
    BoundsInt bounds;
    

    private Vector3Int GridPositionOfMouse3D
    {
        get
        {
            return walkableTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private Vector2Int GridPositionOfMouse2D => (Vector2Int)GridPositionOfMouse3D;

    private Vector2Int GridPositionOfIdie
    {
        get
        {
            return (Vector2Int)walkableTilemap.WorldToCell(idie.position);
        }
    }

    private void Start()
    {
        //Trims any empty cells from the edges of the tilemap
        walkableTilemap.CompressBounds();
        bounds = walkableTilemap.cellBounds;

        CreateGrid();
        astar = new Astar(walkableArea, bounds.size.x, bounds.size.y);
    }

    private void CreateGrid()
    {
        walkableArea = new Vector3Int[bounds.size.x, bounds.size.y];
        for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
        {
            for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
            {
                if (walkableTilemap.HasTile(new Vector3Int(x, y, 0)))
                {
                    walkableArea[i, j] = new Vector3Int(x, y, 0);
                }
                else
                {
                    walkableArea[i, j] = new Vector3Int(x, y, 1);
                }
            }
        }
    }

    private void Update()
    {
        highlight.position = walkableTilemap.GetCellCenterWorld(GridPositionOfMouse3D);

        if (Input.GetMouseButtonDown(0))
        {
            var path = astar.CreatePath(walkableArea, GridPositionOfIdie, GridPositionOfMouse2D);
            if (path != null)
            {
                StartCoroutine(MoveToTarget(path));
            }
        }
    }

    IEnumerator MoveToTarget(List<Spot> path)
    {
        foreach (var step in path)
        {
            var x = step.X + 0.5f;
            var y = step.Y + 0.5f;
            // MovePlayer.Instance.MoveTo(x, y);
            yield return new WaitForSeconds(moveTime);
        }
        yield return null;
    }
}
