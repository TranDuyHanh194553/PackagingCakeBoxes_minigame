using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    [SerializeField] private Tile basketTilePrefab, basketBallTilePrefab, obstaclePrefab;
    [SerializeField] private TileState[] tileStates;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelIndexManager levelIndexManager;
    [SerializeField] private AudioManager audioManager;

    private TileGrid grid;
    private List<Tile> tiles;
    private bool waiting;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool isDetectingSwipe = false;

    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(16);
    }

    private void Start()
    {
        CreateTile();
    }
    public void CreateTile()
    {
        Tile basketTile = Instantiate(basketTilePrefab, grid.transform);
        Tile basketBallTile = Instantiate(basketBallTilePrefab, grid.transform);
        
        basketTile.SetState(tileStates[0]);
        basketTile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(basketTile);

        basketBallTile.SetState(tileStates[1]);
        basketBallTile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(basketBallTile);

        for (int i = 0; i < levelIndexManager.levelIndex; i++) 
        {
            Tile obstacle = Instantiate(obstaclePrefab, grid.transform);

            obstacle.SetState(tileStates[i+3]);
            obstacle.Spawn(grid.GetRandomEmptyCell());
            tiles.Add(obstacle);
        }
    }

    private void CreateObstacle()
    {
        
    }
    private void Update()
    {
        if (!waiting)
        {
            DetectSwipe();
        }
    }

    void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                isDetectingSwipe = true;
            }

            if (touch.phase == TouchPhase.Ended && isDetectingSwipe)
            {
                fingerUpPosition = touch.position;
                HandleSwipe();
                isDetectingSwipe = false;
            }
        }
    }

    void HandleSwipe()
    {
        float deltaX = fingerUpPosition.x - fingerDownPosition.x;
        float deltaY = fingerUpPosition.y - fingerDownPosition.y;

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            // Horizontal
            if (deltaX > 0)
            {
                //Right
                Move(Vector2Int.right, grid.Width - 2, -1, 0, 1);
            }
            else
            {
                // Left
                Move(Vector2Int.left, 1, 1, 0, 1);
            }
        }
        else
        {
            // Vertical
            if (deltaY > 0)
            {
                // Up
                Move(Vector2Int.up, 0, 1, 1, 1);
            }
            else
            {
                // Down
                Move(Vector2Int.down, 0, 1, grid.Height - 2, -1);
            }
        }
    }


    private void Move(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        bool changed = false;

        for (int x = startX; x >= 0 && x < grid.Width; x += incrementX)
        {
            for (int y = startY; y >= 0 && y < grid.Height; y += incrementY)
            {
                TileCell cell = grid.GetCell(x, y);

                if (cell.Occupied) {
                    changed |= MoveTile(cell.tile, direction);
                }
            }
        }

        if (changed) {
            StartCoroutine(WaitForChanges());
        }
        audioManager.PlaySFX(audioManager.slide);

    }

    private bool MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);

        while (adjacent != null)
        {
            if (adjacent.Occupied)
            {
                if (CanMerge(tile, adjacent.tile))
                {
                    MergeTiles(tile, adjacent.tile);
                    return true;
                }
                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }

        if (newCell != null)
        {
            tile.MoveTo(newCell);
            return true;
        }

        return false;
    }

    private bool CanMerge(Tile a, Tile b)
    {
        return a.state == b.state && !b.locked && a.state.number != 3;
    }

    private void MergeTiles(Tile a, Tile b)
    {
        tiles.Remove(a);
        a.Merge(b.cell);

        int index = Mathf.Clamp(IndexOf(b.state) + 1, 0, tileStates.Length - 1);
        TileState newState = tileStates[index];

        b.SetState(newState);
        audioManager.PlaySFX(audioManager.dunk);

        gameManager.Victory();
    }

    private int IndexOf(TileState state)
    {
        for (int i = 0; i < tileStates.Length; i++)
        {
            if (state == tileStates[i]) {
                return i;
            }
        }

        return -1;
    }

    private IEnumerator WaitForChanges()
    {
        waiting = true;

        yield return new WaitForSeconds(0.1f);

        waiting = false;

        foreach (var tile in tiles) {
            tile.locked = false;
        }
    }
}
