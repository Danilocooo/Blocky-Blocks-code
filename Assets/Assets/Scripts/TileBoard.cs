using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TileBoard : MonoBehaviour
{
    public GameManager GameManager;
    public BlockControler BlockPrefab;
    public TileState[] blockStates;
    private TileGrid grid;
    private List<BlockControler> Blocks;
    private Touch finger;
    private Vector2 fingerStartPosition, fingerEndPosition;
    private float horizontalAxis;
    private float verticalAxis;
    private bool wait;
    private Vector2Int[,] NumberOfSameStates = new Vector2Int[4, 4];
    private Vector2Int[] NumberOfStates = new Vector2Int[16];

    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        Blocks = new List<BlockControler>();
    }

    public void CreateBlock()
    {
        BlockControler block = Instantiate(BlockPrefab, grid.transform);
        int RandomNum = Random.Range(0, blockStates.Length);
        block.SetState(blockStates[RandomNum]);
        block.Spawn(grid.GetRandomEmptyCell());
        Blocks.Add(block);
    }

    public void ClearBoard()
    {
        foreach (var cell in grid.cells)
        {
            cell.Block = null;
        }
        foreach (var block in Blocks)
        {
            Destroy(block.gameObject);
        }
        Blocks.Clear();
    }
    private void Update()
    {
        if (grid.size == 0)
        {
            this.CreateBlock();
        }
        if (!wait)
        {
            this.FingerSense();
        }
    }

    void FingerSense()
    {
        Vector2 swipeDelta;

        if (Input.touchCount > 0)
        {
            finger = Input.GetTouch(0);

            if (finger.phase == TouchPhase.Began)
            {
                fingerStartPosition = finger.position;
                horizontalAxis = transform.position.x;
                verticalAxis = transform.position.y;
            }
            else if (finger.phase == TouchPhase.Ended)
            {
                fingerEndPosition = finger.position;
                swipeDelta = fingerEndPosition - fingerStartPosition;
                float x = fingerEndPosition.x - fingerStartPosition.x;
                float y = fingerEndPosition.y - fingerStartPosition.y;

                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    switch (swipeDelta.x)
                    {
                        case > 0:
                            this.MoveBlocks(Vector2Int.right, 2, -1, 0, 1);
                            break;

                        case < 0:
                            this.MoveBlocks(Vector2Int.left, 1, 1, 0, 1);
                            break;
                    }
                }

                else
                {
                    switch (swipeDelta.y)
                    {
                        case > 0:
                            this.MoveBlocks(Vector2Int.up, 0, 1, 1, 1);
                            break;

                        case < 0:
                            this.MoveBlocks(Vector2Int.down, 0, 1, 2, -1);
                            break;
                    }
                }
            }
        }
    }

    private void MoveBlocks(Vector2Int direction, int StartX, int IncrementX, int StartY, int IncrementY)
    {
        bool changed = false;
        for (int x = StartX; x >= 0 && x < grid.width; x += IncrementX)
        {
            for (int y = StartY; y >= 0 && y < grid.height; y += IncrementY)
            {
                CellTiles cell = grid.GetCell(x, y);

                if (cell.Occupied)
                {
                    changed |= this.MoveBlock(cell.Block, direction);
                }

            }
        }
        if (changed)
        {
            this.StartCoroutine(this.WaitForChanges());
            if (GameManager.CanPlaySFX)
            {
                GameManager.Swipe.Play();
            }
        }
    }

    private bool MoveBlock(BlockControler block, Vector2Int direction)
    {
        CellTiles newCell = null;
        CellTiles adjacent = grid.GetAdjacentCell(block.cell, direction);

        while (adjacent != null)
        {
            if (adjacent.Occupied)
            {
                break;
            }
            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }

        if (newCell != null)
        {
            block.MoveTo(newCell);
            return true;
        }
        return false;
    }
    public void CanGetEliminated()
    {
        foreach (CellTiles cell in grid.cells)
        {
            this.NumberOfSameStatesActivator(cell);
        }
    }

    private void NumberOfSameStatesActivator(CellTiles cell)
    {
        int points = 0;

        if (cell != null && cell.Occupied)
        {
            List<CellTiles> adjacentUp = grid.GetAdjacentCellsStates(cell, Vector2Int.up);
            List<CellTiles> adjacentDown = grid.GetAdjacentCellsStates(cell, Vector2Int.down);
            List<CellTiles> adjacentLeft = grid.GetAdjacentCellsStates(cell, Vector2Int.left);
            List<CellTiles> adjacentRight = grid.GetAdjacentCellsStates(cell, Vector2Int.right);

            int coincidencias = adjacentUp.Count + adjacentDown.Count + adjacentLeft.Count + adjacentRight.Count;
            Debug.Log("Coincidencias: " + coincidencias);
            if (coincidencias >= 3)
            {
                Debug.Log("Borrando elementos");
                this.borrarBlock(cell);
                this.borraBlock(adjacentUp);
                this.borraBlock(adjacentDown);
                this.borraBlock(adjacentLeft);
                this.borraBlock(adjacentRight);
                if (grid.BlockColor.Equals(new Color (0f, 0f, 184/255f, 1f)))
                {
                    points = (coincidencias + 1) * 100;
                    Debug.Log("Suma de color BLUE: " + (coincidencias + 1) * 100);
                }
                else if (grid.BlockColor.Equals(new Color (0f, 172/255f, 0f, 1f)))
                {
                    points = (coincidencias + 1) * 101;
                    Debug.Log("Suma de color GREEN: " + (coincidencias + 1) * 100);
                }
                else if (grid.BlockColor.Equals(new Color(228/255f, 148/255f, 0f, 1f)))
                {
                    points = (coincidencias + 1) * 102;
                    Debug.Log("Suma de color ORANGE: " + (coincidencias + 1) * 100);
                }
                GameManager.IncreaseScore(points);
            }
   

        }

    }

    private void borraBlock(List<CellTiles> cellTilesList)
    {
        foreach (CellTiles cellTile in cellTilesList)
        {
            this.borrarBlock(cellTile);

        }
    }

    private void borrarBlock(CellTiles cellTile)
    {
        Blocks.Remove(cellTile.Block);
        Destroy(cellTile.Block.gameObject);
        if (GameManager.CanPlaySFX)
        {
            GameManager.Delete.Play();
        }
    }



    private IEnumerator WaitForChanges()
    {
        wait = true;

        yield return new WaitForSeconds(0.1f);

        wait = false;

        if (Blocks.Count != grid.size)
        {
            yield return new WaitForSeconds(0.2f);
            this.CreateBlock();
            if (GameManager.CanPlaySFX)
            {
                 GameManager.Spawn.Play();
            }

            if (GameManager.Score20Activator)
            {
                if (Blocks.Count != grid.size)
                {
                    this.CreateBlock();

                    if (GameManager.CanPlaySFX)
                    {
                        GameManager.Spawn.Play();
                    }
                }

            }

            yield return new WaitForSeconds(0.2f);
            this.CanGetEliminated();
        }
        if (CheckForGameOver())
        {
            GameManager.GameOver();
        }
    }


    private bool CheckForGameOver()
    {
        if (Blocks.Count != grid.size)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
}
