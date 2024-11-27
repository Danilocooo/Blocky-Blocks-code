using UnityEngine;

public class TileRow : MonoBehaviour
{
    public CellTiles [] Cell { get; private set; }
    private void Awake()
    {
        Cell = GetComponentsInChildren<CellTiles>();
    }
}
