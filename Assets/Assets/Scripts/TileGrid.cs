using UnityEngine;
using System.Collections.Generic;

public class TileGrid : MonoBehaviour
{
    // Array amb les files creades en Unity
    // Creades 4 files
    public TileRow[] Rows { get; private set; }

    // Array amb las c�l�lules creades en Unity
    // Creades 16 c�l�lulas
    public CellTiles[] cells { get; private set; }

    // N�mero total de c�l�lules creades en Unity
    // 16 c�l�lules creades
    public int size => cells.Length;

    // N�mero de files creades en Unity
    // 4 files creades
    public int height => Rows.Length;

    // Amplada de la matriu calculant en base al n�mero de c�l�lulas y el n�mero de files
    public int width => size / height;

    /**
     * Funci� que crea las coordenades cartesianes de la matriu
     * 
     * A cada c�l�lula de la matriu se li asigna la coordinada corresponent
     **/
    public Color BlockColor;
    private void Awake()
    {
        Rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<CellTiles>();

        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Coordinates = new Vector2Int(i % width, i / width);
        }
    }


    /**
     * Funci� que donades les coordenades cartesianes, retorna la c�l�lula
     * param: int
     * param: int
     * return: CellTiles
     **/ 
    public CellTiles GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return Rows[y].Cell[x];
        }
        else
        {
            return null;
        }
    }

    public CellTiles GetCell(Vector2Int coordinates)
    {
        return GetCell(coordinates.x, coordinates.y);
        
    }

    /**
     * Funci� que donades las coordenades d'una cel�la, retorna la cel�la adyacent segons la direcci� indicada
     * param: CellTiles
     * param: Vector2Int
     * return: CellTiles
     **/
    public CellTiles GetAdjacentCell(CellTiles cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.Coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;
        return GetCell(coordinates);
    }

    public List<CellTiles> GetAdjacentCellsStates(CellTiles cell, Vector2Int directionStates)
    {
        List<CellTiles> coordenadasCoincidencias = new List<CellTiles>();

        CellTiles adyacente = this.GetAdjacentCell(cell, directionStates);

        while (
            cell.Block is not null &&
            adyacente is not null && 
            adyacente.Occupied && 
            adyacente.Block.state.backgroundColor.Equals(cell.Block.state.backgroundColor))
        {
            BlockColor = cell.Block.state.backgroundColor;
            coordenadasCoincidencias.Add(adyacente);
            adyacente = this.GetAdjacentCell(adyacente, directionStates);
        }
        
        return coordenadasCoincidencias;
    }
    

    public CellTiles GetRandomEmptyCell()
    {
        int index = UnityEngine.Random.Range(0, cells.Length);
        int startingIndex = index;

        while (cells[index].Occupied)
        {
            index++;

            if (index >= cells.Length)
            {
                index = 0;
            }
            if (index == startingIndex)
            {
                return null;
            }
        }
        return cells[index];
        
    }
}
